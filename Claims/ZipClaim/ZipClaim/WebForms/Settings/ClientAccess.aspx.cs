using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Settings
{
    public partial class ClientAccess : BasePage
    {
        string zipClientRightGroup = ConfigurationManager.AppSettings["zipClientRightGroup"];
        string sysAdminRightGroup = ConfigurationManager.AppSettings["sysAdminRightGroup"];
        string clientZipViewAdGroupName = ConfigurationManager.AppSettings["clientZipViewAdGroupName"];
        string clientCounterViewAdGroupName = ConfigurationManager.AppSettings["clientCounterViewAdGroupName"];
        private const string sysAdminRightGroupVSKey = "sysAdminRightGroupVSKey";
        private const string adGroupZipMembersVSKey = "adGroupZipMembersVSKey";
        private const string adGroupCounterMembersVSKey = "adGroupCounterMembersVSKey";

        private bool UserIsSysAdmin
        {
            get { return (bool)ViewState[sysAdminRightGroupVSKey]; }
            set { ViewState[sysAdminRightGroupVSKey] = value; }
        }

        private string[] AdGroupZipMembers
        {
            get { return (string[])ViewState[adGroupZipMembersVSKey]; }
            set { ViewState[adGroupZipMembersVSKey] = value; }
        }
        private string[] AdGroupCounterMembers
        {
            get { return (string[])ViewState[adGroupCounterMembersVSKey]; }
            set { ViewState[adGroupCounterMembersVSKey] = value; }
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

                AdGroupZipMembers = AdHelper.GetGroupMembers(clientZipViewAdGroupName);
                AdGroupCounterMembers = AdHelper.GetGroupMembers(clientCounterViewAdGroupName);

                FillLists();
            }
        }

        private void FillLists()
        {
            var userList = Db.Db.Users.GetUsersSelectionList();
            var contractors = Db.Db.Users.GetUsersSelectionList(zipClientRightGroup, userList);

            //Пересортируем список контрагентов по имени (изначально мы получаем только логины)
            contractors.Columns.Add("ContractorName", typeof (string));

            foreach (DataRow dr in contractors.Rows)
            {
                string login = dr["login"].ToString();
                dr["ContractorName"] = GetContractorNameByLogin(login);
            }

            DataView dv = contractors.DefaultView;
            dv.Sort = "ContractorName";

            rtrAccessList.DataSource = dv.ToTable();
            rtrAccessList.DataBind();
        }

        public string GetContractorNameByLogin(string login)
        {
            string name = String.Empty;

            //string login = DataBinder.Eval(e.Item.DataItem, "login").ToString();
            int idContractor;

            try
            {
                idContractor = Convert.ToInt32(login);
            }
            catch
            {
                idContractor = 0;
            }

            if (idContractor > 0)
            {
                var dt = Db.Db.Unit.GetContractorSelectionList(null, idContractor);

                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["full_name"].ToString();
                }
            }
            else
            {
                name = String.Format("Не удалось определить имя клиента, логин: {0}", login);
            }

            return name;
        }

        protected void rtrAccessList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    string login = DataBinder.Eval(e.Item.DataItem, "login").ToString();
            //    int idContractor;

            //    try
            //    {
            //        idContractor = Convert.ToInt32(login);
            //    }
            //    catch
            //    {
            //        idContractor = 0;
            //    }

            //    var contractorName = e.Item.FindControl("contractorName") as Label;
            //    var hfIdContractor = e.Item.FindControl("hfIdContractor") as HiddenField;
            //    hfIdContractor.Value = idContractor.ToString();

            //    if (contractorName != null)
            //    {
            //        if (idContractor > 0)
            //        {
            //            var dt = Db.Db.Unit.GetContractorSelectionList(null, idContractor);

            //            if (dt.Rows.Count > 0)
            //            {
            //                contractorName.Text = dt.Rows[0]["full_name"].ToString();
            //            }
            //        }
            //        else
            //        {
            //            contractorName.Text = String.Format("Не удалось определить имя клиента, логин: {0}", login);
            //        }
            //    }

            //}
        }

        protected void SaveClientZipGroup(bool add, string sid)
        {
            string zipGroup = ConfigurationManager.AppSettings["clientZipViewAdGroupName"];

            if (add)
            {
                AdHelper.AddUserToGroup(sid, zipGroup);
            }
            else
            {
                AdHelper.RemoveUserFromGroup(sid, zipGroup);
            }

        }

        //protected void SaveClientCounterGroup(bool add, string sid)
        //{

        //    string counterGroup = ConfigurationManager.AppSettings["clientCounterViewAdGroupName"];

        //    if (add)
        //    {
        //        AdHelper.AddUserToGroup(sid, counterGroup);
        //    }
        //    else
        //    {
        //        AdHelper.RemoveUserFromGroup(sid, counterGroup);
        //    }
        //}

        protected bool UserHaveZipGroup(string login)
        {
            //int idContractor;
            //int.TryParse(login, out idContractor);
            //string zipGroup = ConfigurationManager.AppSettings["clientZipViewAdGroupName"];

            return AdGroupZipMembers.Contains(login);
            //return AdHelper.CheckUserGroup(idContractor, zipGroup);
        }

        protected bool UserHaveCounterGroup(string login)
        {
            //int idContractor;
            //int.TryParse(login, out idContractor);
            //string counterGroup = ConfigurationManager.AppSettings["clientCounterViewAdGroupName"];

            return AdGroupCounterMembers.Contains(login);
            //return AdHelper.CheckUserGroup(idContractor, counterGroup);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            //foreach (RepeaterItem item in rtrAccessList.Items)
            //{
            //    var chkZip = item.FindControl("chkZip") as CheckBox;

            //    if (chkZip != null)
            //    {
            //        //int idContractor;
            //        //int.TryParse(chkZip.Attributes["Value"], out idContractor);
            //        //SaveClientZipGroup(chkZip.Checked, idContractor);
            //        SaveClientZipGroup(chkZip.Checked, chkZip.Attributes["Value"]);
            //    }

            //    var chkCounter = item.FindControl("chkCounter") as CheckBox;

            //    if (chkCounter != null)
            //    {
            //        //int idContractor;
            //        //int.TryParse(chkCounter.Attributes["Value"], out idContractor);
            //        //SaveClientCounterGroup(chkCounter.Checked, idContractor);
            //        SaveClientZipGroup(chkZip.Checked, chkZip.Attributes["Value"]);
            //    }
            //}
            RedirectWithParams();
        }

        protected void chkZip_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);

            if (chk != null)
            {
                bool add = chk.Checked;
                string sid = chk.Attributes["Value"];

                if (add)
                {
                    AdHelper.AddUserToGroup(sid, clientZipViewAdGroupName);
                }
                else
                {
                    AdHelper.RemoveUserFromGroup(sid, clientZipViewAdGroupName);
                }
            }
            //(sender as CheckBox).Attributes.Add("changes", "1");
        }

        protected void chkCounter_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);

            if (chk != null)
            {
                bool add = chk.Checked;
                string sid = chk.Attributes["Value"];

                if (add)
                {
                    AdHelper.AddUserToGroup(sid, clientCounterViewAdGroupName);
                }
                else
                {
                    AdHelper.RemoveUserFromGroup(sid, clientCounterViewAdGroupName);
                }
            }

            //(sender as CheckBox).Attributes.Add("changes", "1");
        }
    }
}