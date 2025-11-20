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
    public partial class frmSanPham : Form
    {
        SanPhamController ctrl = new SanPhamController();
        DonViTinhController ctrlDVT = new DonViTinhController();

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            ctrlDVT.HienthiAutoComboBox(cmbDVT);
            dataGridView.Columns.Add(ctrlDVT.HienthiDataGridViewComboBoxColumn());
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                 txtMaSanPham, txtTenSanPham, cmbDVT, numSoLuong, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
        }


        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            
            bindingNavigator.BindingSource.EndEdit();

            // Kiểm tra dữ liệu rỗng trước khi lưu
            DataTable dt = ctrl.GetDataTable();
            bool hasError = false;
            string errorMessage = "";

            foreach (DataRow row in dt.Rows)
            {
                // Kiểm tra tên sản phẩm không được rỗng
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if (row["TEN_SAN_PHAM"] == DBNull.Value ||
                        string.IsNullOrWhiteSpace(row["TEN_SAN_PHAM"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Tên sản phẩm không được để trống!";
                        break;
                    }

                    // Kiểm tra đơn vị tính phải được chọn
                    if (row["ID_DON_VI_TINH"] == DBNull.Value ||
                        row["ID_DON_VI_TINH"].ToString() == "0" ||
                        string.IsNullOrWhiteSpace(row["ID_DON_VI_TINH"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Đơn vị tính không được để trống!";
                        break;
                    }
                }
            }

            if (hasError)
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            DataRow row = ctrl.NewRow();
            long maso = ThamSo.SanPham;
            ThamSo.SanPham = maso+1;
            row["ID"] = maso;
            row["TEN_SAN_PHAM"] = "";
            row["SO_LUONG"] = 0;
            row["DON_GIA_NHAP"] = 0;
            row["GIA_BAN_SI"] = 0;
            row["GIA_BAN_LE"] = 0;
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();
            
        }

      
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "San Pham", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            
        }

        private void btnThemDVT_Click(object sender, EventArgs e)
        {
            frmDonViTinh DVT = new frmDonViTinh();
            DVT.ShowDialog();
            ctrlDVT.HienthiAutoComboBox(cmbDVT);
        }


        private void toolTimMaSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = true;
            toolTimTenSanPham.Checked = false;
            toolTimSanPham.Text = "";

        }

        private void mnuTimTenSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = false;
            toolTimTenSanPham.Checked = true;
            toolTimSanPham.Text = "";
        }

        private void toolTimSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimSanPham();
            }
        }

        private void toolTimSanPham_Leave(object sender, EventArgs e)
        {
            TimSanPham();
        }

        void TimSanPham()
        {
            if (toolTimMaSanPham.Checked == true)
            {
                if (!string.IsNullOrEmpty(toolTimSanPham.Text))
                {
                    ctrl.TimMaSanPham(Convert.ToInt32(toolTimSanPham.Text));
                    if (bindingNavigator.BindingSource != null)
                    {
                        bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(toolTimSanPham.Text))
                {
                    ctrl.TimTenSanPham(toolTimSanPham.Text);
                    if (bindingNavigator.BindingSource != null)
                    {
                        bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                    }
                }
            }
        }

        private void toolTimSanPham_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
        }
      


    }
}