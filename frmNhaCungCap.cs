using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmNhaCungCap : Form
    {
        CuahangNongduoc.Controller.NhaCungCapController ctrl = new CuahangNongduoc.Controller.NhaCungCapController();
        public frmNhaCungCap()
        {
            InitializeComponent();
            // Đăng ký event FormClosing
            this.FormClosing += new FormClosingEventHandler(frmNhaCungCap_FormClosing);
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Nha Cung Cap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Nha Cung Cap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            long maso = ThamSo.NhaCungCap;
            ThamSo.NhaCungCap = maso + 1;

            DataRowView row = (DataRowView)bindingNavigator.BindingSource.AddNew();
            row["ID"] = maso;
            
        }

        // Hàm kiểm tra một dòng dữ liệu có hợp lệ không
        private bool IsRowValid(DataGridViewRow row)
        {
            if (row.IsNewRow) return true; // Dòng mới (trống) thì bỏ qua
            
            // Kiểm tra Nhà cung cấp (HO_TEN)
            if (row.Cells["colHoTen"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colHoTen"].Value.ToString()))
            {
                return false;
            }
            
            // Kiểm tra Địa chỉ (DIA_CHI)
            if (row.Cells["colDiaChi"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colDiaChi"].Value.ToString()))
            {
                return false;
            }
            
            // Kiểm tra Điện thoại (DIEN_THOAI)
            if (row.Cells["colDienThoai"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colDienThoai"].Value.ToString()))
            {
                return false;
            }
            
            // Kiểm tra Điện thoại chỉ được chứa số
            string dienThoai = row.Cells["colDienThoai"].Value.ToString().Trim();
            foreach (char c in dienThoai)
            {
                // Chỉ cho phép số (0-9), khoảng trắng, dấu gạch ngang (-), và dấu ngoặc đơn ()
                if (!char.IsDigit(c) && c != ' ' && c != '-' && c != '(' && c != ')')
                {
                    return false;
                }
            }
            
            return true; // Tất cả đều hợp lệ
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            
            // Kiểm tra validation TRƯỚC KHI EndEdit
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;
                
                // Kiểm tra Nhà cung cấp (HO_TEN)
                if (row.Cells["colHoTen"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colHoTen"].Value.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập tên Nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView.CurrentCell = row.Cells["colHoTen"];
                    bindingNavigator.BindingSource.CancelEdit(); // Hủy thay đổi
                    return;
                }
                
                // Kiểm tra Địa chỉ (DIA_CHI)
                if (row.Cells["colDiaChi"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colDiaChi"].Value.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập Địa chỉ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView.CurrentCell = row.Cells["colDiaChi"];
                    bindingNavigator.BindingSource.CancelEdit(); // Hủy thay đổi
                    return;
                }
                
                // Kiểm tra Điện thoại (DIEN_THOAI)
                if (row.Cells["colDienThoai"].Value == null || string.IsNullOrWhiteSpace(row.Cells["colDienThoai"].Value.ToString()))
                {
                    MessageBox.Show("Vui lòng nhập Điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView.CurrentCell = row.Cells["colDienThoai"];
                    bindingNavigator.BindingSource.CancelEdit(); // Hủy thay đổi
                    return;
                }
                
                // Kiểm tra Điện thoại chỉ được chứa số
                string dienThoai = row.Cells["colDienThoai"].Value.ToString().Trim();
                bool isValidPhone = true;
                foreach (char c in dienThoai)
                {
                    // Chỉ cho phép số (0-9), khoảng trắng, dấu gạch ngang (-), và dấu ngoặc đơn ()
                    if (!char.IsDigit(c) && c != ' ' && c != '-' && c != '(' && c != ')')
                    {
                        isValidPhone = false;
                        break;
                    }
                }
                
                if (!isValidPhone)
                {
                    MessageBox.Show("Số điện thoại chỉ được chứa chữ số, khoảng trắng, dấu gạch ngang và dấu ngoặc đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView.CurrentCell = row.Cells["colDienThoai"];
                    bindingNavigator.BindingSource.CancelEdit(); // Hủy thay đổi
                    return;
                }
            }
            
            // Chỉ EndEdit khi tất cả validation đã pass
            bindingNavigator.BindingSource.EndEdit();
            
            // Lưu vào database
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Xóa các dòng không hợp lệ trước khi đóng form
            List<DataRowView> rowsToDelete = new List<DataRowView>();
            
            // Duyệt qua BindingSource để tìm các dòng không hợp lệ
            foreach (DataGridViewRow gridRow in dataGridView.Rows)
            {
                if (!IsRowValid(gridRow) && !gridRow.IsNewRow)
                {
                    // Lấy DataRowView từ BindingSource
                    if (gridRow.DataBoundItem != null)
                    {
                        DataRowView dataRow = (DataRowView)gridRow.DataBoundItem;
                        rowsToDelete.Add(dataRow);
                    }
                }
            }
            
            // Xóa các dòng không hợp lệ từ BindingSource
            foreach (DataRowView dataRow in rowsToDelete)
            {
                dataRow.Delete(); // Xóa khỏi DataTable
            }
            
            // Lưu thay đổi nếu có dòng bị xóa
            if (rowsToDelete.Count > 0)
            {
                ctrl.Save(); // Lưu vào database để xóa vĩnh viễn
            }
        }

        private void toolTimNhaCungCap_Enter(object sender, EventArgs e)
        {
            toolTimNhaCungCap.Text = "";
            toolTimNhaCungCap.ForeColor = Color.Black;
        }

        private void toolTimNhaCungCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (toolTimHoTen.Checked)
                {
                    ctrl.TimHoTen(toolTimNhaCungCap.Text);
                }
                else
                {
                    ctrl.TimDiaChi(toolTimNhaCungCap.Text);
                }
            }
        }

        private void toolTimNhaCungCap_Leave(object sender, EventArgs e)
        {
            if (toolTimHoTen.Checked == true)
                toolTimNhaCungCap.Text = "Tìm theo Nhà cung cấp";
            else
                toolTimNhaCungCap.Text = "Tìm theo Địa chỉ";

            toolTimNhaCungCap.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void toolTimHoTen_Click(object sender, EventArgs e)
        {
            toolTimDiaChi.Checked = !toolTimDiaChi.Checked;
            toolTimHoTen.Checked = !toolTimDiaChi.Checked;
            toolTimNhaCungCap.Text = "Tìm theo Nhà cung cấp";
            bindingNavigator.Focus();
        }

        private void toolTimDiaChi_Click(object sender, EventArgs e)
        {
            toolTimHoTen.Checked = !toolTimHoTen.Checked;
            toolTimDiaChi.Checked = !toolTimHoTen.Checked;
            toolTimNhaCungCap.Text = "Tìm theo Địa chỉ";
            bindingNavigator.Focus();
        }
    }
}