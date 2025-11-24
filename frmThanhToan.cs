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
    public partial class frmThanhToan : Form
    {
        KhachHangController ctrlKH = new KhachHangController();
        PhieuThanhToanController ctrl = new PhieuThanhToanController();
        public frmThanhToan()
        {
            InitializeComponent();
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            ctrlKH.HienthiChungAutoComboBox(cmbKhachHang);
            ctrlKH.HienthiKhachHangChungDataGridviewComboBox(colKhachHang);
            ctrl.HienthiPhieuThanhToan(bindingNavigator, dataGridView, cmbKhachHang, txtMaPhieu, dtNgayThanhToan, numTongTien, txtGhiChu);
            bindingNavigator.BindingSource.AddingNew += new AddingNewEventHandler(BindingSource_AddingNew);
        }

        void BindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {

        }

        private void toolAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã phiếu mới
                long newMaphieu = ThamSo.LayMaPhieuThanhToan();

                // Tạo dòng dữ liệu mới
                DataRow newRow = ctrl.NewRow();
                newRow["ID"] = newMaphieu;
                newRow["NGAY_THANH_TOAN"] = DateTime.Now.Date;
                newRow["TONG_TIEN"] = 0;  // Giá trị mặc định
                newRow["GHI_CHU"] = DBNull.Value;  // Giá trị mặc định

                // Thêm vào DataTable
                ctrl.Add(newRow);

                // Di chuyển đến dòng mới
                bindingNavigator.BindingSource.MoveLast();

                // Focus vào combobox Khách hàng
                cmbKhachHang.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa phiếu thanh toán này không?", "Phieu Thanh Toan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "San Pham", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (cmbKhachHang.SelectedValue == null || cmbKhachHang.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbKhachHang.Focus();
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
                    // Xử lý ngày thanh toán
                    currentRow["NGAY_THANH_TOAN"] = dtNgayThanhToan.Value.Date;

                    // Xử lý khách hàng
                    if (cmbKhachHang.SelectedValue != null)
                    {
                        currentRow["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
                    }

                    // Xử lý tổng tiền
                    decimal tongTien = 0;
                    if (numTongTien.Value != null && numTongTien.Value > 0)
                    {
                        tongTien = numTongTien.Value;
                    }
                    currentRow["TONG_TIEN"] = tongTien;

                    // Xử lý ghi chú
                    currentRow["GHI_CHU"] = string.IsNullOrEmpty(txtGhiChu.Text) ?
                                           DBNull.Value : (object)txtGhiChu.Text;

                    // Kết thúc chỉnh sửa
                    bindingNavigator.BindingSource.EndEdit();

                    try
                    {
                        // Lưu dữ liệu
                        ctrl.Save();
                        MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception saveEx)
                    {
                        throw new Exception("Không thể lưu dữ liệu: " + saveEx.Message, saveEx);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật dữ liệu: " + ex.Message, ex);
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
                PhieuThanhToanController ctrlTT = new PhieuThanhToanController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuThanhToan ph = ctrlTT.LayPhieuThanhToan(Convert.ToInt32(ma_phieu));
                frmInPhieuThanhToan PhieuThanhToan = new frmInPhieuThanhToan(ph);
                PhieuThanhToan.Show();
            }
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuThu Tim = new frmTimPhieuThu();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuThanhToan(bindingNavigator, dataGridView, cmbKhachHang, txtMaPhieu, dtNgayThanhToan, numTongTien, txtGhiChu,
                    Convert.ToInt32(Tim.cmbKhachHang.SelectedValue), Tim.dtNgayThu.Value.Date);
            }
        }

    }
}