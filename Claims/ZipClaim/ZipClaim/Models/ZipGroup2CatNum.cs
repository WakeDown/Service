using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZipClaim.Objects.Interfaces;

namespace ZipClaim.Models
{
    public class ZipGroup2CatNum : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdZipGroup { get; set; }
        public string CatalogNum { get; set; }
        public int OrderNum { get; set; }
        public int IdCreator { get; set; }

        public ZipGroup2CatNum()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public ZipGroup2CatNum(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group2cat_num", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getZipGroup2CatNum", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_zip_group2cat_num"];
                IdZipGroup = (int)dr["id_zip_group"];
                CatalogNum = dr["catalog_num"].ToString();
                OrderNum = (int)dr["order_num"];
                IdCreator = (int)dr["id_creator"];
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group2cat_num", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdZipGroup = new SqlParameter() { ParameterName = "id_zip_group", Value = IdZipGroup, DbType = DbType.Int32 };
            SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = CatalogNum, DbType = DbType.AnsiString };
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

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveZipGroup2CatNum", pId, pIdZipGroup, pCatalogNum, pOrderNum, pIdCreator);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group2cat_num", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeZipGroup2CatNum", pId);
        }


        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_zip_group2cat_num", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter()
            {
                ParameterName = "id_creator",
                Value = idCreator,
                DbType = DbType.Int32
            };

            ExecuteStoredProcedure(Zipcl.sp, "closeZipGroup2CatNum", pId, pIdCreator);
        }
    }
}