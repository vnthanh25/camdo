using System;
using System.Collections.Generic;
using System.Text;
//--------------------
using System.Data.SqlClient;
using System.Data;

namespace CamDo.DataProvider
{
    class sqlProvider2
    {
        #region Components
        public string conStr;
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        #endregion Components

        #region Constructors
        public sqlProvider2()
        {
            conStr = global::CamDo.Properties.Settings.Default.CamDoConnectionString;
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
        }
        #endregion Constructors

        #region InitSqlParameters
        SqlParameter[] mtd_InitParasProduct(ProductInfor par_ProductInfor)
        {
            return new SqlParameter[]
                    {
                        new SqlParameter("@ProductFirstDate",par_ProductInfor.ProductFirstDate),
                        new SqlParameter("@ProductId",par_ProductInfor.ProductId),
                        new SqlParameter("@ProductIndex",par_ProductInfor.ProductIndex),
                        new SqlParameter("@ProductFinishDate",par_ProductInfor.ProductFinishDate),
                        new SqlParameter("@ProductLastDate",par_ProductInfor.ProductLastDate),
                        new SqlParameter("@ProductMoney",par_ProductInfor.ProductMoney),
                        new SqlParameter("@ProductName",par_ProductInfor.ProductName),
                        new SqlParameter("@ProductTypeId",par_ProductInfor.ProductTypeId),
                        new SqlParameter("@ProductType",par_ProductInfor.ProductType)
                    };
        }
        SqlParameter[] mtd_InitParasProductType(ProductTypeInfor par_ProductInforType)
        {
            return new SqlParameter[]
                    {
                        new SqlParameter("@ProductType",par_ProductInforType.ProductType),
                        new SqlParameter("@ProductTypeId",par_ProductInforType.ProductTypeId)
                    };
        }
        #endregion InitSqlParameters

        #region Call Product Store Proceduce
        public DataTable tbl_Product_Gets()
        {
            DataTable v_DTbl= new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Gets";
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
            //SqlDataReader var_SqlDataReader = cmd.ExecuteReader();
            //List<ProductInfor> lst_ProductInfor = new List<ProductInfor>();
            //ProductInfor var_ProductInfor;
            //while (var_SqlDataReader.Read())
            //{
            //    var_ProductInfor = new ProductInfor();
            //    var_ProductInfor.ProductId = var_SqlDataReader.GetInt32(0);
            //    var_ProductInfor.ProductIndex = var_SqlDataReader.GetInt32(1);
            //    var_ProductInfor.ProductName = var_SqlDataReader.GetString(2);
            //    var_ProductInfor.ProductTypeId = var_SqlDataReader.GetInt32(3);
            //    var_ProductInfor.ProductType = var_SqlDataReader.GetString(4);
            //    var_ProductInfor.ProductMoney = var_SqlDataReader.GetDecimal(5);
            //    var_ProductInfor.ProductFirstDate = var_SqlDataReader.GetDateTime(6);
            //    var_ProductInfor.ProductLastDate = var_SqlDataReader.GetDateTime(7);
            //    var_ProductInfor.ProductFinishDate = var_SqlDataReader.GetDateTime(8);

            //    lst_ProductInfor.Add(var_ProductInfor);
            //}

            //return lst_ProductInfor;
        }
        public List<ProductInfor> tbl_Product_Get(ProductInfor par_ProductInfor)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Get";
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(mtd_InitParasProduct(par_ProductInfor));
            SqlDataReader var_SqlDataReader = cmd.ExecuteReader();
            List<ProductInfor> lst_ProductInfor = new List<ProductInfor>();
            ProductInfor var_ProductInfor;
            while (var_SqlDataReader.Read())
            {
                var_ProductInfor = new ProductInfor();
                var_ProductInfor.ProductId = var_SqlDataReader.GetInt32(0);
                var_ProductInfor.ProductName = var_SqlDataReader.GetString(1);
                var_ProductInfor.ProductTypeId = var_SqlDataReader.GetInt32(2);
                var_ProductInfor.ProductType = var_SqlDataReader.GetString(3);
                var_ProductInfor.ProductMoney = var_SqlDataReader.GetDecimal(4);
                var_ProductInfor.ProductFirstDate = var_SqlDataReader.GetDateTime(5);
                var_ProductInfor.ProductLastDate = var_SqlDataReader.GetDateTime(6);

                lst_ProductInfor.Add(var_ProductInfor);
            }

            return lst_ProductInfor;
        }
        public int tbl_Product_Ins(ProductInfor par_ProductInfor)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Ins";
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(mtd_InitParasProduct(par_ProductInfor));
            return cmd.ExecuteNonQuery();
        }
        public int tbl_Product_Upd(ProductInfor par_ProductInfor)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Upd";
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(mtd_InitParasProduct(par_ProductInfor));
            return cmd.ExecuteNonQuery();
        }
        public int tbl_Product_Del(ProductInfor par_ProductInfor)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Del";
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(mtd_InitParasProduct(par_ProductInfor));
            return cmd.ExecuteNonQuery();
        }
        #endregion Methods

    }
}
