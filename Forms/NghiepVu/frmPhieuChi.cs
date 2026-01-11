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
    public partial class frmPhieuChi : Form
    {
        LyDoChiController ctrlLyDo = new LyDoChiController();
        PhieuChiController ctrl = new PhieuChiController();
        public frmPhieuChi()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
            ctrlLyDo.HienthiDataGridviewComboBox(colLyDoChi);
            ctrl.HienthiPhieuChi(bindingNavigator, dataGridView, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu);


        }

        private void toolAdd_Click(object sender, EventArgs e)
        {
            try
            {
                long maphieu = ThamSo.PhieuChi;
                DataRow row = ctrl.NewRow();
                row["ID"] = maphieu;
                row["NGAY_CHI"] = dtNgayChi.Value.Date;
                row["TONG_TIEN"] = 0m;  // Set default value to 0
                ctrl.Add(row);
                bindingNavigator.BindingSource.MoveLast();
                numTongTien.Focus();  // Focus on the amount field
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // Lấy ID của phiếu chi hiện tại
            DataGridViewRow row = e.Row;
            if (row.DataBoundItem != null)
            {
                DataRowView view = (DataRowView)row.DataBoundItem;
                int idPhieuChi = Convert.ToInt32(view["ID"]);

                // Kiểm tra xem phiếu chi có liên kết với bảng khác không
                if (ctrl.KiemTraLienKet(idPhieuChi))
                {
                    List<string> danhSachBang = ctrl.LayDanhSachBangLienKet(idPhieuChi);
                    string thongBao = "Không thể xóa phiếu chi này vì đang được sử dụng trong:\n\n";
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

            if (MessageBox.Show("Bạn chắc chắn xóa phiếu chi này không?", "Phieu Chi",   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            DataRowView view = (DataRowView)bindingNavigator.BindingSource.Current;
            if (view != null)
            {
                // Lấy ID của phiếu chi hiện tại
                int idPhieuChi = Convert.ToInt32(view["ID"]);

                // Kiểm tra xem phiếu chi có liên kết với bảng khác không
                if (ctrl.KiemTraLienKet(idPhieuChi))
                {
                    List<string> danhSachBang = ctrl.LayDanhSachBangLienKet(idPhieuChi);
                    string thongBao = "Không thể xóa phiếu chi này vì đang được sử dụng trong:\n\n";
                    foreach (string tenBang in danhSachBang)
                    {
                        thongBao += "- " + tenBang + "\n";
                    }
                    thongBao += "\nVui lòng xóa các bản ghi liên quan trước!";
                    MessageBox.Show(thongBao, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn chắc chắn xóa phiếu chi này không?", "Phieu Chi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bindingNavigator.BindingSource.RemoveCurrent();
                    ctrl.Save();
                }
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (cmbLyDoChi.SelectedValue == null || cmbLyDoChi.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn Lý Do Chi!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbLyDoChi.Focus();
                    return;
                }

                // Lấy dữ liệu hiện tại
                DataRowView currentRow = (DataRowView)bindingNavigator.BindingSource.Current;
                if (currentRow == null)
                {
                    MessageBox.Show("Không có dữ liệu để lưu!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật giá trị
                try
                {
                    // Kết thúc chỉnh sửa hiện tại
                    bindingNavigator.BindingSource.EndEdit();

                    // Lưu dữ liệu
                    ctrl.Save();

                    MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, hủy thay đổi
                    currentRow.Row.RejectChanges();
                    throw new Exception("Lỗi khi lưu dữ liệu: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Dữ liệu không hợp lệ: " + e.Exception.Message,
                          "Lỗi dữ liệu",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
            e.Cancel = true;
        }

        private void toolIn_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuChiController ctrlChi = new PhieuChiController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuChi ph = ctrlChi.LayPhieuChi(Convert.ToInt32(ma_phieu));
                frmInPhieuChi InPhieu = new frmInPhieuChi(ph);
                InPhieu.Show();
            }
        }

        private void btnThemLyDoChi_Click(object sender, EventArgs e)
        {
            frmLyDoChi Chi = new frmLyDoChi();
            Chi.ShowDialog();
            ctrlLyDo.HienthiAutoComboBox(cmbLyDoChi);
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuChi Tim = new frmTimPhieuChi();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuChi(bindingNavigator, dataGridView, cmbLyDoChi, txtMaPhieu, dtNgayChi, numTongTien, txtGhiChu, Convert.ToInt32(Tim.cmbLyDo.SelectedValue), dtNgayChi.Value.Date);
                
            }
        }
    }
}