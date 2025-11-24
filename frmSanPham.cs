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
            // End any active editing in the data grid view
            dataGridView.EndEdit();
            bindingNavigator.BindingSource.EndEdit();

            // Get the current data table
            DataTable dt = ctrl.GetDataTable();
            bool hasError = false;
            string errorMessage = "";

            // Check all rows in the data table
            foreach (DataRow row in dt.Rows)
            {
                // Only check added or modified rows
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    // Check for empty product name
                    if (row["TEN_SAN_PHAM"] == DBNull.Value || string.IsNullOrWhiteSpace(row["TEN_SAN_PHAM"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Tên sản phẩm không được để trống!";
                        break;
                    }

                    // Check if unit of measure is selected
                    if (row["ID_DON_VI_TINH"] == DBNull.Value ||
                        row["ID_DON_VI_TINH"].ToString() == "0" ||
                        string.IsNullOrWhiteSpace(row["ID_DON_VI_TINH"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Vui lòng chọn đơn vị tính!";
                        break;
                    }

                    // Check for required prices
                    if (row["DON_GIA_NHAP"] == DBNull.Value ||
                        row["GIA_BAN_SI"] == DBNull.Value ||
                        row["GIA_BAN_LE"] == DBNull.Value)
                    {
                        hasError = true;
                        errorMessage = "Vui lòng nhập đầy đủ giá nhập, giá bán sỉ và giá bán lẻ!";
                        break;
                    }

                    // Convert to decimal for comparison
                    decimal giaNhap = Convert.ToDecimal(row["DON_GIA_NHAP"]);
                    decimal giaBanSi = Convert.ToDecimal(row["GIA_BAN_SI"]);
                    decimal giaBanLe = Convert.ToDecimal(row["GIA_BAN_LE"]);

                    // Check for positive prices
                    if (giaNhap <= 0 || giaBanSi <= 0 || giaBanLe <= 0)
                    {
                        hasError = true;
                        errorMessage = "Giá nhập và giá bán phải lớn hơn 0!";
                        break;
                    }

                    // Check price relationships
                    if (giaBanSi <= giaNhap)
                    {
                        hasError = true;
                        errorMessage = "Giá bán sỉ phải lớn hơn giá nhập!";
                        break;
                    }

                    if (giaBanLe < giaBanSi)
                    {
                        hasError = true;
                        errorMessage = "Giá bán lẻ không được thấp hơn giá bán sỉ!";
                        break;
                    }
                }
            }

            if (hasError)
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If all validations pass, save the data
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào để lưu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                e.Handled = true; // Prevent the beep sound
                e.SuppressKeyPress = true; // Prevent the ding sound
            }
        }

        private void toolTimSanPham_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(toolTimSanPham.Text))
            {
                ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                    txtMaSanPham, txtTenSanPham, cmbDVT, numSoLuong, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
            }
        }

        void TimSanPham()
        {
            if (string.IsNullOrWhiteSpace(toolTimSanPham.Text))
            {
                // Reload all products
                ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                    txtMaSanPham, txtTenSanPham, cmbDVT, numSoLuong, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
                return;
            }

            if (toolTimMaSanPham.Checked)
            {
                if (int.TryParse(toolTimSanPham.Text, out int maSanPham))
                {
                    ctrl.TimMaSanPham(maSanPham);
                }
            }
            else
            {
                ctrl.TimTenSanPham(toolTimSanPham.Text.Trim());
            }

            // Update the grid with search results
            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
            }
        }

        private void toolTimSanPham_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
        }


        private void TaiLaiDanhSachSanPham()
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                txtMaSanPham, txtTenSanPham, cmbDVT, numSoLuong, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
        }

        private void bindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {
        }
    }
}