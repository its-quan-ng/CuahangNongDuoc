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
            DataRowView deletingRow = e.Row?.DataBoundItem as DataRowView;

            if (!CanDeleteSupplier(deletingRow))
            {
                e.Cancel = true;
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Nha Cung Cap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataRowView currentRow = bindingNavigator.BindingSource.Current as DataRowView;
            if (!CanDeleteSupplier(currentRow))
            {
                return;
            }

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

        private void toolLuu_Click(object sender, EventArgs e)
        {
            // 1. EndEdit cell đang edit
            if (dataGridView.CurrentCell != null)
            {
                dataGridView.EndEdit();
            }

            // 2. Focus ra ngoài
            bindingNavigatorPositionItem.Focus();

            // 3. EndEdit row
            bindingNavigator.BindingSource.EndEdit();

            // 4. Validate từ DataTable - CHỈ bắt buộc Tên NCC
            DataTable dt = ctrl.GetDataTable();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;

                // Chỉ check row mới hoặc đã sửa
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    // CHỈ check HO_TEN (BẮT BUỘC)
                    if (row["HO_TEN"] == DBNull.Value || string.IsNullOrWhiteSpace(row["HO_TEN"].ToString()))
                    {
                        MessageBox.Show("Vui lòng nhập tên Nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // DIA_CHI và DIEN_THOAI là OPTIONAL - Không check
                }
            }

            // 5. Save
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
            // Không auto-delete khi đóng form
            // Nếu user muốn xóa → Dùng button "Xóa" hoặc validation trong toolLuu_Click
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

        private bool CanDeleteSupplier(DataRowView rowView)
        {
            if (rowView == null)
            {
                return false;
            }

            // Cho phép xóa các dòng mới thêm nhưng chưa lưu xuống DB
            if (rowView.Row.RowState == DataRowState.Added)
            {
                return true;
            }

            if (!int.TryParse(Convert.ToString(rowView["ID"]), out int id))
            {
                return true;
            }

            if (ctrl.HasLinkedRecords(id))
            {
                MessageBox.Show(
                    "Không thể xóa Nhà cung cấp này vì đang được sử dụng trong các phiếu nhập.\n" +
                    "Vui lòng kiểm tra và xóa các dữ liệu liên quan trước.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}