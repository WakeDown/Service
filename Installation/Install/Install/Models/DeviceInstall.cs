using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Install.Models
{
    public class DeviceInstall : Db.Db
    {
        public int Id { get; set; }
        public string EtNumber { get; set; }
        public string ContractNumber { get; set; }
        public int IdContractor { get; set; }
        public DateTime PlanDate { get; set; }
        public string DeviceModel { get; set; }
        public int IdCity { get; set; }
        public string Address { get; set; }
        public string ObjectName { get; set; }
        public string ContactName { get; set; }
        public string AddDevices { get; set; }
        public int IdManager { get; set; }
        public int IdServiceAdmin { get; set; }
        public string Comment { get; set; }
        public int? IdCreator { get; set; }

        public DeviceInstall()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public DeviceInstall(int id)
        {
            Get(id);

        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_install", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Devinst.sp, "getDeviceInstall", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device_install"];
                EtNumber = dr["et_number"].ToString();
                ContractNumber = dr["contract_number"].ToString();
                IdContractor = (int) dr["id_contractor"];
                PlanDate = Convert.ToDateTime(dr["plan_date"].ToString());
                DeviceModel = dr["device_model"].ToString();
                IdCity = (int) dr["id_city"];
                Address = dr["address"].ToString();
                ObjectName = dr["object_name"].ToString();
                ContactName = dr["contact_name"].ToString();
                AddDevices = dr["add_devices"].ToString();
                IdManager = (int) dr["id_manager"];
                IdServiceAdmin = (int) dr["id_service_admin"];
                Comment = dr["descr"].ToString();
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_install", Value = Id, DbType = DbType.Int32 };
            SqlParameter pEtNumber = new SqlParameter() { ParameterName = "et_number", Value = EtNumber, DbType = DbType.AnsiString };
            SqlParameter pContractNumber = new SqlParameter() { ParameterName = "contract_number", Value = ContractNumber, DbType = DbType.AnsiString };
            SqlParameter pIdContractor = new SqlParameter() { ParameterName = "id_contractor", Value = IdContractor, DbType = DbType.Int32 };
            SqlParameter pPlanDate = new SqlParameter() { ParameterName = "plan_date", Value = PlanDate, DbType = DbType.DateTime };
            SqlParameter pDeviceModel = new SqlParameter() { ParameterName = "device_model", Value = DeviceModel, DbType = DbType.AnsiString };
            SqlParameter pIdCity = new SqlParameter() { ParameterName = "id_city", Value = IdCity, DbType = DbType.Int32 };
            SqlParameter pAddress = new SqlParameter() { ParameterName = "address", Value = Address, DbType = DbType.AnsiString };
            SqlParameter pObjectName = new SqlParameter() { ParameterName = "object_name", Value = ObjectName, DbType = DbType.AnsiString };
            SqlParameter pContactName = new SqlParameter() { ParameterName = "contact_name", Value = ContactName, DbType = DbType.AnsiString };
            SqlParameter pAddDevices = new SqlParameter() { ParameterName = "add_devices", Value = AddDevices, DbType = DbType.AnsiString };
            SqlParameter pIdManager = new SqlParameter() { ParameterName = "id_manager", Value = IdManager, DbType = DbType.Int32 };
            SqlParameter pIdServiceAdmin = new SqlParameter() { ParameterName = "id_service_admin", Value = IdServiceAdmin, DbType = DbType.Int32 };
            SqlParameter pComment = new SqlParameter() { ParameterName = "descr", Value = Comment, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Devinst.sp, "saveDeviceInstall", pId, pEtNumber, pContractNumber, pIdContractor, pPlanDate, pDeviceModel, pIdCity, pAddress, pObjectName, pContactName, pAddDevices, pIdManager, pIdServiceAdmin, pComment, pIdCreator);


            //if (dt.Rows.Count > 0)
            //{
            //    Id = (int)dt.Rows[0]["id_claim"];
            //}
        }
       
        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device_install", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Devinst.sp, "closeDeviceInstall", pId, pIdCreator);
        }
    }
}