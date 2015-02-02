using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZipClaim.Objects.Interfaces;

namespace ZipClaim.Models
{
    public class Manager2Operator : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdManager { get; set; }
        public int? IdOperator { get; set; }
        public int? IdCreator { get; set; }

        public Manager2Operator()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Manager2Operator(int id)
        {
            Get(id);

        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_manager2operator", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getManager2Operator", pId);
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_manager2operator", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdManager = new SqlParameter() { ParameterName = "id_manager", Value = IdManager, DbType = DbType.Int32 };
            SqlParameter pIdOperator = new SqlParameter() { ParameterName = "id_operator", Value = IdOperator, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveManager2Operator", pId, pIdManager, pIdOperator, pIdCreator);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, int IdCreator)
        {
            throw new NotImplementedException();
        }

        public void Delete(int idManager, int? idOperator, int idCreator)
        {
            //SqlParameter pId = new SqlParameter() { ParameterName = "id_manager2operator", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdManager = new SqlParameter() { ParameterName = "id_manager", Value = idManager, DbType = DbType.Int32 };
            SqlParameter pIdOperator = new SqlParameter() { ParameterName = "id_operator", Value = idOperator, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "closeManager2Operator", pIdManager, pIdOperator, pIdCreator);
        }
    }
}