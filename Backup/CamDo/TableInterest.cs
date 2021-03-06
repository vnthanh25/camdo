using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//--------------------
using CamDo.DataProvider;

namespace CamDo
{
    public partial class TableInterest : Form
    {
        sqlProvider obj_sqlProvider;
        List<string> var_LstId = new List<string>();

        public TableInterest()
        {
            InitializeComponent();
        }

        private void TableInterest_Load(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider = new sqlProvider();
                mtd_BindDataGrid();
            }
            catch (Exception ex)
            {
            }
            obj_sqlProvider = null;
        }

        void mtd_BindDataGrid()
        {
            dataGridView1.DataSource = obj_sqlProvider.tbl_TableInterest_Gets();
            dataGridView1.Columns["colMoney"].HeaderText = "Số tiền";
            dataGridView1.Columns["colGoldWeek"].HeaderText = "Vàng/KC trên tuần";
            dataGridView1.Columns["colGoldMonth"].HeaderText = "Vàng/KC trên tháng";
            dataGridView1.Columns["colMobileWeek"].HeaderText = "ĐTDĐ/ĐH trên tuần";
            dataGridView1.Columns["colMobileMonth"].HeaderText = "ĐTDĐ/ĐH trên tháng";
        }
        #region can delete
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    RawPrinterHelper1.SendStringToPrinter(pd.PrinterSettings.PrinterName, "sdlkgjlk sdjgljll sdajfal\r\njfsl\r\n");
                    //RawPrinterHelper1.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                    //ofd.Reset();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            System.IntPtr lhPrinter = new System.IntPtr();
            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            string st1;
            // text to print with a form feed character
            st1 = "This is an example of printing directly to a printer\f";
            di.pDocName = "my test document";
            di.pDataType = "RAW";
            // the \x1b means an ascii escape character
            st1 = "\x1b*c600a6b0P\f";
            //lhPrinter contains the handle for the printer opened
            //If lhPrinter is 0 then an error has occured
            PrintDirect.OpenPrinter("EPSON LQ-300+ ESC/P 2", ref lhPrinter, 0);
            PrintDirect.StartDocPrinter(lhPrinter, 1, ref di);
            PrintDirect.StartPagePrinter(lhPrinter);
            try
            {
                // Moves the cursor 900 dots (3 inches at 300 dpi) in from the left margin, and
                // 600 dots (2 inches at 300 dpi) down from the top margin.
                st1 = "\x1b*p900x600Y";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Using the print model commands for rectangle dimensions, "600a" specifies a rectangle
                    // with a horizontal size or width of 600 dots, and "6b" specifies a vertical
                    // size or height of 6 dots. The 0P selects the solid black rectangular area fill.
                st1 = "\x1b*c600a6b0P";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Specifies a rectangle with width of 6 dots, height of 600 dots, and a
                // fill pattern of solid black.
                st1 = "\x1b*c6a600b0P";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Moves the current cursor position to 900 dots, from the left margin and
                // 1200 dots down from the top margin.
                st1 = "\x1b*p900x1200Y";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Specifies a rectangle with a width of 606 dots, a height of 6 dots and a
                // fill pattern of solid black.
                st1 = "\x1b*c606a6b0P";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Moves the current cursor position to 1500 dots from the left margin and
                // 600 dots down from the top margin.
                st1 = "\x1b*p1500x600Y";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
                // Specifies a rectangle with a width of 6 dots, a height of 600 dots and a
                // fill pattern of solid black.
                st1 = "\x1b*c6a600b0P";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten); // Send a form feed character to the printer
                st1 = "\f";
                PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            PrintDirect.EndPagePrinter(lhPrinter);
            PrintDirect.EndDocPrinter(lhPrinter);
            PrintDirect.ClosePrinter(lhPrinter);
        }
        #endregion can delete end

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider = new sqlProvider();
                obj_sqlProvider.con.Open();
                // Insert or update row.
                DataTable v_DTbl = (DataTable)dataGridView1.DataSource;
                DataTable v_DTblChange = v_DTbl.GetChanges();
                v_DTbl.AcceptChanges();
                obj_sqlProvider.tbl_TableInterest_InsUpdDel(v_DTblChange);
            }
            catch (Exception ex)
            {
            }
            //dataGridView1.DataSource = obj_sqlProvider.tbl_TableInterest_Gets();
            mtd_BindDataGrid();
            obj_sqlProvider = null;
            //obj_sqlProvider.con.Close();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var_LstId.Add(e.Row.Cells["colId"].Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDirect1 v_PrintDirect1 = new PrintDirect1();
                v_PrintDirect1.Print("\\\\172.16.1.230\\HPLaserJ", "My C#.NET RAW Document", "referencePiece", "designationPiece", "code");
            }
            catch (Exception ex)
            {
            }
        }

        void mtd_KeyEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                btn_Search_Click(btn_Search, new EventArgs());
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                obj_sqlProvider = new sqlProvider();
                string v_ColName = txt_ColName.Text;
                if (v_ColName.Trim() == "")
                {
                    v_ColName = "%%";
                }
                else if (!cbx_SearchExact.Checked)
                {
                    v_ColName = "%" + v_ColName + "%";
                }
                dataGridView1.DataSource = obj_sqlProvider.tbl_TableInterest_GetByMoney("50000");
            }
            catch (Exception ex)
            {
            }
            obj_sqlProvider = null;
        }

        private void txt_ColName_Enter(object sender, EventArgs e)
        {
            txt_ColName.SelectAll();
            txt_ColName.Focus();
        }
    }
}