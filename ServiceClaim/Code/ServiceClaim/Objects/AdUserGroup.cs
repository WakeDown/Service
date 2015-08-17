using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceClaim.Objects
{
    public class AdUserGroup
    {
        public AdGroup Group { get; set; }
        public string Sid { get; set; }
        public string Name { get; set; }

        public AdUserGroup(AdGroup grp, string sid)
        {
            Group = grp;
            Sid = sid;
        }

        public static IEnumerable<AdUserGroup> GetList()
        {
            var list = new List<AdUserGroup>();
            list.Add(new AdUserGroup(AdGroup.SuperAdmin, "S-1-5-21-1970802976-3466419101-4042325969-4031"));//Суперадмин
            return list;
        }
    }
}