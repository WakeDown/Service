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
        public class Zipcl
        {
            #region Константы

            public const string sp = "ui_zip_claims";
            public const string spReports = "ui_zip_claim_reports";

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

            #region EngeneerConclusion

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetEngeneerConclusionSelectionList()
            {
                return GetSelectionList("getEngeneerConclusionSelectionList");
            }

            #endregion

            #region ClaimStates

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetClaimStateSelectionList()
            {
                return GetSelectionList("getClaimStateSelectionList");
            }

            public static DataTable GetEtClaimStateSelectionList()
            {
                return GetSelectionList("getEtClaimStateSelectionList");
            }

            public static DataTable GetWaybillClaimStateSelectionList()
            {
                return GetSelectionList("getWaybillClaimStateSelectionList");
            }

            #endregion

            #region Devices

            public static DataTable CheckDeviceBySerialNum(string serialNum)
            {
                SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "checkDeviceByNum", pSerialNum);
                return dt;
            }

            public static DataTable CheckDeviceByInvNum(string invNum)
            {
                SqlParameter pInvNum = new SqlParameter() { ParameterName = "inv_num", Value = invNum, DbType = DbType.AnsiString };
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "checkDeviceByNum", pInvNum);
                return dt;
            }

            #endregion

            #region FilterSelectionLists


            public static DataTable GetManagerFilterSelectionList()
            {
                return GetSelectionList("");
            }

            public static DataTable GetContractorFilterSelectionList()
            {
                return GetSelectionList("getContractorFilterSelectionList");
            }

            #endregion

            public static DataTable GetClaimReport(int? idContractor, DateTime? dateBegin, DateTime? dateEnd)
            {
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };
                SqlParameter pDateBegin = new SqlParameter() { ParameterName = "date_begin", Value = dateBegin, DbType = DbType.DateTime };
                SqlParameter pDateEnd = new SqlParameter() { ParameterName = "date_end", Value = dateEnd, DbType = DbType.DateTime };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.spReports, "getClaimReport", pIdContractor, pDateBegin, pDateEnd);
                return dt;
            }

            public static DataTable GetLastClaimDaysCount(/*int idDevice,*/ string serialNum, string catalogNum)
            {
                //SqlParameter pIdDevice = new SqlParameter() { ParameterName = "id_device", Value = idDevice, DbType = DbType.Int32 };
                SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };
                SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = catalogNum, DbType = DbType.AnsiString };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getClaimUnitLastClaimDaysCount", /*pIdDevice,*/ pSerialNum, pCatalogNum);
                return dt;
                //string daysCount = dt.Rows[0]["days_count"].ToString();
                //return daysCount;
            }

            public static DataTable GetUserFilter(int idUser)
            {
                SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = idUser, DbType = DbType.Int32 };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getUserFilter", pIdUser);
                return dt;
            }

            public static int SaveUserFilter(int idUser, string filter)
            {
                SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = idUser, DbType = DbType.Int32 };
                SqlParameter pFilter = new SqlParameter() { ParameterName = "filter", Value = filter, DbType = DbType.AnsiString };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveUserFilter", pIdUser, pFilter);
                //return dt;

                int id = 0;

                if (dt.Rows.Count > 0)
                {
                    id = (int)dt.Rows[0]["id_user_filter"];
                }

                return id;
            }

            public static DataTable GetOftenSelectedList(string serialNum)
            {
                SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = serialNum, DbType = DbType.AnsiString };

                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getOftenSelectedList", pSerialNum);
                return dt;
            }


            #region EtCountAndPrice

            public static DataTable GetNomenclatureDataByNumber(string nomenclatureNum)
            {
                SqlParameter pNomNum = new SqlParameter() { ParameterName = "nomenclature_num", Value = nomenclatureNum, DbType = DbType.AnsiString };

                return GetSelectionList("getNomenclatureDataByNumber", pNomNum);
            }

            #endregion

            #region ZipGroup

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static DataTable GetZipGroupSelectionList()
            {
                return GetSelectionList("getZipGroupSelectionList");
            }

            #endregion

            #region Counter

            /// <summary>
            /// SelectionList
            /// </summary>
            /// <returns></returns>
            public static void SaveCounter(int idUser, string decsr, string ipAddress, string login)
            {
                SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = idUser, DbType = DbType.Int32 };
                SqlParameter pDecsr = new SqlParameter() { ParameterName = "descr", Value = decsr, DbType = DbType.AnsiString };
                SqlParameter pUserLogin = new SqlParameter() { ParameterName = "user_login", Value = login, DbType = DbType.AnsiString };
                SqlParameter pIpAddress = new SqlParameter() { ParameterName = "ip_address", Value = ipAddress, DbType = DbType.AnsiString };

                ExecuteStoredProcedure(Zipcl.sp, "saveCounter", pIdUser, pDecsr, pIpAddress, pUserLogin);
            }

            #endregion

            public static DataTable GetManager2OperatorList()
            {
                DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getManager2OperatorList");
                return dt;
            }

            //public static void SaveManager2Operator()
            //{
            //    SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = idUser, DbType = DbType.Int32 };
            //    SqlParameter pDecsr = new SqlParameter() { ParameterName = "descr", Value = decsr, DbType = DbType.AnsiString };
            //    SqlParameter pUserLogin = new SqlParameter() { ParameterName = "user_login", Value = login, DbType = DbType.AnsiString };
            //    SqlParameter pIpAddress = new SqlParameter() { ParameterName = "ip_address", Value = ipAddress, DbType = DbType.AnsiString };

            //    ExecuteStoredProcedure(Zipcl.sp, "saveCounter", pIdUser, pDecsr, pIpAddress, pUserLogin);
            //}
        }
    }
}