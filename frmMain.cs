using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CuahangNongduoc
{
    public partial class frmMain : Form
    {
       
        frmDonViTinh DonViTinh = null;
        frmSanPham SanPham = null;
        frmKhachHang KhachHang = null;
        frmDaiLy DaiLy = null;
        frmDanhsachPhieuNhap NhapHang = null;
        frmDanhsachPhieuBanLe BanLe = null;
        frmDanhsachPhieuBanSi BanSi = null;
        frmThanhToan ThanhToan = null;
        frmDunoKhachhang DunoKhachhang = null;
        frmDoanhThu DoanhThu = null;
        frmSoLuongTon SoLuongTon = null;
        frmSoLuongBan SoLuongBan = null;
        frmSanphamHethan SanphamHethan = null;
        frmThongtinCuahang ThongtinCuahang = null;
        frmThongtinLienhe ThongtinLienhe = null;
        frmNhaCungCap NhaCungCap = null;
        frmLyDoChi LyDoChi = null;
        frmPhieuChi PhieuChi = null;

        public frmMain()
        {
            InitializeComponent();
           
            this.IsMdiContainer = true;
        }

  
        private void CapNhatTrangThai()
        {
            // Ẩn tất cả các form con đang mở (nếu có, khi đăng xuất)
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }

            // Kiểm tra trạng thái đăng nhập
            if (PhienDangNhap.DaDangNhap)
            {
                // ĐÃ ĐĂNG NHẬP

                // 1. Cập nhật Title
                this.Text = String.Format(
                    "Cửa Hàng Nông Dược - {0} [{1}]",
                    PhienDangNhap.LayTenHienThi(),
                    PhienDangNhap.LaAdmin ? "Quản trị" : "Nhân viên"
                );

                // 2. Kích hoạt/Vô hiệu hóa Menu
                mnuDangNhap.Enabled = false;
                mnuDangXuat.Enabled = true;

                // Kích hoạt tất cả menu chính (trước khi phân quyền)
                foreach (ToolStripItem item in menuStrip.Items) // Giả sử menuStrip là tên MenuStrip của bạn
                {
                    item.Visible = true;
                }

               
                if (!PhienDangNhap.LaAdmin)
                {
                    // Ẩn các mục Menu quản trị
                    mnuSanPham.Visible = false;
                    mnuDonViTinh.Visible = false;
                    mnuLyDoChi.Visible = false;
                    mnuNhaCungCap.Visible = false;
                    mnuPhieuChi.Visible = false;
                    mnuTonghopDuno.Visible = false;
                    mnuSoLuongBan.Visible = false;
                    mnuTuychinh.Visible = false;
                    mnuCauHinhKho.Visible = false; 

                 
                       
                        toolSanPham.Enabled = false;

                        // Nhà Cung Cấp (Đã sửa theo tên bạn cung cấp: toolNhaCungCap)
                        toolNhaCungCap.Enabled = false;

                        // Phiếu Chi
                        toolPhieuChi.Enabled = false;

                    itemSanPham.Enabled = false;
                    itemNhaCungCap.Enabled = false;
                    itemPhieuChi.Enabled =false;
                    itemTonghopDoanhthu.Enabled = false;

                }

                // Mở thanh công cụ/chức năng (Mặc định)
                mnuThanhCongCu.Checked = true;
                toolStrip.Visible = true;
                mnuThanhChucNang.Checked = true;
                taskPane.Visible = true;

            }
            else
            {
                // CHƯA ĐĂNG NHẬP (TRẠNG THÁI MẶC ĐỊNH)

                this.Text = "Cửa Hàng Nông Dược - Vui lòng đăng nhập";
                mnuDangNhap.Enabled = true;
                mnuDangXuat.Enabled = false;

                // Ẩn tất cả các chức năng chính (chỉ giữ lại Đăng nhập/Thoát/Trợ giúp)
                foreach (ToolStripItem item in menuStrip.Items) // Lặp qua tất cả menu cấp 1
                {
                    
                    if (item != mnuHeThong && item != mnuTrogiupHuongdan)
                    {
                        item.Visible = false;
                    }
                }

                // Vô hiệu hóa thanh công cụ
                toolStrip.Visible = false;
                taskPane.Visible = false;
            }
        }

 
        private void frmMain_Load(object sender, EventArgs e)
        {
            DataService.OpenConnection();

            // Tự động mở form Đăng nhập nếu chưa đăng nhập
            if (!PhienDangNhap.DaDangNhap)
            {
                mnuDangNhap_Click(sender, e);
            }

            CapNhatTrangThai();
        }



        private void mnuDonViTinh_Click(object sender, EventArgs e)
        {
            if (DonViTinh == null || DonViTinh.IsDisposed)
            {
                DonViTinh = new frmDonViTinh();
                DonViTinh.MdiParent = this;
                DonViTinh.Show();
            }
            else
                DonViTinh.Activate();
        }

   
        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (SanPham == null || SanPham.IsDisposed)
            {
                SanPham = new frmSanPham();
                SanPham.MdiParent = this;
                SanPham.Show();
            }
            else
                SanPham.Activate();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (KhachHang == null || KhachHang.IsDisposed)
            {
                KhachHang = new frmKhachHang();
                KhachHang.MdiParent = this;
                KhachHang.Show();
            }
            else
                KhachHang.Activate();
        }

        private void mnuDaiLy_Click(object sender, EventArgs e)
        {
            if (DaiLy == null || DaiLy.IsDisposed)
            {
                DaiLy = new frmDaiLy();
                DaiLy.MdiParent = this;
                DaiLy.Show();
            }
            else
                DaiLy.Activate();

        }
        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            if (NhapHang == null || NhapHang.IsDisposed)
            {
                NhapHang = new frmDanhsachPhieuNhap();
                NhapHang.MdiParent = this;
                NhapHang.Show();
            }
            else
                NhapHang.Activate();
        }
        private void mnuBanHangKH_Click(object sender, EventArgs e)
        {
            if (BanLe == null || BanLe.IsDisposed)
            {
                BanLe = new frmDanhsachPhieuBanLe();
                BanLe.MdiParent = this;
                BanLe.Show();
            }
            else
                BanLe.Activate();
        }
        private void mnuBanHangDL_Click(object sender, EventArgs e)
        {
            if (BanSi == null || BanSi.IsDisposed)
            {
                BanSi = new frmDanhsachPhieuBanSi();
                BanSi.MdiParent = this;
                BanSi.Show();
            }
            else
                BanSi.Activate();
        }

        private void mnuThanhCongCu_Click(object sender, EventArgs e)
        {
            mnuThanhCongCu.Checked = !mnuThanhCongCu.Checked;
            toolStrip.Visible = mnuThanhCongCu.Checked;
        }

        private void mnuThanhChucNang_Click(object sender, EventArgs e)
        {
            mnuThanhChucNang.Checked = !mnuThanhChucNang.Checked;
            taskPane.Visible = mnuThanhChucNang.Checked;
        }
        private void mnuThanhtoan_Click(object sender, EventArgs e)
        {
            if (ThanhToan == null || ThanhToan.IsDisposed)
            {
                ThanhToan = new frmThanhToan();
                ThanhToan.MdiParent = this;
                ThanhToan.Show();
            }
            else
                ThanhToan.Activate();
        }
        private void mnuTonghopDuno_Click(object sender, EventArgs e)
        {
            if (DunoKhachhang == null || DunoKhachhang.IsDisposed)
            {
                DunoKhachhang = new frmDunoKhachhang();
                DunoKhachhang.MdiParent = this;
                DunoKhachhang.Show();
            }
            else
                DunoKhachhang.Activate();
        }
        private void mnuBaocaoDoanhThu_Click(object sender, EventArgs e)
        {
            if (DoanhThu == null || DoanhThu.IsDisposed)
            {
                DoanhThu = new frmDoanhThu();
                DoanhThu.MdiParent = this;
                DoanhThu.Show();
            }
            else
                DoanhThu.Activate();

        }

        private void mnuBaocaoSoluongton_Click(object sender, EventArgs e)
        {

            if (SoLuongTon == null || SoLuongTon.IsDisposed)
            {
                SoLuongTon = new frmSoLuongTon();
                SoLuongTon.MdiParent = this;
                SoLuongTon.Show();
            }
            else
                SoLuongTon.Activate();

        }
        private void mnuSoLuongBan_Click(object sender, EventArgs e)
        {
            if (SoLuongBan == null || SoLuongBan.IsDisposed)
            {
                SoLuongBan = new frmSoLuongBan();
                SoLuongBan.MdiParent = this;
                SoLuongBan.Show();
            }
            else
                SoLuongBan.Activate();
        }
        private void mnuSanphamHethan_Click(object sender, EventArgs e)
        {
            if (SanphamHethan == null || SanphamHethan.IsDisposed)
            {
                SanphamHethan = new frmSanphamHethan();
                SanphamHethan.MdiParent = this;
                SanphamHethan.Show();
            }
            else
                SanphamHethan.Activate();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mnuTuychinhThongtin_Click(object sender, EventArgs e)
        {

            if (ThongtinCuahang == null || ThongtinCuahang.IsDisposed)
            {
                ThongtinCuahang = new frmThongtinCuahang();
                ThongtinCuahang.MdiParent = this;
                ThongtinCuahang.Show();
            }
            else
                ThongtinCuahang.Activate();
        }
        private void mnuTrogiupLienhe_Click(object sender, EventArgs e)
        {
            if (ThongtinLienhe == null || ThongtinLienhe.IsDisposed)
            {
                ThongtinLienhe = new frmThongtinLienhe();
                ThongtinLienhe.MdiParent = this;
                ThongtinLienhe.Show();
            }
            else
                ThongtinLienhe.Activate();
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            if (NhaCungCap == null || NhaCungCap.IsDisposed)
            {
                NhaCungCap = new frmNhaCungCap();
                NhaCungCap.MdiParent = this;
                NhaCungCap.Show();
            }
            else
                NhaCungCap.Activate();
        }
        private void mnuLyDoChi_Click(object sender, EventArgs e)
        {
            if (LyDoChi == null || LyDoChi.IsDisposed)
            {
                LyDoChi = new frmLyDoChi();
                LyDoChi.MdiParent = this;
                LyDoChi.Show();
            }
            else
                LyDoChi.Activate();
        }

        private void mnuPhieuChi_Click(object sender, EventArgs e)
        {
            if (PhieuChi == null || PhieuChi.IsDisposed)
            {
                PhieuChi = new frmPhieuChi();
                PhieuChi.MdiParent = this;
                PhieuChi.Show();
            }
            else
                PhieuChi.Activate();
        }

        private void mnuTrogiupHuongdan_Click(object sender, EventArgs e)
        {
            // Help.ShowHelp(this, "CPP.CHM");
        }



        private void mnuCauHinhKho_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            if (!PhienDangNhap.LaAdmin)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập chức năng này!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Mở form
            frmCauHinh frm = new frmCauHinh();
            frm.ShowDialog();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            PhienDangNhap.DangXuat(); // Giả sử PhienDangNhap có phương thức DangXuat()
            CapNhatTrangThai();

            // Sau khi đăng xuất, tự động mở lại Form đăng nhập
            mnuDangNhap_Click(sender, e);
        }

        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            // Mở form Đăng nhập (Dùng ShowDialog để bắt buộc đăng nhập)
            frmDangNhap frm = new frmDangNhap();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Nếu đăng nhập thành công, cập nhật giao diện
                CapNhatTrangThai();
            }
            else if (!PhienDangNhap.DaDangNhap)
            {
                // Nếu không đăng nhập được và nhấn Close/Cancel, đóng luôn chương trình
                MessageBox.Show("Chương trình sẽ thoát nếu bạn không đăng nhập.", "Thông báo");
                this.Close();
            }
        }
    }
}