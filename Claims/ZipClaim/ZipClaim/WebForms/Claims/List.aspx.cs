using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Claims
{
    public partial class List : BaseFilteredPage
    {
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        string serviceOperatorRightGroup = ConfigurationManager.AppSettings["serviceOperatorRightGroup"];
        string dsuZipClaimDelete = ConfigurationManager.AppSettings["dsuZipClaimDelete"];
        string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        private const string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        private const string serviceEngeneerRightGroupVSKey = "serviceEngeneerRightGroupVSKey";
        private const string serviceAdminRightGroupVSKey = "serviceAdminRightGroupVSKey";
        private const string serviceOperatorRightGroupVSKey = "serviceOperatorRightGroupVSKey";
        private const string dsuZipClaimDeleteVSKey = "dsuZipClaimDeleteVSKey";
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

        private const string filteredByUserFilterVSKey = "filteredByUserFilterVSKey";
        private const string isSearchVSKey = "isSearchVSKey";

        private bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        private bool UserIsManager
        {
            get { return (bool)ViewState[serviceManagerRightGroupVSKey]; }
            set { ViewState[serviceManagerRightGroupVSKey] = value; }
        }

        protected bool UserIsEngeneer
        {
            get { return (bool)ViewState[serviceEngeneerRightGroupVSKey]; }
            set { ViewState[serviceEngeneerRightGroupVSKey] = value; }
        }

        private bool UserIsServiceAdmin
        {
            get { return (bool)ViewState[serviceAdminRightGroupVSKey]; }
            set { ViewState[serviceAdminRightGroupVSKey] = value; }
        }

        private bool UserIsOperator
        {
            get { return (bool)ViewState[serviceOperatorRightGroupVSKey]; }
            set { ViewState[serviceOperatorRightGroupVSKey] = value; }
        }

        private bool UserCanDeleteClaim
        {
            get { return (bool)ViewState[dsuZipClaimDeleteVSKey]; }
            set { ViewState[dsuZipClaimDeleteVSKey] = value; }
        }

        private bool FilteredByUserFilter
        {
            get { return Session[filteredByUserFilterVSKey] != null && (bool)Session[filteredByUserFilterVSKey]; }
            set { Session[filteredByUserFilterVSKey] = value; }
        }

        private bool IsSearch
        {
            get { return Session[isSearchVSKey] != null && (bool)Session[isSearchVSKey]; }
            set { Session[isSearchVSKey] = value; }
        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("id", txtId));
            FilterLinks.Add(new FilterLink("sdnum", txtSdNum));
            FilterLinks.Add(new FilterLink("csdnum", txtContractorSdNum));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("engr", ddlEngeneer, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("snum", txtSerialNum));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("state", chklClaimState, "1,3,4,5,6,8,9,10,11,12,13,21,22"));
            //FilterLinks.Add(new FilterLink("etste", chklEtClaimState));//, "10,11,12,13"
            FilterLinks.Add(new FilterLink("wayst", chklWaybillClaimState));//, "14,15,16,18,19,20"
            FilterLinks.Add(new FilterLink("mngr", ddlManager, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("oper", ddlOperator));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("inn", txtContractorInn));
            FilterLinks.Add(new FilterLink("nins", rblNotInSystem));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30")); 

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormUrl = FriendlyUrl.Resolve("~/Claims/Editor");

        protected new void Page_Load(object sender, EventArgs e)
        {
            //Подстановка настроек фильтра
            if (!IsPostBack && !IsSearch && !FilteredByUserFilter)
            {
                if (String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    FilteredByUserFilter = true;
                    RedirectWithUserFilter(User.Id);
                }
            }

            //Сбрасываем условия чтобы впоследствии была возможность подставить значение из сохраненного полдьзовательского фильтра
            FilteredByUserFilter = IsSearch = false;   

            if (!IsPostBack)
            {
                FillFilterLists();

                UserIsManager = Db.Db.Users.CheckUserRights(User.Login, serviceManagerRightGroup);

                UserIsEngeneer = Db.Db.Users.CheckUserRights(User.Login, serviceEngeneersRightGroup);

                UserIsServiceAdmin = Db.Db.Users.CheckUserRights(User.Login, serviceAdminRightGroup);

                UserIsOperator = Db.Db.Users.CheckUserRights(User.Login, serviceOperatorRightGroup);

                UserCanDeleteClaim = Db.Db.Users.CheckUserRights(User.Login, dsuZipClaimDelete);

                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
                
                //btnNewClaim.Visible = UserIsEngeneer;

                SetDefaultFilterValues();

                DisplayFormPars();

                if (Request.QueryString["id"] == null)
                {
                    if (UserIsServiceAdmin)
                    {
                        sdsList.SelectParameters["id_service_admin"].DefaultValue = User.Id.ToString();
                    }
                    if (UserIsEngeneer)
                    {
                        sdsList.SelectParameters["id_engeneer"].DefaultValue = User.Id.ToString();
                    }
                    if (UserIsManager)
                    {
                        sdsList.SelectParameters["id_manager"].DefaultValue = User.Id.ToString();
                    }
                    sdsList.DataBind();
                }
            }


            base.Page_Load(sender, e);

            RegisterStartupScripts();
        }

        private void RedirectWithUserFilter(int idUser)
        {
            DataTable dt = Db.Db.Zipcl.GetUserFilter(idUser);
            string filter = String.Empty;
            if (dt.Rows.Count > 0)
            {
                filter = dt.Rows[0]["filter"].ToString();
            }

            if (!String.IsNullOrEmpty(filter))
            {
                RedirectWithParams(filter, false);
            }
        }

        private void SaveUserFilter(int idUser)
        {
            string filter = Request.Url.Query.Replace("?", "");//.PathAndQuery.Replace("//?", "").Replace("//", "");
            int idUserFilter = Db.Db.Zipcl.SaveUserFilter(idUser, filter);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ddlEngeneer.Enabled = !UserIsEngeneer;
        }

        private void DisplayFormPars()
        {
            pnlNewClaim.Visible = UserIsEngeneer || UserIsSysAdmin;

            //Если пользователь не инженер и не серв.Администратор, то показываем цены
            if (UserIsManager || UserIsSysAdmin || UserIsOperator)
            {
                tblList.Columns[5].Visible = tblList.Columns[6].Visible = true;
                pnlSumCount.Visible = true;

                //var price_in_sum = 
                //tblList.Columns.Cast<DataControlField>().First(c => c.HeaderText == "price_in_sum").Visible = true;
                //var price_out_sum = 
                //tblList.Columns.Cast<DataControlField>().First(c => c.HeaderText == "price_out_sum").Visible = true;
            }
        }

        private void SetDefaultFilterValues()
        {
            //if (UserIsEngeneer)
            //{
            //    MainHelper.DdlSetSelectedValue(ref ddlEngeneer, User.Id);
            //}

            //if (UserIsManager)
            //{
            //    MainHelper.DdlSetSelectedValue(ref ddlManager, User.Id);
            //}

            //Бывает что менеджер и оператор одно лицо
            //if (UserIsOperator && !UserIsManager)
            //{
            //    MainHelper.DdlSetSelectedValue(ref ddlOperator, User.Id);
            //}

            //if (UserIsServiceAdmin)
            //{
            //    MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, User.Id);
            //}
        }

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Zipcl.GetContractorFilterSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.ChkListFill(ref chklClaimState, Db.Db.Zipcl.GetClaimStateSelectionList());
            //MainHelper.ChkListFill(ref chklEtClaimState, Db.Db.Zipcl.GetEtClaimStateSelectionList());
            MainHelper.ChkListFill(ref chklWaybillClaimState, Db.Db.Zipcl.GetWaybillClaimStateSelectionList());

            //string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

            var userList = Db.Db.Users.GetUsersSelectionList();
            MainHelper.DdlFill(ref ddlEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlOperator, Db.Db.Users.GetUsersSelectionList(serviceOperatorRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new Claim().Delete(id, User.Id);
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSetStateDone_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(id);
                claim.IdCreator = User.Id;
                claim.SetDoneState();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnMacthPrice_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(id);
                claim.IdCreator = User.Id;
                claim.SetPriceOkState();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnFailPrice_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(id);
                claim.IdCreator = User.Id;
                claim.SetPriceFailState();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            //Так как при загрузке страницы подтаскивается фильтр пользователя, то отмечаем что его подтаскивать не надо
            IsSearch = true;

            Search();
        }

        protected void btnSaveUserFilter_OnClick(object sender, EventArgs e)
        {
            SaveUserFilter(User.Id);
        }

        //protected void rtrContractList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        SqlDataSource ds = (SqlDataSource)e.Item.FindControl("sdsContract2DevicesList");
        //        ds.SelectParameters["id_contract"].DefaultValue =
        //            DataBinder.Eval(e.Item.DataItem, "id_contract").ToString();
        //        Repeater rtr = (Repeater)e.Item.FindControl("rtrContract2DevicesList");
        //        rtr.DataSourceID = ds.ID;
        //    }
        //}

        protected void tblList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnMacthPrice = e.Row.FindControl("btnMacthPrice") as LinkButton;
                LinkButton btnFailPrice = e.Row.FindControl("btnFailPrice") as LinkButton;
                LinkButton btnSetStateDone = e.Row.FindControl("btnSetStateDone") as LinkButton;
                LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                HiddenField hfDisplaySendState = e.Row.FindControl("hfDisplaySendState") as HiddenField;

                btnMacthPrice.Visible = btnFailPrice.Visible = btnSetStateDone.Visible = btnDelete.Visible = false;

                var data = ((DataRowView) e.Row.DataItem);

                if ((UserIsEngeneer || UserIsSysAdmin) && hfDisplaySendState.Value.Equals("True"))
                {
                    btnDelete.Visible = true;
                }

                if ((UserIsManager || UserIsSysAdmin) && data["display_price_states"].ToString().Equals("1"))
                {
                    btnMacthPrice.Visible = btnFailPrice.Visible = true;
                }

                if ((UserIsServiceAdmin || UserIsSysAdmin) && data["display_done_state"].ToString().Equals("1"))
                {
                    btnSetStateDone.Visible = true;
                }

                if (UserCanDeleteClaim || UserIsSysAdmin)
                {
                    btnDelete.Visible = true;
                }

                //    SqlDataSource ds = (SqlDataSource)e.Row.FindControl("sdsContract2DevicesList");
                //    ds.SelectParameters["id_contract"].DefaultValue =
                //        DataBinder.Eval(e.Row.DataItem, "id_contract").ToString();
                //    Repeater rtr = (Repeater)e.Row.FindControl("rtrContract2DevicesList");
                //    rtr.DataSourceID = ds.ID;
            }
        }

        //private void SetRowsCount(int count = 0)
        //{
        //    lRowsCount.Text = count.ToString();
        //}


        //protected void rtrContract2DevicesList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Repeater rtr = (sender as Repeater);

        //    if (!rtr.Visible)rtr.Visible = true;
        //}
        //protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        //{
        //    int count = e.AffectedRows;
        //    //SetRowsCount(count);
        //}

        protected void tblList_DataBound(object sender, EventArgs e)
        {
            SetCounts();
        }

        private void SetCounts()
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);
            lRowsCount.Text = dv.Count.ToString();

            string sumCountIn = String.Format("{0:N2}", dv.Table.Compute("Sum(price_in_sum)", ""));
            if (string.IsNullOrEmpty(sumCountIn)) sumCountIn = 0.ToString();
            lSummCountIn.Text = sumCountIn;

            string sumCountOut = String.Format("{0:N2}", dv.Table.Compute("Sum(price_out_sum)", ""));
            if (string.IsNullOrEmpty(sumCountOut)) sumCountOut = 0.ToString();
            lSummCountOut.Text = sumCountOut;
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlContractor.ClientID, txtContractorInn.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>

            //<Чекбокс с квадратиком (tristate checkbox)>
            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate.ClientID, chklClaimState.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);

            /*script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate1.ClientID, chklEtClaimState.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox1", script, true);*/

            script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});",
                pnlTristate2.ClientID, chklWaybillClaimState.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox2", script, true);
            //</Чекбокс>
        }

        protected string SetRowMark(string statusSysname)
        {
            string result = string.Empty;

            switch (statusSysname.ToUpper())
            {
                case "0":
                    result = "bg-warning";
                    break;
                case "1":
                    result = "bg-success";
                    break;
            }

            return result;
        }

        protected void sdsList_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.CommandTimeout = 10000;
        }

        protected string GetDateText(string date)
        {
            string result = String.Empty;

            if (!String.IsNullOrEmpty(date) && !String.IsNullOrWhiteSpace(date))
            {
                DateTime dt;
                DateTime.TryParse(date, out dt);

                if (dt != new DateTime() && dt.Year > 1990)
                {
                    result = String.Format("{0:d} ({1:N0} дн.)", dt, ((DateTime.Now - dt).TotalDays));
                }
                else if (dt != new DateTime() && dt.Year == 1899)
                {
                    result = "не уствновлена";
                }
                else
                {
                    result = date;
                }
            }

            return result;
        }
    }
}