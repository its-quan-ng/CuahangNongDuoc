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
            if (dataGridView.CurrentCell != null)
            {
                dataGridView.EndEdit();
            }
            bindingNavigator.Focus();

            bindingNavigator.BindingSource.EndEdit();

            DataTable dt = ctrl.GetDataTable();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // Check row mới hoặc modified
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    string tenDV = row["TEN_DON_VI"]?.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(tenDV))
                    {
                        MessageBox.Show(
                            "Vui lòng nhập Tên đơn vị!",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return; 
                    }
                }
            }

            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh để verify
                ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
            }
            else
            {
                MessageBox.Show("Không có thay đổi để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}