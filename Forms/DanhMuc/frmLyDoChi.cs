using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmLyDoChi : Form
    {
        CuahangNongduoc.Controller.LyDoChiController ctrl = new CuahangNongduoc.Controller.LyDoChiController();
        public frmLyDoChi()
        {
            InitializeComponent();
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Lấy ID của lý do chi hiện tại
            DataGridViewRow row = e.Row;
            if (row.DataBoundItem != null)
            {
                DataRowView view = (DataRowView)row.DataBoundItem;
                int idLyDoChi = Convert.ToInt32(view["ID"]);

                // Kiểm tra xem lý do chi có liên kết với bảng khác không
                if (ctrl.KiemTraLienKet(idLyDoChi))
                {
                    List<string> danhSachBang = ctrl.LayDanhSachBangLienKet(idLyDoChi);
                    string thongBao = "Không thể xóa lý do chi này vì đang được sử dụng trong:\n\n";
                    foreach (string tenBang in danhSachBang)
                    {
                        thongBao += "- " + tenBang + "\n";
                    }
                    thongBao += "\nVui lòng xóa các bản ghi liên quan trước!";
                    MessageBox.Show(thongBao, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }

            if (MessageBox.Show("Bạn chắc chắn xóa lý do chi này không?", "Ly Do Chi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            bindingNavigator.BindingSource.EndEdit();
            
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            // Xử lý xóa thông qua DataGridView event
            if (bindingNavigator.BindingSource != null && bindingNavigator.BindingSource.Current != null)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }
    }
}