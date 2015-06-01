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
    public class ClaimUnit : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public int IdClaim { get; set; }
        public string CatalogNum { get; set; }
        public string Name { get; set; }
        public int? Count { get; set; }
        public string NomenclatureNum { get; set; }
        public decimal? PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public int IdCreator { get; set; }
        public string DeliveryTime { get; set; }
        public string NomenclatureClaimNum { get; set; }
        public int IdSupplyMan { get; set; }
        public bool NoNomenclatureNum { get; set; }

        public int IdClaimUnitInfo { get; set; }
        public string Descr { get; set; }
        

        public ClaimUnit()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public ClaimUnit(int id)
        {
            Get(id);

        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim_unit", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getClaimUnit", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_claim_unit"];
                IdClaim = (int)dr["id_claim"];
                CatalogNum = dr["catalog_num"].ToString();
                Name = dr["name"].ToString();
                Count = (int)dr["count"];
                NomenclatureNum = dr["nomenclature_num"].ToString();
                PriceIn = GetValueDecimalOrNull(dr["price_in"].ToString());
                PriceOut = GetValueDecimalOrNull(dr["price_out"].ToString());
                IdCreator = (int)dr["id_creator"];
                DeliveryTime = dr["delivery_time"].ToString();
                NomenclatureClaimNum = dr["nomenclature_claim_num"].ToString();
                NoNomenclatureNum = GetValueBool(dr["no_nomenclature_num"]);
                //IdSupplyMan = 
            }
        }

        public void Save()
        {
            this.Save(false);
        }

        public void Save(bool fromTop)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim_unit", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdClaim = new SqlParameter() { ParameterName = "id_claim", Value = IdClaim, DbType = DbType.Int32 };
            SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = CatalogNum, DbType = DbType.AnsiString };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pCount = new SqlParameter() { ParameterName = "count", Value = Count, DbType = DbType.Int32 };
            SqlParameter pNomenclatureNum = new SqlParameter() { ParameterName = "nomenclature_num", Value = NomenclatureNum, DbType = DbType.AnsiString };
            SqlParameter pPriceIn = new SqlParameter() { ParameterName = "price_in", Value = PriceIn, DbType = DbType.AnsiString };
            SqlParameter pPriceOut = new SqlParameter() { ParameterName = "price_out", Value = PriceOut, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pDeliveryTime = new SqlParameter() { ParameterName = "delivery_time", Value = DeliveryTime, DbType = DbType.AnsiString };
            SqlParameter pFromTop = new SqlParameter() { ParameterName = "from_top", Value = fromTop, DbType = DbType.Boolean };
            SqlParameter pNomenclatureClaimNum = new SqlParameter() { ParameterName = "nomenclature_claim_num", Value = NomenclatureClaimNum, DbType = DbType.AnsiString };
            SqlParameter pIdSupplyMan = new SqlParameter() { ParameterName = "id_supply_man", Value = IdSupplyMan, DbType = DbType.Int32 };
            SqlParameter pNoNomenclatureNum = new SqlParameter() { ParameterName = "no_nomenclature_num", Value = NoNomenclatureNum, DbType = DbType.Boolean };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveClaimUnit", pId, pIdClaim, pCatalogNum, pName, pCount, pNomenclatureNum, pPriceIn, pPriceOut, pIdCreator, pDeliveryTime, pFromTop, pNomenclatureClaimNum, pIdSupplyMan, pNoNomenclatureNum);


            if (dt.Rows.Count > 0)
            {
                Id = (int)dt.Rows[0]["id_claim_unit"];
            }
        }

        public void SaveInfo()
        {
            SqlParameter pIdClaimUnitInfo = new SqlParameter() { ParameterName = "id_claim_unit_info", Value = IdClaimUnitInfo, DbType = DbType.Int32 };
            SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = CatalogNum, DbType = DbType.AnsiString };
            SqlParameter pDescr = new SqlParameter() { ParameterName = "descr", Value = Descr, DbType = DbType.AnsiString };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            ExecuteQueryStoredProcedure(Zipcl.sp, "saveClaimUnitInfo", pIdClaimUnitInfo, pCatalogNum, pDescr, pIdCreator);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim_unit", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeClaimUnit", pId);
        }

        public void Delete(int id, int idCreator)
        {
            throw new NotSupportedException();
        }

        public void SupplyReturn(string descr)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_claim_unit", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };
            SqlParameter pComment = new SqlParameter() { ParameterName = "descr", Value = descr, DbType = DbType.AnsiString };

            ExecuteQueryStoredProcedure(Zipcl.sp, "setClaimUnitSupplyReturn", pId, pIdCreator, pComment);
        }
    }
}