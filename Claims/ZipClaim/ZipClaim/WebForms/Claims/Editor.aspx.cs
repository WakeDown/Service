using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Claims
{
    public partial class Editor : BasePage
    {
        private bool btnRequestPriceDisplay { get; set; }
        protected string ListUrl = FriendlyUrl.Resolve("~/Claims");
        protected const string CountersReportUrl = "http://service-plan.un1t.group/Reports/CountersDetail";

        protected string FormTitle;

        private string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        private string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        private string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        private string serviceOperatorRightGroup = ConfigurationManager.AppSettings["serviceOperatorRightGroup"];
        private string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        private string techRightGroup = ConfigurationManager.AppSettings["techRightGroup"];
        private const string techRightGroupVSKey = "techRightGroupVSKey";
        private const string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        private const string serviceOperatorRightGroupVSKey = "serviceOperatorRightGroupVSKey";
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

        protected int IdDevice
        {
            get
            {
                int id;
                int.TryParse(hfIdDevice.Value, out id);

                return id;
            }
        }

        protected int IdContract
        {
            get
            {
                int id;
                int.TryParse(hfIdContract.Value, out id);

                return id;
            }
        }

        protected bool UserIsTech
        {
            get { return (bool)ViewState[techRightGroupVSKey]; }
            set { ViewState[techRightGroupVSKey] = value; }
        }

        protected bool UserIsManager
        {
            get { return (bool)ViewState[serviceManagerRightGroupVSKey]; }
            set { ViewState[serviceManagerRightGroupVSKey] = value; }
        }

        protected bool UserIsOperator
        {
            get { return (bool)ViewState[serviceOperatorRightGroupVSKey]; }
            set { ViewState[serviceOperatorRightGroupVSKey] = value; }
        }

        protected bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        //protected string c2dFormUrl;

        //protected void Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        FillLists();
        //    }
        //}

        private void SetDafaultValues()
        {
            MainHelper.DdlSetSelectedValue(ref ddlEngeneer, User.Id);

            //Устанавливает параметры инженера и админа
            if (!String.IsNullOrEmpty(Request.QueryString["esid"]))
            {
                int engId = Db.Db.Users.GetUserBySid(Request.QueryString["esid"]).Id;
                MainHelper.DdlSetSelectedValue(ref ddlEngeneer, engId);
            }
            if (!String.IsNullOrEmpty(Request.QueryString["asid"]))
            {
                int admId = Db.Db.Users.GetUserBySid(Request.QueryString["asid"]).Id;
                MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, admId);
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //c2dFormUrl = "~/Contracts/Devices/Editor";

            Claim claim = new Claim();

            if (!IsPostBack)
            {
                lFormTitle.Text = "Добавление заявки на ЗИП";

                //Устанавливаем серийный номер
                if (Request.QueryString["snum"] != null)
                {
                    MainHelper.TxtSetText(ref txtSerialNum, Request.QueryString["snum"]);
                    txtSerialNum_OnTextChanged(txtSerialNum, new EventArgs());
                }

                UserIsManager = Db.Db.Users.CheckUserRights(User.Login, serviceManagerRightGroup);
                UserIsOperator = Db.Db.Users.CheckUserRights(User.Login, serviceOperatorRightGroup);
                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
                UserIsTech = Db.Db.Users.CheckUserRights(User.Login, techRightGroup);

                FillLists();

                bool fillCtrs = false;

                if (Id > 0)
                {
                    claim = new Claim(Id);
                    FillContractorList(claim.IdContractor);
                    fillCtrs = true;
                    FillFormData(claim);
                }

                if (!fillCtrs)
                {
                    FillContractorList();
                }

                //При переходе из ServiceClaim для заказа ЗИП передается id заявки в параметре srvid
                hfServSheetId.Value = Request.QueryString["ssid"];
                hfIdServClaim.Value = Request.QueryString["servid"];
            }

            //sdsList.UpdateParameters["id_creator"].DefaultValue = User.Id.ToString();

            RegisterStartupScripts();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(Id > 0))
                {
                    SetDafaultValues();
                }
            }

            DisplayFormParts();
        }

        //protected void btnCountersReport_Click(object sender, EventArgs e)
        //{
        //    string url = String.Format("{0}?id={1}&cid={2}", CountersReportUrl, IdDevice, IdContract);
        //    Response.Redirect(url);
        //}

        private void DisplayFormParts()
        {
            bool isEdit = Id > 0;

            pnlDevicesListWarning.Visible = !isEdit;
            pnlClaimUnits.Visible = isEdit;
            pnlUsers.Visible = isEdit;

            //Если нет возможности перейти на отчет то выводим предупреждение и дизейблим кнопку
            if (IdDevice <= 0 || IdContract <= 0)
            {
                counterReportError.Visible = true;
                btnCountersReport.Attributes.Add("disabled", "true");
                //btnCountersReport.Attributes.Add("title", "Переход к отчету невозможен!");
            }
            else
            {
                counterReportError.Visible = false;
                //btnCountersReport.Attributes["disabled"]
            }

            //Доступы к элементам и кнопкам по группам
            //сначала скрваем все недоступные элементы, затем показываем в зависимости от доступа
            btnSetStateSend.Visible =
                btnMacthPrice.Visible =
                    btnFailPrice.Visible =
                        txtRequestNum.Enabled =
                            ddlOperator.Enabled =
                                rfvDdlOperator.Enabled =
                                    ddlManager.Enabled =
                                        rfvDdlManager.Enabled =
                                            btnSetStateDone.Visible =
                                                btnSetStatePriceSet.Visible =
                                                    pnlCancelComment.Visible =
                                                        txtServiceDeskNum.Enabled = btnRequestPrice.Visible = btnDelete.Visible = btnZipConfirm.Visible = false;

            string currLogin = User.Login;
            bool userIsEngeneer = Db.Db.Users.CheckUserRights(currLogin, serviceEngeneersRightGroup);
            //bool userIsManager = Db.Db.Users.CheckUserRights(currLogin, serviceManagerRightGroup);
            bool userIsServiceAdmin = Db.Db.Users.CheckUserRights(currLogin, serviceAdminRightGroup);
            //bool userIsOperator = Db.Db.Users.CheckUserRights(currLogin, serviceOperatorRightGroup);
            //bool userIsSysAdmin = Db.Db.Users.CheckUserRights(currLogin, sysAdminRightGroup);

            divContractNumber.Visible = divContractType.Visible = true;

            if ((userIsEngeneer || UserIsSysAdmin))
            {
                txtServiceDeskNum.Enabled = pnlTop.Visible = true;

                if (hfDisplaySendState.Value.Equals("True"))
                {
                    btnSetStateSend.Visible = true;
                    btnDelete.Visible = true;
                }
            }

            if (UserIsManager || UserIsSysAdmin)
            {
                ddlOperator.Enabled = rfvDdlOperator.Enabled = true;

                if (UserIsSysAdmin)
                {
                    rfvDdlOperator.Enabled = false;
                }

                if (hfDisplayPriceStates.Value.Equals("True"))
                {
                    btnMacthPrice.Visible = btnFailPrice.Visible = true;
                }

                if (hfDisplayCancelState.Value.Equals("True"))
                {
                    pnlCancelComment.Visible = true;
                }
            }

            if (userIsServiceAdmin || UserIsSysAdmin)
            {
                ddlManager.Enabled = rfvDdlManager.Enabled = txtServiceDeskNum.Enabled = true;

                if (UserIsSysAdmin)
                {
                    rfvDdlManager.Enabled = false;
                }

                if (hfDisplayDoneState.Value.Equals("True"))
                {
                    btnSetStateDone.Visible = true;
                }
            }

            if (UserIsOperator || UserIsSysAdmin)
            {
                txtRequestNum.Enabled = true;

                if (hfDisplayPriceSet.Value.Equals("True"))
                {
                    btnSetStatePriceSet.Visible = true;
                    //btnRequestPrice.Visible = 
                    btnRequestPriceDisplay = true;//Еще нужно проверить есть ли записи для отправки, это происходит в другом месте
                }
            }

            //Если пользователь не Менеджер, Оператор или админ. системы, то показываем цены
            if (UserIsManager || UserIsSysAdmin || UserIsOperator)
            {
                tblClaimUnitList.Columns[5].Visible =
                    tblClaimUnitList.Columns[6].Visible =
                        tblClaimUnitList.Columns[7].Visible =
                            tblClaimUnitList.Columns[9].Visible =
                                tblClaimUnitList.Columns[10].Visible =
                                    tblClaimUnitList.Columns[11].Visible = tblClaimUnitList.Columns[12].Visible = true;

                pnlSumCount.Visible = true;
            }

            if (UserIsSysAdmin || UserIsTech)
            {
                if (hfDisplayZipConfirmState.Value.Equals("True"))
                {
                    btnZipConfirm.Visible = true;
                }
            }

            DisplayOftenSelectedParts();
            DisplayOftenSelectedNoData();
        }



        private void FillLists()
        {
            MainHelper.DdlFill(ref ddlEngeneerConclusion, Db.Db.Zipcl.GetEngeneerConclusionSelectionList(), true);

            var userList = Db.Db.Users.GetUsersSelectionList();

            MainHelper.DdlFill(ref ddlEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup, userList),
                true);
            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList),
                true);
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup, userList),
                true);
            MainHelper.DdlFill(ref ddlOperator, Db.Db.Users.GetUsersSelectionList(serviceOperatorRightGroup, userList),
                true);
        }

        private void FillContractorList(int? idContractor = null)
        {
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(null, idContractor), true);
        }

        protected void txtContractorInn_OnTextChanged(object sender, EventArgs e)
        {
            string text = MainHelper.TxtGetText(ref txtContractorInn);
            if (!String.IsNullOrEmpty(text))
            {
                MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text), false);
                ddlContractor.Focus();
            }
            else
            {
                ddlContractor.Items.Clear();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Save();
                string queryParams = String.Format("id={0}", id);
                RedirectWithParams(queryParams);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private int Save()
        {
            bool isNewClaim = !(Id > 0);

            Claim claim = GetFormData();
            //if (!String.IsNullOrEmpty(claim.RequestNum)) claim.SetRequestNumState();
            claim.Save();
            //if (!String.IsNullOrEmpty(claim.RequestNum)) claim.SetRequestNumState();
            int id = claim.Id;

            //Для установки статуса Назначено сохраняем еще раз если есть 
            //if (isNewClaim && claim.IdManager > 0) claim.SetManSelState();

            string messageText = String.Format("Сохранение заявки №{0} прошло успешно", claim.Id);
            ServerMessageDisplay(new[] { phServerMessage }, messageText);
            return id;
        }

        protected void btnAddNewClaim_Click(object sender, EventArgs e)
        {
            Save();
            RedirectWithParams("", false);
        }

        protected void btnSaveAndAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams("", false);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }


        private Claim GetFormData()
        {
            Claim claim = new Claim();

            claim.Id = Id;
            claim.IdDevice = MainHelper.HfGetValueInt32(ref hfIdDevice, true);
            claim.SerialNum = MainHelper.TxtGetText(ref txtSerialNum).ToUpper();
            claim.DeviceModel = MainHelper.TxtGetText(ref txtDeviceModel);
            claim.IdCity = MainHelper.HfGetValueInt32(ref hfIdCity, true);
            claim.City = MainHelper.TxtGetText(ref txtCity);
            claim.Address = MainHelper.TxtGetText(ref txtAddress);
            claim.Descr = MainHelper.TxtGetText(ref txtDescr);
            claim.Counter = MainHelper.TxtGetTextInt32(ref txtCounter);
            claim.IdContractor = MainHelper.DdlGetSelectedValueInt(ref ddlContractor);
            claim.IdServiceEngeneer = MainHelper.DdlGetSelectedValueInt(ref ddlEngeneer, true);
            claim.IdEngeneerConclusion = MainHelper.DdlGetSelectedValueInt(ref ddlEngeneerConclusion);
            claim.RequestNum = MainHelper.TxtGetText(ref txtRequestNum);
            claim.IdManager = ddlManager.Enabled
                ? MainHelper.DdlGetSelectedValueInt(ref ddlManager, true)
                : MainHelper.HfGetValueInt32(ref hfIdManager, true);
            claim.IdOperator = MainHelper.DdlGetSelectedValueInt(ref ddlOperator, true);
            claim.IdServiceAdmin = MainHelper.DdlGetSelectedValueInt(ref ddlServiceAdmin, true);
            claim.ServiceDeskNum = MainHelper.TxtGetText(ref txtServiceDeskNum);
            claim.CounterColour = MainHelper.TxtGetTextInt32(ref txtCounterColour);
            claim.CancelComment = MainHelper.TxtGetText(ref txtCancelComment);
            claim.ObjectName = MainHelper.TxtGetText(ref txtObjectName);
            claim.WaybillNum = MainHelper.TxtGetText(ref txtWaybillNum);
            claim.ContractNum = lblContractNumber.Text;
            claim.ContractType = lblContractType.Text;
            claim.ContractorSdNum = MainHelper.TxtGetText(ref txtContractorSdNum);
            claim.ServiceIdServSheet = MainHelper.HfGetValue(ref hfServSheetId);
            claim.ServiceIdClaim = MainHelper.HfGetValue(ref hfIdServClaim);

            claim.IdCreator = User.Id;

            return claim;
        }

        private void FillFormData(Claim claim)
        {
            bool isEdit = Id > 0;

            lFormTitle.Text = !isEdit
                ? "Добавление заявки на ЗИП"
                : String.Format("Редактирование  заявки на ЗИП №{0} от {1:d}", claim.Id, claim.DateCreate);
            hfContractNumAmdDate.Value = !isEdit ? "" : String.Format("№{0} от {1:d}", claim.Id, claim.DateCreate);

            MainHelper.HfSetValue(ref hfDisplayDoneState, claim.DisplayDoneState);
            //MainHelper.HfSetValue(ref hfDi, claim.DisplayPriceSet);
            MainHelper.HfSetValue(ref hfDisplayPriceSet, claim.DisplayPriceSet);
            MainHelper.HfSetValue(ref hfDisplayPriceStates, claim.DisplayPriceState);
            MainHelper.HfSetValue(ref hfDisplaySendState, claim.DisplaySendState);
            MainHelper.HfSetValue(ref hfDisplayZipConfirmState, claim.DisplayZipConfirmState);
            MainHelper.HfSetValue(ref hfDisplayCancelState, claim.DisplayCancelState);

            MainHelper.HfSetValue(ref hfIdDevice, claim.IdDevice);
            MainHelper.HfSetValue(ref hfIdContract, claim.IdContract);
            btnCountersReport.HRef = String.Format("{0}?id={1}&cid={2}", CountersReportUrl, claim.IdDevice, claim.IdContract);

            MainHelper.TxtSetText(ref txtSerialNum, claim.SerialNum);
            MainHelper.TxtSetText(ref txtDeviceModel, claim.DeviceModel);
            MainHelper.TxtSetText(ref txtCity, claim.City);
            MainHelper.HfSetValue(ref hfIdCity, claim.IdCity);
            MainHelper.TxtSetText(ref txtAddress, claim.Address);
            MainHelper.TxtSetText(ref txtCounter, claim.Counter);
            MainHelper.TxtSetText(ref txtDescr, claim.Descr);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(null, claim.IdContractor ?? -1),
                false);
            MainHelper.DdlSetSelectedValue(ref ddlContractor, claim.IdContractor);
            MainHelper.DdlSetSelectedValue(ref ddlEngeneerConclusion, claim.IdEngeneerConclusion);
            MainHelper.DdlSetSelectedValue(ref ddlEngeneer, claim.IdServiceEngeneer);
            MainHelper.DdlSetSelectedValue(ref ddlManager, claim.IdManager);
            MainHelper.HfSetValue(ref hfIdManager, claim.IdManager);
            MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, claim.IdServiceAdmin);
            MainHelper.DdlSetSelectedValue(ref ddlOperator, claim.IdOperator);
            MainHelper.TxtSetText(ref txtRequestNum, claim.RequestNum);
            MainHelper.TxtSetText(ref txtServiceDeskNum, claim.ServiceDeskNum);
            MainHelper.TxtSetText(ref txtCounterColour, claim.CounterColour);
            MainHelper.TxtSetText(ref txtCancelComment, claim.CancelComment);
            lblCancelComment.Text = claim.CancelComment;
            MainHelper.TxtSetText(ref txtObjectName, claim.ObjectName);
            MainHelper.TxtSetText(ref txtWaybillNum, claim.WaybillNum);
            lblContractNumber.Text = claim.ContractNum;
            lblContractType.Text = claim.ContractType;
            MainHelper.TxtSetText(ref txtContractorSdNum, claim.ContractorSdNum);

            DataTable dt = Db.Db.Zipcl.CheckDeviceBySerialNum(claim.SerialNum);
            if (dt.Rows.Count > 0)
            {
                pnlZipState.Attributes.Clear();
                pnlZipState.Attributes.Add("class", dt.Rows[0]["zip_state"].ToString());
                hfZipStateSysName.Value = dt.Rows[0]["zip_state_sys_name"].ToString();
            }

            if (claim.Id > 0)
            {
                txtDeviceModel.Enabled =
                    txtCity.Enabled = txtAddress.Enabled = txtContractorInn.Enabled = ddlContractor.Enabled = false;
            }

            if (UserIsSysAdmin)
            {
                txtDeviceModel.Enabled = true;
            }

            DisplayOftenSelectedParts();

            rfvTxtSerialNum.Enabled = String.IsNullOrEmpty(txtInvNum.Text);
            rfvTxtInvNum.Enabled = String.IsNullOrEmpty(txtSerialNum.Text);

            if (claim.HideTop)
            {
                string script = String.Format(@"$(document).ready(function() {{
    $('#oftenSelectedPanel').collapse({{'toggle': false}});
$('#oftenSelectedPanel').collapse('hide');
}});");
                ScriptManager.RegisterStartupScript(this, GetType(), "HideTopList", script, true);
            }
        }


        //protected string GetDevicesListUrl(int id)
        //{
        //    string url = "~/Contracts/Devices/Editor";

        //    if (id != null)
        //    { url = RedirectWithParams(String.Format("id={0}", id),); }

        //    return url;
        //}

        private void RegisterStartupScripts()
        {
            string script = string.Empty;
            //<Фильтрация списка по вводимому тексту>
            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});",
                ddlContractor.ClientID, txtContractorInn.ClientID);
            //Очень долго отрабатывает на слабых компах, решено заменить на серверный аналог
            //ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>

            //<Копирование информации в буфер>
            //            script = String.Format(@"$(function() {{$('#{0}').click(function() {{var clip = new ZeroClipboard.Client();
            //                var myTextToCopy = 'Hi, this is the text to copy!';
            //                clip.setText( myTextToCopy );
            //                clip.glue( '{0}' );
            //}});}});", aCopyInfo.ClientID);
            //            script = String.Format(@"$(function() {{var clip = new ZeroClipboard.Client();
            //                var myTextToCopy = 'Hi, this is the text to copy!';
            //                clip.setText( myTextToCopy );
            //                clip.glue( '{0}' );
            //alert('test');
            //}});", aCopyInfo.ClientID);
            //            ScriptManager.RegisterStartupScript(this, GetType(), "copyInfo", script, true);
            //</Копирование информации в буфер>



        }



        protected void txtSerialNum_OnTextChanged(object sender, EventArgs e)
        {
            string serialNum = MainHelper.TxtGetText(ref txtSerialNum);
            DataTable dt = Db.Db.Zipcl.CheckDeviceBySerialNum(serialNum);

            FillForm(dt);
        }

        protected void txtInvNum_OnTextChanged(object sender, EventArgs e)
        {
            string invNum = MainHelper.TxtGetText(ref txtInvNum);
            DataTable dt = Db.Db.Zipcl.CheckDeviceByInvNum(invNum);

            FillForm(dt);
        }

        private void FillForm(DataTable dt)
        {
            txtDeviceModel.Enabled =
                txtCity.Enabled =
                    txtAddress.Enabled = txtObjectName.Enabled = txtContractorInn.Enabled = ddlContractor.Enabled = true;
            txtDeviceModel.Text =
                txtCity.Text = txtAddress.Text = txtObjectName.Text = txtContractorInn.Text = String.Empty;
            ddlContractor.Items.Clear();
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlServiceAdmin);
            MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddlManager);
            pnlZipState.Attributes.Clear();

            rfvTxtSerialNum.Enabled = String.IsNullOrEmpty(txtInvNum.Text);
            rfvTxtInvNum.Enabled = String.IsNullOrEmpty(txtSerialNum.Text);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                MainHelper.TxtSetText(ref txtSerialNum, dr["serial_num"].ToString(), false);
                MainHelper.TxtSetText(ref txtInvNum, dr["inv_num"].ToString(), false);

                MainHelper.HfSetValue(ref hfIdDevice, dr["id_device"].ToString());
                MainHelper.TxtSetText(ref txtDeviceModel, dr["model"].ToString(), false);
                MainHelper.HfSetValue(ref hfIdCity, dr["id_city"].ToString());
                MainHelper.TxtSetText(ref txtCity, dr["city"].ToString(), false);
                MainHelper.TxtSetText(ref txtAddress, dr["address"].ToString(), false);
                MainHelper.TxtSetText(ref txtObjectName, dr["object_name"].ToString(), false);
                txtContractorInn.Enabled = false;
                MainHelper.DdlFill(ref ddlContractor,
                    Db.Db.Unit.GetContractorSelectionList(null, Convert.ToInt32(dr["id_contractor"])));
                MainHelper.DdlSetSelectedValue(ref ddlContractor, dr["id_contractor"].ToString(), false);
                MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, dr["id_service_admin"].ToString());
                MainHelper.DdlSetSelectedValue(ref ddlManager, dr["id_manager"].ToString());
                MainHelper.HfSetValue(ref hfIdManager, dr["id_manager"].ToString());
                pnlZipState.Attributes.Add("class", dr["zip_state"].ToString());

                divContractNumber.Visible = divContractType.Visible = true;
                lblContractNumber.Text = dr["contract_number"].ToString();
                lblContractType.Text = dr["contract_type"].ToString();

                MainHelper.DdlSetSelectedValue(ref ddlOperator, dr["id_operator"].ToString());

                txtDescr.Focus();
            }
            else
            {
                txtDeviceModel.Focus();
                divContractNumber.Visible = divContractType.Visible = false;
            }
        }

        protected void btnClaimUnitDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new ClaimUnit().Delete(id);
                //RedirectWithParams();
                tblClaimUnitList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnAddNewOftenSelected_OnCLick(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rtrOftenSelected.Items)
            {
                CheckBox chkOftenSelectedSet = (CheckBox)item.FindControl("chkOftenSelectedSet");

                if (chkOftenSelectedSet.Checked)
                {
                    ClaimUnit claimUnit = new ClaimUnit();
                    claimUnit.IdClaim = Id;

                    Label lblOftenSelectedCatalogNum = (Label)item.FindControl("lblOftenSelectedCatalogNum");
                    claimUnit.CatalogNum = lblOftenSelectedCatalogNum.Text;

                    Label lblOftenSelectedName = (Label)item.FindControl("lblOftenSelectedName");
                    claimUnit.Name = lblOftenSelectedName.Text;

                    TextBox txtCount = (TextBox)item.FindControl("txtOftenSelectedCount");
                    claimUnit.Count = MainHelper.TxtGetTextInt32(ref txtCount);

                    claimUnit.IdCreator = User.Id;

                    claimUnit.Save(true);
                }
            }

            foreach (RepeaterItem item in rtrOftenSelected2.Items)
            {
                CheckBox chkOftenSelectedSet = (CheckBox)item.FindControl("chkOftenSelectedSet");

                if (chkOftenSelectedSet.Checked)
                {
                    ClaimUnit claimUnit = new ClaimUnit();
                    claimUnit.IdClaim = Id;

                    Label lblOftenSelectedCatalogNum = (Label)item.FindControl("lblOftenSelectedCatalogNum");
                    claimUnit.CatalogNum = lblOftenSelectedCatalogNum.Text;

                    Label lblOftenSelectedName = (Label)item.FindControl("lblOftenSelectedName");
                    claimUnit.Name = lblOftenSelectedName.Text;

                    TextBox txtCount = (TextBox)item.FindControl("txtOftenSelectedCount");
                    claimUnit.Count = MainHelper.TxtGetTextInt32(ref txtCount);

                    claimUnit.IdCreator = User.Id;

                    claimUnit.Save(true);
                }
            }

            foreach (RepeaterItem item in rtrOftenSelected3.Items)
            {
                CheckBox chkOftenSelectedSet = (CheckBox)item.FindControl("chkOftenSelectedSet");

                if (chkOftenSelectedSet.Checked)
                {
                    ClaimUnit claimUnit = new ClaimUnit();
                    claimUnit.IdClaim = Id;

                    Label lblOftenSelectedCatalogNum = (Label)item.FindControl("lblOftenSelectedCatalogNum");
                    claimUnit.CatalogNum = lblOftenSelectedCatalogNum.Text;

                    Label lblOftenSelectedName = (Label)item.FindControl("lblOftenSelectedName");
                    claimUnit.Name = lblOftenSelectedName.Text;

                    TextBox txtCount = (TextBox)item.FindControl("txtOftenSelectedCount");
                    claimUnit.Count = MainHelper.TxtGetTextInt32(ref txtCount);

                    claimUnit.IdCreator = User.Id;

                    claimUnit.Save(true);
                }
            }

            rtrOftenSelected.DataBind();
            rtrOftenSelected2.DataBind();
            rtrOftenSelected3.DataBind();
            tblClaimUnitList.DataBind();
            (tblClaimUnitList.FooterRow.FindControl("txtCatalogNum") as TextBox).Focus();
        }

        protected void btnAddNew_OnCLick(object sender, EventArgs e)
        {
            GridViewRow footer = tblClaimUnitList.FooterRow;

            ClaimUnit claimUnit = new ClaimUnit();
            claimUnit.IdClaim = Id;

            TextBox txtCatalogNum = (TextBox)footer.FindControl("txtCatalogNum");
            claimUnit.CatalogNum = MainHelper.TxtGetText(ref txtCatalogNum);

            TextBox txtName = (TextBox)footer.FindControl("txtName");
            claimUnit.Name = MainHelper.TxtGetText(ref txtName);

            TextBox txtCount = (TextBox)footer.FindControl("txtCount");
            claimUnit.Count = MainHelper.TxtGetTextInt32(ref txtCount);

            //TextBox txtDeliveryTime = (TextBox)footer.FindControl("txtDeliveryTime");
            //claimUnit.DeliveryTime = MainHelper.TxtGetText(ref txtDeliveryTime);

            //TextBox txtPriceIn = (TextBox)footer.FindControl("txtPriceIn");
            //claimUnit.PriceIn = MainHelper.TxtGetTextInt32(ref txtPriceIn);

            //TextBox txtPriceOut = (TextBox)footer.FindControl("txtPriceOut");
            //claimUnit.PriceOut = MainHelper.TxtGetTextInt32(ref txtPriceOut);

            claimUnit.IdCreator = User.Id;

            claimUnit.Save();

            //RedirectWithParams();
            tblClaimUnitList.DataBind();

            lLastClaim.Text = String.Empty;
            (tblClaimUnitList.FooterRow.FindControl("txtCatalogNum") as TextBox).Focus();
        }

        protected void tblClaimUnitList_OnPreRender(object sender, EventArgs e)
        {
            //tblClaimUnitList.Columns[0].Visible = false;
            //DisplayStateButtons();
        }

        protected void tblClaimUnitList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string nomNum = ((DataRowView)e.Row.DataItem)["nomenclature_num"].ToString();

                if (!String.IsNullOrEmpty(nomNum))
                {
                    DataTable dt = Db.Db.Zipcl.GetNomenclatureDataByNumber(nomNum);//Берем данные по позиции из Эталон

                    if (dt.Rows.Count > 0)
                    {
                        (e.Row.FindControl("lblEtCount") as Label).Text = dt.Rows[0]["count"].ToString();

                        string priceStr = dt.Rows[0]["price"].ToString();

                        (e.Row.FindControl("lblEtPrice") as Label).Text = priceStr;

                        Label lblPriceIn = (e.Row.FindControl("lblPriceIn") as Label);

                        if (!priceStr.Equals("-") && lblPriceIn != null && String.IsNullOrEmpty(lblPriceIn.Text))
                        {
                            decimal priceIn;
                            decimal.TryParse(priceStr.Replace('.', ','), out priceIn);

                            int priceInInt = Convert.ToInt32((Math.Ceiling(priceIn / 10) * 10));

                            lblPriceIn.Text = priceInInt.ToString();

                            int count = Convert.ToInt32((e.Row.FindControl("lblCount") as Label).Text);

                            (e.Row.FindControl("lblPriceInSum") as Label).Text = (priceInInt * count).ToString();

                            int idClaimUnit = Convert.ToInt32((e.Row.FindControl("lblIdClaimUnit") as Label).Text);

                            ClaimUnit cu = new ClaimUnit(idClaimUnit);
                            cu.PriceIn = priceInInt;
                            cu.Save();
                        }
                    }
                }

                #region PriceRequest

                //Если есть хотя бы одна позиция без цены и срока, то можно передать запрос цен в снабжение
                bool haveUnit2SendPriceRequest = false;

                var dataItem = (DataRowView)e.Row.DataItem;

                if (dataItem != null)
                {
                    string priceIn = dataItem["price_in"].ToString();
                    string deliveryTime = dataItem["delivery_time"].ToString();

                    if (String.IsNullOrEmpty(priceIn) && String.IsNullOrEmpty(deliveryTime))
                    {
                        haveUnit2SendPriceRequest = true;
                    }
                }

                if (!btnRequestPrice.Visible && e.Row.RowIndex > 0)//В нулевой строке содежится пустые данные всегда
                {
                    btnRequestPrice.Visible = btnRequestPriceDisplay && haveUnit2SendPriceRequest;
                }

                #endregion
            }
        }

        //protected void tblClaimUnitList_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int id = Convert.ToInt32(e.OldValues["id_claim_unit"]);
        //    ClaimUnit claimUnit = new ClaimUnit(id);
        //    claimUnit.PriceIn = Convert.ToDecimal(e.OldValues["price_in"]);
        //    claimUnit.PriceOut = Convert.ToDecimal(e.OldValues["price_out"]);
        //}

        //protected void btnSetRequestNum_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
        //        Claim claim = new Claim(Id);
        //        claim.IdCreator = User.Id;
        //        claim.SetRequestNumState();
        //        //RedirectWithParams();
        //        Response.Redirect(ListUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
        //    }
        //}

        protected void btnRequestPrice_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.SetRequestPriceState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSetStatePriceSet_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.SetPriceSetState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSetStateDone_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.SetDoneState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnMacthPrice_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.SetPriceOkState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }


        protected void btnFailPrice_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.SetPriceFailState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Claim claim = new Claim();
                claim.Delete(Id, User.Id);
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnZipConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Не даем Передать в работу если не заполнен список
                if (tblClaimUnitList.Rows.Count > 1) //Там есть срока для вставки нового ЗИПа, ее не считаем
                {

                    //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    Claim claim = new Claim(Id);
                    claim.IdCreator = User.Id;
                    claim.SetSendState();
                    //RedirectWithParams();
                    Response.Redirect(ListUrl);

                }
                else
                {
                    ServerMessageDisplay(new[] { phServerMessage },
                        "Для передачи заявки в работу необходимо заполнить список ЗИПов", true);
                }
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }
        

        protected void btnSetStateSend_Click(object sender, EventArgs e)
        {
            try
            {
                //Не даем Передать в работу если не заполнен список
                if (tblClaimUnitList.Rows.Count > 1) //Там есть срока для вставки нового ЗИПа, ее не считаем
                {

                    //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    Claim claim = new Claim(Id);
                    claim.IdCreator = User.Id;
                    if (hfZipStateSysName.Value.ToUpper().Equals("LESSZIP"))
                    {
                        claim.SetZipConfirmState();
                    }
                    else
                    {
                        claim.SetSendState();
                    }
                    //RedirectWithParams();
                    Response.Redirect(ListUrl);

                }
                else
                {
                    ServerMessageDisplay(new[] { phServerMessage },
                        "Для передачи заявки в работу необходимо заполнить список ЗИПов", true);
                }
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnCancelState_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Claim claim = new Claim(Id);
                claim.IdCreator = User.Id;
                claim.CancelComment = MainHelper.TxtGetText(ref txtCancelComment);
                claim.SetCancelState();
                //RedirectWithParams();
                Response.Redirect(ListUrl);
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void txtPriceIn_OnTextChanged(object sender, EventArgs e)
        {
            //tblClaimUnitList.Rows[0].RowState = DataControlRowState.Normal;

            //sdsList.Update();
            //tblClaimUnitList.DataBind();
        }

        protected void btnCopyInfo_Click(object sender, EventArgs e)
        {
            string model = txtDeviceModel.Text.IndexOf("(") > 0
                ? txtDeviceModel.Text.Remove(txtDeviceModel.Text.IndexOf("("))
                : txtDeviceModel.Text;
            string engeneer = ddlEngeneer.SelectedItem.Text.Replace("--выберите значение--", "");
            string contractor = ddlContractor.SelectedItem.Text.Replace("--выберите значение--", "");

            lInfo.Text = String.Format("{0} {1} {2} {3} {7} {4} {6} {8} {5} {9} {10}", hfContractNumAmdDate.Value,
                txtServiceDeskNum.Text, txtSerialNum.Text, model,
                txtAddress.Text, engeneer, lblContractNumber.Text, txtCity.Text, contractor, lblContractNumber.Text,
                lblContractType.Text);
            btnCopyInfo.Focus();
            //<Выделение текста>
            string script = String.Format(@"$(function() {{
var e=document.getElementById('{0}');
if(window.getSelection)
{{
    var s=window.getSelection();
    if(s.setBaseAndExtent){{
        s.setBaseAndExtent(e,0,e,e.innerText.length-1)
    }}
    else
    {{
        var r=document.createRange();
        r.selectNodeContents(e);
        s.removeAllRanges();
        s.addRange(r);
    }}
}}
else 
    if(document.getSelection)
    {{
        var s=document.getSelection();
        var r=document.createRange();
        r.selectNodeContents(e);
        s.removeAllRanges();
        s.addRange(r)
    }}
    else
        if(document.selection)
        {{
            var r=document.body.createTextRange();
            r.moveToElementText(e);
            r.select();
        }}
}});", pnlCopyInfo.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "pickCopyInfo", script, true);
            //</Выделение текста>
        }

        protected void txtCatalogNum_OnTextChanged(object sender, EventArgs e)
        {
            //int idDevice = 0;
            string serialNum = MainHelper.TxtGetText(ref txtSerialNum);
            string catalogNum = (tblClaimUnitList.FooterRow.FindControl("txtCatalogNum") as TextBox).Text;
            catalogNum = String.IsNullOrEmpty(catalogNum) ? null : catalogNum;

            DataTable dt = Db.Db.Zipcl.GetLastClaimDaysCount(serialNum, catalogNum);
            string lastClaimText = String.Empty;

            if (dt.Rows.Count > 0)
            {
                string daysCount = dt.Rows[0]["days_count"].ToString();
                string claimDate = dt.Rows[0]["claim_date"].ToString();
                string claimState = dt.Rows[0]["claim_state"].ToString();
                lastClaimText = String.Format("Дата последнего заказа {0} ({1}д.) - {2}",
                    Convert.ToDateTime(claimDate).ToShortDateString(), daysCount, claimState);
            }
            else
            {
                if (!String.IsNullOrEmpty(catalogNum))
                {
                    lastClaimText = "Нет информации о дате последнего заказа";
                }
            }

            lLastClaim.Text = lastClaimText;
            //(tblClaimUnitList.FooterRow.FindControl("lLastClaim") as Literal).Text = lastClaimText;
            (tblClaimUnitList.FooterRow.FindControl("txtName") as TextBox).Focus();
        }

        protected void btnAddressChangeNote_Click(object sender, EventArgs e)
        {
            pnlAddressChangeNote.Visible = true;
            txtAddressChangeNote.Focus();
        }

        protected void btnSendNote_Click(object sender, EventArgs e)
        {
            pnlAddressChangeNote.Visible = false;
            string changeAddressNote = MainHelper.TxtGetText(ref txtAddressChangeNote);
            string serailNum = MainHelper.TxtGetText(ref txtSerialNum);
            string currentAddress = MainHelper.TxtGetText(ref txtAddress);
            string currentCity = MainHelper.TxtGetText(ref txtCity);
            string currentObject = MainHelper.TxtGetText(ref txtObjectName);
            string mailText =
                String.Format(
                    "Добрый день.\r\n{3} сообщает, что необходимо изменить местонахождение аппарата с серийным номером {0}\r\nТекущее местоположение:\r\n{1} {2} {5}\r\nИнформация о новом местоположении:\r\n{4}",
                    serailNum, currentCity, currentAddress, User.FullName, changeAddressNote, currentObject);
            string mailTo = ConfigurationManager.AppSettings["changeAddressNoteMailTo"];

            MessageHelper.Email.SendMail(mailTo, "Уведомление об изменении адреса", mailText);
        }



        protected void rtrCommentHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lblCommentHistoryHeader.Visible = true;
            }
        }

        protected void rtrHistory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string currentDateChange = ((DataRowView)e.Item.DataItem)["change_date"].ToString();

                if (!String.IsNullOrEmpty(hfLastHistDate.Value))
                {
                    DateTime lastDateChange = Convert.ToDateTime(hfLastHistDate.Value);
                    DateTime currentDateChangeDT = Convert.ToDateTime(currentDateChange);
                    TimeSpan dateDiff = (currentDateChangeDT - lastDateChange);
                    int days = dateDiff.Days;
                    int hours = dateDiff.Hours + (days * 24);

                    (e.Item.FindControl("lDateDiff") as Literal).Text = String.Format("{0}ч.({1}дн.)", hours, days);
                }

                hfLastHistDate.Value = currentDateChange;
            }
        }

        private void DisplayOftenSelectedParts()
        {
            bool displayNoOpportunity = String.IsNullOrEmpty(hfIdDevice.Value);

            pnlOftenSelectedList.Visible = !displayNoOpportunity;
            lblNoDataOpportunity.Visible = displayNoOpportunity;
        }

        protected void DisplayOftenSelectedNoData()
        {
            bool displayNoData = rtrOftenSelected.Items.Count == 0;

            //lblOftenSelectedNoData.Visible = displayNoData;
            // btnAddNewOftenSelected.Visible = !displayNoData;
        }

        protected void tblClaimUnitList_DataBound(object sender, EventArgs e)
        {
            SetCounts();
            //DisplayStateButtons();
        }

        private void DisplayStateButtons()
        {
            #region PriceRequest

            //Если есть хотя бы одна позиция без цены и срока, то можно передать запрос цен в снабжение
            bool haveUnit2SendPriceRequest = false;

            foreach (GridViewRow row in tblClaimUnitList.Rows)
            {
                //Label lblPriceIn = (Label)row.FindControl("lblPriceIn");
                //Label lblDeliveryTime = (Label)row.FindControl("lblDeliveryTime");

                var dataItem = (DataRowView)row.DataItem;

                if (dataItem != null)
                {
                    string priceIn = dataItem["price_in"].ToString();
                    string deliveryTime = dataItem["delivery_time"].ToString();

                    if (String.IsNullOrEmpty(priceIn) && String.IsNullOrEmpty(deliveryTime))
                    {
                        haveUnit2SendPriceRequest = true;
                        break;
                    }
                }
            }

            btnRequestPrice.Visible = btnRequestPriceDisplay && haveUnit2SendPriceRequest;

            #endregion
        }

        private void SetCounts()
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);

            string sumCountIn = String.Format("{0:N2}", dv.Table.Compute("Sum(price_in_sum)", ""));
            if (string.IsNullOrEmpty(sumCountIn)) sumCountIn = 0.ToString();
            lSummCountIn.Text = sumCountIn;

            string sumCountOut = String.Format("{0:N2}", dv.Table.Compute("Sum(price_out_sum)", ""));
            if (string.IsNullOrEmpty(sumCountOut)) sumCountOut = 0.ToString();
            lSummCountOut.Text = sumCountOut;
        }

        protected void chklNoNomNum_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32((sender as CheckBoxList).Attributes["RowIndex"]);

            bool enabled = (tblClaimUnitList.Rows[rowIndex].FindControl("chklNoNomNum") as CheckBoxList).SelectedValue != "1";

            //RequiredFieldValidator rfvTxtNomenclatureNum = (tblClaimUnitList.Rows[rowIndex].FindControl("rfvTxtNomenclatureNum") as RequiredFieldValidator);
            TextBox txtNomenclatureNum = (tblClaimUnitList.Rows[rowIndex].FindControl("txtNomenclatureNum") as TextBox);
            txtNomenclatureNum.Text = String.Empty;
            //txtNomenclatureNum.Enabled = 
            txtNomenclatureNum.Attributes.Add("placeholder", "№ Заявки");
            //rfvTxtNomenclatureNum.Enabled = !enabled;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32((sender as LinkButton).Attributes["RowIndex"]);

            try
            {
                ClaimUnit cu = GetClaimUnitData(rowIndex);
                cu.Save();
                //var rs = tblClaimUnitList.Rows[rowIndex].RowState;
                //   rs = DataControlRowState.Selected;
                //tblClaimUnitList.DataBind();
                //tblClaimUnitList.Rows[rowIndex].RowState = DataControlRowState.Insert;
                //tblClaimUnitList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }

        }

        private ClaimUnit GetClaimUnitData(int rowIndex)
        {
            ClaimUnit cu = new ClaimUnit();

            GridViewRow row = tblClaimUnitList.Rows[rowIndex];

            Label lblIdClaimUnit = (row.FindControl("lblIdClaimUnit") as Label);
            cu.Id = MainHelper.LblGetValueInt32(ref lblIdClaimUnit);

            cu.IdClaim = Id;

            TextBox txtCatalogNum = (row.FindControl("txtCatalogNum") as TextBox);
            cu.CatalogNum = MainHelper.TxtGetText(ref txtCatalogNum);

            TextBox txtName = (row.FindControl("txtName") as TextBox);
            cu.Name = MainHelper.TxtGetText(ref txtName);

            TextBox txtNomenclatureNum = (row.FindControl("txtNomenclatureNum") as TextBox);
            cu.NomenclatureNum = MainHelper.TxtGetText(ref txtNomenclatureNum);

            CheckBoxList chklNoNomNum = row.FindControl("chklNoNomNum") as CheckBoxList;
            cu.NoNomenclatureNum = chklNoNomNum.SelectedValue == "1";

            TextBox txtDeliveryTime = (row.FindControl("txtDeliveryTime") as TextBox);
            //cu.DeliveryTime = MainHelper.TxtGetText(ref txtDeliveryTime);
            cu.DeliveryTime = txtDeliveryTime.Text;//Чтобы можно было очистить

            TextBox txtPriceIn = (row.FindControl("txtPriceIn") as TextBox);
            //cu.PriceIn = MainHelper.TxtGetTextDecimal(ref txtPriceIn);
            decimal? priceIn = MainHelper.TxtGetTextDecimal(ref txtPriceIn, true);
            cu.PriceIn = priceIn ?? -9999;//Чтобы можно было очистить

            TextBox txtPriceOut = (row.FindControl("txtPriceOut") as TextBox);
            //cu.PriceOut = MainHelper.TxtGetTextDecimal(ref txtPriceOut, true);
            decimal? priceOut = MainHelper.TxtGetTextDecimal(ref txtPriceOut, true);
            cu.PriceOut = priceOut ?? -9999;//Чтобы можно было очистить

            cu.IdCreator = User.Id;



            return cu;
        }
    }
}