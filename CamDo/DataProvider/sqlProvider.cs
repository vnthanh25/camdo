using System;
using System.Collections.Generic;
using System.Text;
//--------------------
using System.Data.SqlClient;
using System.Data;

namespace CamDo.DataProvider
{
    class sqlProvider
    {
        #region Components
        public string conStr;
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        /// <summary>
        /// * 1 SetIn: Cầm; 2 GetOut: Chuộc; 3 TimeOut: Hết hạn; 4 Finish: Đã thanh lý.
        /// </summary>
        public enum enu_ProductStatus:int 
        { 
            SetIn = 1, 
            GetOut = 2, 
            TimeOut =3, 
            Finish = 4 
        };
        /// <summary>
        /// * Phone = 1, Watch = 2, Gold = 3, Diamon = 4
        /// </summary>
        public enum enu_ProductType : int
        {
            Phone = 1,
            Watch = 2,
            Gold = 3,
            Diamon = 4
        };
        #endregion Components

        #region Constructors
        public sqlProvider()
        {
            conStr = global::CamDo.Properties.Settings.Default.CamDoConnectionString;
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            da = new SqlDataAdapter(cmd);
        }
        #endregion Constructors

        #region InitSqlParameters
        void mtd_InitParasProduct(SqlCommand par_SqlCommand)
        {
            par_SqlCommand.Parameters.Clear();
            par_SqlCommand.Parameters.AddRange(new SqlParameter[]{
                        new SqlParameter("@ProductId", SqlDbType.Int),
                        new SqlParameter("@ProductIndex", SqlDbType.Int),
                        new SqlParameter("@ProductName", SqlDbType.NVarChar),
                        new SqlParameter("@ProductGold", SqlDbType.NVarChar),
                        new SqlParameter("@ProductMobile", SqlDbType.NVarChar),
                        new SqlParameter("@ProductAmount", SqlDbType.Int),
                        new SqlParameter("@ProductFirstMoney", SqlDbType.Decimal),
                        new SqlParameter("@ProductLastMoney", SqlDbType.Decimal),
                        new SqlParameter("@ProductFirstDate", SqlDbType.DateTime),
                        new SqlParameter("@ProductLastDate", SqlDbType.DateTime),
                        new SqlParameter("@ProductFinishDate", SqlDbType.DateTime),
                        new SqlParameter("@ProductStatusId", SqlDbType.Int),
                        new SqlParameter("@MaxId", SqlDbType.Int),
                        new SqlParameter("@MaxIndex", SqlDbType.Int)}
                    );
        }
        SqlParameter[] mtd_InitParasProductType(ProductTypeInfor par_ProductInforType)
        {
            return new SqlParameter[]
                    {
                        new SqlParameter("@ProductType",par_ProductInforType.ProductType),
                        new SqlParameter("@ProductTypeId",par_ProductInforType.ProductTypeId)
                    };
        }
        SqlParameter[] mtd_InitParasTableInterest()
        {
            return new SqlParameter[] 
                    {
                        new SqlParameter("@colMoney","colMoney"),
                        new SqlParameter("@colGoldWeek","colGoldWeek"),
                        new SqlParameter("@colGoldMonth","colGoldMonth"),
                        new SqlParameter("@colMobileWeek","colMobileWeek"),
                        new SqlParameter("@colMobileMonth","colMobileMonth")
                    };
        }
        SqlParameter[] mtd_InitParasTableCost()
        {
            return new SqlParameter[] 
                    {
                        new SqlParameter("@colId","colId"),
                        new SqlParameter("@colName","colName"),
                        new SqlParameter("@colDateTime","colDateTime"),
                        new SqlParameter("@colVendor","colVendor"),
                        new SqlParameter("@colCost","colCost"),
                        new SqlParameter("@colFunctions","colFunctions"),
                        new SqlParameter("@colNote","colNote")
                    };
        }

        int mtd_ExecProduct(SqlCommand par_cmd, DataTable par_DTbl, int par_MaxId, int par_MaxIndex)
        {
           int count = 0;
            //try
            {
                for (int i = 0; i < par_DTbl.Rows.Count; i++)
                {
                    par_cmd.Parameters["@ProductId"].Value = int.Parse(par_DTbl.Rows[i]["ProductId"].ToString());
                    par_cmd.Parameters["@ProductIndex"].Value = int.Parse(par_DTbl.Rows[i]["ProductIndex"].ToString());
                    par_cmd.Parameters["@ProductName"].Value = par_DTbl.Rows[i]["ProductName"].ToString();
                    par_cmd.Parameters["@ProductGold"].Value = par_DTbl.Rows[i]["ProductGold"].ToString();
                    par_cmd.Parameters["@ProductMobile"].Value = par_DTbl.Rows[i]["ProductMobile"].ToString();
                    par_cmd.Parameters["@ProductAmount"].Value = int.Parse(par_DTbl.Rows[i]["ProductAmount"].ToString());
                    par_cmd.Parameters["@ProductFirstMoney"].Value = Decimal.Parse(par_DTbl.Rows[i]["ProductFirstMoney"].ToString());
                    par_cmd.Parameters["@ProductLastMoney"].Value = Decimal.Parse(par_DTbl.Rows[i]["ProductLastMoney"].ToString());
                    par_cmd.Parameters["@ProductFirstDate"].Value = DateTime.Parse(par_DTbl.Rows[i]["ProductFirstDate"].ToString());
                    par_cmd.Parameters["@ProductLastDate"].Value = DateTime.Parse(par_DTbl.Rows[i]["ProductLastDate"].ToString());
                    par_cmd.Parameters["@ProductFinishDate"].Value = DateTime.Parse(par_DTbl.Rows[i]["ProductFinishDate"].ToString());
                    par_cmd.Parameters["@ProductStatusId"].Value = int.Parse(par_DTbl.Rows[i]["ProductStatusId"].ToString());
                    par_cmd.Parameters["@MaxId"].Value = par_MaxId;
                    par_cmd.Parameters["@MaxIndex"].Value = par_MaxIndex;
                    count += cmd.ExecuteNonQuery();
                }
            }
            return count;
        }
        #endregion InitSqlParameters

        #region Call Product Store Proceduce 
        #region Get Product
        public DataTable tbl_Product_Gets()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Gets";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;

            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_GetAll()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetAll";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;

            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectFilter(List<string> par_LstPara)
        {
            DataTable v_DTbl= new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectFilter";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductIndexFirst", par_LstPara[0]));
            cmd.Parameters.Add(new SqlParameter("@ProductIndexLast", par_LstPara[1]));
            cmd.Parameters.Add(new SqlParameter("@ProductFirstDateFirst", par_LstPara[2]));
            cmd.Parameters.Add(new SqlParameter("@ProductFirstDateLast", par_LstPara[3]));
            cmd.Parameters.Add(new SqlParameter("@ProductName", par_LstPara[4]));
            cmd.Parameters.Add(new SqlParameter("@ProductGold", par_LstPara[5]));
            cmd.Parameters.Add(new SqlParameter("@ProductMobile", par_LstPara[6]));
            cmd.Parameters.Add(new SqlParameter("@ProductFirstMoneyFirst", par_LstPara[7]));
            cmd.Parameters.Add(new SqlParameter("@ProductFirstMoneyLast", par_LstPara[8]));
            cmd.Parameters.Add(new SqlParameter("@ProductLastDateFirst", par_LstPara[9]));
            cmd.Parameters.Add(new SqlParameter("@ProductLastDateLast", par_LstPara[10]));
            cmd.Parameters.Add(new SqlParameter("@ProductStatusId", par_LstPara[11]));
            da.SelectCommand = cmd;

            da.Fill(v_DTbl);
            return v_DTbl;
        }
       
        public DataTable tbl_Product_GetId(string par_ProductIndex, string par_ProductId)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetId";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductIndex", par_ProductIndex));
            cmd.Parameters.Add(new SqlParameter("@ProductId", par_ProductId));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_GetStatusId(string par_ProductIndex, string par_ProductStatusId)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetStatusId";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductIndex", par_ProductIndex));
            cmd.Parameters.Add(new SqlParameter("@ProductStatusId", par_ProductStatusId));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_Get(string par_ProductIndex, string par_ProductId)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Get";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductIndex", par_ProductIndex));
            cmd.Parameters.Add(new SqlParameter("@ProductId", par_ProductId));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        
        public int tbl_Product_GetCount()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetCount";
            return int.Parse(cmd.ExecuteScalar().ToString());
        }

        public int tbl_Product_GetCountSetIn()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetCountSetIn";
            return int.Parse(cmd.ExecuteScalar().ToString());
        }

        public int tbl_Product_GetIndexBottom()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_GetIndexBottom";
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_NextIndex_GetId()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_NextIndex_GetId";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_NextIndex_GetIndex()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_NextIndex_GetIndex";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_NextIndex_GetMaxIndex()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_NextIndex_GetMaxIndex";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        #endregion Get Product

        #region Select Search Product
        public string tbl_Product_SelectExistMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectExistMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return cmd.ExecuteScalar().ToString();
        }
        public int tbl_Product_SelectFirstMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectFirstMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_Product_SelectLastMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectLastMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_Product_SelectInteresMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectInteresMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_Product_SelectAllFirstMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectAllFirstMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
        public int tbl_Product_SelectAllLastMoney()
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectAllLastMoney";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@MaxIndex", par_MaxIndex));
            return int.Parse(cmd.ExecuteScalar().ToString());
        }


        public DataTable tbl_Product_SelectFinishDate(string par_FinishDate)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectFinishDate";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductFinishDate", par_FinishDate));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectFirstDate(string par_ProductFirstDate)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectFirstDate";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductFirstDate", par_ProductFirstDate));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectIndex(string par_ProductIndex)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectIndex";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductIndex", par_ProductIndex));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectLastDate(string par_ProductLastDate)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectLastDate";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductLastDate", par_ProductLastDate));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectMoney(string par_ProductFirstMoney)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectMoney";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductFirstMoney", par_ProductFirstMoney));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectName(string par_ProductName)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectName";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductName", par_ProductName));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectStatus(string par_ProductStatus)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectStatus";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductStatus", par_ProductStatus));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectType(string par_ProductType)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectType";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductType", par_ProductType));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        #endregion Select Search Product

        #region Select View Product
        public DataTable tbl_Product_SelectTimeOuts()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectTimeOuts";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        public DataTable tbl_Product_SelectFinishs()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectFinishs";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectGetOuts()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectGetOuts";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_Product_SelectOlders()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_SelectOlders";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        #endregion Select View Product

        #region Orther Product
        public int tbl_Product_Ins_Upd(DataTable par_DTbl, int par_MaxId, int par_MaxIndex)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Ins_Upd";
            mtd_InitParasProduct(cmd);
            return mtd_ExecProduct(cmd, par_DTbl, par_MaxId, par_MaxIndex);
        }

        public int tbl_Product_Ins(DataTable par_DTbl, int par_MaxId, int par_MaxIndex)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Ins";
            mtd_InitParasProduct(cmd);
            return mtd_ExecProduct(cmd, par_DTbl, par_MaxId, par_MaxIndex);
        }

        public int tbl_Product_Upd(DataTable par_DTbl)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Upd";
            mtd_InitParasProduct(cmd);
            return mtd_ExecProduct(cmd, par_DTbl, 0, 0);
        }

        public int tbl_Product_UpdOlder(string par_ProductId, string par_ProductIndex, bool par_ProductOlder)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_UpdOlder";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = int.Parse(par_ProductId);
            cmd.Parameters.Add("@ProductIndex", SqlDbType.Int).Value = int.Parse(par_ProductIndex);
            cmd.Parameters.Add("@ProductOlder", SqlDbType.Bit).Value = par_ProductOlder;
            return cmd.ExecuteNonQuery();
        }

        public int tbl_Product_Del(string par_ProductId)
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_Del";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = int.Parse(par_ProductId);
            return cmd.ExecuteNonQuery();
        }

        public int tbl_Product_DelAll() 
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_Product_DelAll";
            cmd.Parameters.Clear();
            return cmd.ExecuteNonQuery();
        }
        #endregion Orther Product

        #endregion Methods

        #region Call TableCost Store Procedure
        public DataTable tbl_TableCost_Gets()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_TableCost_Gets";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public DataTable tbl_TableCost_SearchColNames(string p_ColName)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_TableCost_SearchColNames";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@colName", p_ColName));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public int tbl_TableCost_Del(List<string> p_LstId)
        {
            int result = 0;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@colId", ""));
            cmd.CommandText = "tbl_TableCost_Del";
            for (result = 0; result < p_LstId.Count; result++)
            {
                cmd.Parameters["@colId"].Value = p_LstId[result];
                cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int tbl_TableCost_Ins(DataTable p_DTbl)
        {
            int result = 0;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_TableCost_Ins";
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(mtd_InitParasTableCost());

            for (result = 0; result < p_DTbl.Rows.Count; result++)
            {
                cmd.Parameters["@colId"].Value = p_DTbl.Rows[result]["colId"];
                cmd.Parameters["@colName"].Value = p_DTbl.Rows[result]["colName"];
                cmd.Parameters["@colDateTime"].Value = p_DTbl.Rows[result]["colDateTime"];
                cmd.Parameters["@colVendor"].Value = p_DTbl.Rows[result]["colVendor"];
                cmd.Parameters["@colCost"].Value = p_DTbl.Rows[result]["colCost"];
                cmd.Parameters["@colFunctions"].Value = p_DTbl.Rows[result]["colFunctions"];
                cmd.Parameters["@colNote"].Value = p_DTbl.Rows[result]["colNote"];
                cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int tbl_TableCost_InsUpdDel(DataTable p_DTbl)
        {
            int result = 0;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            // Init command parameters.
            cmd.Parameters.AddRange(mtd_InitParasTableCost());
            for (result = 0; result < p_DTbl.Rows.Count; result++)
            {
                if (p_DTbl.Rows[result].RowState != DataRowState.Deleted)
                {
                    // Set values for parameters. 
                    cmd.Parameters["@colName"].Value = p_DTbl.Rows[result]["colName"];
                    cmd.Parameters["@colDateTime"].Value = p_DTbl.Rows[result]["colDateTime"];
                    cmd.Parameters["@colVendor"].Value = p_DTbl.Rows[result]["colVendor"];
                    cmd.Parameters["@colCost"].Value = p_DTbl.Rows[result]["colCost"];
                    cmd.Parameters["@colFunctions"].Value = p_DTbl.Rows[result]["colFunctions"];
                    cmd.Parameters["@colNote"].Value = p_DTbl.Rows[result]["colNote"];
                    if (p_DTbl.Rows[result].RowState == DataRowState.Added)
                    {
                        cmd.Parameters["@colId"].Value = 0;
                        cmd.CommandText = "tbl_TableCost_Ins";
                    }
                    else if (p_DTbl.Rows[result].RowState == DataRowState.Modified)
                    {
                        cmd.Parameters["@colId"].Value = p_DTbl.Rows[result]["colId"];
                        cmd.CommandText = "tbl_TableCost_Upd";
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        #endregion Call TableCost Store Procedure

        #region Call TableInterest Store Procedure
        public DataTable tbl_TableInterest_Gets()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_TableInterest_Gets";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        public DataTable tbl_TableInterest_GetByMoney(string parMoney)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_TableInterest_GetByMoney";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@colMoney", parMoney);
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }

        public int tbl_TableInterest_InsUpdDel(DataTable p_DTbl)
        {
            int result = 0;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            // Init command parameters.
            cmd.Parameters.AddRange(mtd_InitParasTableInterest());
            for (result = 0; result < p_DTbl.Rows.Count; result++)
            {
                if (p_DTbl.Rows[result].RowState != DataRowState.Deleted)
                {
                    // Set values for parameters. 
                    cmd.Parameters["@colMoney"].Value = p_DTbl.Rows[result]["colMoney"];
                    cmd.Parameters["@colGoldWeek"].Value = p_DTbl.Rows[result]["colGoldWeek"];
                    cmd.Parameters["@colGoldMonth"].Value = p_DTbl.Rows[result]["colGoldMonth"];
                    cmd.Parameters["@colMobileWeek"].Value = p_DTbl.Rows[result]["colMobileWeek"];
                    cmd.Parameters["@colMobileMonth"].Value = p_DTbl.Rows[result]["colMobileMonth"];
                    if (p_DTbl.Rows[result].RowState == DataRowState.Added)
                    {
                        cmd.CommandText = "tbl_TableInterest_Ins";
                    }
                    else if (p_DTbl.Rows[result].RowState == DataRowState.Modified)
                    {
                        cmd.CommandText = "tbl_TableInterest_Upd";
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            return result;
        }
        #endregion Call TableCost Store Procedure

        #region Call ProductTypeType Store Proceduce
        public DataTable tbl_ProductType_Gets()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_ProductType_Gets";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        public DataTable tbl_ProductType_Get(string par_ProductTypeId)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_ProductType_Get";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductTypeId", par_ProductTypeId));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        #endregion Methods

        #region Call ProductStatus Store Proceduce
        public DataTable tbl_ProductStatus_Gets()
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_ProductStatus_Gets";
            cmd.Parameters.Clear();
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        public DataTable tbl_ProductStatus_Get(string par_ProductStatusId)
        {
            DataTable v_DTbl = new DataTable();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "tbl_ProductStatus_Get";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@ProductStatusId", par_ProductStatusId));
            da.SelectCommand = cmd;
            da.Fill(v_DTbl);
            return v_DTbl;
        }
        #endregion Methods
    }
}
