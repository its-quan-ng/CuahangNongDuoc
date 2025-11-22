using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmTimPhieuChi : Form
    {
        PhieuChiController ctrl = new PhieuChiController();
       LyDoChiController ctrlLDC = new  LyDoChiController();
        public frmTimPhieuChi()
        {
            InitializeComponent();
        }

        private void frmTimPhieuChi_Load(object sender, EventArgs e)
        {
            ctrlLDC.HienthiAutoComboBox(cmbLyDo);

            DateTime now = DateTime.Now;
            dtTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtDenNgay.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}