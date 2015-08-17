using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using ZipClaim.Helpers;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Reports
{
    public partial class ClaimReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FillLists();
            }

            RegisterStartupScripts();
        }

        private void FillLists()
        {
            MainHelper.DdlFill(ref ddlContractor, Db.Db.Zipcl.GetContractorFilterSelectionList(),true, MainHelper.ListFirstItemType.SelectAll);
        }

        protected void btnLoadExcel_OnClick(object sender, EventArgs e)
        {
            int? IdContractor = MainHelper.DdlGetSelectedValueInt(ref ddlContractor, true);
            DateTime? dateBegin = MainHelper.TxtGetTextDateTime(ref txtDateBegin, true);
            DateTime? dateEnd = MainHelper.TxtGetTextDateTime(ref txtDateEnd, true);

            DataTable dt = Db.Db.Zipcl.GetClaimReport(IdContractor, dateBegin, dateEnd);

            byte[] file = CreateExcelFile(dt);
            Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.OutputStream.Write(file, 0, file.Length);
            string fileName = "ClaimReport.xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //Response.AddHeader("Content-Length", file.Length.ToString());
            Response.OutputStream.Write(file, 0, file.Length);
            Response.Flush();
            Response.End();
        }

        private byte[] CreateExcelFile(DataTable dt)
        {
            XLWorkbook wb = new XLWorkbook();
            IXLWorksheet sheet = wb.AddWorksheet("Отчет");

            const int startCol = 1;
            const int startRow = 1;

            int row = startRow;
            int col = startCol;
            //Заголовок
            sheet.Cell(row, col).Value = "ID Заявки"; //1
            sheet.Column(col).Width = 11;
            sheet.Cell(row, ++col).Value = "Дата Заявки";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Модель";
            sheet.Column(col).Width = 25;
            sheet.Cell(row, ++col).Value = "Серийный номер";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Контрагент";//5
            sheet.Column(col).Width = 30;
            sheet.Cell(row, ++col).Value = "Город";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Адрес";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Инженер";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Сервисный администратор";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Оператор";//10
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Менеджер";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Текущий статус заявки";
            sheet.Column(col).Width = 20;
            sheet.Cell(row, ++col).Value = "Состояние оборудования";
            sheet.Column(col).Width = 40;
            sheet.Cell(row, ++col).Value = "Количество ЗИП в данной заявке";
            sheet.Column(col).Width = 13;
            sheet.Cell(row, ++col).Value = "Передано";//15
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Назначено";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Проставлены цены";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Согласованы цены";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Не согласованы цены";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Указан номер требования";//20
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Завершена";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Заказано";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Оформлено";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Готово к отгрузке";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Отгружено";//25
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Запрошены цены";
            sheet.Column(col).Width = 15;
            sheet.Cell(row, ++col).Value = "Номер требования";
            sheet.Cell(row, ++col).Value = "Причина отклонения";
            sheet.Column(col).Width = 40;
            // />Заголовок
            int lastCol = col;

            var header = sheet.Range(sheet.Cell(row, startCol), sheet.Cell(row, lastCol));
            header.Style.Font.SetBold();

            foreach (DataRow dr in dt.Rows)
            {
                row++;
                col = startCol;

                
                sheet.Cell(row, col).Value = dr[0].ToString();
                sheet.Cell(row, ++col).Value = String.Format("{0:dd.MM.yyyy}",dr[1]);
                sheet.Cell(row, ++col).Value = dr[2].ToString();
                sheet.Cell(row, ++col).Value = dr[3].ToString();
                sheet.Cell(row, ++col).Value = dr[4].ToString();
                sheet.Cell(row, ++col).Value = dr[5].ToString();
                sheet.Cell(row, ++col).Value = dr[27].ToString();
                sheet.Cell(row, ++col).Value = dr[6].ToString();
                sheet.Cell(row, ++col).Value = dr[7].ToString();
                sheet.Cell(row, ++col).Value = dr[8].ToString();
                sheet.Cell(row, ++col).Value = dr[9].ToString();
                sheet.Cell(row, ++col).Value = dr[10].ToString();
                sheet.Cell(row, ++col).Value = dr[11].ToString();
                sheet.Cell(row, ++col).Value = dr[12].ToString();
                sheet.Cell(row, ++col).Value = dr[13].ToString();
                sheet.Cell(row, ++col).Value = dr[14].ToString();
                sheet.Cell(row, ++col).Value = dr[15].ToString();
                sheet.Cell(row, ++col).Value = dr[16].ToString();
                sheet.Cell(row, ++col).Value = dr[17].ToString();
                sheet.Cell(row, ++col).Value = dr[18].ToString();
                sheet.Cell(row, ++col).Value = dr[19].ToString();
                sheet.Cell(row, ++col).Value = dr[21].ToString();
                sheet.Cell(row, ++col).Value = dr[22].ToString();
                sheet.Cell(row, ++col).Value = dr[23].ToString();
                sheet.Cell(row, ++col).Value = dr[24].ToString();
                sheet.Cell(row, ++col).Value = dr[25].ToString();
                sheet.Cell(row, ++col).Value = dr[26].ToString();
                sheet.Cell(row, ++col).Value = dr[20].ToString();
                //sheet.Cell(row, col).Style.Alignment.SetWrapText();
            }

            var all = sheet.Range(sheet.Cell(startRow, startCol), sheet.Cell(row, lastCol));
            all.Style.Font.SetFontSize(10);
            all.CreateTable();
            
            all.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            all.Style.Border.SetBottomBorderColor(XLColor.Gray);
            all.Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            all.Style.Border.SetTopBorderColor(XLColor.Gray);
            all.Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            all.Style.Border.SetRightBorderColor(XLColor.Gray);
            all.Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            all.Style.Border.SetLeftBorderColor(XLColor.Gray);

            var ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});",
                ddlContractor.ClientID, txtContractorInn.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterContractorListByInn", script, true);
            //</Фильтрация списка>
        }
    }
}