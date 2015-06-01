using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Objects;
using ZipClaim.WebForms.Masters;

namespace ZipClaim.WebForms.Client
{
    public partial class List : BaseFilteredPage
    {
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

        //private const string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        //private const string serviceEngeneerRightGroupVSKey = "serviceEngeneerRightGroupVSKey";
        //private const string serviceEngeneersRightGroupVSKey = "serviceEngeneersRightGroupVSKey";

        private const string isSearchVSKey = "isSearchVSKey";

        //public bool UserIsEngeneer
        //{
        //    get { return (bool)ViewState[serviceEngeneersRightGroupVSKey]; }
        //    set { ViewState[serviceEngeneersRightGroupVSKey] = value; }
        //}


        protected string DetailUrl = FriendlyUrl.Resolve("~/Client/Detail");

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("id", txtId));
            FilterLinks.Add(new FilterLink("engr", ddlEngeneer));
            FilterLinks.Add(new FilterLink("snum", txtSerialNum));
            FilterLinks.Add(new FilterLink("state", chklClaimState, "1,3,4,5,6,8,9,10,11,12,13"));
            //FilterLinks.Add(new FilterLink("etste", chklEtClaimState));//, "10,11,12,13"
            FilterLinks.Add(new FilterLink("wayst", chklWaybillClaimState));//, "14,15,16,18,19,20"
            FilterLinks.Add(new FilterLink("mngr", ddlManager, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);


            if (!IsPostBack)
            {
                int contractorId;
                int.TryParse(User.Login, out contractorId);//Вставляем только число id_contractor 

                sdsList.SelectParameters["id_contractor"].DefaultValue = contractorId.ToString();

                if (contractorId > 0)
                {
                    lblContractorName.Text = Db.Db.Unit.GetContractorSelectionList(null, contractorId).Rows[0]["full_name"].ToString();
                    DataTable dtCtrs =Db.Db.Srvpl.GetContractList(contractorId <= 0 ? -999 : contractorId);
                    pnlNoData.Controls.Clear();
                    //Если нет активных договоров
                    if (dtCtrs.Rows.Count == 0)
                    {
                        var h2 = new HtmlGenericControl("h2");
                        h2.InnerText = "Нет активных договоров";

                        pnlNoData.Controls.Add(h2);
                    }
                }
            }

            RegisterStartupScripts();
        }



        private void FillFilterLists()
        {
            MainHelper.ChkListFill(ref chklClaimState, Db.Db.Zipcl.GetClaimStateSelectionList());
            //MainHelper.ChkListFill(ref chklEtClaimState, Db.Db.Zipcl.GetEtClaimStateSelectionList());
            MainHelper.ChkListFill(ref chklWaybillClaimState, Db.Db.Zipcl.GetWaybillClaimStateSelectionList());

            //string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

            var userList = Db.Db.Users.GetUsersSelectionList();
            MainHelper.DdlFill(ref ddlEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void sdsList_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.CommandTimeout = 10000;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void tblList_DataBound(object sender, EventArgs e)
        {
            SetCounts();
        }

        private void SetCounts()
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);
            lRowsCount.Text = dv.Count.ToString();
        }

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            //<Чекбокс с квадратиком (tristate checkbox)>
            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate.ClientID, chklClaimState.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);

            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate2.ClientID, chklWaybillClaimState.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox2", script, true);
            //</Чекбокс>
        }
    }
}