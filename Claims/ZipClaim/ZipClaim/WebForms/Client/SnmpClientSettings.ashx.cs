using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Client
{
    /// <summary>
    /// Сводное описание для SnmpClientSettings
    /// </summary>
    public class SnmpClientSettings : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strContractorId = context.Request.QueryString["ctrid"];
            int contractorId;
            int.TryParse(strContractorId, out contractorId);

            if (contractorId > 0)
            {
                string strSettings = CreateXmlSettings(contractorId);
                string pass = ConfigurationManager.AppSettings["snmpCryptoPassword"];
                string strEncrypted = Cryptor.Encrypt(strSettings, pass);

                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strEncrypted);
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=license.un1t");
                //context.Response.ContentType = "application/un1t";
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
        }

        private string CreateXmlSettings(int contractorId)
        {
            var doc = new XDocument();

            var settings = new XElement("Settings");
            doc.Add(settings);

            var common = new XElement("Common");
            settings.Add(common);
            string mailTo = ConfigurationManager.AppSettings["snmpMailTo"];
            common.Add(new XAttribute("mailTo", mailTo));
            string mailFrom = ConfigurationManager.AppSettings["snmpMailFrom"];
            common.Add(new XAttribute("mailFrom", mailFrom));
            string mailSubject = ConfigurationManager.AppSettings["snmpMailSubject"];
            common.Add(new XAttribute("mailSubject", mailSubject));
            common.Add(new XAttribute("contractorId", contractorId));

            var schedule = new XElement("Schedule");
            settings.Add(schedule);
            string defaultScanDelay = ConfigurationManager.AppSettings["snmpDefaultScanDelay"];
            schedule.Add(new XAttribute("scanDelay", defaultScanDelay));
            string defaultMinExchangeDelay = ConfigurationManager.AppSettings["snmpDefaultMinExchangeDelay"];
            schedule.Add(new XAttribute("minExchangeDelay", defaultMinExchangeDelay));
            string defaultMaxExchangeDelay = ConfigurationManager.AppSettings["snmpDefaultMaxExchangeDelay"];
            schedule.Add(new XAttribute("maxExchangeDelay", defaultMaxExchangeDelay));

            var oidList = new XElement("OidList");
            settings.Add(oidList);
            string serialNumOidList = ConfigurationManager.AppSettings["snmpSerialNumOidList"];
            foreach (var oid in serialNumOidList.Split('|'))
            {
                oidList.Add(new XElement("SerialNum", oid));
            }
            string totalCounterOidList = ConfigurationManager.AppSettings["snmpTotalCounterOidList"];
            foreach (var oid in totalCounterOidList.Split('|'))
            {
                oidList.Add(new XElement("TotalCounter", oid));
            }

            var deviceList = new XElement("DeviceList");
            settings.Add(deviceList);

            DataTable dtDevices = Db.Db.Srvpl.GetContractorDeviceList(contractorId);

            if (dtDevices.Rows.Count > 0)
            {
                foreach (DataRow row in dtDevices.Rows)
                {
                    var device = new XElement("Device");
                    deviceList.Add(device);
                    string serivalNum = row["serial_num"].ToString();
                    device.Add(new XAttribute("serialNum", serivalNum));
                    //string ip = row[""].ToString();
                    //device.Add(new XAttribute("ip", ip));
                }
            }

            return doc.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}