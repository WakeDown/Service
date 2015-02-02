using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZipClaim.Objects.Interfaces;

namespace ZipClaim.Models
{
    public class ZipGroup : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNum { get; set; }
        public int IdCreator { get; set; }
        public string Colour { get; set; }

        public ZipGroup()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public ZipGroup(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getZipGroup", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_zip_group"];
                Name = dr["name"].ToString();
                OrderNum = (int)dr["order_num"];
                IdCreator = (int)dr["id_creator"];
                Colour = dr["colour"].ToString();
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group", Value = Id, DbType = DbType.Int32 };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pColour = new SqlParameter() { ParameterName = "colour", Value = Colour, DbType = DbType.AnsiString };
            SqlParameter pOrderNum = new SqlParameter()
            {
                ParameterName = "order_num",
                Value = OrderNum,
                DbType = DbType.Int32
            };
            SqlParameter pIdCreator = new SqlParameter()
            {
                ParameterName = "id_creator",
                Value = IdCreator,
                DbType = DbType.Int32
            };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveZipGroup", pId, pName, pOrderNum, pIdCreator, pColour);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeZipGroup", pId);
        }


        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter()
            {
                ParameterName = "id_creator",
                Value = idCreator,
                DbType = DbType.Int32
            };

            ExecuteStoredProcedure(Zipcl.sp, "closeZipGroup", pId, pIdCreator);
        }
    }
}