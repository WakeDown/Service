using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ZipClaim.Helpers;
using ZipClaim.Objects.Interfaces;

namespace ZipClaim.Models
{
    public class Claim : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int? IdDevice { get; set; }
        public string SerialNum { get; set; }
        public string DeviceModel { get; set; }
        public string Contractor { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int? Counter { get; set; }
        public int IdEngeneerConclusion { get; set; }
        public int? IdClaimState { get; set; }
        public string RequestNum { get; set; }
        public string Descr { get; set; }
        public int? IdManager { get; set; }
        public int? IdOperator { get; set; }
        public int? IdServiceEngeneer { get; set; }
        public int? IdServiceAdmin { get; set; }
        public int? IdContractor { get; set; }
        public int? IdCity { get; set; }
        public int IdCreator { get; set; }
        public bool DisplayPriceState { get; set; }
        public bool DisplayDoneState { get; set; }
        public bool DisplaySendState { get; set; }
        public bool DisplayZipConfirmState { get; set; }
        public bool DisplayPriceSet { get; set; }
        public bool DisplayCancelState { get; set; }
        public string ServiceDeskNum { get; set; }
        public DateTime DateCreate { get; set; }
        public int? CounterColour { get; set; }
        public string CancelComment { get; set; }
        public string ObjectName { get; set; }
        //public StateHistory[] ChangeStateHistory { get; set; }
        public string WaybillNum { get; set; }
        public string ContractNum { get; set; }
        public string ContractType { get; set; }
        public string ContractorSdNum { get; set; }

        //Данные для клиентов
        public string ServiceEngeneer { get; set; }
        public string Manager { get; set; }
        public string EngeneerConclusion { get; set; }
        public string ManagerMail { get; set; }
        public int? IdContract { get; set; }
        public bool HideTop { get; set; }
        public string ServiceIdServSheet { get; set; }
        public string ServiceIdClaim { get; set; }


        public Claim()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Claim(int id)
        {
            Get(id);

        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getClaim", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_claim"];
                IdDevice = GetValueIntOrNull(dr["id_device"].ToString());
                SerialNum = dr["serial_num"].ToString();
                DeviceModel = dr["device_model"].ToString();
                Contractor = dr["contractor"].ToString();
                City = dr["city"].ToString();
                Address = dr["address"].ToString();
                Counter = GetValueIntOrNull(dr["counter"].ToString());
                IdEngeneerConclusion = (int)dr["id_engeneer_conclusion"];
                IdClaimState = GetValueIntOrNull(dr["id_claim_state"].ToString());
                RequestNum = dr["request_num"].ToString();
                Descr = dr["descr"].ToString();
                IdManager = GetValueIntOrNull(dr["id_manager"].ToString());
                IdOperator = GetValueIntOrNull(dr["id_operator"].ToString());
                IdServiceEngeneer = GetValueIntOrNull(dr["id_engeneer"].ToString());
                IdServiceAdmin = GetValueIntOrNull(dr["id_service_admin"].ToString());
                IdContractor = GetValueIntOrNull(dr["id_contractor"].ToString());
                IdCity = GetValueIntOrNull(dr["id_city"].ToString());
                IdCreator = (int)dr["id_creator"];
                DisplayPriceState = GetValueBool(dr["display_price_states"]);
                DisplayDoneState = GetValueBool(dr["display_done_state"]);
                DisplaySendState = GetValueBool(dr["display_send_state"]);
                DisplayZipConfirmState = GetValueBool(dr["display_zip_confirm_state"]);
                DisplayPriceSet = GetValueBool(dr["display_price_set"]);
                DisplayCancelState = GetValueBool(dr["display_cancel_state"]);
                ServiceDeskNum = dr["service_desk_num"].ToString();
                DateCreate = Convert.ToDateTime(dr["date_create"].ToString());
                CounterColour = GetValueIntOrNull(dr["counter_colour"].ToString());
                CancelComment = dr["cancel_comment"].ToString();
                ObjectName = dr["object_name"].ToString();
                WaybillNum = dr["waybill_num"].ToString();
                ContractNum = dr["contract_num"].ToString();
                ContractType = dr["contract_type"].ToString();
                ServiceEngeneer = dr["service_engeneer"].ToString();
                Manager = dr["manager"].ToString();
                EngeneerConclusion = dr["engeneer_conclusion"].ToString();
                ManagerMail = dr["manager_mail"].ToString();
                ContractorSdNum = dr["contractor_sd_num"].ToString();
                IdContract = GetValueIntOrNull(dr["id_contract"].ToString());
                HideTop = GetValueBool(dr["hide_top"].ToString());

                //SqlParameter pIdClaim = new SqlParameter() { ParameterName = "id_claim", Value = id, DbType = DbType.Int32 };
                //dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getClaimStateHistory", pIdClaim);
                //if (dt.Rows.Count > 0)
                //{
                //    List<StateHistory> lstHist = new List<StateHistory>();

                //    foreach (DataRow row in dt.Rows)
                //    {
                //        lstHist.Add(new StateHistory() { IdClaim = (int)row["id_claim"], StateName = row["claim_state"].ToString(), ChangeDate = GetValueDateTimeOrNull(row["change_date"].ToString()) });
                //    }

                //    ChangeStateHistory = lstHist.ToArray();
                //}
                //else
                //{
                //    ChangeStateHistory = new StateHistory[0];
                //}
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = IdDevice, DbType = DbType.Int32 };
            SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = SerialNum, DbType = DbType.AnsiString };
            SqlParameter pDeviceModel = new SqlParameter() { ParameterName = "device_model", Value = DeviceModel, DbType = DbType.AnsiString };
            SqlParameter pContractor = new SqlParameter() { ParameterName = "contractor", Value = Contractor, DbType = DbType.AnsiString };
            SqlParameter pCity = new SqlParameter() { ParameterName = "city", Value = City, DbType = DbType.AnsiString };
            SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = Address, DbType = DbType.AnsiString };
            SqlParameter pCounter = new SqlParameter() { ParameterName = "counter", Value = Counter, DbType = DbType.Int32 };
            SqlParameter pIdEngeneerConclusion = new SqlParameter() { ParameterName = "id_engeneer_conclusion", Value = IdEngeneerConclusion, DbType = DbType.Int32 };
            SqlParameter pIdClaimState = new SqlParameter() { ParameterName = "id_claim_state", Value = IdClaimState, DbType = DbType.Int32 };
            SqlParameter pRequestNum = new SqlParameter() { ParameterName = "request_num", Value = RequestNum, DbType = DbType.AnsiString };
            SqlParameter pDescr = new SqlParameter() { ParameterName = "descr", Value = Descr, DbType = DbType.AnsiString };
            SqlParameter pIdManager = new SqlParameter() { ParameterName = "id_manager", Value = IdManager, DbType = DbType.Int32 };
            SqlParameter pIdOperator = new SqlParameter() { ParameterName = "id_operator", Value = IdOperator, DbType = DbType.Int32 };
            SqlParameter pIdServiceEngeneer = new SqlParameter() { ParameterName = "id_engeneer", Value = IdServiceEngeneer, DbType = DbType.Int32 };
            SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = IdServiceAdmin, DbType = DbType.Int32 };
            SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = IdContractor, DbType = DbType.Int32 };
            SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = IdCity, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pServiceDeskNum = new SqlParameter() { ParameterName = "service_desk_num", Value = ServiceDeskNum, DbType = DbType.AnsiString };
            SqlParameter pCounterColour = new SqlParameter() { ParameterName = "counter_colour", Value = CounterColour, DbType = DbType.Int32 };
            SqlParameter pCancelComment = new SqlParameter() { ParameterName = "cancel_comment", Value = CancelComment, DbType = DbType.AnsiString };
            SqlParameter pObjectName = new SqlParameter() { ParameterName = "object_name", Value = ObjectName, DbType = DbType.AnsiString };
            SqlParameter pWaybillNum = new SqlParameter() { ParameterName = "waybill_num", Value = WaybillNum, DbType = DbType.AnsiString };
            SqlParameter pContractNum = new SqlParameter() { ParameterName = "contract_num", Value = ContractNum, DbType = DbType.AnsiString };
            SqlParameter pContractType = new SqlParameter() { ParameterName = "contract_type", Value = ContractType, DbType = DbType.AnsiString };
            SqlParameter pContractorSdNum = new SqlParameter() { ParameterName = "contractor_sd_num", Value = ContractorSdNum, DbType = DbType.AnsiString };
            SqlParameter pServiceIdServSheet = new SqlParameter() { ParameterName = "service_id_serv_sheet", Value = ServiceIdServSheet, DbType = DbType.AnsiString };
            SqlParameter pServiceIdClaim = new SqlParameter() { ParameterName = "service_id_claim", Value = ServiceIdClaim, DbType = DbType.AnsiString };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveClaim", pId, pIdDevice, pSerialNum, pDeviceModel, pContractor, pCity, pAddress, pIdEngeneerConclusion, pCounter, pIdClaimState, pRequestNum, pDescr, pIdManager, pIdOperator, pIdServiceEngeneer, pIdServiceAdmin, pIdContractor, pIdCity, pIdCreator, pServiceDeskNum, pCounterColour, pCancelComment, pObjectName, pWaybillNum, pContractNum, pContractType, pContractorSdNum, pServiceIdServSheet, pServiceIdClaim);


            if (dt.Rows.Count > 0)
            {
                Id = (int)dt.Rows[0]["id_claim"];
            }
        }

        public void Delete(int id)
        {
            throw new NotSupportedException();
            //SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = id, DbType = DbType.Int32 };

            //ExecuteStoredProcedure(Srvpl.sp, "closeClaim", pId);
        }

        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeClaim", pId, pIdCreator);
        }

        public void SetPriceOkState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimPriceOkState", pId,  pIdCreator);
        }

        public void SetPriceFailState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimPriceFailState", pId, pIdCreator);
        }

        public void SetZipConfirmState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimZipConfirmState", pId, pIdCreator);
        }

        public void SetSendState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimSendState", pId, pIdCreator);
        }

        public void SetDoneState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimDoneState", pId, pIdCreator);
        }

        public void SetRequestPriceState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimRequestPriceState", pId, pIdCreator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifSetPriceDone">Меняем статус если не осталось непроставленных цен в позициях по данной заявке</param>
        public void SetPriceSetState(bool ifSetPriceDone = false)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pIfSetPriceDone = new SqlParameter() { ParameterName = "if_set_price_done", Value = ifSetPriceDone, DbType = DbType.Boolean };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimPriceSetState", pId, pIdCreator, pIfSetPriceDone);
        }

        public void SupplyReturnClaim()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimSupplyReturn", pId, pIdCreator);
        }

        public void SetRequestNumState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimRequestNumState", pId, pIdCreator);
        }

         public void SetManSelState()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimManSelState", pId, pIdCreator);
        }

         public void SetCancelState()
         {
             SqlParameter pId = new SqlParameter() { ParameterName = "id_claim", Value = Id, DbType = DbType.Int32 };
             SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
             SqlParameter pCnacelComment = new SqlParameter() { ParameterName = "cancel_comment", Value = CancelComment, DbType = DbType.AnsiString };

             DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimCancelState", pId, pIdCreator, pCnacelComment);
         }
    }

    //public class StateHistory
    //{
    //    public int IdClaim { get; set; }
    //    public string StateName { get; set; }
    //    public DateTime? ChangeDate { get; set; }
    //}
}