using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ZipClaim.Models
{
    public class Contract : Db.Db
    {
        public int Id;
        public string Number;
        public decimal? Price;
        public int? IdServiceType;
        public int? IdContractType;
        public int? IdContractor;
        public int? IdContractStatus;
        public int? IdManager;
        public DateTime? DateBegin;
        public DateTime? DateEnd;
        public int? IdCreator;
        public int? IdZipState;
        public string Note;
        public int? IdContractProlong;
        public int? IdPriceDiscount;
        public bool PeriodReduction;
        public int? HandlingDevices;

        public Contract()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Contract(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Db.Db.Srvpl.sp, "getContract", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_contract"];
                Number = dr["number"].ToString();
                Price = GetValueDeciamlOrNull(dr["price"].ToString());
                IdServiceType = GetValueIntOrNull(dr["id_service_type"].ToString());
                IdContractType = (int)dr["id_contract_type"];
                IdContractor = (int)dr["id_contractor"];
                IdContractStatus = (int)dr["id_contract_status"];
                IdManager = (int)dr["id_manager"];
                DateBegin = GetValueDateTimeOrNull(dr["date_begin"].ToString());
                DateEnd = GetValueDateTimeOrNull(dr["date_end"].ToString());
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());
                IdZipState = GetValueIntOrNull(dr["id_zip_state"].ToString());
                Note = dr["note"].ToString();
                IdContractProlong = GetValueIntOrNull(dr["id_contract_prolong"].ToString());
                IdPriceDiscount = GetValueIntOrNull(dr["id_price_discount"].ToString());
                PeriodReduction = GetValueBool(dr["period_reduction"].ToString());
                HandlingDevices = GetValueIntOrNull(dr["handling_devices"].ToString());
            }
        }
    }
}