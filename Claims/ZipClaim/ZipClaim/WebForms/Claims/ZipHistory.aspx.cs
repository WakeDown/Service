using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Claims
{
    public partial class ZipHistory : BaseFilteredPage
    {
        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("snum", txtSerialNum));
            FilterLinks.Add(new FilterLink("all", rblFirstFif, "-13"));

            BtnSearchClientId = btnSearch.ClientID;
        }
         
        protected string FormUrl = FriendlyUrl.Resolve("~/Claims/Editor");

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }


        protected void txtSerialNum_OnTextChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnAddClaim_Click(object sender, EventArgs e)
        {
            string serialNum = MainHelper.TxtGetText(ref txtSerialNum);
            string queryParams = String.Empty;

            if (serialNum != null)
            {
                queryParams = String.Format("snum={0}", serialNum);
            }

            RedirectWithParams(queryParams, false, FormUrl);
        }

        protected void sdsClimUnitsHistory_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            pnlNoRows.Visible = txtSerialNum.Text.Trim().Length >0 && e.AffectedRows <= 0;
        }
    }
}