using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmKhachHang : Form
    {
        CuahangNongduoc.Controller.KhachHangController ctrl = new CuahangNongduoc.Controller.KhachHangController();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {

            ctrl.HienthiKhachHangDataGridview(dataGridView, bindingNavigator);
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();

            // Trước hết commit dữ liệu từ grid/binding source xuống DataTable
            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.EndEdit();
            }
            dataGridView.EndEdit();

            // Sau khi dữ liệu đã được đẩy vào DataRowView, mới kiểm tra hợp lệ
            if (!KiemTraKhachHangHopLe())
            {
                return;
            }

            try
            {
                bool ketQua = ctrl.Save();
                if (!ketQua)
                {
                    MessageBox.Show(
                        "Lưu khách hàng thất bại. Không có dữ liệu nào được cập nhật.",
                        "Khách hàng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lưu khách hàng lỗi: " + ex.Message);
                MessageBox.Show(
                    "Đã xảy ra lỗi khi lưu khách hàng.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Validate bản ghi khách hàng hiện tại (grid-based)
        private bool KiemTraKhachHangHopLe()
        {
            if (bindingNavigator.BindingSource == null || bindingNavigator.BindingSource.Current == null)
            {
                return true; // Không có bản ghi nào để lưu
            }

            DataRowView rowView = bindingNavigator.BindingSource.Current as DataRowView;
            if (rowView == null)
            {
                return true;
            }

            string hoTen = Convert.ToString(rowView["HO_TEN"]).Trim();
            string diaChi = Convert.ToString(rowView["DIA_CHI"]).Trim();
            string dienThoai = Convert.ToString(rowView["DIEN_THOAI"]).Trim();

            // Họ tên bắt buộc nhập
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show(
                    "Họ tên khách hàng không được để trống.",
                    "Khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // Tìm và focus vào ô Họ tên của dòng hiện tại
                int rowIndex = dataGridView.CurrentCell != null ? dataGridView.CurrentCell.RowIndex : -1;
                if (rowIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells["colHoTen"];
                    dataGridView.BeginEdit(true);
                }

                return false;
            }

            // Validate số điện thoại: nếu nhập thì phải là số nguyên
            if (!string.IsNullOrWhiteSpace(dienThoai) && !ThamSo.LaSoNguyen(dienThoai))
            {
                MessageBox.Show(
                    "Số điện thoại phải là số (0-9).",
                    "Khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                int rowIndex = dataGridView.CurrentCell != null ? dataGridView.CurrentCell.RowIndex : -1;
                if (rowIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells["colDienThoai"];
                    dataGridView.BeginEdit(true);
                }

                return false;
            }

            // Có thể thêm validate khác (địa chỉ) nếu cần

            return true;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            long maso = ThamSo.KhachHang;
            ThamSo.KhachHang = maso + 1;

            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row["ID"] = maso;
            
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Dai Ly", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Khach Hang", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolTimHoTen_Click(object sender, EventArgs e)
        {
            toolTimDiaChi.Checked = !toolTimDiaChi.Checked;
            toolTimHoTen.Checked = !toolTimDiaChi.Checked;
            toolTimKhachHang.Text = "Tìm theo Họ tên";
            bindingNavigator.Focus();

        }

        private void toolTimDiaChi_Click(object sender, EventArgs e)
        {
            toolTimHoTen.Checked = !toolTimHoTen.Checked;
            toolTimDiaChi.Checked = !toolTimHoTen.Checked;
            toolTimKhachHang.Text = "Tìm theo Địa chỉ";
            bindingNavigator.Focus();
        }

        private void toolTimKhachHang_Enter(object sender, EventArgs e)
        {
            toolTimKhachHang.Text = "";
            toolTimKhachHang.ForeColor = Color.Black;
        }

        private void toolTimKhachHang_Leave(object sender, EventArgs e)
        {
            if (toolTimHoTen.Checked==true)
                toolTimKhachHang.Text = "Tìm theo Họ tên";
            else
                toolTimKhachHang.Text = "Tìm theo Địa chỉ";

            toolTimKhachHang.ForeColor = Color.FromArgb(224,224,224);
        }

        private void toolTimKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 13)
            {
                if (toolTimHoTen.Checked)
                    ctrl.TimHoTen(toolTimKhachHang.Text, false);
                else
                    ctrl.TimDiaChi(toolTimKhachHang.Text, false);
            }
        }

       
    }
}