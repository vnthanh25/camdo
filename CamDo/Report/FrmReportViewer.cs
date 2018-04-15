using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CamDo.Report
{
    public partial class FrmReportViewer : Form
    {
        CamDo.DataProvider.sqlProvider obj_sqlProvider = new CamDo.DataProvider.sqlProvider();
        public FrmReportViewer()
        {
            InitializeComponent();
        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
        }

        public void Mtd_SetMessage(string pMess)
        {
            CamDo.Report.crpInterest ocrpInterest = new CamDo.Report.crpInterest();
            ocrpInterest.SetParameterValue("PhieuTinhLai", pMess);
            crystalReportViewer1.ReportSource = ocrpInterest;
        }
    }
}
