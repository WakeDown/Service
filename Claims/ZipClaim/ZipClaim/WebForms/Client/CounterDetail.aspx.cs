using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZipClaim.Helpers;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Client
{
    public partial class CounterDetail : BasePage
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

        private int IdContract
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["cid"], out id);

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
                    Device device = new Device(Id);
                    FillFormData(device);
                }

                //int contractId;
                //int.TryParse(User.Login, out contractId);//Вставляем только число id_contract 

                //sdsList.SelectParameters["id_contractor"].DefaultValue = contractId.ToString();
            }
        }

        private void FillFormData(Device device)
        {
            bool isEdit = Id > 0;

            lFormTitle.Text = !isEdit ? String.Empty : String.Format("Просмотр истории объема печати аппарата {1} №{0}", device.SerialNum, device.Model);

            var dt = Db.Db.Srvpl.GetContractorDevice(Id, IdContract);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];

                lDeviceAddress.Text = !isEdit
                    ? String.Empty
                    : String.Format("{0}{3} {1}{4} {2}", row["city"], row["address"], row["object_name"],
                        !String.IsNullOrEmpty(row["address"].ToString()) ? "," : String.Empty,
                        !String.IsNullOrEmpty(row["object_name"].ToString()) ? "," : String.Empty);
            }
        }

        protected void tblDeviceCounterHistory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex > 0)
                {
                    var prevItem = tblDeviceCounterHistory.Items[e.Item.ItemIndex - 1];
                    var tdPrevCounter = prevItem.FindControl("tdCounter") as HtmlTableCell;
                    if (tdPrevCounter != null)
                    {
                        int prevCounter;
                        int.TryParse(tdPrevCounter.InnerText.Replace(" ", String.Empty).Trim(), out prevCounter);

                        var tdCounter = e.Item.FindControl("tdCounter") as HtmlTableCell;

                        if (tdCounter != null)
                        {
                            int counter;
                            int.TryParse(tdCounter.InnerText.Replace(" ", String.Empty).Trim(), out counter);

                            var tdPrevDiff = prevItem.FindControl("tdDiff") as HtmlTableCell;
                            if (tdPrevDiff != null)
                            {
                                tdPrevDiff.InnerText = (prevCounter - counter).ToString("### ### ### ### ###");
                            }
                        }
                    }
                }
            }

        }
    }
}