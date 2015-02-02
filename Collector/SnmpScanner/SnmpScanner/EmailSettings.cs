using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public class EmailSettings
    {
        public MailAddress MailFrom;
        public MailAddress MailTo;

        public string Host;
        public int Port;
        public string Login;
        public string Password;
        public bool EnableSsl;

        public bool Save2SentFolder;

        public string SentMethod;
        public string SentHost;
        public int SentPort;
        public string SentLogin;
        public string SentPassword;
        public bool SentEnableSsl;

        public EmailSettings(bool defaultSettings)
        {
            var settings = new Settings();
            MailTo = new MailAddress(settings.MailTo);
            MailFrom = new MailAddress(settings.MailFrom);

            if (defaultSettings) LoadDefault();
        }

        private void LoadDefault()
        {
            Host = ConfigurationManager.AppSettings["smtpHost"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["smtpPort"]))
            {
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            }

            Login = ConfigurationManager.AppSettings["smtpLogin"];
            Password = ConfigurationManager.AppSettings["smtpPassword"];
            string ssl = ConfigurationManager.AppSettings["smtpEnableSsl"];
            EnableSsl = !String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);

            string save = ConfigurationManager.AppSettings["save2SentFolder"];
            Save2SentFolder = !String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

            SentHost = ConfigurationManager.AppSettings["sentHost"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["sentPort"]))
            {
                SentPort = Convert.ToInt32(ConfigurationManager.AppSettings["sentPort"]);
            }

            SentLogin = ConfigurationManager.AppSettings["sentLogin"];
            SentPassword = ConfigurationManager.AppSettings["sentPassword"];
            string sentSsl = ConfigurationManager.AppSettings["sentEnableSsl"];
            SentEnableSsl = !String.IsNullOrEmpty(sentSsl) && Convert.ToBoolean(sentSsl);

            //SentMethod = ConfigurationManager.AppSettings["sentMethod"];
        }
    }
}
