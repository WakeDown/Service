using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Models;

namespace ZipClaim.WebForms.Masters
{
    public partial class Site : MasterPage
    {
        const string vskUser = "vskUser";
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];
        string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        string zipClientRightGroup = ConfigurationManager.AppSettings["zipClientRightGroup"];
        private string zipClaimAccessRightGroup = ConfigurationManager.AppSettings["zipClaimAccessRightGroup"];
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];
        string serviceOperatorRightGroup = ConfigurationManager.AppSettings["serviceOperatorRightGroup"];
        string zipClaimClientZipViewRightGroup = ConfigurationManager.AppSettings["zipClaimClientZipViewRightGroup"];
        string zipClaimClientCounterViewRightGroup = ConfigurationManager.AppSettings["zipClaimClientCounterViewRightGroup"];

        private const string serviceManagerRightGroupVSKey = "serviceManagerRightGroupVSKey";
        private const string serviceOperatorRightGroupVSKey = "serviceOperatorRightGroupVSKey";
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";
        private const string serviceEngeneersRightGroupVSKey = "serviceEngeneersRightGroupVSKey";
        private const string serviceAdminRightGroupVSKey = "serviceAdminRightGroupVSKey";
        private const string zipClientRightGroupVSKey = "zipClientRightGroupVSKey";
        private const string clientCounterViewRightGroupVSKey = "clientCounterViewRightGroupVSKey";

        private const string supplyManRightGroupVSKey = "supplyManRightGroupVSKey";
        string supplyManRightGroup = ConfigurationManager.AppSettings["supplyManRightGroup"];

        protected bool UserIsSupplyMan
        {
            get { return (bool)ViewState[supplyManRightGroupVSKey]; }
            set { ViewState[supplyManRightGroupVSKey] = value; }
        }

        protected bool UserIsServiceAdmin
        {
            get { return (bool)ViewState[serviceAdminRightGroupVSKey]; }
            set { ViewState[serviceAdminRightGroupVSKey] = value; }
        }

        protected bool UserIsEngeneer
        {
            get { return (bool)ViewState[serviceEngeneersRightGroupVSKey]; }
            set { ViewState[serviceEngeneersRightGroupVSKey] = value; }
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

        protected bool UserIsClient
        {
            get { return (bool)ViewState[zipClientRightGroupVSKey]; }
            set { ViewState[zipClientRightGroupVSKey] = value; }
        }

        protected bool ClientCounterView
        {
            get { return (bool)ViewState[clientCounterViewRightGroupVSKey]; }
            set { ViewState[clientCounterViewRightGroupVSKey] = value; }
        }
       

        public User User { get { return (User)ViewState[vskUser] ?? new User(); } 
            set { ViewState[vskUser] = value; } }

        

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                //Записываем счетчик посещений
                if (Session[Global.logonSesKey] != null && Session[Global.logonSesKey] != "saved")
                {
                    Db.Db.Zipcl.SaveCounter(User.Id, null, Request.UserHostAddress, User.Login);
                    Session[Global.logonSesKey] = "saved";
                }

                string currLogin = User.Login;

                //Проверка доступа в программу
                bool userCanAccess = Db.Db.Users.CheckUserRights(currLogin, zipClaimAccessRightGroup);
                if (!userCanAccess) Response.Redirect(FriendlyUrl.Href("~/Error"));

                UserIsServiceAdmin = Db.Db.Users.CheckUserRights(User.Login, serviceAdminRightGroup); 
                UserIsManager = Db.Db.Users.CheckUserRights(User.Login, serviceManagerRightGroup);
                UserIsOperator = Db.Db.Users.CheckUserRights(User.Login, serviceOperatorRightGroup);
                UserIsEngeneer = Db.Db.Users.CheckUserRights(User.Login, serviceEngeneersRightGroup);
                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);
                UserIsSupplyMan = Db.Db.Users.CheckUserRights(User.Login, supplyManRightGroup);

                //Зашел клиент
                UserIsClient = Db.Db.Users.CheckUserRights(currLogin, zipClientRightGroup);

                bool clientZipView = Db.Db.Users.CheckUserRights(currLogin, zipClaimClientZipViewRightGroup);
                ClientCounterView = Db.Db.Users.CheckUserRights(currLogin, zipClaimClientCounterViewRightGroup);

                liClientZip.Visible = clientZipView;
                liClientCounter.Visible = ClientCounterView;

                //if (!clientZipView && !clientCounterView) clientZipView = true;

                if (UserIsClient)
                {
                    liClaims.Visible = liZipHistory.Visible = false;
                }

                if (!UserIsClient && !UserIsEngeneer && !UserIsManager && !UserIsOperator && !UserIsSysAdmin && !UserIsServiceAdmin && !UserIsSupplyMan)
                {
                    Response.Redirect(FriendlyUrl.Href("~/ErrorGrp"));
                }

                if (UserIsClient && !clientZipView && !ClientCounterView)
                {
                    Response.Redirect(FriendlyUrl.Href("~/Error"));
                }

                //Автопереход для клиентов на страницу клиента
                if (UserIsClient && clientZipView && ClientCounterView && !Request.Path.Equals(FriendlyUrl.Href("~/Client")) && !Request.Path.Equals(FriendlyUrl.Href("~/Client/Detail")) && !Request.Path.Equals(FriendlyUrl.Href("~/ClientCounter")) && !Request.Path.Equals(FriendlyUrl.Href("~/Client/CounterDetail")))
                {//Если есть доступ и к странице Счетчик и к странице ЗИП
                    Response.Redirect(FriendlyUrl.Href("~/Client"));
                    
                }

                if (UserIsClient && clientZipView && !ClientCounterView && !Request.Path.Equals(FriendlyUrl.Href("~/Client")) && !Request.Path.Equals(FriendlyUrl.Href("~/Client/Detail")))
                {//Если есть доступ только к странице ЗИП
                    Response.Redirect(FriendlyUrl.Href("~/Client"));
                }

                if (UserIsClient && ClientCounterView && !clientZipView && !Request.Path.Equals(FriendlyUrl.Href("~/ClientCounter")) && !Request.Path.Equals(FriendlyUrl.Href("~/Client/CounterDetail")))
                {//Если есть доступ только к странице Счетчик
                    Response.Redirect(FriendlyUrl.Href("~/ClientCounter"));
                    
                }

                //Автопереход для снабжения на страницу Снабжение
                if ((UserIsEngeneer /*|| userIsSysAdmin*/) && Request.Path.Equals("/"))
                {
                    Response.Redirect(FriendlyUrl.Href("~/Claims/ZipHistory"));
                }

                LoadFormSettings();
                SetUserName();

                //bool userIsEngeneer = Db.Db.Users.CheckUserRights(currLogin, serviceEngeneersRightGroup);
                //bool userIsSysAdmin = Db.Db.Users.CheckUserRights(currLogin, sysAdminRightGroup);

                

                //Автопереход для инженеров на страницу История
                if ((UserIsEngeneer /*|| userIsSysAdmin*/) && Request.Path.Equals("/"))
                {
                    Response.Redirect(FriendlyUrl.Href("~/Claims/ZipHistory"));
                }

                ShowFormParts();
            }
            

            RegisterStartupScripts();
        }

        private void ShowFormParts()
        {
            liClaims.Visible = UserIsEngeneer || UserIsManager || UserIsOperator || UserIsSysAdmin || UserIsServiceAdmin;
            liZipHistory.Visible = UserIsEngeneer || UserIsSysAdmin;
            liSupply.Visible = UserIsSysAdmin || UserIsSupplyMan;
            liSettings.Visible = UserIsSysAdmin;
            pnlClientFeed.Visible = UserIsClient;
            pnlSd.Visible = !UserIsClient;
            liDownloadSettings.Visible = UserIsClient && ClientCounterView;
            liReports.Visible = UserIsSysAdmin || UserIsManager;
        }

        private void SetUserName()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string curName = User.DisplayName;//Page.User.Identity.Name;
                LoginName.FormatString = curName;
            }
        }

        private void LoadFormSettings()
        {
            aServiceDesk.HRef = WebConfigurationManager.AppSettings["serviceDeskAddress"];
            //aReports.HRef = WebConfigurationManager.AppSettings["reportsUrl"];

            int contractorId;
            int.TryParse(User.Login, out contractorId);

            string contractorName = contractorId <= 0 ? String.Empty : Db.Db.Unit.GetContractorSelectionList(null, contractorId).Rows[0]["full_name"].ToString();

            contractorName = contractorName.Replace("\"", "");

            aClientFeedback.HRef = String.Format("mailto:{0}?subject={1}&body=Cлужебная информация - {2} - {3}", WebConfigurationManager.AppSettings["clientFeedbackAddress"], WebConfigurationManager.AppSettings["clientFeedbackSubject"], User.Login, contractorName);
        }

        private void RegisterStartupScripts()
        {
            ScriptManager.ScriptResourceMapping.AddDefinition(
        "jquery",
        new ScriptResourceDefinition
        {
            Path = "~/jquery-2.1.0.min.js",
            DebugPath = "~/jquery-2.1.0.js",
            LoadSuccessExpression = "jQuery"
        });

            //--Текущая вкладка меню
            string script = @"$(document).ready(function () {
                var url = window.location;
                $('ul.nav').find('.active').removeClass('active');
                $('ul.nav li a').each(function () {
                    if (this.href == url) {
                        $(this).parent().addClass('active');
                    }
                }); 
            });";

            ScriptManager.RegisterStartupScript(this, GetType(), "navCurrentTab", script, true);

            //====/>

            //--Включаем datepicker.js если атрибут date не поддерживается в текущем браузере
            script = @"$('.datepicker-btn').datepicker({ language: 'ru', todayBtn: 'linked', format: 'dd.mm.yyyy', autoclose: true });";

            ScriptManager.RegisterStartupScript(this, GetType(), "datepickerActivate", script, true);

            script = @"$('.datepicker-btn-month').datepicker({ language: 'ru', todayBtn: 'linked', minViewMode: 1, format: 'mm.yyyy', autoclose: true });";

            ScriptManager.RegisterStartupScript(this, GetType(), "datepickerMonthsActivate", script, true);

            //====/>

            //--Активируем подсказки
            script = @"$('[data-toggle=tooltip]').tooltip();";

            ScriptManager.RegisterStartupScript(this, GetType(), "tooltipActivate", script, true);
            //====/>
            //--Убираем автоподсказки для текстовых полей подсказки
            script = @"$(document).ready(function(){$(document).on('focus', ':input', function(){ var attr = $(this).attr('noautocopml'); if (typeof attr == 'undefined'){ $( this ).attr( 'autocomplete', 'off' );} else {$( this ).attr( 'autocomplete', 'on' );}});});";

            ScriptManager.RegisterStartupScript(this, GetType(), "tooltipAutocompleteOff", script, true);
            //====/>

            //--Активируем логгер
            //            script = @"var log = log4javascript.getDefaultLogger(); $('a').on('click', function() { var actn = new Object();
            //actn.id = $(this).prop('id');	
            //actn.href = $(this).prop('href');
            //	
            //	log.info(actn); });";

            //            ScriptManager.RegisterStartupScript(this, GetType(), "logger", script, true);
            //====/>

            //--Снежинки
            var now = DateTime.Now;
            if ((now.Month == 12 && now.Day > 15) || (now.Month == 1 && now.Day < 20)) ScriptManager.RegisterClientScriptInclude(this, GetType(), "happyNewYear", "http://www.fortress-design.com/js/snow-fall.js");
            //====/>
            //<script src="http://www.fortress-design.com/js/snow-fall.js" type="text/javascript"></script>
        }

        protected void btnDownloadSettings_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{1}?ctrid={0}", User.Login, ResolveClientUrl("~/WebForms/Client/SnmpClientSettings.ashx")));
        }

        protected void btnDownloadProgram_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}?p=scaner", ResolveClientUrl("~/WebForms/Client/ProgramDownloader.ashx")));
        }
        
    }
}