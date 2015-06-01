using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using ZipClaim.Helpers;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Client
{
    public partial class ListCounter : BaseFilteredPage
    {
        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];
        string serviceEngeneersRightGroup = ConfigurationManager.AppSettings["serviceEngeneersRightGroup"];

        protected string DetailUrl = FriendlyUrl.Resolve("~/Client/CounterDetail");

        private const string ctrtrIdVsKey = "ctrtrIdVsKey";
        private const string dataCountersVsKey = "dataCountersVsKey";

        protected int ContractorId
        {
            get
            {
                int id;
                int.TryParse(ViewState[ctrtrIdVsKey].ToString(), out id);

                return id;
            }
            set { ViewState[ctrtrIdVsKey] = value; }
        }



        //protected DataTable DataCounters
        //{
        //    get
        //    {
        //        return ViewState[dataCountersVsKey] as DataTable;
        //    }
        //    set { ViewState[dataCountersVsKey] = value; }
        //}

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("ctr", ddlContract));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "50"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ctrId;
                int.TryParse(User.Login, out ctrId);//Вставляем только число id_contractor 

                //ctrId = 12130825;

                ContractorId = ctrId;

                if (ContractorId <= 0) Response.Redirect(FriendlyUrl.Resolve("~/Error"));

                if (ContractorId > 0)
                {
                    lblContractorName.Text = Db.Db.Unit.GetContractorSelectionList(null, ContractorId).Rows[0]["full_name"].ToString();
                }

                FillFilterLists();

                SetDefaults();

                LoadData();

                pnlNoData.Controls.Clear();
                //Если нет активных договоров
                if (ddlContract.Items.Count == 0 || (ddlContract.Items.Count == 1 && ddlContract.Items[0].Text.Equals(MainHelper.ddlSelectAllText)))
                {
                    var h2 = new HtmlGenericControl("h2");
                    h2.InnerText = "Нет активных договоров";

                    pnlNoData.Controls.Add(h2);
                }
            }

            base.Page_Load(sender, e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            CreateList();
        }


        private void CreateList()
        {
            

            int? idContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract, true);
            int? rowsCount = MainHelper.TxtGetTextInt32(ref txtRowsCount, true);

            var dates = GetShownDates();
            DateTime lastDate = dates[dates.Count() - 1].Date;

            DateTime dateBegin = new DateTime(lastDate.Year, lastDate.Month, 1);
            DateTime dateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));


            var devices = Db.Db.Srvpl.GetContract2DevicesList(ContractorId, idContract);
            var deviceData = Db.Db.Srvpl.GetDevicesCounterList(dateBegin, dateEnd, ContractorId, idContract);

            int devRows = devices.Rows.Count;
            lDevicesCount.Text = devRows.ToString();
            lRowsCount.Text = rowsCount < devRows ? rowsCount.ToString() : devRows.ToString();

            var devs = devices.Select(String.Format("row_num <= {0}", rowsCount)).ToList();

            StringBuilder tbl = new StringBuilder();
            tbl.Append("<table class='table table-striped'>");
            //Total
            tbl.Append(@"<tr class='total-counter-row'><td colspan='5' class='text-right bold'>ВСЕГО</td>");

            foreach (ShownDates date in dates)
            {
                var volumeSum = deviceData.Compute("Sum(volume_total_counter)", String.Format("year={0} and month={1}", date.Date.Year, date.Date.Month));

                tbl.Append(
                     String.Format(@"<td class='text-right bold'>{0:### ### ### ### ###}</td>", volumeSum));
            }

            var volumeTotalSum = deviceData.Compute("Sum(volume_total_counter)", "");

            tbl.Append(String.Format(@"<td class='text-right bold'>{0:### ### ### ### ###}</td>", volumeTotalSum));
            tbl.Append("</tr>");
            //</Total

            //Header
            tbl.Append("<tr>");
            tbl.Append(@"<td class='min-width'></td><th>Модель</th><th>№ договора</th><th class='min-width text-center'>Дата</th><th class='text-right curr-counter-col'>Текущий</th>");

            foreach (ShownDates date in dates)
            {
                tbl.Append(String.Format(@"<th class='text-right'>{0:MMM yyyy}</th>", date.Date));
            }
                        
            tbl.Append(@"<th class='text-right total-counter-col'>ИТОГО</th>");
            tbl.Append("</tr>");
            //</Header

            //Tbody
            foreach (DataRow row in devs)
            {
                DateTime? lastCounterDate = Db.Db.GetValueDateTimeOrNull(row["last_counter_date"].ToString());
                int daysDiff = lastCounterDate == null ? 0 : Convert.ToInt32((new DateTime(lastCounterDate.Value.Year, lastCounterDate.Value.Month, lastCounterDate.Value.Day) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).TotalDays);

                DateTime? lastUnitCounterDate = Db.Db.GetValueDateTimeOrNull(row["unit_counter_last_date"].ToString());
                int unitCounterHoursDiff = lastUnitCounterDate == null
                    ? 0
                    : Convert.ToInt32((DateTime.Now - lastUnitCounterDate.Value).TotalHours);

                tbl.Append("<tr>");
                tbl.Append(String.Format("<td class='min-width'><div class='row-mark low {0}'>&nbsp;</div></td>", unitCounterHoursDiff <= 24 && unitCounterHoursDiff > 0 ? "bg-success" : String.Empty));
                tbl.Append("<td class='min-width'>");
                string address = String.Format("{0}{3} {1}{4} {2}", row["city"], row["address"], row["object_name"], !String.IsNullOrEmpty(row["address"].ToString()) ? "," : String.Empty, !String.IsNullOrEmpty(row["object_name"].ToString()) ? "," : String.Empty);
                tbl.Append(String.Format(@"<div data-toggle='tooltip' title='{0}' class='nowrap'>", address));

                string href =
                    GetRedirectUrlWithParams(String.Format("id={0}&cid={1}", row["id_device"], row["id_contract"]),
                        false, DetailUrl);
                tbl.Append(String.Format(@"<a runat='server' href='{0}' target='_blank' class='btn btn-link'>{1}</a>", href, row["device"]));
                tbl.Append("</div>");
                tbl.Append("</td>");

                string strDate = lastCounterDate == null
                    ? String.Empty
                    : String.Format("{0:dd/MM/yyyy}&nbsp;({1})", lastCounterDate, daysDiff);

                tbl.Append(String.Format(@"<td class='min-width nowrap'>{0:### ### ### ### ###}</td><td class='text-nowrap text-center'>{2}</td><td class='min-width nowrap text-right curr-counter-col'>{1:### ### ### ### ###}</td>", row["contract_num"], row["last_counter"], strDate));

                foreach (ShownDates date in dates)
                {
                    var dr =
                        deviceData.Select(String.Format("year={0} and month={1} and id_device={2}", date.Date.Year,
                            date.Date.Month, row["id_device"]));

                    if (dr != null && dr.Any())
                    {
                        var volume = dr[0]["volume_total_counter"];
                        tbl.Append(String.Format(@"<td class='text-right'>{0:### ### ### ### ###}</th>", volume));
                    }
                    else
                    {
                        tbl.Append(String.Format(@"<td class='text-right'></th>"));
                    }

                    
                }

                var volSum = deviceData.Compute("Sum(volume_total_counter)", String.Format("id_device={0}", row["id_device"]));

                tbl.Append(String.Format(@"<td class='text-right bold'><span class='total-counter-col'>{0}</span></td>", volSum));

                tbl.Append("</tr>");
            }
            
            
            //</Tbody

            tbl.Append("</table>");

            pnlList.InnerHtml = tbl.ToString();
        }

        private void LoadData()
        {
            //DataCounters = Db.Db.Srvpl.GetDeviceCounterByDate(DateTime.Now, ContractorId);

            
        }

        private void SetDefaults()
        {
            //sdsList.SelectParameters["id_contractor"].DefaultValue = ContractorId.ToString();
            //sdsList.SelectParameters["id_contract"].DefaultValue = ddlContract.SelectedValue;
        }

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlContract, Db.Db.Srvpl.GetContractList(ContractorId <= 0 ? -999 : ContractorId), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.ChkListFill(ref chklClaimState, Db.Db.Zipcl.GetClaimStateSelectionList());
            //MainHelper.ChkListFill(ref chklWaybillClaimState, Db.Db.Zipcl.GetWaybillClaimStateSelectionList());

            //var userList = Db.Db.Users.GetUsersSelectionList();
            //MainHelper.DdlFill(ref ddlEngeneer, Db.Db.Users.GetUsersSelectionList(serviceEngeneersRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
            //MainHelper.DdlFill(ref ddlManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup, userList), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void tblList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rtrClientCounterMonthes = e.Item.FindControl("rtrClientCounterMonthes") as Repeater;

            if (rtrClientCounterMonthes != null)
            {
                var lst = GetShownDates();

                //var phDeviceCounters = e.Item.FindControl("phDeviceCounters") as PlaceHolder;



                //rtrClientCounterMonthes.DataSource = lst;
                //rtrClientCounterMonthes.DataBind();
            }

            if (e.Item.ItemType == ListItemType.Header)
            {
                var rtrClientTotalCounterMonthes = e.Item.FindControl("rtrClientTotalCounterMonthes") as Repeater;

                if (rtrClientTotalCounterMonthes != null)
                {
                    var lst = GetShownDates();
                    rtrClientTotalCounterMonthes.DataSource = lst;
                    rtrClientTotalCounterMonthes.DataBind();
                }
            }
        }

        private ShownDates[] GetShownDates()
        {
            var lstDates = new List<ShownDates>();
            var currDate = DateTime.Now;
            var needed = 2;

            for (int i = 0; i <= needed; i++)
            {
                var d = new ShownDates(currDate.AddMonths(-i));

                d.ShowVol = true;

                lstDates.Add(d);
            }

            return lstDates.ToArray();
        }

        //protected string GetDeviceCounter(int idDevice, int idContract, DateTime date)
        //{
        //    string counter = "-";
        //    string counterColor = "-";

        //    var dt = Db.Db.Srvpl.GetDeviceCounterByDate(idDevice, idContract, date);

        //    if (dt.Rows.Count > 0)
        //    {
        //        counter = dt.Rows[0]["counter"].ToString();
        //        counterColor = dt.Rows[0]["counter_colour"].ToString();
        //    }

        //    return counter;
        //}

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);

            SetDevicesCount();
        }

        private void SetDevicesCount()
        {
            int idContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract);

            var dt = Db.Db.Srvpl.GetContractDevicesCount(ContractorId, idContract);

            if (dt.Rows.Count > 0)
            {
                lDevicesCount.Text = dt.Rows[0]["device_count"].ToString();
            }
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }

        protected void rtrClientCounterMonthes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            var lvDeviceCounter = e.Item.FindControl("lvDeviceCounter") as ListView;
            if (lvDeviceCounter != null)
            {
                int idDevice = Convert.ToInt32(DataBinder.Eval((e.Item.Parent.Parent as RepeaterItem).DataItem, "id_device"));
                int idContract = Convert.ToInt32(DataBinder.Eval((e.Item.Parent.Parent as RepeaterItem).DataItem, "id_contract"));
                DateTime planingDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Date"));

                var dt = Db.Db.Srvpl.GetDeviceCounterByDate(planingDate, null, idContract, idDevice);

                lvDeviceCounter.DataSource = dt;
                lvDeviceCounter.DataBind();

                int vol = 0;

                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        vol = Convert.ToInt32(dt.Rows[0]["volume"]);
                    }
                    catch { }
                }

                var ses = Session[String.Format("volYear{0}Mth{1}", planingDate.Year, planingDate.Month)];

                int cnt = Convert.ToInt32(ses);
                ses = cnt + vol;

                var lVolRowTotal = (e.Item.Parent.Parent as RepeaterItem).FindControl("lVolRowTotal") as Label;

                if (lVolRowTotal != null)
                {
                    string strCnt = lVolRowTotal.Text.Replace(" ", String.Empty);
                    if (String.IsNullOrEmpty(strCnt)) strCnt = "0";

                    int count = Convert.ToInt32(strCnt);

                    count += vol;

                    lVolRowTotal.Text = String.Format("{0:### ### ### ### ###}",count);
                }
            }
        }

        protected void tblList_OnLoad(object sender, EventArgs e)
        {
            var dates = GetShownDates();

            foreach (var shownDatese in dates)
            {
                Session[String.Format("volYear{0}Mth{1}", shownDatese.Date.Year, shownDatese.Date.Month)] = 0;
            }
        }

        protected void rtrClientTotalCounterMonthes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lvVolumeSum = e.Item.FindControl("lvVolumeSum") as ListView;
            if (lvVolumeSum != null)
            {
                int idContract = MainHelper.DdlGetSelectedValueInt(ref ddlContract);
                DateTime planingDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Date"));

                var dt = Db.Db.Srvpl.GetVolumeSumByDate(ContractorId, idContract, planingDate);

                lvVolumeSum.DataSource = dt;
                lvVolumeSum.DataBind();
            }
            
        }

        //protected void btnDownloadSettings_OnClick(object sender, EventArgs e)
        //{
        //    RedirectWithParams(String.Format("ctrid={0}", ContractorId), false, ResolveClientUrl("~/WebForms/Client/SnmpClientSettings.ashx"));
        //}
    }
}

public class ShownDates
{
    public DateTime Date { get; set; }
    public string Month { get; set; }
    public bool ShowVol { get; set; }

    public ShownDates(DateTime date)
    {
        Date = date;
        Month = date.Month.ToString("00");
        ShowVol = false;
    }
}