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

        private void frmBanLe_Load(object sender, EventArgs e)
        {
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);

            cmbSanPham.SelectedIndexChanged += new EventHandler(cmbSanPham_SelectedIndexChanged);
            cmbMaSanPham.SelectedIndexChanged += new EventHandler(cmbMaSanPham_SelectedIndexChanged);

            ctrlKhachHang.HienthiAutoComboBox(cmbKhachHang, false);

            ctrlPhieuBan.HienthiPhieuBan(bindingNavigator, cmbKhachHang, txtMaPhieu, dtNgayLapPhieu, numTongTien, numDaTra, numConNo, numChiPhiVanChuyen, numChiPhiDichVu);

            bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
            bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);

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
            MaSanPhamController ctrl = new MaSanPhamController();
            MaSanPham masp = ctrl.LayMaSanPham(cmbMaSanPham.SelectedValue.ToString());
            numDonGia.Value = masp.SanPham.GiaBanLe;
            txtGiaNhap.Text = masp.GiaNhap.ToString("#,###0");
            txtGiaBanSi.Text = masp.SanPham.GiaBanSi.ToString("#,###0");
            txtGiaBanLe.Text = masp.SanPham.GiaBanLe.ToString("#,###0");
            txtGiaBQGQ.Text = masp.SanPham.DonGiaNhap.ToString("#,###0");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (cmbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbMaSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Mã số sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
                int soLuongCanBan = Convert.ToInt32(numSoLuong.Value);
                string maSanPhamDaChon = cmbMaSanPham.SelectedValue.ToString();

                // Lấy thông tin mã sản phẩm đã chọn
                MaSanPham maSPDaChon = ctrlMaSanPham.LayMaSanPham(maSanPhamDaChon);
                
                if (maSPDaChon == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin mã sản phẩm!", "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra số lượng tồn kho
                if (maSPDaChon.SoLuong < soLuongCanBan)
                {
                    MessageBox.Show($"Số lượng tồn kho không đủ!\nTồn kho: {maSPDaChon.SoLuong}\nYêu cầu: {soLuongCanBan}", 
                        "Bán lẻ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy tên sản phẩm từ mã số đã chọn
                string tenSanPham = maSPDaChon.SanPham != null ? maSPDaChon.SanPham.TenSanPham : "";

                // Kiểm tra xem mã sản phẩm đã tồn tại trong DataGridView chưa
                BindingSource bs = (BindingSource)dgvDanhsachSP.DataSource;
                DataTable dt = (DataTable)bs.DataSource;
                bool daTonTai = false;
                
                foreach (DataRow existingRow in dt.Rows)
                {
                    if (Convert.ToString(existingRow["ID_MA_SAN_PHAM"]) == maSanPhamDaChon)
                    {
                        // Nếu đã tồn tại, cộng thêm số lượng
                        int soLuongCu = Convert.ToInt32(existingRow["SO_LUONG"]);
                        int soLuongMoi = soLuongCu + soLuongCanBan;
                        
                        // Kiểm tra tồn kho cho số lượng mới
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
                
                // Nếu chưa tồn tại, thêm dòng mới
                if (!daTonTai)
                {
                    DataRow row = ctrlChiTiet.NewRow();
                    row["ID_MA_SAN_PHAM"] = maSanPhamDaChon;
                    row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
                    row["DON_GIA"] = numDonGia.Value;
                    row["SO_LUONG"] = soLuongCanBan;
                    row["THANH_TIEN"] = numDonGia.Value * soLuongCanBan;
                    // Thêm tên sản phẩm từ mã số
                    if (row.Table.Columns.Contains("TEN_SAN_PHAM"))
                    {
                        row["TEN_SAN_PHAM"] = tenSanPham;
                    }
                    ctrlChiTiet.Add(row);
                }
                
                // Refresh DataGridView để hiển thị giá trị mới
                dgvDanhsachSP.Refresh();

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

                numSoLuong.Value = 0;
                numThanhTien.Value = 0;
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void toolChinhSua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            // Load dữ liệu từ database vào factory để có thể chỉnh sửa
            ctrlChiTiet = new ChiTietPhieuBanController();
            ctrlChiTiet.LoadData(Convert.ToInt32(txtMaPhieu.Text));
            // Bind DataGridView với DataTable từ factory
            BindingSource bs = new BindingSource();
            bs.DataSource = ctrlChiTiet.GetDataTable();
            dgvDanhsachSP.DataSource = bs;
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
