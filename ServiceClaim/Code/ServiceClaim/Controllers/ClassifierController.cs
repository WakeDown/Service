using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceClaim.Models;
using ServiceClaim.Objects;

namespace ServiceClaim.Controllers
{
    public class ClassifierController : BaseController
    {
        public ActionResult List()
        {
            var list = ClassifierCaterory.GetList();

            return View(list);
        }

        [HttpPost]
        public ActionResult ImportExcel()
        {
            //if (!user.UserCanEdit()) return RedirectToAction("AccessDenied", "Error");
            int id = 0;
            if (Request.Files.Count > 0)
            {
                try
                {
                    for (int i = 0; i < 1; i++)
                    {
                        var file = Request.Files[i];
                        if (Path.GetExtension(file.FileName) != ".xlsx" && Path.GetExtension(file.FileName) != ".xls")
                        {
                            throw new ArgumentException("Файл не был загружем. Формат файла отличается от XLSX и XLS.");
                        }

                        byte[] fileData = null;
                        using (var br = new BinaryReader(file.InputStream))
                        {
                            fileData = br.ReadBytes(file.ContentLength);
                        }

                        
                        //var doc = new Document() { Data = fileData, Name = file.FileName };
                        ResponseMessage responseMessage;
                        Classifier.SaveFromExcel(fileData, out responseMessage);
                        //bool complete = doc.Save(out responseMessage);
                        //if (!complete) throw new Exception(responseMessage.ErrorMessage);
                        //TempData["noPdf"] = noPdf;
                    }
                }
                catch (Exception ex)
                {
                    TempData["ServerError"] = ex.Message;
                    return RedirectToAction("List");
                }
            }
            return RedirectToAction("List");
        }
    }
}