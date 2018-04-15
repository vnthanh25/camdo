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

namespace CamDo
{
    public partial class InterestView : Form
    {
        sqlProvider var_sqlProvider = new sqlProvider();
        public DataTable var_DTbl;

        public InterestView()
        {
            InitializeComponent();
        }

        List<string> mtd_GetControlValues()
        {
            List<string> v_LstPara = new List<string>();

            v_LstPara.Add(txt_IndexFirst.Text.Trim());
            v_LstPara.Add(txt_IndexLast.Text.Trim());
            v_LstPara.Add(dtp_FirstDateFirst.Value.ToShortDateString());
            v_LstPara.Add(dtp_FirstDateLast.Value.ToShortDateString());
            v_LstPara.Add(txt_Name.Text.Trim());
            v_LstPara.Add(txt_Gold.Text.Trim());
            v_LstPara.Add(txt_Mobile.Text.Trim());
            v_LstPara.Add(txt_MoneyFirst.Text.Trim());
            v_LstPara.Add(txt_MoneyLast.Text.Trim());
            v_LstPara.Add(dtp_LastDateFirst.Value.ToShortDateString());
            v_LstPara.Add(dtp_LastDateLast.Value.ToShortDateString());
            v_LstPara.Add(cbb_Status.SelectedValue.ToString());

            if (v_LstPara[0] == "")//IndexFirst
                v_LstPara[0] = "0";
            if (v_LstPara[1] == "")//IndexLast
                v_LstPara[1] = "1000000000"; //1.000.000.000
            if (v_LstPara[4] == "")//Name
                v_LstPara[4] = "%%";
            if (v_LstPara[5] == "")//Gold
                v_LstPara[5] = "%%";
            if (v_LstPara[6] == "")//Mobile
                v_LstPara[6] = "%%";
            if (v_LstPara[7] == "")//MoneyFirst
                v_LstPara[7] = "0";
            if (v_LstPara[8] == "")//MoneyLast
                v_LstPara[8] = "10000000000"; //10.000.000.000
            return v_LstPara;
        }

        void mtd_BindCbx_Status()
        {
            cbb_Status.DataSource = var_sqlProvider.tbl_ProductStatus_Gets();
            cbb_Status.DisplayMember = "ProductStatus";
            cbb_Status.ValueMember = "ProductStatusId";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var_DTbl = var_sqlProvider.tbl_Product_SelectFilter(mtd_GetControlValues());
            this.Close();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            dtp_FirstDateFirst.Value = DateTime.Now.AddYears(-1);
            dtp_FirstDateLast.Value = DateTime.Now.AddYears(+1);
            dtp_LastDateFirst.Value = DateTime.Now.AddYears(-1);
            dtp_LastDateLast.Value = DateTime.Now.AddYears(+1);
            mtd_BindCbx_Status();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}