using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Settings
{
    public partial class ZipGroups : BasePage
    {
        //protected const string qsKeyZipGroup = "zgr";

        protected int IdZipGroup
        {
            get
            {
                return String.IsNullOrEmpty(hfIdZipGroup.Value) ? -1 : Convert.ToInt32(hfIdZipGroup.Value);
            }

            set
            {
                hfIdZipGroup.Value = value.ToString();
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            string lgn = User.Login;

            RegisterStartupScripts();
        }

        private void RegisterStartupScripts()
        {
            //            //--Текущая группа
            //            string script = @"$(document).ready(function () {
            //                var url = window.location;
            //                $('ul.nav').find('.active').removeClass('active');
            //                $('ul.nav li a').each(function () {
            //                    if (this.href == url) {
            //                        $(this).parent().addClass('active');
            //                    }
            //                }); 
            //            });";

            //            ScriptManager.RegisterStartupScript(this, GetType(), "navCurrentTab", script, true);

            //            //====/>
        }

        //protected new void Page_PreLoad(object sender, EventArgs e)
        //{
        //    base.Page_PreLoad(sender, e);

        //    if (!IsPostBack)
        //    {
        //        FillLists();
        //    }
        //}

        private void FillLists()
        {
            //MainHelper.LbFill(ref lbZipGroupList, Db.Db.Srvpl.GetZipGroupSelectionList());
        }

        protected void btnZipGroupAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GroupAdd();
                //RedirectWithParams();
                tblZipGroupList.DataBind();
                rtrZipOftSelList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void GroupAdd()
        {
            string name = MainHelper.TxtGetText(ref txtZipGroupName);
            string colour = MainHelper.TxtGetText(ref txtZipGroupColour);
            int orderNUm = MainHelper.TxtGetTextInt32(ref txtZipGroupOrderNum);

            ZipGroup zipGroup = new ZipGroup() { Name = name, IdCreator = User.Id, Colour = colour, OrderNum = orderNUm };
            zipGroup.Save();
        }

        protected void btnDeleteGroup_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new ZipGroup().Delete(id, User.Id);
                //RedirectWithParams();
                tblZipGroupList.DataBind();
                rtrZipOftSelList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void tblZipGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idZipGroup = ((HiddenField)e.Row.FindControl("hfIdZipGroup")).Value;
                string css = "zip-grp-nomark";

                if (IdZipGroup.ToString() == idZipGroup) css = "zip-grp-current";

                e.Row.CssClass += css;
            }
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (IdZipGroup <= 0) return;

            try
            {
                int idZipGroup = IdZipGroup;
                string catalogNum = (sender as LinkButton).CommandArgument;

                ZipGroup2CatNum z2c = new ZipGroup2CatNum() { IdZipGroup = idZipGroup, CatalogNum = catalogNum, IdCreator = User.Id };
                z2c.Save();
                //RedirectWithParams();
                rtrZipOftSelList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                new ZipGroup2CatNum().Delete(id, User.Id);
                //RedirectWithParams();
                rtrZipOftSelList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void btnSelectGroup_OnClick(object sender, EventArgs e)
        {
            IdZipGroup = Convert.ToInt32((sender as LinkButton).CommandArgument);
            tblZipGroupList.DataBind();
        }

        protected void txtZipGroupColour_OnTextChanged(object sender, EventArgs e)
        {
            SaveGroups();
        }

        protected void txtZipGroupOrderNum_OnTextChanged(object sender, EventArgs e)
        {
            SaveGroups();
        }

        private void SaveGroups()
        {
            try
            {
                foreach (GridViewRow row in tblZipGroupList.Rows)
                {
                    HiddenField hfIdZipGroup = (HiddenField)row.FindControl("hfIdZipGroup");
                    int idZipGroup = MainHelper.HfGetValueInt32(ref hfIdZipGroup);

                    TextBox txtZipGroupColour = ((TextBox)row.FindControl("txtZipGroupColour"));
                    string color = MainHelper.TxtGetText(ref txtZipGroupColour);

                    TextBox txtZipGroupOrderNum = ((TextBox)row.FindControl("txtZipGroupOrderNum"));
                    int orderNUm = MainHelper.TxtGetTextInt32(ref txtZipGroupOrderNum);

                    ZipGroup zg = new ZipGroup() { Id = idZipGroup, Colour = color, OrderNum = orderNUm };
                    zg.Save();
                }
                tblZipGroupList.DataBind();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        protected void txtClaimUnitComment_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            string catNum = txt.Attributes["cat_num"];
            string descr = MainHelper.TxtGetText(ref txt);

            ClaimUnit cu = new ClaimUnit() { CatalogNum = catNum, Descr = descr, IdCreator = User.Id };
            try
            {
                cu.SaveInfo();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }
    }
}