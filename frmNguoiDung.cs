using CuahangNongduoc.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmNguoiDung : Form
    {
        NguoiDungController ctrl = new NguoiDungController();

        public frmNguoiDung()
        {
            InitializeComponent();
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            // Populate comboboxes with distinct values
            ctrl.HienthiAutoComboBoxQuyenHan(cmbQuyenHan);
            ctrl.HienthiAutoComboBoxTrangThai(cmbTrangThai);

            // Load data into DataGridView with bindings
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                txtMaNguoiDung, txtTenNguoiDung, txtTaiKhoan, txtMatKhau, txtSDT, cmbQuyenHan, cmbTrangThai);
            dataGridView.AutoGenerateColumns = false;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            bindingNavigator.BindingSource.EndEdit();

            // Validate data before saving
            DataTable dt = ctrl.GetDataTable();
            bool hasError = false;
            string errorMessage = "";

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    // Check required fields
                    if (row["HO_TEN"] == DBNull.Value || string.IsNullOrWhiteSpace(row["HO_TEN"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Tên người dùng không được để trống!";
                        break;
                    }

                    if (row["TEN_DANG_NHAP"] == DBNull.Value || string.IsNullOrWhiteSpace(row["TEN_DANG_NHAP"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Tài khoản không được để trống!";
                        break;
                    }

                    if (row["MAT_KHAU"] == DBNull.Value || string.IsNullOrWhiteSpace(row["MAT_KHAU"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Mật khẩu không được để trống!";
                        break;
                    }
                    if (row["QUYEN_HAN"] == DBNull.Value || string.IsNullOrWhiteSpace(row["QUYEN_HAN"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Quyền hạn không được để trống !";
                        break;
                    }
                    if (row["TRANG_THAI"] == DBNull.Value || string.IsNullOrWhiteSpace(row["TRANG_THAI"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Trạng thái không được để trống!";
                        break;
                    }
                    if (row["SO_DIEN_THOAI"] == DBNull.Value || string.IsNullOrWhiteSpace(row["SO_DIEN_THOAI"].ToString()))
                    {
                        hasError = true;
                        errorMessage = "Số điện thoại không được để trống!";
                        break;
                    }
                    // Encrypt password if it's a new row or password has changed
                    if (row.RowState == DataRowState.Added)
                    {
                        string plainPassword = row["MAT_KHAU"].ToString();
                        row["MAT_KHAU"] = NguoiDungController.MaHoaMD5(plainPassword);
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
            long maso = ThamSo.NguoiDung;
            ThamSo.NguoiDung = maso + 1;
            row["ID"] = maso;
            row["HO_TEN"] = "";
            row["TEN_DANG_NHAP"] = "";
            row["MAT_KHAU"] = "";
            row["QUYEN_HAN"] = "USER";  // Changed from "User" to "USER" to match database
            row["TRANG_THAI"] = "Hoạt động";  // Changed from "Active" to "Hoạt động" to match database constraint
            row["SO_DIEN_THOAI"] = "";
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Người dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void toolTimMaSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = true;
            toolTimTenSanPham.Checked = false;
            toolTimSanPham.Text = "";
        }

        private void toolTimTenSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = false;
            toolTimTenSanPham.Checked = true;
            toolTimSanPham.Text = "";
        }

        private void toolTimSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimNguoiDung();
            }
        }

        private void toolTimSanPham_Leave(object sender, EventArgs e)
        {
            TimNguoiDung();
        }

        void TimNguoiDung()
        {
            if (!string.IsNullOrEmpty(toolTimSanPham.Text))
            {
                // Tìm kiếm theo cả tài khoản và tên người dùng
                ctrl.TimTheoTaiKhoanHoacTen(toolTimSanPham.Text);
                if (bindingNavigator.BindingSource != null)
                {
                    bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                }
            }
            else
            {
                // Nếu ô tìm kiếm trống, load lại toàn bộ dữ liệu
                ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                    txtMaNguoiDung, txtTenNguoiDung, txtTaiKhoan, txtMatKhau, txtSDT, cmbQuyenHan, cmbTrangThai);
            }
        }

        private void toolTimSanPham_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
