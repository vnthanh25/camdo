using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//----------------------
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;

namespace CamDo
{
    public partial class InputPassword : Form
    {
        public string var_Pass = "";
        public InputPassword(string p_Index)
        {
            InitializeComponent();
            lbl_Index.Text += p_Index;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var_Pass = txt_Pass.Text;
            this.Close();
        }

    }
}
