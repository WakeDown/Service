using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZipClaim.Db
{
    public partial class Db
    {
        public class Srvpl
        {
            #region Константы

            public const string sp = "ui_service_planing";

            #endregion

            #region Common

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetSelectionList(string action, params SqlParameter[] sqlParams)
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, action, sqlParams);
                return dt;
            }

            #endregion

            #region CartridgeTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetCartridgeTypeSelectionList()
            {
                return GetSelectionList("getCartridgeTypeSelectionList");
            }

            #endregion

            #region ServiceClaimStatuses

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceClaimStatusSelectionList()
            {
                return GetSelectionList("getServiceClaimStatusSelectionList");
            }

            #endregion

            #region ContractStatuses

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractStatusSelectionList()
            {
                return GetSelectionList("getContractStatusSelectionList"); 
            }

            #endregion

            #region ContractTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractTypeSelectionList()
            {
                return GetSelectionList("getContractTypeSelectionList");
            }

            #endregion

            #region DeviceImprints

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceImprintSelectionList()
            {
                return GetSelectionList("getDeviceImprintSelectionList");
            }

            #endregion

            #region DeviceOptions

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceOptionSelectionList()
            {
                return GetSelectionList("getDeviceOptionSelectionList");
            }

            #endregion

            #region Devices

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceSelectionList()
            {
                return GetSelectionList("getDeviceSelectionList");
            }

            /// <summary>
            /// FilteredSelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceSelectionList(int idContract, string deviceIds = null)
            {
                //id_contract для того чтобы органичить список "Без уже добавленых"
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pDeviceIds = new SqlParameter() { ParameterName = "lst_id_device", Value = deviceIds, DbType = DbType.AnsiString };

                return GetSelectionList("getDeviceSelectionList", pIdContract, pDeviceIds);
            }

            internal static DataTable GetDeviceCounterByDate(DateTime planingDate, int? idContractor, int? idContract = null, int? idDevice = null)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pPlaningDate = new SqlParameter() { ParameterName = "planing_date", Value = planingDate, DbType = DbType.DateTime };

                var dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getDeviceCounterByDate", pIdDevice, pIdContract, pPlaningDate, pIdContractor);
                return dt;
            }

            public static DataTable GetDevicesCounterList(DateTime dateBegin, DateTime dateEnd, int? idContractor = null, int? idContract = null)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.Date };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.Date };

                return ExecuteQueryStoredProcedure(Srvpl.sp, "getDeviceCounterList", pIdContractor, pIdContract, pDateBegin, pDateEnd);
            }
            
            #endregion

            #region Contract2Devices

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            //public static DataTable GetContract2DevicesSelectionList()
            //{
            //    return GetSelectionList("getDeviceSelectionList");
            //}

            /// <summary>
            /// FilteredSelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContract2DevicesSelectionList(int idContract)
            {
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };

                return GetSelectionList("getContract2DevicesSelectionList", pIdContract);
            }

            public static DataTable GetContract2DevicesList(int idContractor, int? idContract = null, int? rowsCount = null/*, DateTime? periodBegin = null, DateTime? periodEnd = null*/)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pRowsCount = new SqlParameter() { ParameterName = "rows_count", Value = rowsCount, DbType = DbType.Int32 };
                /*SqlParameter pPeriodBegin = new SqlParameter() { ParameterName = "period_begin", Value = periodBegin, DbType = DbType.DateTime };
                SqlParameter pPeriodEnd = new SqlParameter() { ParameterName = "period_end", Value = periodEnd, DbType = DbType.DateTime };*/

                return ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2DevicesList", pIdContractor, pIdContract, pRowsCount/*, pPeriodBegin, pPeriodEnd*/);
            }

            

            #endregion

            #region DeviceModels

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceModelSelectionList()
            {
                return GetSelectionList("getDeviceModelSelectionList");
            }

            #endregion

            #region DeviceTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDeviceTypeSelectionList()
            {
                return GetSelectionList("getDeviceTypeSelectionList");
            }

            #endregion

            #region PrintTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetPrintTypeSelectionList()
            {
                return GetSelectionList("getPrintTypeSelectionList");
            }

            #endregion

            #region ServiceActionTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceActionTypeSelectionList()
            {
                return GetSelectionList("getServiceActionTypeSelectionList");
            }

            #endregion

            #region ServiceIntervals

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceIntervalSelectionList()
            {
                return GetSelectionList("getServiceIntervalSelectionList");
            }

            #endregion

            #region ServiceTypes

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceTypeSelectionList()
            {
                return GetSelectionList("getServiceTypeSelectionList");
            }

            #endregion

            #region ServiceZones

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceZoneSelectionList()
            {
                return GetSelectionList("getServiceZoneSelectionList");
            }

            #endregion

            #region FilterSelectionLists


            public static DataTable GetManagerFilterSelectionList()
            {
                return GetSelectionList("getContractsManagerFilterSelectionList");
            }

            public static DataTable GetContractorFilterSelectionList()
            {
                return GetSelectionList("getContractContractorFilterSelectionList");
            }

            #endregion

            #region Contracts

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractSelectionList()
            {
                return GetSelectionList("getContractSelectionList");
            }

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractList(int idContractor)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };

                return GetSelectionList("getContractList", pIdContractor);
            }
            

            #endregion

            #region ServiceClaims

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetServiceClaimSelectionList(int? idServiceClaim = null, int? idServiceClaimStatus = null)
            {
                SqlParameter pIdServiceClaim = new SqlParameter() { ParameterName = "id_service_claim", Value = idServiceClaim, DbType = DbType.Int32 };
                SqlParameter pIdServiceClaimStatus = new SqlParameter() { ParameterName = "id_service_claim_status", Value = idServiceClaimStatus, DbType = DbType.Int32 };

                return Zipcl.GetSelectionList("getServiceClaimSelectionList", pIdServiceClaim, pIdServiceClaimStatus);
            }

            #endregion



            internal static DataTable GetVolumeSumByDate(int idContractor, int idContract, DateTime planingDate)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };
                SqlParameter pPlaningDate = new SqlParameter() { ParameterName = "planing_date", Value = planingDate, DbType = DbType.DateTime };

                var dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getVolumeSumByDate", pIdContractor, pIdContract, pPlaningDate);
                return dt;
            }

            internal static DataTable GetContractDevicesCount(int idContractor, int idContract)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };

                var dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2DevicesTotalCount", pIdContractor, pIdContract);
                return dt;
            }

            internal static DataTable GetContractorDeviceList(int idContractor/*, int idContract*/)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                //SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };

                var dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2DevicesList", pIdContractor/*, pIdContract*/);
                return dt;
            }

            internal static DataTable GetContractorDevice(int idDevice, int idContract)
            {
                SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };
                SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_contract", Value = idContract, DbType = DbType.Int32 };

                var dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getContract2Devices", pIdDevice, pIdContract);
                return dt;
            }
        }
    }
}