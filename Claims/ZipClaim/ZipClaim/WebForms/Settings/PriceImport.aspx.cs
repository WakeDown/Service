using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic.FileIO;
using ZipClaim.Objects;

namespace ZipClaim.WebForms.Settings
{
    public partial class Import : BaseFilteredPage
    {
        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("catn", txtCatalogNum));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        //protected void UploadButton_Click(object sender, EventArgs e)
        //{
        //    if (FileUploader.HasFile)
        //    {
        //        try
        //        {
        //            using (TextFieldParser parser = new TextFieldParser(FileUploader.FileContent))
        //            {
        //                parser.TextFieldType = FieldType.Delimited;
        //                parser.SetDelimiters(",");
        //                while (!parser.EndOfData)
        //                {
        //                    //Processing row
        //                    string[] fields = parser.ReadFields();
        //                    foreach (string field in fields)
        //                    {
        //                        //TODO: Process field
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Label1.Text = "ERROR: " + ex.Message.ToString();
        //        }
        //    }
        //    else
        //    {
        //        Label1.Text = "You have not specified a file.";
        //    }
        //}

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        protected void tblList_DataBound(object sender, EventArgs e)
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);
            lRowsCount.Text = dv.Count.ToString();
        }
    }
}