using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ZipClaim.Objects.Interfaces;

namespace ZipClaim.Models
{
    public class Device : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int IdModel { get; set; }
        public string SerialNum { get; set; }
        public string InvNum { get; set; }
        public int? Counter { get; set; }
        public int? CounterColour { get; set; }
        public int? Age { get; set; }
        public DateTime? InstalationDate { get; set; }
        public int? IdCreator { get; set; }

        //public string City { get; set; }
        //public string Address { get; set; }
        //public string ObjectName { get; set; }


        public Device()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Device(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getDevice", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device"];
                Model = dr["model"].ToString();
                IdModel = (int)dr["id_device_model"];
                SerialNum = dr["serial_num"].ToString();
                InvNum = dr["inv_num"].ToString();
                Counter = GetValueIntOrNull(dr["counter"].ToString());
                CounterColour = GetValueIntOrNull(dr["counter_colour"].ToString());
                Age = GetValueIntOrNull(dr["age"].ToString());
                InstalationDate = GetValueDateTimeOrNull(dr["instalation_date"].ToString());
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());

                //City = dr["city"].ToString();
                //Address = dr["address"].ToString();
                //ObjectName = dr["object_name"].ToString();
            }
        }








        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}