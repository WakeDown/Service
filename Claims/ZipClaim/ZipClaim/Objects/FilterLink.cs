using System;
using System.Web.UI;

namespace ZipClaim.Objects
{
    [Serializable]
    public class FilterLink
    {
        public string ParamName { get; set; }
        //public System.Web.UI.Control Control { get; set; }
        //public string ControlClientId { get; set; }
        public string ControlType { get; private set; }
        public string ControlId { get; set; }
        public string Value { get; set; }
        public string DefaultValue { get; set; }
        public string ControlPageId { get; set; }

        public FilterLink(string paramName, string controlId, string defaultValue = null)
        {
            ParamName = paramName;
            ControlId = controlId;
            DefaultValue = defaultValue;
        }

        public FilterLink(string paramName, Control control, string defaultValue = null)
            : this(paramName, control.UniqueID, defaultValue)
        {
            ControlType = control.GetType().Name;
            ControlPageId = control.ClientID;
        }

        
    }
}