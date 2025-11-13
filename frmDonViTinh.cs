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
    public partial class frmDonViTinh : Form
    {
        DonViTinhController ctrl = new DonViTinhController();
        public frmDonViTinh()
        {
            InitializeComponent();
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigator.BindingSource.EndEdit();
            
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}