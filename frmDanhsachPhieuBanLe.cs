using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmDanhsachPhieuBanLe : Form
    {
        public frmDanhsachPhieuBanLe()
        {
            InitializeComponent();
        }

        PhieuBanController ctrl = new PhieuBanController();
        KhachHangController ctrlKH = new KhachHangController();
        private void frmDanhsachPhieuNhap_Load(object sender, EventArgs e)
        {
            ctrlKH.HienthiKhachHangDataGridviewComboBox(colKhachhang);
            ctrl.HienthiPhieuBanLe(bindingNavigator, dataGridView);
            
            // Ẩn các cột chi phí vận chuyển và chi phí dịch vụ
            if (dataGridView.Columns.Contains("CHI_PHI_VAN_CHUYEN"))
            {
                dataGridView.Columns["CHI_PHI_VAN_CHUYEN"].Visible = false;
            }
            if (dataGridView.Columns.Contains("CHI_PHI_DICH_VU"))
            {
                dataGridView.Columns["CHI_PHI_DICH_VU"].Visible = false;
            }

            // YC4: Đổi tên và ẩn cột khuyến mãi
            if (dataGridView.Columns.Contains("CHIET_KHAU"))
            {
                dataGridView.Columns["CHIET_KHAU"].HeaderText = "Chiết khấu (%)";
            }

            if (dataGridView.Columns.Contains("ID_KHUYEN_MAI"))
            {
                dataGridView.Columns["ID_KHUYEN_MAI"].Visible = false;
            }

            if (dataGridView.Columns.Contains("TEN_KHUYEN_MAI"))
            {
                dataGridView.Columns["TEN_KHUYEN_MAI"].HeaderText = "Khuyến mãi";
                dataGridView.Columns["TEN_KHUYEN_MAI"].Width = 200;
            }
        }
        frmBanLe BanLe = null;
        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmBanLe(ctrl);
                BanLe.Show();
            }
            else
                BanLe.Activate();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmBanLe();
                BanLe.Show();
            }
            else
                BanLe.Activate();
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRowView view = (DataRowView)bindingNavigator.BindingSource.Current;
            if (view == null)
            {
                e.Cancel = true;
                return;
            }

            // Lấy ID của phiếu bán hiện tại
            int idPhieuBan = Convert.ToInt32(view["ID"]);

            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            // Cộng lại số lượng vào kho trước khi xóa
            ChiTietPhieuBanController ctrlChiTiet = new ChiTietPhieuBanController();
            IList<ChiTietPhieuBan> ds = ctrlChiTiet.ChiTietPhieuBan(idPhieuBan);
            foreach (ChiTietPhieuBan ct in ds)
            {
                CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(ct.MaSanPham.Id, ct.SoLuong);
            }

            // Xóa phiếu bán và chi tiết (cascade delete thủ công)
            if (ctrl.DeletePhieuBan(idPhieuBan))
            {
                // Xóa thành công → Remove từ BindingSource để update UI
                // KHÔNG cancel event - để row biến mất khỏi grid
            }
            else
            {
                MessageBox.Show("Lỗi khi xóa phiếu bán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
             DataRowView view =  (DataRowView)bindingNavigator.BindingSource.Current;
             if (view != null)
             {
                 // Lấy ID của phiếu bán hiện tại
                 int idPhieuBan = Convert.ToInt32(view["ID"]);

                 if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                     // Cộng lại số lượng vào kho trước khi xóa
                     ChiTietPhieuBanController ctrlChiTiet = new ChiTietPhieuBanController();
                     IList<ChiTietPhieuBan> ds = ctrlChiTiet.ChiTietPhieuBan(idPhieuBan);
                     foreach (ChiTietPhieuBan ct in ds)
                     {
                         CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(ct.MaSanPham.Id, ct.SoLuong);
                     }

                     // Xóa phiếu bán và chi tiết (cascade delete thủ công)
                     if (ctrl.DeletePhieuBan(idPhieuBan))
                     {
                         // Xóa thành công → Remove từ BindingSource để update UI
                         bindingNavigator.BindingSource.RemoveCurrent();
                         MessageBox.Show("Xóa phiếu bán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }
                     else
                     {
                         MessageBox.Show("Lỗi khi xóa phiếu bán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
             }
        }

        private void toolPrint_Click(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            if (row != null)
            {
                PhieuBanController ctrlPB = new PhieuBanController();
                String ma_phieu = row["ID"].ToString();
                CuahangNongduoc.BusinessObject.PhieuBan ph = ctrlPB.LayPhieuBan(Convert.ToInt32(ma_phieu));
                frmInPhieuBan PhieuBan = new frmInPhieuBan(ph);
                PhieuBan.Show();
            }
        }

        private void toolTimKiem_Click(object sender, EventArgs e)
        {
            frmTimPhieuBanLe Tim = new frmTimPhieuBanLe(false);
            Point p = PointToScreen(toolTimKiem.Bounds.Location);
            p.X += toolTimKiem.Width;
            p.Y += toolTimKiem.Height;
            Tim.Location = p;
            Tim.ShowDialog();
            if (Tim.DialogResult == DialogResult.OK)
            {
                ctrl.TimPhieuBan(Convert.ToInt32(Tim.cmbNCC.SelectedValue), Tim.dtNgayNhap.Value.Date);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}