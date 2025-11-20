using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmTimPhieuNhap : Form
    {
        PhieuNhapController ctrl = new PhieuNhapController();
        NhaCungCapController ctrlNCC = new NhaCungCapController();

        public frmTimPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmTimPhieuNhap_Load(object sender, EventArgs e)
        {
            ctrlNCC.HienthiAutoComboBox(cmbNCC);

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