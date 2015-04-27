using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Install.Models;
using Install.Objects;
using Microsoft.AspNet.FriendlyUrls;

namespace Install.WebForms.DevInst
{
    public partial class List : BaseFilteredPage
    {
        protected string FormUrl = FriendlyUrl.Resolve("~/DevInst/Editor");

        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("number", txtNumber));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("state", ddlClaimState));
            FilterLinks.Add(new FilterLink("mngr", ddlManager));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("inn", txtContractorInn));
            FilterLinks.Add(new FilterLink("sernum", txtFilterSerialNum));
            FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "10"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            lRowsCount.Text = e.AffectedRows.ToString();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new DeviceInstall().Delete(id, User.Id);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }
    }
}