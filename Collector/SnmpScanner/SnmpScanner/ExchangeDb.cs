using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using S22.Imap;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public class ExchangeDb
    {
        private Thread _snmpThread;
        private Thread _exchangeThread;

        private bool _threadMustStop
        {
            get
            {
                return Program._threadMustStop;
            }
            set
            {
                Program._threadMustStop = value;
            }
        }

        private bool _dbIsBuzy
        {
            get
            {
                return Program._dbIsBuzy;
            }
            set
            {
                Program._dbIsBuzy = value;
            }
        }

        static object gate = new object();

        private Settings AppSettings { get { return Program.AppSettings; } }

        public static void SendMail(string subject, string body, EmailSettings settings)
        {
            //switch (settings.Method)
            //{
            //        case MailMethod.SMTP:
            //        SendMailSmtp(subject, body, settings);
            //        break;
            //        case MailMethod.IMAP:
            //        break;
            //}

            //IMail email = Mail.Text(body).To(settings.MailTo.ToString()).From(settings.EnableSsl ? settings.Login : settings.MailFrom.ToString()).Subject(subject).Create();


            SendMailSmtp(subject, body, settings);
            if (settings.Save2SentFolder)
            {
                SaveMail2SentFolder(subject, body, settings);
            }
        }

        public static void SaveMail2SentFolder(string subject, string body, EmailSettings settings)
        {
            using (ImapClient imap = new ImapClient(settings.SentHost, settings.SentPort, settings.SentLogin, settings.SentPassword, AuthMethod.Auto, settings.SentEnableSsl))
            {
                MailMessage message = new MailMessage();

                if (!String.IsNullOrEmpty(settings.SentLogin))
                {
                    message.From = new MailAddress(settings.SentLogin);
                }
                else
                {
                    message.From = settings.MailFrom;
                }

                message.To.Add(settings.MailTo);

                message.Subject = subject;
                message.Body = body;

                string mailbox = null;

                foreach (var mb in imap.ListMailboxes())
                {
                    if (mb.ToLower().Equals("sent") || mb.ToLower().Equals("отправленные") || mb.ToLower().Contains("sent"))
                    {
                        mailbox = mb;
                        break;
                    }
                }

                uint uid = imap.StoreMessage(message, false, mailbox);
                //imap.SetMessageFlags(uid, null, );
            }
        }

        public static void SendMailSmtp(string subject, string body, EmailSettings settings)
        {
            MailMessage mail = new MailMessage();

            SmtpClient client = new SmtpClient();
            client.Port = settings.Port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (!String.IsNullOrEmpty(settings.Login))
            {
                client.Credentials = new NetworkCredential(settings.Login, settings.Password);
                mail.From = new MailAddress(settings.Login);
            }
            else
            {
                mail.From = settings.MailFrom;
            }

            client.EnableSsl = settings.EnableSsl;

            mail.To.Add(settings.MailTo);

            

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            client.Host = settings.Host;

            client.Send(mail);
        }

        public bool DoExchangeDb(bool exchangeNow = false)
        {
            bool result = false;
            bool canDelete = false;

            try
            {
                SendEmailExchangeDb();
                canDelete = true;
                result = true;
            }
            catch (Exception)
            {
                canDelete = false;
                result = false;
                //exchangeSuccess = false;
            }

            if (canDelete)
            {
                lock (gate)
                {
                    while (_dbIsBuzy) { Thread.Sleep(10); }
                }

                lock (gate)
                {
                    _dbIsBuzy = true;
                }

                try
                {

                    Request.DeleteAll();
                }
                finally
                {
                    lock (gate)
                    {
                        _dbIsBuzy = false;
                    }
                }
            }

            //if (!_background)
            //{
            //this.Invoke(new Action<string>((s) => lblLastExchange.Text = s), DateTime.Now.ToString("g"));
            //}

            if (!exchangeNow)
            {
                if (!_threadMustStop) Thread.Sleep(Program.GetExchangeDelayValue() * 60 * 60 * 1000);
            }

            return result;
        }

        private void SendEmailExchangeDb()
        {
            lock (gate)
            {
                while (_dbIsBuzy) { Thread.Sleep(10); }
            }

            lock (gate)
            {
                _dbIsBuzy = true;
            }

            try
            {
                Request[] lst = Request.GetList();

                StringBuilder content = new StringBuilder();

                content.Append("<DeviceRequestRoot>");

                content.Append(String.Format(@"<SysInfo idContractor=""{0}"" dateSend=""{1:dd-MM-yyyy HH:mm:ss zzz}"" progVersion=""{2}"" />",
                    AppSettings.ContractorId, DateTime.Now, Program.progVersion));

                content.Append("<DeviceRequestList>");
                foreach (Request request in lst)
                {
                    content.Append(
                        String.Format(
                            @"<DeviceRequest serialNum=""{5}"" date=""{0}"" host=""{1}"" oid=""{2}"" value=""{3}"" oidType=""{4}"" />",
                            request.DateRequest, request.Host, request.Oid, request.Value.Replace("\"", ""), request.OidType, request.SerialNum.Replace("\"", "")));
                }
                content.Append("</DeviceRequestList>");
                content.Append("</DeviceRequestRoot>");


                var emailSettings = new EmailSettings(true);
                SendMail(AppSettings.MailSubject, content.ToString(), emailSettings);
            }
            finally
            {
                lock (gate)
                {
                    _dbIsBuzy = false;
                }
            }

            //string path = "d:\\devPool.txt";
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //using (FileStream fs = File.Create(path))
            //{
            //    Byte[] info = new UTF8Encoding(true).GetBytes(content.ToString());
            //    fs.Write(info, 0, info.Length);
            //}
        }
    }
}
