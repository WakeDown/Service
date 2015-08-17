using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceClaim.Helpers
{
    public class MainHelper
    {
        public static int GetValueInt(object obj)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                
            }

            return result;
        }

    }
}