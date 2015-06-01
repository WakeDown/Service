using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Claims
{
    public partial class Supply : BaseFilteredPage
    {
        string supplyManRightGroup = ConfigurationManager.AppSettings["supplyManRightGroup"];
        private const string supplyManRightGroupVSKey = "supplyManRightGroupVSKey";

        string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];

        protected bool UserIsSupplyMan
        {
            get { return (bool)ViewState[supplyManRightGroupVSKey]; }
            set { ViewState[supplyManRightGroupVSKey] = value; }
        }

        protected bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            //FilterLinks.Add(new FilterLink("id", txtId));
            //FilterLinks.Add(new FilterLink("state", chklClaimState, "1,3,4,5,6,8,9,10,11,12,13"));
            //FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            //FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("supm", ddlSupplyMan, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("mgr", ddlManager));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();

                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
                UserIsSupplyMan = Db.Db.Users.CheckUserRights(User.Login, supplyManRightGroup);

                SetDefaultVlaues();
            }

            base.Page_Load(sender, e);
        }

        private void SetDefaultVlaues()
        {
            if (UserIsSupplyMan)
            {
                sdsList.SelectParameters["id_resp_supply"].DefaultValue = User.Id.ToString();
            }
        }

        private void FillFilterLists()
        {
            //MainHelper.ChkListFill(ref chklClaimState, Db.Db.Zipcl.GetClaimStateSelectionList());

            var userList = Db.Db.Users.GetUsersSelectionList();
            //MainHelper.DdlFill(ref ddlEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlSupplyMan, Db.Db.Users.GetUsersSelectionList(supplyManRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.DdlFill(ref ddlOperator, Db.Db.Users.GetUsersSelectionList(serviceOperatorRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void tblList_DataBound(object sender, EventArgs e)
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);
            lRowsCount.Text = dv.Count.ToString();
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SetRowEditState(id);
        }

        private void SetRowEditState(int idClaimUnit, bool edit = true)
        {
            foreach (GridViewRow row in tblList.Rows)
            {
                int currId = Convert.ToInt32(((HiddenField)row.FindControl("hfIdClaimUnit")).Value);

                if (currId == idClaimUnit)
                {
                    row.FindControl("btnEdit").Visible = row.FindControl("lblPriceIn").Visible = row.FindControl("lblDeliveryTime").Visible = row.FindControl("btnReturn").Visible = !edit;
                    row.FindControl("btnSave").Visible = row.FindControl("btnCancel").Visible = row.FindControl("txtPriceIn").Visible = row.FindControl("txtDeliveryTime").Visible = (row.FindControl("cvTxtPriceIn") as CompareValidator).Enabled = (row.FindControl("rfvTxtPriceIn") as RequiredFieldValidator).Enabled = (row.FindControl("rfvTxtDeliveryTime") as RequiredFieldValidator).Enabled = edit;

                    //Если Номенклатурный номер запрошен у Снабжения, то обязательно к заполнению
                    if ((row.FindControl("hfNoNomenclatureNum") as HiddenField).Value == "True")
                    {
                        row.FindControl("lblNomenclatureNum").Visible = !edit;
                        row.FindControl("txtNomenclatureNum").Visible = (row.FindControl("rfvTxtNomenclatureNum") as RequiredFieldValidator).Enabled = edit;
                    }

                    if (edit)
                    {
                        ((TextBox)row.FindControl("txtPriceIn")).Focus();
                    }
                }
            }
        }

        protected void btnSendReturn_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int rowIndex = -1;

            foreach (GridViewRow row in tblList.Rows)
            {
                int currId = Convert.ToInt32(((HiddenField)row.FindControl("hfIdClaimUnit")).Value);
                if (currId == id)
                {
                    rowIndex = row.RowIndex;
                    break;
                }
            }

            //((WebControl)sender).NamingContainer.ID

            if (rowIndex >= 0)
            {
                HiddenField hfIdClaim = (HiddenField)tblList.Rows[rowIndex].FindControl("hfIdClaim");
                int idClaim = MainHelper.HfGetValueInt32(ref hfIdClaim);
                var txtReturnDescr = tblList.Rows[rowIndex].FindControl("txtReturnDescr") as TextBox;
                string descr = MainHelper.TxtGetText(ref txtReturnDescr);

                ClaimUnit cu = new ClaimUnit()
                {
                    Id = id,
                    IdClaim = idClaim,
                    IdCreator = User.Id
                };

                try
                {
                    cu.SupplyReturn(descr);
                }
                catch (Exception ex)
                {
                    ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
                }

                Claim c = new Claim() { Id = idClaim, IdCreator = User.Id };

                try
                {
                    c.SupplyReturnClaim();
                }
                catch (Exception ex)
                {
                    ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
                }

                tblList.DataBind();
            }

            SetRowEditState(id, false);
        }

        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            var pnl = ((sender as WebControl).NamingContainer as GridViewRow).FindControl("pnlReturnDescr") as Panel;

            if (pnl != null)
            {
                pnl.Visible = true;

                UpdatePanel1.Update();
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            tblList.DataBind();

            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SetRowEditState(id, false);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            int rowIndex = -1;

            foreach (GridViewRow row in tblList.Rows)
            {
                int currId = Convert.ToInt32(((HiddenField)row.FindControl("hfIdClaimUnit")).Value);
                if (currId == id)
                {
                    rowIndex = row.RowIndex;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                HiddenField hfIdClaim = (HiddenField)tblList.Rows[rowIndex].FindControl("hfIdClaim");
                int idClaim = MainHelper.HfGetValueInt32(ref hfIdClaim);

                TextBox txtPriceIn = (TextBox)tblList.Rows[rowIndex].FindControl("txtPriceIn");
                decimal priceIn = MainHelper.TxtGetTextDecimal(ref txtPriceIn);

                TextBox txtDeliveryTime = (TextBox)tblList.Rows[rowIndex].FindControl("txtDeliveryTime");
                string deliveryTime = MainHelper.TxtGetText(ref txtDeliveryTime);

                TextBox txtNomenclatureNum = (TextBox)tblList.Rows[rowIndex].FindControl("txtNomenclatureNum");
                string nomenclatureNum = MainHelper.TxtGetText(ref txtNomenclatureNum);

                ClaimUnit cu = new ClaimUnit()
                {
                    Id = id,
                    IdClaim = idClaim,
                    PriceIn = priceIn,
                    DeliveryTime = deliveryTime,
                    IdCreator = User.Id,
                    Count = null,
                    NomenclatureNum = nomenclatureNum,
                    IdSupplyMan = User.Id
                };

                try
                {
                    cu.Save();
                }
                catch (Exception ex)
                {
                    ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
                }
                
                Claim c = new Claim() { Id = idClaim, IdCreator = User.Id };

                try
                {
                    c.SetPriceSetState(true);
                }
                catch (Exception ex)
                {
                    ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
                }

                tblList.DataBind();
            }

            SetRowEditState(id, false);
        }

        protected void btnReturnCancel_OnClick(object sender, EventArgs e)
        {
            var pnl = ((sender as WebControl).NamingContainer as GridViewRow).FindControl("pnlReturnDescr") as Panel;

            if (pnl != null)
            {
                pnl.Visible = false;

                UpdatePanel1.Update();
            }
        }
    }
}