using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Install.Models;

namespace Install
{
    public partial class Site : MasterPage
    {
        const string vskUser = "vskUser";

        public User User
        {
            get { return (User)ViewState[vskUser] ?? new User(); }
            set { ViewState[vskUser] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                LoadFormSettings();
                SetUserName(); 
            }

            RegisterStartupScripts();
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
        }
    }
}