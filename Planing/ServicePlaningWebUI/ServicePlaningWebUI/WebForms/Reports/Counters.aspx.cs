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
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class Counters : BaseFilteredPage
    {
        protected bool UserIsSysAdmin
        {
            get { return (Page.Master.Master as Site).UserIsSysAdmin; }
        }

        protected bool UserIsReport
        {
            get { return (Page.Master.Master as Site).UserIsReport; }
        }

        string serviceManagerRightGroup = ConfigurationManager.AppSettings["serviceManagerRightGroup"];

        protected override void FillFilterLinksDefaults()
        {
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("mth", txtDateMonth, DateTime.Now.ToString("MM.yyyy")));
            FilterLinks.Add(new FilterLink("mgr", ddlServiceManager, User.Id.ToString()));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);
        }

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlServiceManager, Db.Db.Users.GetUsersSelectionList(serviceManagerRightGroup), true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!UserIsSysAdmin && !UserIsReport)
            {
                Response.Redirect(FriendlyUrl.Href("~/Error"));
            }

            SetDefaults();
        }

        private void SetDefaults()
        {
            if (!IsPostBack)
            {
                sdsCounterReport.SelectParameters["date_month"].DefaultValue = DateTime.Now.ToString("MM.yyyy");
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        private void FillTotalRow()
        {
            sDevCountTotal.InnerText = ContractsList.Compute("Sum(dev_count)", "").ToString();
            sCurVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
            sPrevVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
            sPrevPrevVolTotal.InnerText = DeviceData.Select(String.Format("d_year={0} and d_month={1}", GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total")).ToString("### ### ### ### ### ### ###");
        }

        protected void tblCounterReport_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.FindControl("hfIdContractor") == null)
                return;

            //Подтягиваем список договоров для контрагента
            int idContractor = Convert.ToInt32((e.Item.FindControl("hfIdContractor") as HiddenField).Value);

            //int? idManager = MainHelper.DdlGetSelectedValueInt(ref ddlServiceManager);
            //DateTime? dateMonth = MainHelper.TxtGetTextDateTime(ref txtDateMonth);


           var rtrContractorContractList =  (e.Item.FindControl("rtrContractorContractList") as Repeater);

           var ctr = ContractsList.Select(String.Format("id_contractor = {0}", idContractor));

            var devCount = ctr.Sum(r => r.Field<int>("dev_count"));
            (e.Item.FindControl("sDevCount") as HtmlContainerControl).InnerText = devCount.ToString();

            var ctrCurVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sContractorCurVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrCurVol);

            var ctrPrevVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sContractorPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevVol);

            var ctrPrevPrevVol = DeviceData.Select(String.Format("id_contractor={0} and d_year={1} and d_month={2}", idContractor, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sContractorPrevPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevPrevVol);

            rtrContractorContractList.DataSource = ctr;
            rtrContractorContractList.DataBind();
        }

        protected void rtrContractorContractList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.FindControl("hfIdContract") == null)
                return;

            int idContract = Convert.ToInt32((e.Item.FindControl("hfIdContract") as HiddenField).Value);

            StringBuilder list = new StringBuilder();

            DataRow[] dev = DeviceList.Select(String.Format("id_contract = {0}", idContract));

            var ctrCurVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetCurMonth().Year, GetCurMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sCurVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrCurVol);

            var ctrPrevVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetPrevMonth().Year, GetPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevVol);

            var ctrPrevPrevVol = DeviceData.Select(String.Format("id_contract={0} and d_year={1} and d_month={2}", idContract, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month)).Sum(r => r.Field<int>("volume_total"));
            (e.Item.FindControl("sPrevPrevVol") as HtmlContainerControl).InnerText = String.Format("{0: ### ### ### ### ### ### ### ###}", ctrPrevPrevVol);

            foreach (DataRow row in dev)
            {
                int idDevice = Convert.ToInt32(row[1]);
                
                //Объем печати
                var cur = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and d_year={2} and d_month={3}", idContract, idDevice, GetCurMonth().Year, GetCurMonth().Month));
                var prev = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and  d_year={2} and d_month={3}", idContract, idDevice, GetPrevMonth().Year, GetPrevMonth().Month));
                var prevPrev = DeviceData.Select(String.Format("id_contract={0} and id_device={1} and  d_year={2} and d_month={3}", idContract, idDevice, GetPrevPrevMonth().Year, GetPrevPrevMonth().Month));

                var curVol = cur.Any() ? cur[0][7] : String.Empty;
                var prevVol = prev.Any() ? prev[0][7] : String.Empty;
                var prevPrevVol = prevPrev.Any() ? prevPrev[0][7] : String.Empty;

                //Загрузка аппарата
                int? maxVolume = Db.Db.GetValueIntOrNull(row[5]);

                decimal? curLoading = null;
                decimal? prevLoading = null;
                decimal? prevPrevLoading = null;

                if (maxVolume != null && maxVolume > 0)
                {
                    curLoading = !String.IsNullOrEmpty(curVol.ToString()) ? Convert.ToDecimal(int.Parse(curVol.ToString()) / Convert.ToDecimal(maxVolume.Value)) : -999;
                    prevLoading = !String.IsNullOrEmpty(prevVol.ToString()) ? Convert.ToDecimal(int.Parse(prevVol.ToString())) / Convert.ToDecimal(maxVolume.Value) : -999;
                    prevPrevLoading = !String.IsNullOrEmpty(prevPrevVol.ToString()) ? Convert.ToDecimal(int.Parse(prevPrevVol.ToString())) / Convert.ToDecimal(maxVolume.Value) : -999;

                    if (curLoading == -999){ curLoading = null;}
                    if (prevLoading == -999){prevLoading = null;}
                    if (prevPrevLoading == -999) {prevPrevLoading = null;}
                }

                //string bg = !String.IsNullOrEmpty(prevVol.ToString()) && prev[0][12].ToString().Equals("1")
                //    ? "bg-warning"
                //    : String.Empty;

                //if (!String.IsNullOrEmpty(bg))
                //{
                //    string s = "";
                //}

                list.Append("<div class='row bg-white'>");
                list.Append(String.Format(@"<div class='col-sm-3'><span class='pad-l-mid'>{0}</span></div>", row[2]));
                list.Append(String.Format(@"<div class='col-sm-1'></div>"));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", curVol, !String.IsNullOrEmpty(curVol.ToString()) && cur[0][12].ToString().Equals("True") ? "bg-warning" : String.Empty));//Выделяем цветом если выводится среднее значение
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", prevVol, !String.IsNullOrEmpty(prevVol.ToString()) && prev[0][12].ToString().Equals("True")
                    ? "bg-warning"
                    : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:### ### ### ### ### ###}</div>", prevPrevVol, !String.IsNullOrEmpty(prevPrevVol.ToString()) && prevPrev[0][12].ToString().Equals("True") ? "bg-warning" : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:P0}</div>", curLoading, curLoading.HasValue && curLoading.Value > 1 ? "bg-danger" : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:P0}</div>", prevLoading, prevLoading.HasValue && prevLoading.Value > 1 ? "bg-danger" : String.Empty));
                list.Append(String.Format(@"<div class='col-sm-1 align-right {1}'>{0:P0}</div>", prevPrevLoading, prevPrevLoading.HasValue && prevPrevLoading.Value > 1 ? "bg-danger" : String.Empty));
                //list.Append(String.Format(@"<div class='col-sm-1'>{0:dd.MM.yyyy}</div>", row[3]));//Последняя дата счетчика
                list.Append(String.Format(@"<div class='col-sm-1 align-right bold text-success'>{0:P0}</div>", GetLoadingAverage(curLoading, prevLoading, prevPrevLoading)));
                list.Append(String.Format(@"<div class='col-sm-1 align-right'>{0:### ### ### ### ### ###}</div>", row[4]));
                list.Append("</div>");
            }

            var phContractorContractsDevicesList = e.Item.FindControl("phContractorContractsDevicesList") as HtmlContainerControl;
            phContractorContractsDevicesList.InnerHtml = list.ToString();
        }

        private decimal? GetLoadingAverage(params decimal?[] arr)
        {
            List<decimal> list = new List<decimal>();
            decimal? average = null;

            foreach (decimal? d in arr)
            {
                if (d != null)
                {
                    list.Add(d.Value);
                }
            }

            if (list.Any())
            {
                average = list.ToArray().Average();
            }

            return average;
        }

        protected DateTime GetCurMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);

            return dt;
        }
        protected DateTime GetPrevMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
            return dt.AddMonths(-1);
        }
        protected DateTime GetPrevPrevMonth()
        {
            DateTime dt = MainHelper.TxtGetTextDateTime(ref txtDateMonth);
            return dt.AddMonths(-2);
        }


        DataTable DeviceList { get; set; }
        DataTable DeviceData { get; set; }
        DataTable ContractsList { get; set; }

        protected void tblCounterReport_OnDataBinding(object sender, EventArgs e)
        {
            //Выбираем все чтобы фильтровать на веб сервере при отрисовке таблицы

            int? idManager = MainHelper.DdlGetSelectedValueInt(ref ddlServiceManager);
            DateTime? dateMonth = MainHelper.TxtGetTextDateTime(ref txtDateMonth);

            DeviceList = Db.Db.Srvpl.GetCounterReportContractorContractDeviceList(null, idManager, dateMonth);
            DeviceData = Db.Db.Srvpl.GetCounterReportContractorContractDeviceData(null, idManager, dateMonth);
            ContractsList = Db.Db.Srvpl.GetCounterReportContractorContractList(null, idManager, dateMonth);

            FillTotalRow();
        }

        protected string GetItem(object dataItem, int i)
        {
            return (dataItem as DataRow)[i].ToString();
        }

        
    }
}