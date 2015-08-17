using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceClaim.Objects;

namespace ServiceClaim.Models
{
    public class Classifier : DbModel
    {
        public static bool SaveFromExcel(byte[] data, out ResponseMessage responseMessage)
        {
            Uri uri = new Uri(String.Format("{0}/Classifier/SaveFromExcel", OdataServiceUri));
            string json = JsonConvert.SerializeObject(data);
            bool result = PostJson(uri, json, out responseMessage);
            return result;
        }
    }
}