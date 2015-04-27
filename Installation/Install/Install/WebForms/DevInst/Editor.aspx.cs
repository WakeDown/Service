using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Install.Helpers;
using Install.Models;
using Install.Objects;
using Microsoft.AspNet.FriendlyUrls;

namespace Install.WebForms.DevInst
{
    public partial class Editor : BasePage
    {
        protected string FormTitle;
        protected string ListUrl = FriendlyUrl.Resolve("~/DevInst");

        private string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        private string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        private string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        private string serviceOperatorRightGroup = ConfigurationManager.AppSettings["serviceOperatorRightGroup"];
        private string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        private const string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        private const string serviceOperatorRightGroupVSKey = "serviceOperatorRightGroupVSKey";
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

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

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FormTitle = "Новая заявка на инсталляцию";

                UserIsManager = Db.Db.Users.CheckUserRights(User.Login, serviceManagerRightGroup);
                UserIsOperator = Db.Db.Users.CheckUserRights(User.Login, serviceOperatorRightGroup);
                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);

                FillLists();

                if (Id > 0)
                {
                    DeviceInstall deviceInstall = new DeviceInstall(Id);
                    FillContractorList(deviceInstall.IdContractor);
                    FillFormData(deviceInstall);
                }
            }

            RegisterStartupScripts();
        }

        private void FillFormData(DeviceInstall deviceInstall)
        {
            FormTitle = String.Format("Редактирование заявки №{0}", deviceInstall.Id);

            MainHelper.TxtSetText(ref txtEtNumber, deviceInstall.EtNumber);
            MainHelper.TxtSetText(ref txtContractNumber, deviceInstall.ContractNumber);
            MainHelper.DdlSetSelectedValue(ref ddlContractor, deviceInstall.IdContractor);
            MainHelper.TxtSetDate(ref txtPlanDate, deviceInstall.PlanDate);
            MainHelper.TxtSetText(ref txtDeviceModel, deviceInstall.DeviceModel);
            MainHelper.DdlSetSelectedValue(ref ddlCity, deviceInstall.IdCity);
            MainHelper.TxtSetDate(ref txtAddress, deviceInstall.Address);
            MainHelper.TxtSetText(ref txtObjectName, deviceInstall.ObjectName);
            MainHelper.TxtSetText(ref txtContactName, deviceInstall.ContactName);
            MainHelper.TxtSetDate(ref txtAddDevices, deviceInstall.AddDevices);
            MainHelper.DdlSetSelectedValue(ref ddlManager, deviceInstall.IdManager);
            MainHelper.DdlSetSelectedValue(ref ddlServiceAdmin, deviceInstall.IdServiceAdmin);
            MainHelper.TxtSetText(ref txtComment, deviceInstall.Comment);
        }

        private void FillContractorList(int? idContractor = null)
        {
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(null, idContractor), true);
        }

        private void RegisterStartupScripts()
        {
            string script = String.Empty;

            script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlCity.ClientID, txtCityFilter.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterCity", script, true);
        }

        private void FillLists()
        {
            var userList = Db.Db.Users.GetUsersSelectionList();

            MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList),
                true);
            MainHelper.DdlFill(ref ddlServiceAdmin, Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup, userList),
                true);
            MainHelper.DdlFill(ref ddlCity, Db.Db.Unit.GetCitiesSelectionList(), true);
            //MainHelper.DdlFill(ref ddlAddress, Db.Db.Srvpl.GetAddressesSelectionList(), true);
        }

        protected void txtContractorInn_OnTextChanged(object sender, EventArgs e)
        {
            string text = MainHelper.TxtGetText(ref txtContractorInn);
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Unit.GetContractorSelectionList(text));
            ddlContractor.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            DeviceInstall deviceInstall = GetFormData();
            deviceInstall.Save();
        }

        private DeviceInstall GetFormData()
        {
            DeviceInstall deviceInstall = new DeviceInstall();
            deviceInstall.Id = Id;
            deviceInstall.EtNumber = MainHelper.TxtGetText(ref txtEtNumber);
            deviceInstall.ContractNumber = MainHelper.TxtGetText(ref txtContractNumber);
            deviceInstall.IdContractor = MainHelper.DdlGetSelectedValueInt(ref ddlContractor);
            deviceInstall.PlanDate = MainHelper.TxtGetTextDateTime(ref txtPlanDate);
            deviceInstall.DeviceModel = MainHelper.TxtGetText(ref txtDeviceModel);
            deviceInstall.IdCity = MainHelper.DdlGetSelectedValueInt(ref ddlCity);
            deviceInstall.Address = MainHelper.TxtGetText(ref txtAddress);
            deviceInstall.ObjectName = MainHelper.TxtGetText(ref txtObjectName);
            deviceInstall.AddDevices = MainHelper.TxtGetText(ref txtAddDevices);
            deviceInstall.ContactName = MainHelper.TxtGetText(ref txtContactName);
            deviceInstall.IdManager = MainHelper.DdlGetSelectedValueInt(ref ddlManager);
            deviceInstall.IdServiceAdmin = MainHelper.DdlGetSelectedValueInt(ref ddlServiceAdmin);
            deviceInstall.Comment = MainHelper.TxtGetText(ref txtComment);

            return deviceInstall;
        }
    }
}