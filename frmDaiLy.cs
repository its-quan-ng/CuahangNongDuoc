using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDaiLy : Form
    {
        CuahangNongduoc.Controller.KhachHangController ctrl = new CuahangNongduoc.Controller.KhachHangController();
        public frmDaiLy()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {

            ctrl.HienthiDaiLyDataGridview(dataGridView, bindingNavigator);
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            // Gọi hàm lưu dùng chung để có thể tái sử dụng khi thoát form
            LuuDaiLy();
        }

        // Hàm lưu dùng chung, trả về true nếu lưu thành công
        private bool LuuDaiLy()
        {
            // Đẩy dữ liệu đang nhập trên lưới/binding xuống DataTable
            bindingNavigatorPositionItem.Focus();

            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.EndEdit();
            }
            dataGridView.EndEdit();

            // Kiểm tra dữ liệu hợp lệ trước khi lưu
            if (!KiemTraDaiLyHopLe())
            {
                return false;
            }

            try
            {
                bool ketQua = ctrl.Save();
                if (ketQua)
                {
                    MessageBox.Show(
                        "Lưu đại lý thành công.",
                        "Đại lý",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show(
                        "Lưu đại lý thất bại. Không có dữ liệu nào được cập nhật.",
                        "Đại lý",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lưu đại lý lỗi: " + ex.Message);
                MessageBox.Show(
                    "Đã xảy ra lỗi khi lưu đại lý.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        // Validate dữ liệu đại lý hiện tại (dựa trên dòng đang chọn trên grid)
        private bool KiemTraDaiLyHopLe()
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

            string tenDaiLy = Convert.ToString(rowView["HO_TEN"]).Trim();
            string diaChi = Convert.ToString(rowView["DIA_CHI"]).Trim();
            string dienThoai = Convert.ToString(rowView["DIEN_THOAI"]).Trim();

            // Tên đại lý bắt buộc nhập
            if (string.IsNullOrWhiteSpace(tenDaiLy))
            {
                MessageBox.Show(
                    "Tên đại lý không được để trống.",
                    "Đại lý",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                int rowIndex = dataGridView.CurrentCell != null ? dataGridView.CurrentCell.RowIndex : -1;
                if (rowIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells["colHoTen"];
                    dataGridView.BeginEdit(true);
                }

                return false;
            }

            // Nếu có nhập điện thoại thì phải là số
            if (!string.IsNullOrWhiteSpace(dienThoai) && !ThamSo.LaSoNguyen(dienThoai))
            {
                MessageBox.Show(
                    "Số điện thoại phải là số (0-9).",
                    "Đại lý",
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
            // Đảm bảo commit dữ liệu đang sửa xuống DataTable trước khi kiểm tra thay đổi
            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.EndEdit();
            }
            dataGridView.EndEdit();

            // Lấy DataTable gốc từ BindingSource để kiểm tra thay đổi
            DataTable tbl = null;
            BindingSource bs = bindingNavigator.BindingSource as BindingSource;
            if (bs != null)
            {
                tbl = bs.DataSource as DataTable;
            }

            bool hasChanges = (tbl != null && tbl.GetChanges() != null);

            if (!hasChanges)
            {
                // Không có gì thay đổi, thoát luôn
                this.Close();
                return;
            }

            // Có thay đổi, hỏi người dùng có muốn lưu trước khi thoát không
            DialogResult result = MessageBox.Show(
                "Dữ liệu đã được thay đổi. Bạn có muốn lưu trước khi thoát không?",
                "Đại lý",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                // Ở lại form
                return;
            }

            if (result == DialogResult.No)
            {
                // Không lưu, thoát luôn
                this.Close();
                return;
            }

            if (result == DialogResult.Yes)
            {
                // Thử lưu, chỉ thoát nếu lưu thành công
                if (LuuDaiLy())
                {
                    this.Close();
                }
            }
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
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Dai Ly", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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

        private void toolTimKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (toolTimHoTen.Checked)
                    ctrl.TimHoTen(toolTimKhachHang.Text, true);
                else
                    ctrl.TimDiaChi(toolTimKhachHang.Text, true);
            }
        }

        private void toolTimKhachHang_Leave(object sender, EventArgs e)
        {
            if (toolTimHoTen.Checked == true)
                toolTimKhachHang.Text = "Tìm theo Họ tên";
            else
                toolTimKhachHang.Text = "Tìm theo Địa chỉ";

            toolTimKhachHang.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void toolTimKhachHang_Enter(object sender, EventArgs e)
        {
            toolTimKhachHang.Text = "";
            toolTimKhachHang.ForeColor = Color.Black;
        }
    }
}