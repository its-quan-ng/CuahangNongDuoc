using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc
{
    public partial class frmBanLe: Form
    {
        SanPhamController ctrlSanPham = new SanPhamController();
        KhachHangController ctrlKhachHang = new KhachHangController();
        MaSanPhamController ctrlMaSanPham = new MaSanPhamController();
        PhieuBanController ctrlPhieuBan = new PhieuBanController();
        ChiTietPhieuBanController ctrlChiTiet = new ChiTietPhieuBanController();
        IList<MaSanPham> deleted = new List<MaSanPham>();
        Controll status = Controll.Normal;

        public frmBanLe()
        {
            InitializeComponent();
            status = Controll.AddNew;
        }

     
        public frmBanLe(PhieuBanController ctrlPB)
            : this()
        {
            this.ctrlPhieuBan = ctrlPB;
            status = Controll.Normal;
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[frmBanLe] Form load started...");

                ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
                ctrlMaSanPham.HienThiDataGridViewComboBox(colMaSanPham);

                cmbSanPham.SelectedIndexChanged += new EventHandler(cmbSanPham_SelectedIndexChanged);

                ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);

                ctrlPhieuBan.HienthiPhieuBan(bindingNavigator, cmbKhachHang, txtMaPhieu, dtNgayLapPhieu, numTongTien, numDaTra, numConNo);

                bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
                bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);

                ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, Convert.ToInt32(txtMaPhieu.Text));

                if (status == Controll.AddNew)
                {
                    txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
                }
                else
                {
                    this.Allow(false);
                }

                string phuongPhap = ThamSo.PhuongPhapXuatKho;
                string tinhGia = ThamSo.PhuongPhapTinhGiaXuat;
                System.Diagnostics.Debug.WriteLine(
                    $"[frmBanLe] Cấu hình: Xuất kho = {phuongPhap}, Tính giá = {tinhGia}"
                );
                System.Diagnostics.Debug.WriteLine("[frmBanLe] Form load completed successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] frmBanLe.Load: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show(
                    $"Lỗi khi mở form:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (status == Controll.Normal)
            {
                ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, Convert.ToInt32(txtMaPhieu.Text));
            }
        }


        void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue != null)
            {
                MaSanPhamController ctrlMSP = new MaSanPhamController();

                cmbMaSanPham.SelectedIndexChanged -= new EventHandler(cmbMaSanPham_SelectedIndexChanged);
                ctrlMSP.HienThiAutoComboBox(Convert.ToInt32(cmbSanPham.SelectedValue), cmbMaSanPham);
                cmbMaSanPham.SelectedIndexChanged += new EventHandler(cmbMaSanPham_SelectedIndexChanged);
            }
        }

        void cmbMaSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaSanPham.SelectedValue == null) return;

            MaSanPhamController ctrl = new MaSanPhamController();
            MaSanPham masp = ctrl.LayMaSanPham(cmbMaSanPham.SelectedValue.ToString());

            if (masp == null || masp.SanPham == null) return;

            numDonGia.Value = masp.SanPham.GiaBanLe;
            txtGiaNhap.Text = masp.GiaNhap.ToString("#,###0");
            txtGiaBanSi.Text = masp.SanPham.GiaBanSi.ToString("#,###0");
            txtGiaBanLe.Text = masp.SanPham.GiaBanLe.ToString("#,###0");

    
            try
            {
                int idSanPham = Convert.ToInt32(masp.SanPham.Id);
                long giaXuat = ctrl.TinhGiaXuat(idSanPham);
                txtGiaBQGQ.Text = giaXuat.ToString("#,###0");
            }
            catch (Exception ex)
            {
                // Nếu không tính được giá xuất, hiển thị 0
                txtGiaBQGQ.Text = "0";
                System.Diagnostics.Debug.WriteLine($"[WARNING] Không tính được giá xuất: {ex.Message}");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            // VALIDATION
            if (cmbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Phiếu Bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Phiếu Bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
            int soLuongCan = Convert.ToInt32(numSoLuong.Value);

            // ⭐ CHECK CẤU HÌNH XUẤT KHO
            if (ThamSo.PhuongPhapXuatKho == "FIFO")
            {
                // ══════════════════════════════════════════════════
                // MODE FIFO: TỰ ĐỘNG CHỌN LÔ (STRATEGY PATTERN)
                // ══════════════════════════════════════════════════
                try
                {
                    // ⭐ GỌI METHOD MỚI (Strategy Pattern)
                    IList<MaSanPham> danhSachLo = ctrlMaSanPham.ChonLoTheoConfig(idSanPham, soLuongCan);

                    if (danhSachLo == null || danhSachLo.Count == 0)
                    {
                        MessageBox.Show(
                            "Không thể chọn lô xuất kho!\nVui lòng kiểm tra lại dữ liệu.",
                            "Phiếu Bán",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // THÊM TỪNG LÔ VÀO DATAGRIDVIEW
                    foreach (MaSanPham maSp in danhSachLo)
                    {
                        DataRow row = ctrlChiTiet.NewRow();
                        row["ID_MA_SAN_PHAM"] = maSp.Id;
                        row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                        row["DON_GIA"] = numDonGia.Value;
                        row["SO_LUONG"] = maSp.SoLuong;
                        row["THANH_TIEN"] = numDonGia.Value * maSp.SoLuong;

                        ctrlChiTiet.Add(row);
                        numTongTien.Value += Convert.ToDecimal(row["THANH_TIEN"]);
                    }

                    System.Diagnostics.Debug.WriteLine(
                        $"[frmBanLe] Đã thêm {danhSachLo.Count} lô, tổng {soLuongCan} sản phẩm"
                    );

                    // Reset controls
                    numSoLuong.Value = 0;
                    numThanhTien.Value = 0;
                    cmbSanPham.Focus();  // Focus về combobox để tiếp tục thêm
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Không thể thêm sản phẩm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[ERROR] frmBanLe.btnAdd_Click: {ex.Message}\n{ex.StackTrace}");
                    MessageBox.Show(
                        $"Đã xảy ra lỗi không mong muốn:\n{ex.Message}\n\nVui lòng liên hệ quản trị viên.",
                        "Lỗi hệ thống",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else // CHI_DINH - THỦ CÔNG
            {
                // ══════════════════════════════════════════════════
                // MODE CHỈ ĐỊNH: USER CHỌN LÔ (LOGIC CŨ)
                // ══════════════════════════════════════════════════
                if (cmbMaSanPham.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn lô!", "Phiếu Bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (numDonGia.Value * numSoLuong.Value != numThanhTien.Value)
                {
                    MessageBox.Show("Thành tiền sai!", "Phiếu Bán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                numTongTien.Value += numThanhTien.Value;
                DataRow row = ctrlChiTiet.NewRow();
                row["ID_MA_SAN_PHAM"] = cmbMaSanPham.SelectedValue;
                row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                row["DON_GIA"] = numDonGia.Value;
                row["SO_LUONG"] = numSoLuong.Value;
                row["THANH_TIEN"] = numThanhTien.Value;
                ctrlChiTiet.Add(row);
            }

        }

        private void numDonGia_ValueChanged(object sender, EventArgs e)
        {
            numThanhTien.Value = numDonGia.Value * numSoLuong.Value;
        }

        private void numTongTien_ValueChanged(object sender, EventArgs e)
        {
            numConNo.Value = numTongTien.Value - numDaTra.Value;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            this.Luu();
            status = Controll.Normal;
            this.Allow(false);
        }

        void Luu()
        {
            if (status == Controll.AddNew)
            {
                ThemMoi();
            }
            else
            {
                CapNhat();
            }
        }

        void CapNhat()
        {
            foreach (MaSanPham masp in deleted)
            {
                CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(masp.Id, masp.SoLuong);
            }
            deleted.Clear();

            ctrlChiTiet.Save();

            ctrlPhieuBan.Update();
        }

        void ThemMoi()
        {
            DataRow row = ctrlPhieuBan.NewRow();
            row["ID"] = txtMaPhieu.Text;
            row["ID_KHACH_HANG"] = cmbKhachHang.SelectedValue;
            row["NGAY_BAN"] = dtNgayLapPhieu.Value.Date;
            row["TONG_TIEN"] = numTongTien.Value;
            row["DA_TRA"] = numDaTra.Value;
            row["CON_NO"] = numConNo.Value;
            ctrlPhieuBan.Add(row);

            PhieuBanController ctrl = new PhieuBanController();

            if (ctrl.LayPhieuBan(Convert.ToInt32(txtMaPhieu.Text)) != null)
            {
                MessageBox.Show("Mã Phiếu bán này đã tồn tại !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ThamSo.LaSoNguyen(txtMaPhieu.Text))
            {
                long so = Convert.ToInt64(txtMaPhieu.Text);
                if (so >= ThamSo.LayMaPhieuBan())
                {
                    ThamSo.GanMaPhieuBan(so + 1);
                }
            }

            ctrlPhieuBan.Save();

            ctrlChiTiet.Save();
        }

        private void toolLuu_Them_Click(object sender, EventArgs e)
        {
            ctrlPhieuBan = new PhieuBanController();
            status = Controll.AddNew;
            txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
            numTongTien.Value = 0;
            ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, Convert.ToInt32(txtMaPhieu.Text));
            this.Allow(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                numTongTien.Value -= Convert.ToInt64(row["THANH_TIEN"]);
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));
                bs.RemoveCurrent();
            }
           
        }

        private void dgvDanhsachSP_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                numTongTien.Value -= Convert.ToInt64(row["THANH_TIEN"]);
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));

            }
        }

        private void toolLuuIn_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                MessageBox.Show("Vui lòng lưu lại Phiếu bán hiện tại!", "Phieu Ban Le", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String ma_phieu = txtMaPhieu.Text;

                PhieuBanController ctrlPB = new PhieuBanController();

                CuahangNongduoc.BusinessObject.PhieuBan ph = ctrlPB.LayPhieuBan(Convert.ToInt32(ma_phieu));

                frmInPhieuBan InPhieuBan = new frmInPhieuBan(ph);

                InPhieuBan.Show();

            }
        }

        private void dgvDanhsachSP_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void toolChinhSua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            this.Allow(true);
        }

        void Allow(bool val)
        {
            txtMaPhieu.Enabled = val;
            dtNgayLapPhieu.Enabled = val;
            numTongTien.Enabled = val;
            btnAdd.Enabled = val;
            btnRemove.Enabled = val;
            dgvDanhsachSP.Enabled = val;
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                if (MessageBox.Show("Bạn có muốn lưu lại Phiếu bán này không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Luu();
                }
            }
            this.Close();
        }

        private void toolXoa_Click(object sender, EventArgs e)
        {
            DataRowView view =  (DataRowView)bindingNavigator.BindingSource.Current;
            if (view != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();
                    IList<ChiTietPhieuBan> ds = ctrl.ChiTietPhieuBan(Convert.ToInt32(view["ID"]));
                    foreach (ChiTietPhieuBan ct in ds)
                    {
                        CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(ct.MaSanPham.Id, ct.SoLuong);
                    }
                    bindingNavigator.BindingSource.RemoveCurrent();
                    ctrlPhieuBan.Save();
                }
            }
        }

        private void btnThemDaiLy_Click(object sender, EventArgs e)
        {
            frmKhachHang KhachHang = new frmKhachHang();
            KhachHang.ShowDialog();
            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham SanPham = new frmSanPham();
            SanPham.ShowDialog();
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
        }


     }
}
