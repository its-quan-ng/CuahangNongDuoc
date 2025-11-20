using System;
using System.Collections;
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
    public partial class frmNhapHang : Form
    {
        SanPhamController ctrlSanPham = new SanPhamController();
        PhieuNhapController ctrl = new PhieuNhapController();
        MaSanPhamController ctrlMaSP = new MaSanPhamController();
        NhaCungCapController ctrlNCC = new NhaCungCapController();
        PhieuNhap m_PhieuNhap = null;


        Controll status = Controll.Normal;

        public frmNhapHang()
        {
            InitializeComponent();
            status = Controll.AddNew;
            dataGridView.AutoGenerateColumns = false;
        }
        public frmNhapHang(PhieuNhapController ctrlPN)
            : this()
        {
            this.ctrl = ctrlPN;
            status = Controll.Normal;
        }


        void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (status == Controll.Normal)
            {
                if (string.IsNullOrWhiteSpace(txtMaPhieu.Text))
                    return;

                if (!int.TryParse(txtMaPhieu.Text, out int maPhieu))
                {
                    System.Diagnostics.Debug.WriteLine($"Mã phiếu không hợp lệ: {txtMaPhieu.Text}");
                    return;
                }

                ctrlMaSP.HienThiChiTietPhieuNhap(maPhieu, dataGridView);

            }
        }


        private void frmNhapHang_Load(object sender, EventArgs e)
        {

            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
            ctrlNCC.HienthiAutoComboBox(cmbNhaCungCap);



            ctrl.HienthiPhieuNhap(bindingNavigator, txtMaPhieu, cmbNhaCungCap, dtNgayNhap, numTongTien, numDaTra, numConNo);
            bindingNavigator.BindingSource.CurrentChanged -= new EventHandler(BindingSource_CurrentChanged);
            bindingNavigator.BindingSource.CurrentChanged += new EventHandler(BindingSource_CurrentChanged);


            if (status == Controll.AddNew)
            {
                txtMaPhieu.Text = ThamSo.LayMaPhieuNhap().ToString();
                Allow(true);
                // Phiếu mới → DataGridView trống (không query)
                dataGridView.DataSource = null;
            }
            else
            {
                Allow(false);
                // Phiếu cũ → Load chi tiết từ database
                ctrlMaSP.HienThiChiTietPhieuNhap(Convert.ToInt32(txtMaPhieu.Text), dataGridView);
            }

            cmbSanPham.SelectedIndexChanged += cmbSanPham_SelectedIndexChanged;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maLo = txtMaSo.Text.Trim();
            MaSanPham masp = ctrlMaSP.LayMaSanPham(maLo);

            if (masp == null)
            {
                foreach (DataGridViewRow view in dataGridView.Rows)
                {
                    if (txtMaSo.Text.Trim().Equals(view.Cells["colMaSanPham"].Value))
                    {
                        MessageBox.Show("Mã sản phẩm này đã tồn tại trong danh sách! Vui lòng nhập lại !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                if (txtMaSo.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Mã sản phẩm !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (numGiaNhap.Value <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Đơn giá !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (numSoLuong.Value <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Số lượng !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dtNgaySanXuat.Value >= dtNgayHetHan.Value)
                {
                    MessageBox.Show("Ngày hết hạn phải lớn hơn ngày sản xuất!", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    numTongTien.Value += numThanhTien.Value;

                    // Gọi NewRow() TRƯỚC để trigger LoadSchema()
                    DataRow row = ctrlMaSP.NewRow();

                    // SAU ĐÓ mới lấy DataTable (đã có columns)
                    DataTable dt = ctrlMaSP.GetCurrentDataTable();

                    if (!dt.Columns.Contains("TEN_SAN_PHAM"))
                    {
                        dt.Columns.Add("TEN_SAN_PHAM", typeof(string));
                    }

                    // Fill data vào row
                    row["ID_SAN_PHAM"] = cmbSanPham.SelectedValue;
                    row["ID_PHIEU_NHAP"] = txtMaPhieu.Text;
                    row["ID"] = txtMaSo.Text;
                    row["DON_GIA_NHAP"] = numGiaNhap.Value;
                    row["SO_LUONG"] = numSoLuong.Value;
                    row["NGAY_NHAP"] = dtNgaySanXuat.Value.Date;
                    row["NGAY_SAN_XUAT"] = dtNgaySanXuat.Value.Date;
                    row["NGAY_HET_HAN"] = dtNgayHetHan.Value.Date;
                    row["TEN_SAN_PHAM"] = cmbSanPham.Text;

                    ctrlMaSP.Add(row);

                    dataGridView.DataSource = null;
                    dataGridView.DataSource = dt;

                    txtMaSo.Clear();
                    cmbSanPham.SelectedIndex = -1;
                    numGiaNhap.Value = 0;
                    numSoLuong.Value = 0;
                    numThanhTien.Value = 0;
                    dtNgaySanXuat.Value = DateTime.Now;
                    dtNgayHetHan.Value = DateTime.Now.AddMonths(12);

                    cmbSanPham.Focus();

                    MessageBox.Show(
                        $"Đã thêm lô {row["ID"]} vào phiếu!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            else
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại! Vui lòng nhập lại !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void numGiaNhap_ValueChanged(object sender, EventArgs e)
        {
            numThanhTien.Value = numGiaNhap.Value * numSoLuong.Value;
        }



        private void toolLuuThoat_Click(object sender, EventArgs e)
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
            ctrlMaSP.Save();

            ctrl.Update();
        }
        void ThemMoi()
        {
            DataRow row = ctrl.NewRow();
            row["ID"] = txtMaPhieu.Text;
            row["NGAY_NHAP"] = dtNgayNhap.Value

               .Date;
            row["TONG_TIEN"] = numTongTien.Value;
            row["ID_NHA_CUNG_CAP"] = cmbNhaCungCap.SelectedValue;
            row["DA_TRA"] = numDaTra.Value;
            row["CON_NO"] = numConNo.Value;
            ctrl.Add(row);

            PhieuNhapController ctrlPN = new PhieuNhapController();

            if (ctrlPN.LayPhieuNhap(Convert.ToInt32(txtMaPhieu.Text)) != null)
            {
                MessageBox.Show("Mã Phiếu nhập này đã tồn tại !", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ThamSo.LaSoNguyen(txtMaPhieu.Text))
            {
                long so = Convert.ToInt64(txtMaPhieu.Text);
                if (so >= ThamSo.LayMaPhieuNhap())
                {
                    ThamSo.GanMaPhieuNhap(so + 1);
                }
            }

            ctrl.Save();
            ctrlMaSP.Save();

            SanPhamController ctrlSP = new SanPhamController();
            foreach (DataGridViewRow view in dataGridView.Rows)
            {
                if (view.DataBoundItem != null)
                {
                    DataRowView rowView = (DataRowView)view.DataBoundItem;
                    int idSanPham = Convert.ToInt32(rowView["ID_SAN_PHAM"]);
                    long giaNhap = Convert.ToInt64(rowView["DON_GIA_NHAP"]);
                    long soLuong = Convert.ToInt64(rowView["SO_LUONG"]);

                    ctrlSP.CapNhatGiaNhap(idSanPham, giaNhap, soLuong);
                }
            }

        }

        private void toolLuuThem_Click(object sender, EventArgs e)
        {
            // Tạo Controller MỚI cho phiếu mới
            ctrl = new PhieuNhapController();
            ctrlMaSP = new MaSanPhamController();  // ← Tạo MaSanPhamController mới!

            status = Controll.AddNew;

            txtMaPhieu.Text = ThamSo.LayMaPhieuNhap().ToString();
            numTongTien.Value = 0;
            numDaTra.Value = 0;
            numConNo.Value = 0;

            // Khi thêm phiếu mới → DataGridView trống (không query)
            dataGridView.DataSource = null;

            this.Allow(true);
        }

        private void toolSavePrint_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                MessageBox.Show("Vui lòng lưu lại Phiếu nhập hiện tại!", "Phieu Nhap", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                String ma_phieu = txtMaPhieu.Text;

                PhieuNhapController ctrlPN = new PhieuNhapController();

                CuahangNongduoc.BusinessObject.PhieuNhap ph = ctrlPN.LayPhieuNhap(Convert.ToInt32(ma_phieu));

                frmInPhieuNhap PhieuNhap = new frmInPhieuNhap(ph);

                PhieuNhap.Show();
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            if (status != Controll.Normal)
            {
                if (MessageBox.Show("Bạn có muốn lưu lại Phiếu nhập này không?", "Phieu Nhap Hang", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Luu();
                }
            }

            this.Close();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham SanPham = new frmSanPham();
            SanPham.ShowDialog();
            ctrlSanPham.HienthiAutoComboBox(cmbSanPham);
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Log chi tiết để debug
            System.Diagnostics.Debug.WriteLine($"DataGridView DataError: {e.Exception?.Message}");
            System.Diagnostics.Debug.WriteLine($"Row: {e.RowIndex}, Column: {e.ColumnIndex}");

            // Cancel để không crash app
            e.Cancel = true;

            // Thông báo user
            MessageBox.Show(
                "Có lỗi khi hiển thị dữ liệu trong bảng.\nVui lòng kiểm tra lại giá trị đã chọn.",
                "Cảnh báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        void Allow(bool val)
        {
            txtMaPhieu.Enabled = val;
            cmbNhaCungCap.Enabled = val;
            dtNgayNhap.Enabled = val;
            numTongTien.Enabled = val;
            numDaTra.Enabled = val;
            numConNo.Enabled = val;
            btnAdd.Enabled = val;
            btnRemove.Enabled = val;
            dataGridView.Enabled = val;
        }

        private void toolChinhsua_Click(object sender, EventArgs e)
        {
            status = Controll.Edit;
            this.Allow(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa Phiếu Nhập này không?", "Phieu Nhap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
                numTongTien.Value -= Convert.ToInt64(row["DON_GIA_NHAP"]) * Convert.ToInt64(row["SO_LUONG"]);
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn xóa Phiếu Nhập này không?", "Phieu Nhap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                numTongTien.Value -= Convert.ToInt64(e.Row.Cells["colDonGiaNhap"].Value) * Convert.ToInt64(e.Row.Cells["colSoLuong"].Value);
            }
        }

        private void toolXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Phieu Nhap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
                ctrl.Save();
            }
        }

        private void numDaTra_ValueChanged(object sender, EventArgs e)
        {
            numConNo.Value = numTongTien.Value - numDaTra.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmNhaCungCap NCC = new frmNhaCungCap();
            NCC.ShowDialog();
            ctrlNCC.HienthiAutoComboBox(cmbNhaCungCap);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (status != Controll.AddNew)
            {
                if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.DataBoundItem != null)
                {
                    try
                    {
                        DataRowView row = (DataRowView)dataGridView.CurrentRow.DataBoundItem;

                        txtMaSo.Text = row["ID"].ToString();
                        if (row["ID_SAN_PHAM"] != DBNull.Value)
                        {
                            cmbSanPham.SelectedValue = Convert.ToInt32(row["ID_SAN_PHAM"]);
                        }

                        numGiaNhap.Value = Convert.ToDecimal(row["DON_GIA_NHAP"]);
                        numSoLuong.Value = Convert.ToDecimal(row["SO_LUONG"]);
                        dtNgaySanXuat.Value = Convert.ToDateTime(row["NGAY_SAN_XUAT"]);
                        dtNgayHetHan.Value = Convert.ToDateTime(row["NGAY_HET_HAN"]);
                        numThanhTien.Value = numGiaNhap.Value * numSoLuong.Value;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"SelectionChanged Error: {ex.Message}");
                    }
                }
            }
        }

        private void cmbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status != Controll.AddNew)
                return;

            if (cmbSanPham.SelectedIndex < 0)
                return;

            string maLoMoi = TaoMaLoTuDong();
            txtMaSo.Text = maLoMoi;

            dtNgaySanXuat.Value = DateTime.Now;
            dtNgayHetHan.Value = DateTime.Now.AddMonths(12);

            numGiaNhap.Focus();
        }

        private string TaoMaLoTuDong()
        {
            string maPhieu = txtMaPhieu.Text.Trim();

            if (string.IsNullOrEmpty(maPhieu))
            {
                maPhieu = "TEMP";
            }

            int soLuongLo = 0;

            if (dataGridView.DataSource != null)
            {
                DataTable dt = dataGridView.DataSource as DataTable;
                if (dt != null)
                {
                    soLuongLo = dt.Rows.Count;
                }
            }
            else
            {
                soLuongLo = dataGridView.Rows.Count;
            }

            int stt = soLuongLo + 1;
            string maLo = $"LOT-{maPhieu}-{stt:D3}";

            bool trung = true;
            while (trung)
            {
                trung = false;

                if (dataGridView.DataSource != null)
                {
                    DataTable dt = dataGridView.DataSource as DataTable;
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ID"].ToString() == maLo)
                            {
                                trung = true;
                                stt++;
                                maLo = $"LOT-{maPhieu}-{stt:D3}";
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Cells["colMaSanPham"]?.Value?.ToString() == maLo)
                        {
                            trung = true;
                            stt++;
                            maLo = $"LOT-{maPhieu}-{stt:D3}";
                            break;
                        }
                    }
                }
            }
            return maLo;
        }
    }
}