using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Settings
{
    public partial class ManagerOperator : BasePage
    {
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string serviceOperatorRightGroup = ConfigurationManager.AppSettings["serviceOperatorRightGroup"];
        string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];

        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";

        private bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                UserIsSysAdmin = Db.Db.Users.CheckUserRights(User.Login, sysAdminRightGroup);

                if (!UserIsSysAdmin)
                {
                    Response.Redirect(FriendlyUrl.Href("~/Error"));
                }

                FillList();
            }
            
        }

        private void FillList()
        {
            var userList = Db.Db.Users.GetUsersSelectionList();
            var Man2OperList = Db.Db.Zipcl.GetManager2OperatorList();

            var dtManagers = Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList);
            tblManager2OperatorList.DataSource = dtManagers;
            tblManager2OperatorList.DataBind();

            var operatorsList = Db.Db.Users.GetUsersSelectionList(serviceOperatorRightGroup, userList);

            foreach (RepeaterItem item in tblManager2OperatorList.Items)
            {
                var ddlOperator = item.FindControl("ddlOperator") as DropDownList;

                if (ddlOperator != null)
                {
                    MainHelper.DdlFill(ref ddlOperator,operatorsList,true);

                    var hfIdManager = item.FindControl("hfIdManager") as HiddenField;

                    if (hfIdManager != null)
                    {
                        var idManager = Convert.ToInt32(hfIdManager.Value);
                        var rows = Man2OperList.Select(String.Format("id_manager = {0}", idManager));

                        if (rows.Count() > 0)
                        {
                            var idOperator = rows[0]["id_operator"].ToString();

                            MainHelper.DdlSetSelectedValue(ref ddlOperator, idOperator);
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in tblManager2OperatorList.Items)
            {
                var hfIdManager = item.FindControl("hfIdManager") as HiddenField;

                if (hfIdManager != null)
                {
                    int idManager = Convert.ToInt32(hfIdManager.Value);
                    var ddlOperator = item.FindControl("ddlOperator") as DropDownList;

                    if (ddlOperator != null)
                    {
                        int? idOperator = MainHelper.DdlGetSelectedValueInt(ref ddlOperator, true);

                        if (idOperator != null)
                        {
                            var man2Oper = new Manager2Operator()
                            {
                                IdManager = idManager,
                                IdOperator = idOperator,
                                IdCreator = User.Id
                            };
                            man2Oper.Save();
                        }
                        else
                        {
                            new Manager2Operator().Delete(idManager, null, User.Id);
                        }
                    }
                }
            }
            
            RedirectWithParams();
        }
    }
}