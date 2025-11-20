using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

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
            bindingNavigator.BindingSource.EndEdit();


            DataRowView currentRow = (DataRowView)bindingNavigator.BindingSource.Current;


            if (currentRow != null)
            {
                object hoTenValue = currentRow["HO_TEN"];
                string hoTen = (hoTenValue == DBNull.Value || hoTenValue == null) ? string.Empty : hoTenValue.ToString();

                if (string.IsNullOrWhiteSpace(hoTen))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView.Focus();
                    return;
                }
            }

           
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            DataRowView currentRow = (DataRowView)bindingNavigator.BindingSource.Current;
            if (currentRow == null)
                return;

            int idKhachHang = Convert.ToInt32(currentRow["ID"]);

            // Kiểm tra liên kết trước khi xóa
            if (ctrl.KiemTraLienKet(idKhachHang))
            {
                List<string> danhSachBang = ctrl.LayDanhSachBangLienKet(idKhachHang);
                string thongBao = "Không thể xóa khách hàng này vì đã được sử dụng trong:\n";
                thongBao += string.Join("\n", danhSachBang.Select(bang => "• " + bang));
                thongBao += "\n\nVui lòng xóa dữ liệu liên quan trước khi xóa khách hàng này.";

                MessageBox.Show(thongBao, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRowView rowView = (DataRowView)e.Row.DataBoundItem;
            if (rowView == null)
            {
                e.Cancel = true;
                return;
            }

            int idKhachHang = Convert.ToInt32(rowView["ID"]);

            // Kiểm tra liên kết trước khi xóa
            if (ctrl.KiemTraLienKet(idKhachHang))
            {
                List<string> danhSachBang = ctrl.LayDanhSachBangLienKet(idKhachHang);
                string thongBao = "Không thể xóa khách hàng này vì đã được sử dụng trong:\n";
                thongBao += string.Join("\n", danhSachBang.Select(bang => "• " + bang));
                thongBao += "\n\nVui lòng xóa dữ liệu liên quan trước khi xóa khách hàng này.";

                MessageBox.Show(thongBao, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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