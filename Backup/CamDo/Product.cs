using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//----------------
using CamDo.DataProvider;
using System.Data.SqlClient;
using System.IO;

namespace CamDo
{

    public partial class frm_Product : Form
    {
        sqlProvider obj_sqlProvider = new sqlProvider();
        TableCost obj_TableCost;
        TableInterest obj_TableInterest;
        TextBox var_PreviousControl = new TextBox();
        VNTSecurity.VNTCrypt.Crypt1 objCrypt1 = new VNTSecurity.VNTCrypt.Crypt1();
        VNTSecurity.System.SystemInfor objSystemInfor = new VNTSecurity.System.SystemInfor();
        string varBoardMakerSerialNumber = "";
        int var_MaxId = 9;
        int var_MaxIndex = 3;
        int var_Index = 1;
        int var_Id = 1;
        int var_PageSize = 2;
        // Page Print 
        int var_Height = 300, var_Width = 300, var_X = 20, var_Y = 20, var_RowUp = 20, var_RowDown = 20, var_FontSize = 10;
        string var_FontName = "Arial", var_Password = "", var_Content = "", var_PrintName;

        public frm_Product()
        {
            InitializeComponent();
        }

        void mtd_llbClick(int par_PageNum)
        {
            for (int i = 0; i < dgv_Product.Rows.Count; i++)
            {
                if (i >= (par_PageNum - 1) * var_PageSize && i < par_PageNum * var_PageSize)
                {
                    dgv_Product.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Product.CurrentCell = null;
                    dgv_Product.Rows[i].Visible = false;
                }
            }
        }

        void mtd_llbClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel v_llb = (LinkLabel)sender;
            int v_PageNum = int.Parse(v_llb.Text);
            mtd_llbClick(v_PageNum);
        }

        void mtd_InitPagNum()
        {
            if (dgv_Product.Rows.Count > 0)
            {
                LinkLabel v_llb;
                int i = 0, v_X = 150, v_Y =5;
                for (int v_PageNum = 0; v_PageNum < dgv_Product.Rows.Count; v_PageNum += var_PageSize)
                {
                    v_llb = new LinkLabel();
                    i++;
                    v_llb.Text = i.ToString();
                    v_llb.Font = new Font("Arial", 14);
                    if (v_X >= panel1.Width - 100)
                    {
                        v_X = 150;
                        v_Y += 30;
                        panel1.Height += 30;
                    }
                    v_llb.Location = new Point(v_X, v_Y);
                    v_X += 40;
                    v_llb.Width = 50;
                    v_llb.TextAlign = ContentAlignment.MiddleCenter;
                    v_llb.LinkClicked += new LinkLabelLinkClickedEventHandler(mtd_llbClick);
                    panel1.Controls.Add(v_llb);
                }
            }
        }

        void mtd_SetDataSource(DataTable par_DTbl)
        {
            dgv_Product.DataSource = mtd_SetOrdinal(par_DTbl);
            if (dgv_Product.CurrentRow != null)
                dgv_Product.CurrentRow.Selected = false;
            lbl_Note.Visible = true;
            if(par_DTbl.Rows.Count<1)
                lbl_Note.Visible = false;

            lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ.";
            panel1.Controls.Clear();
            panel1.Height = 30;
            panel1.Controls.Add(lbl_Pages);
            //mtd_InitPagNum();
            //mtd_llbClick(1);
        }

        private void frm_Product_Load(object sender, EventArgs e)
        {
            var_PageSize = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["pageSize"]);
            obj_sqlProvider.con.Open();
            //var_MaxIndex = var_sqlProvider.tbl_NextIndex_GetMaxIndex();
            //Product
            mtd_BindDataGrid();
            //ProductStatus
            mtd_BindCbx_Status();
            //mtd_InitPagNum();
            //Reset controls
            mtd_ResetControl();
            obj_sqlProvider.con.Close();
            //txt_Index.Focus();
            //txt_Index.Select(0, txt_Index.Text.Length);
        }

        void mtd_BindDataGrid()
        {
            //DataTable dt = var_sqlProvider.tbl_Product_Gets();
            mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
            //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
            //dgv_Product.CurrentRow.Selected = false;
            lbl_Count.Text = "Tổng cộng : " + obj_sqlProvider.tbl_Product_GetCountSetIn().ToString() + " đồ cầm.";
            //Columns visible
            dgv_Product.Columns["ProductIndex"].HeaderText = lbl_Index.Text;
            dgv_Product.Columns["ProductName"].HeaderText = lbl_Name.Text;
            dgv_Product.Columns["ProductGold"].HeaderText = lbl_Gold.Text;
            dgv_Product.Columns["ProductMobile"].HeaderText = lbl_Mobile.Text;
            dgv_Product.Columns["ProductFirstMoney"].HeaderText = lbl_Money.Text;
            dgv_Product.Columns["ProductFirstDate"].HeaderText = lbl_FirstDate.Text;
            //Columns not visible
            dgv_Product.Columns["ProductId"].Visible = false;
            dgv_Product.Columns["ProductAmount"].Visible = false;
            dgv_Product.Columns["ProductStatusId"].Visible = false;
            dgv_Product.Columns["ProductLastMoney"].Visible = false;
            dgv_Product.Columns["ProductFinishDate"].Visible = false;
            dgv_Product.Columns["ProductLastDate"].Visible = false;
            dgv_Product.Columns["ProductStatus"].Visible = false;
            //Set width
            //dgv_Product.Columns["ProductIndex"].Width = txt_Index.Width + 5;
            //dgv_Product.Columns["ProductName"].Width = txt_Name.Width + 5;
            //dgv_Product.Columns["ProductFirstMoney"].Width = txt_Money.Width + 5;
            //dgv_Product.Columns["ProductFirstDate"].Width = dtp_FirstDate.Width + 8;
        }

        DataTable mtd_SetOrdinal(DataTable par_dt)
        {
            par_dt.Columns["ProductIndex"].SetOrdinal(0);
            par_dt.Columns["ProductFirstDate"].SetOrdinal(1);
            par_dt.Columns["ProductName"].SetOrdinal(2);
            par_dt.Columns["ProductGold"].SetOrdinal(3);
            par_dt.Columns["ProductMobile"].SetOrdinal(4);
            par_dt.Columns["ProductAmount"].SetOrdinal(5);
            par_dt.Columns["ProductFirstMoney"].SetOrdinal(6);
            par_dt.Columns["ProductLastDate"].SetOrdinal(7);
            par_dt.Columns["ProductStatus"].SetOrdinal(8);
            return par_dt;
        }

        void mtd_ResetControl()
        {
            //var_Index = var_sqlProvider.tbl_NextIndex_GetIndex(var_MaxIndex);
            txt_Index.Text = obj_sqlProvider.tbl_NextIndex_GetIndex().ToString();
            DataTable v_DTbl = obj_sqlProvider.tbl_Product_SelectIndex(txt_Index.Text);
            if (v_DTbl.Rows.Count > 0)
            {
                lbl_Note.Visible = true;
                mtd_SetControlValues(v_DTbl, false);
                //mtd_SetDataSource(var_sqlProvider.tbl_Product_SelectIndex(txt_Index.Text));
            }
            else
            {
                lbl_Note.Visible = false;
                txt_Name.Text = "";
                txt_Gold.Text = "";
                txt_Mobile.Text = "";
                txt_Amount.Text = "1";
                txt_Money.Text = "";
                cbx_Status.SelectedIndex = 0;
                dtp_FirstDate.Value = DateTime.Now;
                dtp_LastDate.Value = DateTime.Now;
            }
            txt_Index.Focus();
            txt_Index.SelectAll();
        }

        void mtd_BindCbx_Status()
        {
            cbx_Status.DataSource = obj_sqlProvider.tbl_ProductStatus_Gets();
            cbx_Status.DisplayMember = "ProductStatus";
            cbx_Status.ValueMember = "ProductStatusId";
        }

        bool mtd_CheckNumber(string par_Num)
        {
            try
            {
                if (Decimal.Parse(par_Num) < 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Return lbl.text coresponse error txt.
        List<string> mtd_GetMessageNumError()
        {
            var_MaxIndex = obj_sqlProvider.tbl_NextIndex_GetMaxIndex();
            List<string> v_LstMessage = new List<string>();
            if (!mtd_CheckNumber(txt_Index.Text.Trim()) || int.Parse(txt_Index.Text.Trim()) > var_MaxIndex)
                v_LstMessage.Add(lbl_Index.Text);
            if (!mtd_CheckNumber(txt_Amount.Text.Trim()))
                v_LstMessage.Add(lbl_Amount.Text);
            if (!mtd_CheckNumber(txt_Money.Text.Trim().Replace(".","")))
                v_LstMessage.Add(lbl_Money.Text);
            //if (v_LstMessage.Count > 0)
            //{
            //    v_Message = v_LstMessage[0];
            //    for (int i = 1; i < v_LstMessage.Count; i++)
            //        v_Message += ", " + v_LstMessage[i];
            //}
            return v_LstMessage;
        }

        List<string> mtd_GetMessageBlankError()
        {
           List<string> v_LstMessage = new List<string>();
           if (txt_Name.Text.Trim() == "")
               v_LstMessage.Add(lbl_Name.Text);
           return v_LstMessage;
        }

        string mtd_CheckLogicError()
        {
            // Get error messages for number and blank.
            List<string> v_LstMessage = mtd_GetMessageNumError();
            v_LstMessage.AddRange(mtd_GetMessageBlankError());
            //
            string v_Message = "";
            if (v_LstMessage.Count > 0)
            {
                v_Message = v_LstMessage[0];
                for (int i = 1; i < v_LstMessage.Count; i++)
                    v_Message += ", " + v_LstMessage[i];
            }
            return v_Message;
        }

        /// <summary>
        /// * Return false if have change.
        /// </summary>
        /// <param name="par_Row"></param>
        /// <returns></returns>
        bool mtd_CheckSelectRow(DataRow par_Row)
        {
            if (txt_Index.Text.Trim() != par_Row["ProductIndex"].ToString())
                return false;
            if (txt_Gold.Text.Trim() != par_Row["ProductGold"].ToString())
                return false;
            if (txt_Mobile.Text.Trim() != par_Row["ProductMobile"].ToString())
                return false;
            if (txt_Money.Text.Trim().Replace(".", "") != par_Row["ProductFirstMoney"].ToString())
                return false;
            if (txt_Name.Text.Trim() != par_Row["ProductName"].ToString())
                return false;
            if (par_Row["ProductFirstDate"].ToString() != dtp_FirstDate.Text)
                return false;
            if (par_Row["ProductLastDate"].ToString() != dtp_LastDate.Text 
                && dtp_LastDate.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
                return false;
            if (par_Row["ProductStatusId"].ToString() != cbx_Status.SelectedValue.ToString())
                return false;
            return true;
        }

        void mtd_InsertUdateProduct(int par_LastMoney)
        {
            string v_Message = mtd_CheckLogicError();
            // Show error message.
            if (v_Message != "")
            {
                MessageBox.Show(v_Message + " : không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txt_Money.Focus();
                //txt_Money.Select(0, txt_Money.Text.Length);
                return;
            }
            // Insert or Update or Cancel.
            DataTable v_DTbl = obj_sqlProvider.tbl_Product_Get(txt_Index.Text.Trim(), var_Id.ToString());
            int v_count = v_DTbl.Rows.Count;
            DialogResult v_DialogResult;
            if (v_count == 0)
            {
                v_DialogResult = MessageBox.Show("Thêm mới", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (v_DialogResult == DialogResult.Cancel)
                    return;
                mtd_InsertProduct(par_LastMoney);
            }
            else if (v_count == 1)
            {
                var_Password = System.Configuration.ConfigurationSettings.AppSettings["del"];

                InputPassword obj_InputPass = new InputPassword(var_Index.ToString());
                obj_InputPass.ShowDialog();
                DialogResult v_DiaR;
                if (obj_InputPass.var_Pass == var_Password)
                {
                    v_DialogResult = MessageBox.Show(
                        "******************\r\n"
                        + "Cập nhật thông tin\r\n"
                        + "******************"
                        , "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (v_DialogResult == DialogResult.Cancel)
                        return;
                    var_Id = int.Parse(v_DTbl.Rows[0]["ProductId"].ToString().Trim());
                    mtd_UdateProduct(par_LastMoney);
                }
                else
                    MessageBox.Show("Mật mã không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int mtd_InsertProduct(int par_LastMoney)
        {
            var_MaxIndex = obj_sqlProvider.tbl_NextIndex_GetMaxIndex();
            int v_count = -1;
            v_count = obj_sqlProvider.tbl_Product_Ins(mtd_GetControlValues(par_LastMoney), var_MaxId, var_MaxIndex);
            mtd_BindDataGrid();
            dgv_Product.Rows[0].Selected = false;
            //Reset Control
            mtd_ResetControl();
            return v_count;
        }

        int mtd_UdateProduct(long par_LastMoney)
        {
            int v_count = -1;
            v_count = obj_sqlProvider.tbl_Product_Upd(mtd_GetControlValues(par_LastMoney));
            //mtd_BindDataGrid();
            //dgv_Product.Rows[0].Selected = false;
            mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
            //Reset Control
            mtd_ResetControl();
            return v_count;
        }

        DataTable mtd_GetControlValues(long par_LastMoney)
        {
            // Create new row, add into table. After update database from this table.
            DataTable dTbl = (DataTable)dgv_Product.DataSource;
            dTbl = dTbl.Copy();
            //dTbl.AcceptChanges();

            DataRow row = dTbl.NewRow();
            row["ProductId"] = var_Id;
            row["ProductIndex"] = txt_Index.Text.Trim();
            row["ProductName"] = txt_Name.Text.Trim();
            row["ProductGold"] = txt_Gold.Text.Trim();
            row["ProductMobile"] = txt_Mobile.Text.Trim();
            row["ProductAmount"] = txt_Amount.Text.Trim();
            row["ProductFirstMoney"] = txt_Money.Text.Trim().Replace(".", "");
            row["ProductLastMoney"] = par_LastMoney;
            row["ProductFirstDate"] = dtp_FirstDate.Value.ToShortDateString();
            row["ProductLastDate"] = dtp_LastDate.Value.ToShortDateString();
            row["ProductFinishDate"] = DateTime.Now;
            row["ProductStatusId"] = cbx_Status.SelectedValue;

            dTbl.Rows.Add(row);
            return dTbl.GetChanges();
        }

        void mtd_SetControlValues(DataTable par_DTbl, bool par_IsChange)
        {
            DataRow row = par_DTbl.Rows[0];
            var_Id = int.Parse(row["ProductId"].ToString());
            var_Index = int.Parse(row["ProductIndex"].ToString());
            txt_Index.Text =  row["ProductIndex"].ToString() ;
            txt_Name.Text = row["ProductName"].ToString();
            txt_Gold.Text = row["ProductGold"].ToString();
            txt_Mobile.Text = row["ProductMobile"].ToString();
            txt_Amount.Text = row["ProductAmount"].ToString();
            txt_Money.Text = row["ProductFirstMoney"].ToString();
            txt_Money.Text = mtd_InsertDot(txt_Money.Text);
            dtp_FirstDate.Value = DateTime.Parse(mtd_ChangDateFormat(row["ProductFirstDate"].ToString()));
            dtp_LastDate.Value = DateTime.Parse(mtd_ChangDateFormat(row["ProductLastDate"].ToString()));
            if (row["ProductStatusId"].ToString() == "1" && par_IsChange)
                dtp_LastDate.Value = DateTime.Now;
            cbx_Status.SelectedValue = row["ProductStatusId"].ToString();
        }

        void mtd_SetControlEnable(bool v_Enable)
        {
            toolStrip1.Enabled = v_Enable;
            btn_Filter.Enabled = v_Enable;
            btn_ProcessInterest.Enabled = v_Enable;
            btn_Reset.Enabled = v_Enable;
            btn_Update.Enabled = v_Enable;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string vBMSN = global::CamDo.Properties.Settings.Default.vBMSN;
            try
            {
                obj_sqlProvider.con.Open();
                if (objCrypt1.Encrypt_Active(objSystemInfor.GetBoardMakerSerialNumber()) != vBMSN)
                {
                    //obj_sqlProvider.tbl_Product_DelAll();
                    return;
                }

                mtd_InsertUdateProduct(0);
                txt_Index.Focus();
                txt_Index.SelectAll();
            }
            catch (Exception ex)
            {
            }
            obj_sqlProvider.con.Close();
        }

        private void dgv_Product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var_Id = int.Parse(dgv_Product.Rows[e.RowIndex].Cells["ProductId"].Value.ToString());
                var_Index = int.Parse(dgv_Product.Rows[e.RowIndex].Cells["ProductIndex"].Value.ToString());
                // Chang date format for FirstDate.
                string st_FristDate = mtd_ChangDateFormat(dgv_Product.Rows[e.RowIndex].Cells["ProductFirstDate"].Value.ToString());
                // Chang date format for LastDate.
                string st_LastDate = mtd_ChangDateFormat(dgv_Product.Rows[e.RowIndex].Cells["ProductLastDate"].Value.ToString());

                txt_Index.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductIndex"].Value.ToString();
                txt_Name.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                txt_Gold.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductGold"].Value.ToString();
                txt_Mobile.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductMobile"].Value.ToString();
                //txt_Amount.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductAmount"].Value.ToString();
                txt_Money.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductFirstMoney"].Value.ToString();
                txt_Money.Text = mtd_InsertDot(txt_Money.Text);
                dtp_FirstDate.Value = DateTime.Parse(st_FristDate);
                dtp_LastDate.Value = DateTime.Parse(st_LastDate);
                if (dgv_Product.Rows[e.RowIndex].Cells["ProductStatusId"].Value.ToString() == "1")
                    dtp_LastDate.Value = DateTime.Now;
                cbx_Status.Text = dgv_Product.Rows[e.RowIndex].Cells["ProductStatus"].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_ResetControl();
                obj_sqlProvider.con.Close();
            }
            catch (Exception ex)
            { }
        }

        string mtd_ChangDateFormat(string par_Date)
        {
            string[] strs = par_Date.Split('/');
            string stDate = "";
            if (strs.Length == 3)
            {
                stDate = strs[1] + "/" + strs[0] + "/" + strs[2];
            }
            return stDate;
        }

        void mtd_ProcessInterest()
        {
            #region Check an show message.
            int rowIndex = dgv_Product.SelectedRows.Count;
            long money = 0, moneyWeek = 0, moneyMonth = 0, moneyYear = 0;
            int numWeek = 0, numMonth = 0, numYear = 0;
            DialogResult obj_DialogResult;
            // Select ProductIndex after set values for controls.
            DataTable v_DTbl = obj_sqlProvider.tbl_Product_Get(var_Index.ToString(), var_Id.ToString());
            int v_Count = v_DTbl.Rows.Count;
            if (v_Count > 0)
            {
                if (!mtd_CheckSelectRow(v_DTbl.Rows[0]))
                {
                    obj_DialogResult = MessageBox.Show(
                    "****************\r\n"
                    + "Cập nhật thông tin\r\n"
                    + "****************"
                        , "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (obj_DialogResult == DialogResult.OK)
                    {
                        string v_Message = mtd_CheckLogicError();
                        // Show error message.
                        if (v_Message != "")
                        {
                            MessageBox.Show(v_Message + " : không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        obj_sqlProvider.tbl_Product_Upd(mtd_GetControlValues(0));
                        int v_Index = var_Index, v_Id = var_MaxId;
                        mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                        var_Index = v_Index; var_MaxId = v_Id;
                        //mtd_BindDataGrid();
                    }
                    v_DTbl = obj_sqlProvider.tbl_Product_Get(var_Index.ToString(), var_Id.ToString());
                    mtd_SetControlValues(v_DTbl, false);
                }
                //mtd_SetControlValues(v_DTbl);
            }
            else // Product index not exist. Check number.
            {
                string v_Message = mtd_CheckLogicError();
                if (v_Message != "")
                {
                    MessageBox.Show(v_Message + " : không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion Check an show message. end
            // Account number of week and month.
            #region Process number of days.
            DateTime dt_FirstDate = dtp_FirstDate.Value;
            DateTime dt_LastDate = dtp_LastDate.Value;
            numYear = dt_LastDate.Year - dt_FirstDate.Year;
            numMonth = dt_LastDate.Month - dt_FirstDate.Month;
            // Account month
            if (dt_LastDate.Month < dt_FirstDate.Month)
            {
                numYear--;
                numMonth += 12;
            }
            numMonth += numYear * 12;
            dt_FirstDate = dt_FirstDate.AddMonths(numMonth);
            if (dt_LastDate.Day < dt_FirstDate.Day)
            {
                dt_FirstDate = dt_FirstDate.AddMonths(-1);
                numMonth--;
            }
            // Account day
            TimeSpan ts = dt_LastDate.Subtract(dt_FirstDate);
            numWeek = ts.Days / 7 + 1;
            //if (numMonth==0 && ts.Days > 0)
            //     + 1;
            bool v_MinMoney = false;
            if (numWeek > 3 || (numWeek == 3 && int.Parse(txt_Money.Text.Trim().Replace(".", "")) <= 100000))
            {
                numMonth++;
                numWeek = 0;
                v_MinMoney = true;
            }
            if (dtp_FirstDate.Value.Day == dtp_LastDate.Value.Day && numMonth > 0)
                numWeek = 0;
            #endregion Process number of days. end
            money = long.Parse(txt_Money.Text.Trim().Replace(".", ""));
            // Get Interest values.
            DataTable vTbl_Interest = obj_sqlProvider.tbl_TableInterest_GetByMoney(money.ToString());
            // Type : Phone or Watch
            #region Phone or Watch
            if (txt_Mobile.Text.Trim() != "")
            {
                long colMobileWeek = long.Parse(vTbl_Interest.Rows[0]["colMobileWeek"].ToString());
                long colMobileMonth = long.Parse(vTbl_Interest.Rows[0]["colMobileMonth"].ToString());
                if (money > 1000000)
                {
                    moneyWeek = ((money * colMobileWeek) / 100000) * numWeek;
                    moneyMonth = ((money * colMobileMonth) / 100000) * numMonth;
                }
                else
                {
                    moneyWeek = colMobileWeek * numWeek;
                    moneyMonth = colMobileMonth * numMonth;
                }
                //else if (money <= 50000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 100000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 200000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 300000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 400000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 500000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 600000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 700000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money <= 800000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
                //else if (money < 1000000)
                //{
                //    moneyWeek = colMobileWeek * numWeek;
                //    moneyMonth = colMobileMonth * numMonth;
                //}
            }
            #endregion Phone or Watch
            // Type : Gold or Diamon
            #region Gold or Diamon
            if (txt_Gold.Text.Trim() != "")
            {
                long colGoldWeek = long.Parse(vTbl_Interest.Rows[0]["colGoldWeek"].ToString());
                long colGoldMonth = long.Parse(vTbl_Interest.Rows[0]["colGoldMonth"].ToString());
                if (money > 1000000)
                {
                    moneyWeek = ((money * colGoldWeek) / 100000) * numWeek;
                    moneyMonth = ((money * colGoldMonth) / 100000) * numMonth;
                }
                else
                {
                    moneyWeek = colGoldWeek * numWeek;
                    moneyMonth = colGoldMonth * numMonth;
                }

                //else if (money <= 50000)
                //{
                //    moneyWeek = 2000 * numWeek;
                //    moneyMonth = 4000 * numMonth;
                //}
                //else if (money <= 100000)
                //{
                //    moneyWeek = 2000 * numWeek;
                //    moneyMonth = 4000 * numMonth;
                //}
                //else if (money <= 200000)
                //{
                //    moneyWeek = 2000 * numWeek;
                //    moneyMonth = 8000 * numMonth;
                //}
                //else if (money <= 300000)
                //{
                //    moneyWeek = 3000 * numWeek;
                //    moneyMonth = 12000 * numMonth;
                //}
                //else if (money <= 400000)
                //{
                //    moneyWeek = 4000 * numWeek;
                //    moneyMonth = 16000 * numMonth;
                //}
                //else if (money <= 500000)
                //{
                //    moneyWeek = 5000 * numWeek;
                //    moneyMonth = 20000 * numMonth;
                //}
                //else if (money <= 600000)
                //{
                //    moneyWeek = 6000 * numWeek;
                //    moneyMonth = 24000 * numMonth;
                //}
                //else if (money <= 700000)
                //{
                //    moneyWeek = 7000 * numWeek;
                //    moneyMonth = 28000 * numMonth;
                //}
                //else if (money <= 800000)
                //{
                //    moneyWeek = 8000 * numWeek;
                //    moneyMonth = 30000 * numMonth;
                //}
                //else if (money < 1000000)
                //{
                //    moneyWeek = 9000 * numWeek;
                //    moneyMonth = 30000 * numMonth;
                //}
            }
            #endregion Gold or Diamon
            // Account remainder
            money = moneyMonth + moneyWeek;
            if (v_MinMoney)
            {
                numMonth--;
                numWeek = 3;
            }
            // Account total money
            long moneyTotal = long.Parse(txt_Money.Text.Trim().Replace(".", "")) + money;
            long v_MoneyOld = moneyTotal;
            moneyTotal = (moneyTotal / 1000) * 1000;
            long v_Odd = (v_MoneyOld - moneyTotal);
            if (v_Odd > 0)
                moneyTotal += 1000;
            // Create message.
            #region Update and show message next print.
            MessageBoxIcon v_MessageBoxIcon = MessageBoxIcon.Question;
            string v_Warning = "*** Có lấy không ? ***";
            if (v_Count < 1)
            {
                v_MessageBoxIcon = MessageBoxIcon.Error;
                v_Warning = "";
            }
            string v_Type = lbl_Gold.Text + " : " + txt_Gold.Text;
            string v_TypePrint = lbl_Gold.Text;
            if (txt_Gold.Text.Trim() == "")
            {
                v_Type = lbl_Mobile.Text + " : " + txt_Mobile.Text;
                v_TypePrint = lbl_Mobile.Text;
            }

            string v_Content1 =
                "DNTN DICH VU CAM DO KIM THUY" + "\r\n" +
                "\r\n" +
                "- Chi so : " + txt_Index.Text + "\r\n" +
                "- Ten : " + txt_Name.Text + "\r\n" +
                "- Ngay cam : " + dtp_FirstDate.Text + "\r\n" +
                "- Ngay chuoc : " + dtp_LastDate.Text + "\r\n"
                ;
            string v_Content2 = "- So thang cam : " + numMonth.ToString() + "\r\n" +
                "- So tuan cam : " + numWeek.ToString() + "\r\n" +
                "----------------------------" + "\r\n" +
                "- So tien cam : " + txt_Money.Text.Trim() + "\r\n" +
                "----------------------------" + "\r\n" +
                "- So tien lai la : " + mtd_InsertDot(money.ToString()) + "\r\n" +
                "----------------------------" + "\r\n" +
                "* Tong cong la : " + mtd_InsertDot(moneyTotal.ToString());
            ;

            var_Content = v_Content1 + "- Loai : " + v_TypePrint + "\r\n" + "\r\n" + v_Content2 + "\r\n";
            System.Configuration.ConfigurationSettings.AppSettings["Content"] = var_Content;

            obj_DialogResult = MessageBox.Show(
                 v_Content1 +
                 "- " + v_Type + "\r\n" +
                 "\r\n" +
                 v_Content2 +
                 "\r\n" +
                 v_Warning,
                 "Thông báo"
                 , MessageBoxButtons.OKCancel, v_MessageBoxIcon);
            if (obj_DialogResult == DialogResult.OK)
            {
                if (v_Count > 0)
                {
                    var_Content = System.Configuration.ConfigurationSettings.AppSettings["Content"];
                    cbx_Status.SelectedValue = 2;
                    var_Id = int.Parse(v_DTbl.Rows[0]["ProductId"].ToString().Trim());
                    var_PrintName = System.Configuration.ConfigurationSettings.AppSettings["PrintName"];
                    var_RowUp = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["RowUp"]);
                    var_RowDown = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["RowDown"]);
                    mtd_UdateProduct(moneyTotal);
                    //RawPrinterHelper1.SendStringToPrinter(var_PrintName, v_Content1);
                    //RawPrinterHelper1.SendStringToPrinter(var_PrintName, v_Content2);
                    if(MessageBox.Show("Có muốn in không?", "Cảnh báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)== DialogResult.OK)
                        RawPrinterHelper1.SendStringToPrinter(var_PrintName, mtd_GetEndLines(var_RowUp) + var_Content + mtd_GetEndLines(var_RowDown));
                    //if (!RawPrinterHelper1.SendStringToPrinter(var_PrintName, mtd_GetEndLines(2) + var_Content))
                    //{
                    //    obj_DialogResult = MessageBox.Show("Không thể in được. Có chấp nhận lấy không?", "Lỗi máy in", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    //}
                    //if (obj_DialogResult == DialogResult.OK)
                    //{
                    //    mtd_UdateProduct(moneyTotal);
                    //}
                }
            }
            #endregion Update and show message next print. end

            mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
            mtd_ResetControl();
        }

        string mtd_GetEndLines(int p_Count)
        {
            string result = "";
            for (int i = 0; i < p_Count; i++)
            {
                result += "\r\n";
            }
            return result;
        }

        string mtd_InsertDot(string par_number)
        {
            int v_Length = par_number.Length;
            int v_PosDot = v_Length / 3;
            if (v_PosDot * 3 == v_Length)
                v_PosDot--;
            for (int i = 0; i < v_PosDot; i++)
                par_number = par_number.Insert(v_Length - (3 + i * 3), ".");
            return par_number;
        }

        #region ToolStripMenu
        /// <summary>
        /// * Tìm chỉ số của những món đồ đang cầm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tìmChỉSốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                if (mtd_CheckNumber(toolStripTextBox1.Text.Trim()))
                {
                    if (toolStripTextBox1.Text.Trim() == "")
                        mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                    //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                    else
                        mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectIndex(toolStripTextBox1.Text));
                    //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectIndex(toolStripTextBox1.Text));
                    //dgv_Product.CurrentRow.Selected = false;
                    //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
                }
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }
        private void tênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectName(toolStripTextBox1.Text));
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectName(toolStripTextBox1.Text));
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void lọaiĐồToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                if (toolStripTextBox1.Text.Trim() == "")
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                else
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectType(toolStripTextBox1.Text));
                //    dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectType(toolStripTextBox1.Text));
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void ngàyCầmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.Parse(mtd_ChangDateFormat(toolStripTextBox1.Text));
                //dt = DateTime.Parse(toolStripTextBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giá trị tìm kiến không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                obj_sqlProvider.con.Open();
                if (toolStripTextBox1.Text.Trim() == "")
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                else
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectFirstDate(dt.ToShortDateString()));
                //    dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectFirstDate(dt.ToShortDateString()));
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void ngàyChuộcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.Parse(mtd_ChangDateFormat(toolStripTextBox1.Text));
                //dt = DateTime.Parse(toolStripTextBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giá trị tìm kiến không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                obj_sqlProvider.con.Open();
                if (toolStripTextBox1.Text.Trim() == "")
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                else
                    mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectLastDate(dt.ToShortDateString()));
                //    dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectLastDate(dt.ToShortDateString()));
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void sốTiềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mtd_CheckNumber(toolStripTextBox1.Text.Trim()))
                {
                    obj_sqlProvider.con.Open();
                    if (toolStripTextBox1.Text.Trim() == "")
                        mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                    //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                    else
                        mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectMoney(toolStripTextBox1.Text));
                    //    dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectMoney(toolStripTextBox1.Text));
                    //dgv_Product.CurrentRow.Selected = false;
                    //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
                }
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void quáHạnChuộcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectTimeOuts());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectTimeOuts());
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void đãThanhLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectFinishs());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectFinishs());
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }
        #endregion ToolStripMenu

        private void btn_ProcessInterest_Click(object sender, EventArgs e)
        {
            if (cbx_Status.SelectedValue.ToString() == "1")
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    obj_sqlProvider.con.Open();
                    mtd_ProcessInterest();
                }
                catch (Exception ex)
                {
                }
                obj_sqlProvider.con.Close();
                this.Cursor = Cursors.Default;
            }
        }

        private void tiềnTồnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                string v_ExistMoney = obj_sqlProvider.tbl_Product_SelectExistMoney();
                MessageBox.Show("* Số tiền tồn (đang cầm) là : " + mtd_InsertDot(v_ExistMoney));

                //var_sqlProvider.con.Open();
                //int v_FirstMoney, v_LastMoney, v_InterestMoney;
                //v_FirstMoney = var_sqlProvider.tbl_Product_SelectFirstMoney();
                //v_LastMoney = var_sqlProvider.tbl_Product_SelectLastMoney();
                //v_InterestMoney = var_sqlProvider.tbl_Product_SelectInteresMoney();
                //var_sqlProvider.con.Close();
                //MessageBox.Show(
                //        "- Số tiền cầm : " + v_FirstMoney + "\r\n" +
                //        "--------------------------------" + "\r\n" +
                //        "- Số tiền lãi là : " + v_InterestMoney + "\r\n" +
                //        "--------------------------------" + "\r\n" +
                //        "* Số tiền chuộc là : " + v_LastMoney + "\r\n" ,
                //        "Thông báo"
                //        , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
            obj_sqlProvider.con.Close();
        }

        private void tòanBộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                //mtd_SetDataSource(var_sqlProvider.tbl_Product_GetAll());
                panel1.Height = 30;
                panel1.Controls.Clear();
                dgv_Product.DataSource = mtd_SetOrdinal(obj_sqlProvider.tbl_Product_GetAll());
                dgv_Product.CurrentRow.Selected = false;
                lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        void mtd_EnterType(object sender, EventArgs e)
        {
            TextBox v_TextBox = (TextBox)sender;
            v_TextBox.SelectAll();
            // txt Gold
            if (v_TextBox == txt_Gold && txt_Mobile.Text.Trim()!="")
            {
                txt_Mobile.Focus();
                txt_Mobile.SelectAll();
            }
            // txt Mobile
            if (v_TextBox == txt_Mobile && txt_Gold.Text.Trim() != "")
            {
                txt_Gold.Focus();
                txt_Gold.SelectAll();
            }
        }

        void mtd_EnterFocus(object sender, EventArgs e)
        {
            TextBox v_TextBox = (TextBox)sender;
            //if (var_PreviousControl == v_TextBox)
            //{
            //    //v_TextBox.Refresh();
            //    //var_PreviousControl.Focus();
            //    //v_TextBox.SelectionStart = 0;
            //    v_TextBox.SelectionLength = 0;
            //}
            //else
            {
                //var_PreviousControl = v_TextBox;
                v_TextBox.SelectAll();
            }
        }

        void mtd_ReleaseFocus(object sender, EventArgs e)
        {
            TextBox v_Txt = (TextBox)sender;
            if (v_Txt.Text == "")
            {
                v_Txt.Focus();
                MessageBox.Show("Giá trị ở vùng này không được trắng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void mtd_AutoAccountInterest()
        {
            DataTable v_DTbl = obj_sqlProvider.tbl_Product_Get(toolStripTextBox1.Text.Trim(), "-1");
            if (v_DTbl.Rows.Count > 0)
            {
                mtd_SetDataSource(v_DTbl);
                //dgv_Product.DataSource = v_DTbl;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
                mtd_SetControlValues(v_DTbl, true);
                //dtp_LastDate.Value = DateTime.Now;
                mtd_ProcessInterest();
            }
            else
            {
                MessageBox.Show("Chỉ số không tìm thấy", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            toolStripTextBox1.Focus();
            toolStripTextBox1.SelectAll();
        }

        void mtd_KeyEnter(object sender, KeyPressEventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                if (e.KeyChar == Convert.ToChar(13))
                {
                    if (sender is ToolStripTextBox)
                    {
                        mtd_AutoAccountInterest();
                    }
                    else
                    {
                        Control v_Control = (Control)sender;
                        if (v_Control == txt_Gold && txt_Gold.Text != "")
                            txt_Money.Focus();
                        else if (v_Control == txt_Mobile && txt_Mobile.Text == "")
                            txt_Gold.Focus();
                        else
                            this.GetNextControl(v_Control, true).Focus();
                    }
                }
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void txt_Money_KeyUp(object sender, KeyEventArgs e)
        {
            int v_Key = e.KeyValue;
            if (v_Key == 8 || v_Key == 46 || (v_Key >=35 && v_Key <=40) || 
                (v_Key >= 48 && v_Key <= 57) || (v_Key >= 96 && v_Key <= 105))
            {
                int v_Start = txt_Money.SelectionStart;
                int v_Num1 = txt_Money.Text.Trim().Split('.').Length - 1;
                txt_Money.Text = mtd_InsertDot(txt_Money.Text.Trim().Replace(".", ""));
                int v_Num2 = txt_Money.Text.Trim().Split('.').Length - 1;
                v_Start += v_Num2-v_Num1;
                if(v_Start>-1)
                    txt_Money.SelectionStart = v_Start;
            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.SelectAll();
        }

        private void llb_All_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_Gets());
                mtd_SetControlEnable(true);
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_Gets());
                //dgv_Product.CurrentRow.Selected = false;
                lbl_Count.Text = "Tổng cộng : " + obj_sqlProvider.tbl_Product_GetCountSetIn().ToString() + " đồ cầm.";
                txt_Index.Focus();
                txt_Index.SelectAll();
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void btn_Filter_Click(object sender, EventArgs e)
        {
            CamDo.Filter v_Filter = new Filter();
            v_Filter.MdiParent = this.MdiParent;
            v_Filter.ShowDialog();
            if (v_Filter.var_DTbl != null)
            {
                mtd_SetDataSource(v_Filter.var_DTbl);
                //dgv_Product.DataSource = v_Filter.var_DTbl;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
        }

        private void đãChuộcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectGetOuts());
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectFinishs());
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        } 

        private void txt_Index_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_Index_Leave(object sender, EventArgs e)
        {
            try
            {
                lbl_Note.Visible = false;
                //var_sqlProvider.con.Open();
                if (mtd_CheckNumber(txt_Index.Text.Trim()))
                {
                    DataTable v_DTbl = obj_sqlProvider.tbl_Product_SelectIndex(txt_Index.Text);
                    if (v_DTbl.Rows.Count > 0)
                    {
                        lbl_Note.Visible = true;
                        mtd_SetControlValues(v_DTbl, true);
                        //mtd_SetDataSource(var_sqlProvider.tbl_Product_SelectIndex(txt_Index.Text));
                    }
                }
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void toolStripXóa_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                if (obj_sqlProvider.tbl_Product_GetId(txt_Index.Text.Trim(), var_Id.ToString()).Rows.Count < 1)
                {
                    MessageBox.Show("Không thể xóa chỉ số " + txt_Index.Text, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var_Password = System.Configuration.ConfigurationSettings.AppSettings["del"];

                    InputPassword obj_InputPass = new InputPassword(var_Index.ToString());
                    obj_InputPass.ShowDialog();
                    DialogResult v_DiaR;
                    if (obj_InputPass.var_Pass == var_Password)
                    {
                        v_DiaR = MessageBox.Show("Chỉ số [" + txt_Index.Text + "] sẽ bị xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (v_DiaR == DialogResult.OK)
                        {
                            obj_sqlProvider.tbl_Product_UpdOlder(var_Id.ToString(), txt_Index.Text.Trim(), true);
                            mtd_BindDataGrid();
                            //mtd_ResetControl();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật mã không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        private void đãXóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider.con.Open();
                mtd_SetDataSource(obj_sqlProvider.tbl_Product_SelectOlders());
                mtd_SetControlEnable(false);
                //dgv_Product.DataSource = mtd_SetOrdinal(var_sqlProvider.tbl_Product_SelectFinishs());
                //dgv_Product.CurrentRow.Selected = false;
                //lbl_Count.Text = "Tổng cộng : " + dgv_Product.Rows.Count.ToString() + " đồ cầm.";
            }
            catch (Exception ex) { }
            obj_sqlProvider.con.Close();
        }

        void mtd_Print()
        {
                var_Width = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["Width"]);
                var_Height = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["Hieght"]);
                var_X = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["X"]);
                var_Y = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["Y"]);
                var_FontName = System.Configuration.ConfigurationSettings.AppSettings["FontName"];
                var_FontSize = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["FontSize"]);
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("size1", var_Width, var_Height);
                try
                {
                    printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể in.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        IAsyncResult obj_IAsyncResult;
        delegate void DoPrint();
        void mtd_PrintASyn()
        {
            printDocument1.Print();
        }

        //[DllImport("user32.dll")]
        //public static extern bool PrintWindow(IntPtr Ptr1, IntPtr Ptr2, uint Sayi);

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //var_Content = "579340579834759769797324958734\r\neworuoweuroioweurouweoruoweurouweoruowe\r\n";
            Font v_Font = new Font(var_FontName, var_FontSize);
            e.Graphics.DrawString(var_Content, v_Font, Brushes.Black,
               var_X, var_Y, new StringFormat());
            //while (true)
            //{
            //    //printDocument1.PrinterSettings.
            //}
        }

        private void printDocument1_QueryPageSettings(object sender, System.Drawing.Printing.QueryPageSettingsEventArgs e)
        {
            e.PageSettings.PaperSize = new System.Drawing.Printing.PaperSize("size1", var_Width, var_Height);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (obj_TableInterest == null || obj_TableInterest.IsDisposed)
                obj_TableInterest = new TableInterest();
            obj_TableInterest.Show();
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (obj_TableCost == null || obj_TableCost.IsDisposed)
                obj_TableCost = new TableCost();
            obj_TableCost.Show();
        }

    }
}