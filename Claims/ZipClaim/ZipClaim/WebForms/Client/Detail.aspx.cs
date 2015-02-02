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

namespace ZipClaim.WebForms.Client
{
    public partial class Detail : BasePage
    {
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
                if (Id > 0)
                {
                    Claim claim = new Claim(Id);
                    FillFormData(claim);
                }

                int contractorId;
                int.TryParse(User.Login, out contractorId);//Вставляем только число id_contractor 

                sdsList.SelectParameters["id_contractor"].DefaultValue = contractorId.ToString();
            }
        }

        private void FillFormData(Claim claim)
        {
            bool isEdit = Id > 0;

            lFormTitle.Text = !isEdit ? "" : String.Format("Просмотр заявки на ЗИП №{0} от {1:d}", claim.Id, claim.DateCreate);
            hfContractNumAmdDate.Value = !isEdit ? "" : String.Format("№{0} от {1:d}", claim.Id, claim.DateCreate);


            MainHelper.HfSetValue(ref hfIdDevice, claim.IdDevice);
            MainHelper.LblSetText(ref txtSerialNum, claim.SerialNum.ToUpper());
            MainHelper.LblSetText(ref txtDeviceModel, claim.DeviceModel);
            MainHelper.LblSetText(ref txtCity, claim.City);
            MainHelper.LblSetText(ref txtAddress, claim.Address);
            MainHelper.LblSetText(ref txtCounter, claim.Counter);
            MainHelper.LblSetText(ref ddlEngeneerConclusion, claim.EngeneerConclusion);
            MainHelper.LblSetText(ref ddlEngeneer, claim.ServiceEngeneer);
            MainHelper.LblSetText(ref ddlManager, claim.Manager);
            MainHelper.LblSetText(ref txtServiceDeskNum, claim.ServiceDeskNum);
            MainHelper.LblSetText(ref txtCounterColour, claim.CounterColour);
            lblCancelComment.Text = claim.CancelComment;
            MainHelper.LblSetText(ref txtObjectName, claim.ObjectName);
            lblContractNumber.Text = claim.ContractNum;
            MainHelper.HfSetValue(ref hfManagerMail, claim.ManagerMail);
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

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            //hfShowAll.Value = "1";
            sdsClimUnitsHistory.DataBind();
            rtrClimUnitsHistory.DataBind();
            lblShowLast.Text = "Показаны все записи";
            //btnShowAll.Visible = false;
        }

        protected void btnSendMail2Manager_Click(object sender, EventArgs e)
        {
            pnlMail2Manager.Visible = false;
            string message = MainHelper.TxtGetText(ref txtMail2Manager);
            //string sdNum = txtServiceDeskNum.Text;
            //string serailNum = txtSerialNum.Text;
            //string model = txtDeviceModel.Text;
            //string currentAddress = MainHelper.TxtGetText(ref txtAddress);
            //string currentCity = MainHelper.TxtGetText(ref txtCity);
            //string currentObject = MainHelper.TxtGetText(ref txtObjectName);
            string mailText =
                String.Format(
                    "Добрый день.\r\n По <a href=\"http://dsu-zip.un1t.group/?id={1}\">заявке на ЗИП №{1}</a> клиент сообщает:\r\n{0}",
                     message, Id);

            string mailTo = MainHelper.HfGetValue(ref hfManagerMail);

            MessageHelper.Email.SendMail(mailTo, "Заявка на ЗИП сообщение от клиента", mailText);
        }

        protected void btnShowPnlMail2Manager_Click(object sender, EventArgs e)
        {
            pnlMail2Manager.Visible = true;
            txtMail2Manager.Focus();
        }
    }
}