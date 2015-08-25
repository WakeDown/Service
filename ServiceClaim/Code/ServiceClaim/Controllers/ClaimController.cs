using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceClaim.Helpers;
using ServiceClaim.Models;
using ServiceClaim.Objects;

namespace ServiceClaim.Controllers
{
    public class ClaimController : BaseController
    {
        //[HttpGet]
        //public ActionResult Index2(int? id)
        //{
        //    if (!id.HasValue) return RedirectToAction("New");
        //    Claim model = new Claim(id.Value);
        //    return View(model);
        //}

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue) return RedirectToAction("New");
            Claim model=new Claim();
            try
            {
                model = new Claim(id.Value);
            }
            catch (Exception ex)
            {
                return RedirectToAction("HandledError", "Error", new { message= ex.Message});
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(Claim model)
        //{
        //    //var model = new Claim(id);
        //    return View();
        //}

        public JsonResult ClaimSave(int? id, string descr)
        {
            try
            {
                if (!id.HasValue) throw new ArgumentException("Не указана заявка!");
                ResponseMessage responseMessage;
                var model = new Claim();
                model.Id = id.Value;
                model.Descr = descr;
                bool complete = model.Save(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
            return Json(new { });
        }

        public JsonResult ClaimContinue(int? id, string descr)
        {
            try
            {
                if (!id.HasValue) throw new ArgumentException("Не указана заявка!");
                ResponseMessage responseMessage;
                var model = new Claim();
                model.Id = id.Value;
                model.Descr = descr;
                bool complete = model.SaveAndGoNextState(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
            return Json(new { });
        }
        public JsonResult ClaimEnd(int? id, string descr)
        {
            try
            {
                if (!id.HasValue) throw new ArgumentException("Не указана заявка!");
                ResponseMessage responseMessage;
                var model = new Claim();
                model.Id = id.Value;
                model.Descr = descr;
                bool complete = model.SaveAndGoEndState(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
            return Json(new { });
        }

        public ActionResult List()
        {
            ListResult<Claim> result = Claim.GetList(topRows: 10);

            return View(result);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Claim model)
        {
            //if (!CurUser.UserCanCreateClaim()) return RedirectToAction("AccessDenied", "Error");

            //Создаем заявку с основными полями и одельно первый статус с комментарием
            try
            {
                ResponseMessage responseMessage;
                model.Contractor = new Contractor() { Id = MainHelper.GetValueInt(Request.Form["ctrList"]) };
                model.Contract = new Contract() { Id = MainHelper.GetValueInt(Request.Form["contList"]) };
                model.Device = new Device() { Id = MainHelper.GetValueInt(Request.Form["devList"]) };
                model.Descr = Request.Form["descr"];
                bool complete = model.Save(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);

                return RedirectToAction("Index", new { id = responseMessage.Id });
            }
            catch (Exception ex)
            {
                TempData["ServerError"] = ex.Message;
                return View("New", model);
            }

            //return RedirectToAction("New", model);
        }

        [HttpPost]
        public JsonResult GetCtors(int? idContractor = null, string contractorName = null, int? idContract = null, string contractNumber = null, int? idDevice = null, string deviceName = null)
        {
            var list = Contractor.GetServiceList(idContractor, contractorName, idContract, contractNumber, idDevice, deviceName);
            return Json(list);
        }

        [HttpPost]
        public JsonResult GetConts(int? idContractor = null, string contractorName = null, int? idContract = null, string contractNumber = null, int? idDevice = null, string deviceName = null)
        {
            var list = Contract.GetList(idContractor, contractorName, idContract, contractNumber, idDevice, deviceName);
            return Json(list);
        }

        [HttpPost]
        public JsonResult GetDevices(int? idContractor = null, string contractorName = null, int? idContract = null, string contractNumber = null, int? idDevice = null, string deviceName = null)
        {
            var result = Device.GetSearchList(idContractor, contractorName, idContract, contractNumber, idDevice, deviceName);

            return Json(result);
        }

       
        [HttpPost]
        public ActionResult StateNew(Claim model)
        {
            try
            {
                ResponseMessage responseMessage;
                bool complete = model.SaveAndGoNextState(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);

                //return RedirectToAction("Index", new { id = responseMessage.Id });
            }
            catch (Exception ex)
            {
                TempData["ServerError"] = ex.Message;
                return RedirectToAction("Index", new {id=model.Id});
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult StateNewadd(Claim model)
        {
            try
            {
                ResponseMessage responseMessage;
                bool complete = model.SaveAndGoNextState(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);

                //return RedirectToAction("Index", new { id = responseMessage.Id });
            }
            catch (Exception ex)
            {
                TempData["ServerError"] = ex.Message;
                return RedirectToAction("Index", new { id = model.Id });
            }

            return RedirectToAction("List");
        }

        [HttpPost]
       
        public ActionResult StateSet(Claim model)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.Form["ClaimWorkConfirm"]))
                {
                    ResponseMessage responseMessage;
                    bool complete = model.SaveAndGoNextState(out responseMessage);
                    if (!complete) throw new Exception(responseMessage.ErrorMessage);

                    //return RedirectToAction("Index", new { id = responseMessage.Id });
                }
                else if (!String.IsNullOrEmpty(Request.Form["ClaimWorkCancel"]))
                {
                    ResponseMessage responseMessage;
                    model.Descr = Request.Form["ClaimWorkCancelDescr"];
                    bool complete = model.GoBackState(out responseMessage);
                    if (!complete) throw new Exception(responseMessage.ErrorMessage);

                    //return RedirectToAction("Index", new { id = responseMessage.Id });
                }
            }
            catch (Exception ex)
            {
                TempData["ServerError"] = ex.Message;
                return RedirectToAction("Index", new { id = model.Id });
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult StateTechWork(ServiceSheet model)
        {
            try
            {
                ResponseMessage responseMessage;
                bool complete = model.Save(out responseMessage);
                if (!complete) throw new Exception(responseMessage.ErrorMessage);

                //return RedirectToAction("Index", new { id = responseMessage.Id });
            }
            catch (Exception ex)
            {
                TempData["ServerError"] = ex.Message;
                return RedirectToAction("Index", new { id = model.Id });
            }

            return RedirectToAction("List");
        }
    }
}