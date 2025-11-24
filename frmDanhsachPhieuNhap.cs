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
    public partial class frmDanhsachPhieuNhap : Form
    {
        public frmDanhsachPhieuNhap()
        {
            InitializeComponent();
        }

        PhieuNhapController ctrl = new PhieuNhapController();
        NhaCungCapController ctrlNCC = new NhaCungCapController();

        private void frmDanhsachPhieuNhap_Load(object sender, EventArgs e)
        {
            ctrlNCC.HienthiDataGridviewComboBox(colNhaCungCap);
            ctrl.HienthiPhieuNhap(bindingNavigator, dataGridView);
        }
        frmNhapHang NhapHang = null;
        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmNhapHang(ctrl);
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmNhapHang();
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }

        private void toolIn_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuNhapController ctrlPN = new PhieuNhapController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuNhap ph =  ctrlPN.LayPhieuNhap(Convert.ToInt32(ma_phieu));
                frmInPhieuNhap PhieuNhap = new frmInPhieuNhap(ph);
                PhieuNhap.Show();
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (bindingNavigator.BindingSource.Current == null)
                return;

            // Get the current PhieuNhap ID
            DataRowView currentRow = (DataRowView)bindingNavigator.BindingSource.Current;
            int phieuNhapId = Convert.ToInt32(currentRow["ID"]);

            // Check for related records
            if (ctrl.HasRelatedRecords(phieuNhapId))
            {
                MessageBox.Show("Không thể xóa phiếu nhập này vì có chi tiết phiếu nhập còn nợ chưa thanh toán .\nVui lòng xóa các chi tiết phiếu nhập trước khi xóa phiếu nhập.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bindingNavigator.BindingSource.RemoveCurrent();
                    ctrl.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa phiếu nhập: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {

            frmTimPhieuNhap TimPhieu = new frmTimPhieuNhap();
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            TimPhieu.Location = p;
            TimPhieu.ShowDialog();
            if (TimPhieu.DialogResult == DialogResult.OK)
            {
                if (TimPhieu.cmbNCC.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maNCC = Convert.ToInt32(TimPhieu.cmbNCC.SelectedValue);
                ctrl.TimPhieuNhap(maNCC, TimPhieu.dtTuNgay.Value.Date, TimPhieu.dtDenNgay.Value.Date);

                if (bindingNavigator.BindingSource != null)
                {
                    bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                }

                DataTable dt = ctrl.GetDataTable();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy phiếu nhập nào!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Tìm thấy {dt.Rows.Count} phiếu nhập!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}