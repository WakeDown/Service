using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;

namespace ZipClaim.WebForms.Client
{
    /// <summary>
    /// Сводное описание для ProgramDownloader
    /// </summary>
    public class ProgramDownloader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string progName = context.Request.QueryString["p"];

            if (progName.Equals("scaner"))
            {
                context.Response.ContentType = "application/zip";
                context.Response.AddHeader("content-disposition", "attachment; filename=UN1TCounter.zip");
                context.Response.WriteFile("~/Files/UN1TCounter.zip");
            }
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