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
        ToolTip toolTip = new ToolTip();
        Dictionary<int, long> cacheGiaXuat = new Dictionary<int, long>();

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

        private void frmBanLe_Load(object sender, EventArgs e)
        {
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);

            cmbSanPham.SelectedIndexChanged += new EventHandler(cmbSanPham_SelectedIndexChanged);
            cmbMaSanPham.SelectedIndexChanged += new EventHandler(cmbMaSanPham_SelectedIndexChanged);

            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);

            ctrlPhieuBan.HienthiPhieuBan(bindingNavigator, cmbKhachHang, txtMaPhieu, dtNgayLapPhieu, numTongTien, numDaTra, numConNo, numChiPhiVanChuyen, numChiPhiDichVu);

            bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
            bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);

            // Thêm SelectionChanged event cho DataGridView để fill fields khi click row
            dgvDanhsachSP.SelectionChanged += dgvDanhsachSP_SelectionChanged;

            // Cấu hình DataGridView columns
            dgvDanhsachSP.AutoGenerateColumns = false;
            
            // Ẩn cột ID_PHIEU_BAN nếu tồn tại
            if (dgvDanhsachSP.Columns.Contains("ID_PHIEU_BAN"))
            {
                dgvDanhsachSP.Columns["ID_PHIEU_BAN"].Visible = false;
            }
            
            // Đảm bảo cột TEN_SAN_PHAM hiển thị nếu có trong Designer
            if (dgvDanhsachSP.Columns.Contains("colSanPham"))
            {
                dgvDanhsachSP.Columns["colSanPham"].DataPropertyName = "TEN_SAN_PHAM";
                dgvDanhsachSP.Columns["colSanPham"].HeaderText = "Sản phẩm";
                dgvDanhsachSP.Columns["colSanPham"].Width = 200;
            }

            // Cập nhật label và tooltip giá xuất theo config
            label15.Text = ThamSo.LayTenPhuongPhapTinhGia();
            toolTip.SetToolTip(label15, ThamSo.LayTooltipPhuongPhapTinhGia("label"));
            toolTip.SetToolTip(txtGiaBQGQ, ThamSo.LayTooltipPhuongPhapTinhGia("textbox"));

            // Ẩn/Hiện combo Mã số theo config xuất kho
            string phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;
            if (phuongPhapXuatKho == "FIFO")
            {
                cmbMaSanPham.Visible = false;
                label4.Visible = false;
            }
            else
            {
                cmbMaSanPham.Visible = true;
                label4.Visible = true;
            }

            if (status == Controll.AddNew)
            {
                txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
                // Bind DataGridView với DataTable từ factory để hiển thị sản phẩm khi thêm mới
                BindingSource bs = new BindingSource();
                bs.DataSource = ctrlChiTiet.GetDataTable();
                dgvDanhsachSP.DataSource = bs;
            }
            else
            {
                this.Allow(false);

                // Load chi tiết phiếu hiện tại ngay khi mở form (không đợi event)
                if (!string.IsNullOrWhiteSpace(txtMaPhieu.Text) && int.TryParse(txtMaPhieu.Text, out int maPhieu))
                {
                    ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, maPhieu);
                }
            }
        }

        void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (status == Controll.Normal)
            {
                // Kiểm tra txtMaPhieu.Text trước khi convert
                if (!string.IsNullOrWhiteSpace(txtMaPhieu.Text) && int.TryParse(txtMaPhieu.Text, out int maPhieu))
                {
                    ctrlChiTiet.HienThiChiTiet(dgvDanhsachSP, maPhieu);
                }
            }
        }


        void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue != null)
            {
                int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
                MaSanPhamController ctrlMSP = new MaSanPhamController();

                string phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

                if (phuongPhapXuatKho == "CHI_DINH")
                {
                    cmbMaSanPham.SelectedIndexChanged -= new EventHandler(cmbMaSanPham_SelectedIndexChanged);
                    ctrlMSP.HienThiAutoComboBox(idSanPham, cmbMaSanPham);
                    cmbMaSanPham.SelectedIndexChanged += new EventHandler(cmbMaSanPham_SelectedIndexChanged);

                    if (cmbMaSanPham.Items.Count > 0 && cmbMaSanPham.SelectedValue != null)
                    {
                        FillThongTinSanPhamTheoMaLo(cmbMaSanPham.SelectedValue.ToString());
                    }
                }
                else
                {
                    FillThongTinSanPhamTheoId(idSanPham);
                }
            }
        }

        void cmbMaSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaSanPham.SelectedValue != null)
            {
                FillThongTinSanPhamTheoMaLo(cmbMaSanPham.SelectedValue.ToString());
            }
        }

        void FillThongTinSanPhamTheoMaLo(string idMaLo)
        {
            MaSanPhamController ctrl = new MaSanPhamController();
            MaSanPham masp = ctrl.LayMaSanPham(idMaLo);
            if (masp != null && masp.SanPham != null)
            {
                numDonGia.Value = masp.SanPham.GiaBanLe;
                txtGiaNhap.Text = masp.GiaNhap.ToString("#,###0");
                txtGiaBanSi.Text = masp.SanPham.GiaBanSi.ToString("#,###0");
                txtGiaBanLe.Text = masp.SanPham.GiaBanLe.ToString("#,###0");

                int idSanPham = Convert.ToInt32(masp.SanPham.Id);
                FillGiaXuat(idSanPham);
            }
        }

        void FillThongTinSanPhamTheoId(int idSanPham)
        {
            SanPhamController ctrlSP = new SanPhamController();
            SanPham sp = ctrlSP.LaySanPham(idSanPham);
            if (sp != null)
            {
                numDonGia.Value = sp.GiaBanLe;
                txtGiaNhap.Text = sp.DonGiaNhap.ToString("#,###0");
                txtGiaBanSi.Text = sp.GiaBanSi.ToString("#,###0");
                txtGiaBanLe.Text = sp.GiaBanLe.ToString("#,###0");

                FillGiaXuat(idSanPham);
            }
        }

        void FillGiaXuat(int idSanPham)
        {
            try
            {
                long giaXuat;

                if (cacheGiaXuat.ContainsKey(idSanPham))
                {
                    giaXuat = cacheGiaXuat[idSanPham];
                }
                else
                {
                    giaXuat = ctrlMaSanPham.TinhGiaXuat(idSanPham);
                    cacheGiaXuat[idSanPham] = giaXuat;
                }

                txtGiaBQGQ.Text = giaXuat > 0 ? giaXuat.ToString("#,###0") : "N/A";
            }
            catch (Exception ex)
            {
                txtGiaBQGQ.Text = "N/A";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numDonGia.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập Đơn giá!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

            try
            {
                int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
                int soLuongCanBan = Convert.ToInt32(numSoLuong.Value);
                BindingSource bs = (BindingSource)dgvDanhsachSP.DataSource;
                DataTable dt = (DataTable)bs.DataSource;

                if (phuongPhapXuatKho == "FIFO")
                {
                    // Tự động chọn lô theo FEFO
                    IList<MaSanPham> danhSachLo = ctrlMaSanPham.ChonLoTheoConfig(idSanPham, soLuongCanBan);

                    // Lấy tên sản phẩm từ cmbSanPham (đã có sẵn)
                    string tenSanPham = cmbSanPham.Text;

                    foreach (MaSanPham lo in danhSachLo)
                    {
                        DataRow row = ctrlChiTiet.NewRow();
                        row["ID_MA_SAN_PHAM"] = lo.Id;
                        row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                        row["DON_GIA"] = numDonGia.Value;
                        row["SO_LUONG"] = lo.SoLuong;
                        row["THANH_TIEN"] = numDonGia.Value * lo.SoLuong;

                        if (row.Table.Columns.Contains("TEN_SAN_PHAM"))
                        {
                            row["TEN_SAN_PHAM"] = tenSanPham;
                        }

                        if (row.Table.Columns.Contains("NGAY_HET_HAN") && lo.NgayHetHan != DateTime.MinValue)
                        {
                            row["NGAY_HET_HAN"] = lo.NgayHetHan;
                        }

                        ctrlChiTiet.Add(row);
                    }
                }
                else
                {
                    // Chọn lô thủ công
                    if (cmbMaSanPham.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn Mã số sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string maSanPhamDaChon = cmbMaSanPham.SelectedValue.ToString();
                    MaSanPham maSPDaChon = ctrlMaSanPham.LayMaSanPham(maSanPhamDaChon);

                    if (maSPDaChon == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin mã sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (maSPDaChon.SoLuong < soLuongCanBan)
                    {
                        MessageBox.Show($"Số lượng tồn kho không đủ!\nTồn kho: {maSPDaChon.SoLuong}\nYêu cầu: {soLuongCanBan}",
                            "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string tenSanPham = maSPDaChon.SanPham != null ? maSPDaChon.SanPham.TenSanPham : "";

                    bool daTonTai = false;
                    foreach (DataRow existingRow in dt.Rows)
                    {
                        if (Convert.ToString(existingRow["ID_MA_SAN_PHAM"]) == maSanPhamDaChon)
                        {
                            int soLuongCu = Convert.ToInt32(existingRow["SO_LUONG"]);
                            int soLuongMoi = soLuongCu + soLuongCanBan;

                            if (maSPDaChon.SoLuong < soLuongMoi)
                            {
                                MessageBox.Show($"Số lượng tồn kho không đủ!\nTồn kho: {maSPDaChon.SoLuong}\nĐã có trong phiếu: {soLuongCu}\nYêu cầu thêm: {soLuongCanBan}\nTổng: {soLuongMoi}",
                                    "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            existingRow["SO_LUONG"] = soLuongMoi;
                            existingRow["THANH_TIEN"] = numDonGia.Value * soLuongMoi;
                            daTonTai = true;
                            break;
                        }
                    }

                    if (!daTonTai)
                    {
                        DataRow row = ctrlChiTiet.NewRow();
                        row["ID_MA_SAN_PHAM"] = maSanPhamDaChon;
                        row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                        row["DON_GIA"] = numDonGia.Value;
                        row["SO_LUONG"] = soLuongCanBan;
                        row["THANH_TIEN"] = numDonGia.Value * soLuongCanBan;

                        if (row.Table.Columns.Contains("TEN_SAN_PHAM"))
                        {
                            row["TEN_SAN_PHAM"] = tenSanPham;
                        }

                        if (row.Table.Columns.Contains("NGAY_HET_HAN") && maSPDaChon.NgayHetHan != DateTime.MinValue)
                        {
                            row["NGAY_HET_HAN"] = maSPDaChon.NgayHetHan;
                        }

                        ctrlChiTiet.Add(row);
                    }
                }

                // Refresh DataGridView
                bs.ResetBindings(false);

                // Cập nhật tổng tiền từ các dòng trong DataGridView
                decimal tongTien = 0;
                if (bs != null && bs.DataSource != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        tongTien += Convert.ToDecimal(dr["THANH_TIEN"]);
                    }
                }
                numTongTien.Value = tongTien + numChiPhiDichVu.Value + numChiPhiVanChuyen.Value;

                // Clear fields sau khi Add thành công
                numSoLuong.Value = 0;
                numDonGia.Value = 0;
                numThanhTien.Value = 0;
                if (phuongPhapXuatKho == "CHI_DINH")
                {
                    cmbMaSanPham.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void numChiPhiVanChuyen_ValueChanged(object sender, EventArgs e)
        {
            // Cập nhật tổng tiền khi chi phí vận chuyển thay đổi
            decimal tongTienSP = 0;
            BindingSource bs = (BindingSource)dgvDanhsachSP.DataSource;
            if (bs != null && bs.DataSource != null)
            {
                DataTable dt = (DataTable)bs.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    tongTienSP += Convert.ToDecimal(dr["THANH_TIEN"]);
                }
            }
            numTongTien.Value = tongTienSP + numChiPhiDichVu.Value + numChiPhiVanChuyen.Value;
        }

        private void numChiPhiDichVu_ValueChanged(object sender, EventArgs e)
        {
            // Cập nhật tổng tiền khi chi phí dịch vụ thay đổi
            decimal tongTienSP = 0;
            BindingSource bs = (BindingSource)dgvDanhsachSP.DataSource;
            if (bs != null && bs.DataSource != null)
            {
                DataTable dt = (DataTable)bs.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    tongTienSP += Convert.ToDecimal(dr["THANH_TIEN"]);
                }
            }
            numTongTien.Value = tongTienSP + numChiPhiDichVu.Value + numChiPhiVanChuyen.Value;
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
            row["CHI_PHI_VAN_CHUYEN"] = numChiPhiVanChuyen.Value;
            row["CHI_PHI_DICH_VU"] = numChiPhiDichVu.Value;
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
            ctrlChiTiet = new ChiTietPhieuBanController();
            cacheGiaXuat.Clear();
            status = Controll.AddNew;
            txtMaPhieu.Text = ThamSo.LayMaPhieuBan().ToString();
            numTongTien.Value = 0;
            numChiPhiVanChuyen.Value = 0;
            numChiPhiDichVu.Value = 0;
            // Bind DataGridView với DataTable từ factory để hiển thị sản phẩm khi thêm mới
            BindingSource bs = new BindingSource();
            bs.DataSource = ctrlChiTiet.GetDataTable();
            dgvDanhsachSP.DataSource = bs;
            this.Allow(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BindingSource bs = ((BindingSource)dgvDanhsachSP.DataSource);
                DataRowView row = (DataRowView)bs.Current;
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));
                bs.RemoveCurrent();
                
                // Cập nhật tổng tiền từ các dòng trong DataGridView
                decimal tongTien = 0;
                if (bs != null && bs.DataSource != null)
                {
                    DataTable dt = (DataTable)bs.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tongTien += Convert.ToDecimal(dr["THANH_TIEN"]);
                    }
                }
                numTongTien.Value = tongTien + numChiPhiDichVu.Value + numChiPhiVanChuyen.Value;
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
                deleted.Add(new MaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]), Convert.ToInt32(row["SO_LUONG"])));
                
                // Cập nhật tổng tiền sau khi xóa (sẽ được tính lại trong event handler)
                // Tạm thời trừ đi thành tiền của dòng bị xóa
                numTongTien.Value -= Convert.ToDecimal(row["THANH_TIEN"]);
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

        private void dgvDanhsachSP_SelectionChanged(object sender, EventArgs e)
        {
            if (status != Controll.AddNew && dgvDanhsachSP.CurrentRow != null && dgvDanhsachSP.CurrentRow.DataBoundItem != null)
            {
                try
                {
                    DataRowView row = (DataRowView)dgvDanhsachSP.CurrentRow.DataBoundItem;

                    string phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

                    if (phuongPhapXuatKho == "CHI_DINH")
                    {
                        if (row["ID_MA_SAN_PHAM"] != DBNull.Value)
                        {
                            cmbMaSanPham.SelectedValue = row["ID_MA_SAN_PHAM"];
                        }
                    }

                    numDonGia.Value = row["DON_GIA"] != DBNull.Value ? Convert.ToDecimal(row["DON_GIA"]) : 0;
                    numSoLuong.Value = row["SO_LUONG"] != DBNull.Value ? Convert.ToDecimal(row["SO_LUONG"]) : 0;
                    numThanhTien.Value = numDonGia.Value * numSoLuong.Value;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SelectionChanged Error: {ex.Message}");
                }
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            // Lấy ID của phiếu bán hiện tại
            if (string.IsNullOrWhiteSpace(txtMaPhieu.Text) || !int.TryParse(txtMaPhieu.Text, out int idPhieuBan))
            {
                MessageBox.Show("Vui lòng chọn phiếu bán cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem phiếu bán có liên kết với bảng khác không
            if (ctrlPhieuBan.KiemTraLienKet(idPhieuBan))
            {
                List<string> danhSachBang = ctrlPhieuBan.LayDanhSachBangLienKet(idPhieuBan);
                string thongBao = "Không thể xóa phiếu bán này vì đang được sử dụng trong:\\n\\n";
                foreach (string tenBang in danhSachBang)
                {
                    thongBao += "- " + tenBang + "\\n";
                }
                thongBao += "\\nVui lòng xóa các bản ghi liên quan trước!";
                MessageBox.Show(thongBao, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu bán này?", "Xác nhận xóa", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolChinhSua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            // KHÔNG reset Controller - Dùng data hiện tại trong memory
            // Nếu đã có data trong m_Ds thì giữ nguyên, nếu chưa thì load từ DB
            DataTable dt = ctrlChiTiet.GetDataTable();
            if (dt.Rows.Count == 0)
            {
                // Chưa có data trong memory → Load từ DB
                ctrlChiTiet.LoadData(Convert.ToInt32(txtMaPhieu.Text));
            }
            this.Allow(true);
        }

        void Allow(bool val)
        {
            txtMaPhieu.Enabled = val;
            dtNgayLapPhieu.Enabled = val;
            numTongTien.Enabled = val;
            numChiPhiVanChuyen.Enabled = val;
            numChiPhiDichVu.Enabled = val;
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
                // Lấy ID của phiếu bán hiện tại
                int idPhieuBan = Convert.ToInt32(view["ID"]);

                // Kiểm tra xem phiếu bán có liên kết với bảng khác không
                if (ctrlPhieuBan.KiemTraLienKet(idPhieuBan))
                {
                    List<string> danhSachBang = ctrlPhieuBan.LayDanhSachBangLienKet(idPhieuBan);
                    string thongBao = "Không thể xóa phiếu bán này vì đang được sử dụng trong:\n\n";
                    foreach (string tenBang in danhSachBang)
                    {
                        thongBao += "- " + tenBang + "\n";
                    }
                    thongBao += "\nVui lòng xóa các bản ghi liên quan trước!";
                    MessageBox.Show(thongBao, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Ban Le", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();
                    IList<ChiTietPhieuBan> ds = ctrl.ChiTietPhieuBan(idPhieuBan);
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

        // Public method để refresh UI khi config thay đổi
        public void RefreshConfigUI()
        {
            try
            {
                // Clear cache giá xuất
                cacheGiaXuat.Clear();

                // Cập nhật label và tooltip
                label15.Text = ThamSo.LayTenPhuongPhapTinhGia();
                toolTip.SetToolTip(label15, ThamSo.LayTooltipPhuongPhapTinhGia("label"));
                toolTip.SetToolTip(txtGiaBQGQ, ThamSo.LayTooltipPhuongPhapTinhGia("textbox"));

                // Ẩn/Hiện combo Mã số và cột trong DataGridView
                string phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;
                if (phuongPhapXuatKho == "FIFO")
                {
                    cmbMaSanPham.Visible = false;
                    label4.Visible = false;

                    if (dgvDanhsachSP.Columns.Contains("colMaSanPham"))
                    {
                        dgvDanhsachSP.Columns["colMaSanPham"].Visible = false;
                    }
                }
                else
                {
                    cmbMaSanPham.Visible = true;
                    label4.Visible = true;

                    if (dgvDanhsachSP.Columns.Contains("colMaSanPham"))
                    {
                        dgvDanhsachSP.Columns["colMaSanPham"].Visible = true;
                    }
                }

                // Refresh giá xuất nếu đang chọn sản phẩm
                if (cmbSanPham.SelectedValue != null)
                {
                    int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
                    FillGiaXuat(idSanPham);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RefreshConfigUI Error: {ex.Message}");
            }
        }

    }
}
