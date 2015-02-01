using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class Counters : BaseFilteredPage
    {
        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsReport
        {
            get { return (Page.Master.Master as Site).UserIsReport; }
        }

        protected override void FillFilterLinksDefaults()
        {
            if (FilterLinks != null) return;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!UserIsSysAdmin && !UserIsReport)
            {
                Response.Redirect(FriendlyUrl.Href("~/Error"));
            }
        }
    }
}