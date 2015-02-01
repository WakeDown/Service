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
        public class Unit
        {
            #region Константы

            public const string sp = "ui_unit";

            #endregion

            /// <summary>
            /// Города (список выбора)
            /// </summary>
            /// <returns></returns>
            public static DataTable GetCitiesSelectionList()
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getCitiesSelectionList");
                return dt;
            }

            /// <summary>
            /// Подразделения компании (список выбора)
            /// </summary>
            /// <returns></returns>
            public static DataTable GetDepartmentSelectionList()
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getDepartmentSelectionList");
                return dt;
            }

            /// <summary>
            /// Организации (список выбора)
            /// </summary>
            /// <returns></returns>
            public static DataTable GetCompanySelectionList()
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getCompanySelectionList");
                return dt;
            }

            /// <summary>
            /// Контрагенты из программы Эталон (список выбора)
            /// </summary>
            /// <returns></returns>
            public static DataTable GetContractorSelectionList(string filterText = null, int? idContractor = null)
            {
                SqlParameter pFilterText = new SqlParameter() { ParameterName = "filter_text", Value = filterText, DbType = DbType.AnsiString };
                SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = idContractor, DbType = DbType.Int32 };

                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getContractorSelectionList", pFilterText, pIdContractor);
                return dt;
            }

            ///// <summary>
            ///// Получаем название контрагента по ID
            ///// </summary>
            ///// <returns></returns>
            //public static ContractorData GetContractorData(int icContractor)
            //{
            //    SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = icContractor, DbType = DbType.Int32 };

            //    DataTable dt = new DataTable();

            //    dt = ExecuteQueryStoredProcedure(sp, "getContractorData", pIdContractor);
            //    DataRow dr = dt.Rows[0];

            //    string name = dr["NAME"].ToString();
            //    string inn = dr["inn"].ToString();
            //    ContractorData cd = new ContractorData() { Name = name, Inn = inn};

            //    return cd;
            //}
        }
    }
}