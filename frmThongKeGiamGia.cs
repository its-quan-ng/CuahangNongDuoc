using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using CuahangNongduoc.DataLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmThongKeGiamGia : Form
    {
        ThongKeGiamGiaController controller = new ThongKeGiamGiaController();
        NguoiDungFactory fac = new NguoiDungFactory();
        DataTable dt;

        // Lưu DataTable gốc để in report
        private DataTable dtChietKhauGoc;
        private DataTable dtKhuyenMaiGoc;

        public frmThongKeGiamGia()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            // Validation: Kiểm tra ngày hợp lệ
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show(
                    "Từ ngày phải nhỏ hơn hoặc bằng Đến ngày!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                dtTuNgay.Focus();
                return;
            }

            try
            {
                // Lấy filter
                DateTime tuNgay = dtTuNgay.Value.Date; // Lấy date, bỏ time
                DateTime denNgay = dtDenNgay.Value.Date.AddDays(1).AddSeconds(-1); // 23:59:59
                int idNhanVien = Convert.ToInt32(cmbNhanVien.SelectedValue);

                DataTable dtChietKhau, dtKhuyenMai;

                // Lấy data theo filter nhân viên
                if (idNhanVien == 0) // Tất cả
                {
                    dtChietKhau = controller.ThongKeChietKhau(tuNgay, denNgay);
                    dtKhuyenMai = controller.ThongKeKhuyenMai(tuNgay, denNgay);
                }
                else // Theo nhân viên cụ thể
                {
                    dtChietKhau = controller.ThongKeChietKhauTheoNhanVien(idNhanVien, tuNgay, denNgay);
                    dtKhuyenMai = controller.ThongKeKhuyenMaiTheoNhanVien(idNhanVien, tuNgay, denNgay);
                }

                // Lưu DataTable gốc để in report
                dtChietKhauGoc = dtChietKhau.Copy();
                dtKhuyenMaiGoc = dtKhuyenMai.Copy();

                // DEBUG: Kiểm tra dữ liệu
                string debugInfo = $"DEBUG INFO:\n\n" +
                    $"Từ ngày: {tuNgay:dd/MM/yyyy HH:mm:ss}\n" +
                    $"Đến ngày: {denNgay:dd/MM/yyyy HH:mm:ss}\n" +
                    $"Nhân viên ID: {idNhanVien}\n\n" +
                    $"Chiết khấu:\n" +
                    $"  - Null? {dtChietKhau == null}\n" +
                    $"  - Rows: {(dtChietKhau == null ? "N/A" : dtChietKhau.Rows.Count.ToString())}\n" +
                    $"  - Columns: {(dtChietKhau == null ? "N/A" : dtChietKhau.Columns.Count.ToString())}\n\n" +
                    $"Khuyến mãi:\n" +
                    $"  - Null? {dtKhuyenMai == null}\n" +
                    $"  - Rows: {(dtKhuyenMai == null ? "N/A" : dtKhuyenMai.Rows.Count.ToString())}\n" +
                    $"  - Columns: {(dtKhuyenMai == null ? "N/A" : dtKhuyenMai.Columns.Count.ToString())}";

                MessageBox.Show(debugInfo, "Debug - Trước khi Bind");

                // Bind vào DataGridView
                dgvChietKhau.DataSource = null; // Clear trước
                dgvChietKhau.DataSource = dtChietKhau;
                
                dgvGiamGia.DataSource = null; // Clear trước
                dgvGiamGia.DataSource = dtKhuyenMai;

                // DEBUG: Kiểm tra sau khi bind
                MessageBox.Show(
                    $"Sau khi bind:\n" +
                    $"dgvChietKhau.Rows: {dgvChietKhau.Rows.Count}\n" +
                    $"dgvGiamGia.Rows: {dgvGiamGia.Rows.Count}",
                    "Debug - Sau khi Bind"
                );

                // Format columns - Bọc trong try-catch riêng
                try
                {
                    FormatDataGridViewChietKhau();
                }
                catch (Exception exFormat1)
                {
                    MessageBox.Show("Lỗi khi format Chiết khấu: " + exFormat1.Message);
                }

                try
                {
                    FormatDataGridViewKhuyenMai();
                }
                catch (Exception exFormat2)
                {
                    MessageBox.Show("Lỗi khi format Khuyến mãi: " + exFormat2.Message);
                }

                // Tính tổng kết
                try
                {
                    TinhTongKet();
                }
                catch (Exception exTong)
                {
                    MessageBox.Show("Lỗi khi tính tổng: " + exTong.Message);
                }

                // Thông báo thành công
                MessageBox.Show(
                    $"Đã tải xong!\n" +
                    $"- Chiết khấu: {(dtChietKhau == null ? 0 : dtChietKhau.Rows.Count)} phiếu\n" +
                    $"- Khuyến mãi: {(dtKhuyenMai == null ? 0 : dtKhuyenMai.Rows.Count)} phiếu",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu:\n" + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );


            }
        }
        private void FormatDataGridViewChietKhau()
        {
            if (dgvChietKhau.Columns.Count == 0) return;

            try
            {
                // Set header text
                if (dgvChietKhau.Columns["STT"] != null)
                {
                    dgvChietKhau.Columns["STT"].HeaderText = "STT";
                    dgvChietKhau.Columns["STT"].Width = 50;
                    dgvChietKhau.Columns["STT"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvChietKhau.Columns["Ngay_Ban"] != null)
                {
                    dgvChietKhau.Columns["Ngay_Ban"].HeaderText = "Ngày bán";
                    dgvChietKhau.Columns["Ngay_Ban"].Width = 100;
                    dgvChietKhau.Columns["Ngay_Ban"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvChietKhau.Columns["Ma_Phieu"] != null)
                {
                    dgvChietKhau.Columns["Ma_Phieu"].HeaderText = "Mã phiếu";
                    dgvChietKhau.Columns["Ma_Phieu"].Width = 80;
                    dgvChietKhau.Columns["Ma_Phieu"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvChietKhau.Columns["Khach_Hang"] != null)
                {
                    dgvChietKhau.Columns["Khach_Hang"].HeaderText = "Khách hàng";
                    dgvChietKhau.Columns["Khach_Hang"].Width = 150;
                }

                if (dgvChietKhau.Columns["Chiet_Khau_Percent"] != null)
                {
                    dgvChietKhau.Columns["Chiet_Khau_Percent"].HeaderText = "Chiết khấu (%)";
                    dgvChietKhau.Columns["Chiet_Khau_Percent"].DefaultCellStyle.Format = "N2";
                    dgvChietKhau.Columns["Chiet_Khau_Percent"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleRight;
                    dgvChietKhau.Columns["Chiet_Khau_Percent"].Width = 110;
                }

                if (dgvChietKhau.Columns["So_Tien_Giam"] != null)
                {
                    dgvChietKhau.Columns["So_Tien_Giam"].HeaderText = "Số tiền giảm (VND)";
                    dgvChietKhau.Columns["So_Tien_Giam"].DefaultCellStyle.Format = "N0";
                    dgvChietKhau.Columns["So_Tien_Giam"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleRight;
                    dgvChietKhau.Columns["So_Tien_Giam"].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvChietKhau.Columns["So_Tien_Giam"].DefaultCellStyle.Font = new Font(dgvChietKhau.Font,
FontStyle.Bold);
                    dgvChietKhau.Columns["So_Tien_Giam"].Width = 130;
                }

                if (dgvChietKhau.Columns["Nguoi_Tao"] != null)
                {
                    dgvChietKhau.Columns["Nguoi_Tao"].HeaderText = "Người tạo";
                    dgvChietKhau.Columns["Nguoi_Tao"].Width = 120;
                }

                if (dgvChietKhau.Columns["Tong_Tien"] != null)
                {
                    dgvChietKhau.Columns["Tong_Tien"].HeaderText = "Tổng tiền";
                    dgvChietKhau.Columns["Tong_Tien"].DefaultCellStyle.Format = "N0";
                    dgvChietKhau.Columns["Tong_Tien"].DefaultCellStyle.Alignment =
DataGridViewContentAlignment.MiddleRight;
                    dgvChietKhau.Columns["Tong_Tien"].Width = 120;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Format error: " + ex.Message);
            }
        }

        // ========================================
        // METHOD: FORMAT DATAGRIDVIEW KHUYẾN MÃI
        // ========================================
        private void FormatDataGridViewKhuyenMai()
        {
            // Kiểm tra nếu chưa có columns (chưa bind data)
            if (dgvGiamGia.Columns.Count == 0)
            {
                return; // Bỏ qua, chưa có data
            }

            try
            {
                // Set header text
                if (dgvGiamGia.Columns["STT"] != null)
                {
                    dgvGiamGia.Columns["STT"].HeaderText = "STT";
                    dgvGiamGia.Columns["STT"].Width = 50;
                    dgvGiamGia.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvGiamGia.Columns["Ngay_Ban"] != null)
                {
                    dgvGiamGia.Columns["Ngay_Ban"].HeaderText = "Ngày bán";
                    dgvGiamGia.Columns["Ngay_Ban"].Width = 100;
                    dgvGiamGia.Columns["Ngay_Ban"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvGiamGia.Columns["Ma_Phieu"] != null)
                {
                    dgvGiamGia.Columns["Ma_Phieu"].HeaderText = "Mã phiếu";
                    dgvGiamGia.Columns["Ma_Phieu"].Width = 80;
                    dgvGiamGia.Columns["Ma_Phieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvGiamGia.Columns["Khach_Hang"] != null)
                {
                    dgvGiamGia.Columns["Khach_Hang"].HeaderText = "Khách hàng";
                    dgvGiamGia.Columns["Khach_Hang"].Width = 130;
                }

                if (dgvGiamGia.Columns["Chuong_Trinh"] != null)
                {
                    dgvGiamGia.Columns["Chuong_Trinh"].HeaderText = "Chương trình KM";
                    dgvGiamGia.Columns["Chuong_Trinh"].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    dgvGiamGia.Columns["Chuong_Trinh"].DefaultCellStyle.Font = new Font(dgvGiamGia.Font, FontStyle.Bold);
                    dgvGiamGia.Columns["Chuong_Trinh"].Width = 180;
                }

                if (dgvGiamGia.Columns["Ty_Le_Giam"] != null)
                {
                    dgvGiamGia.Columns["Ty_Le_Giam"].HeaderText = "Tỷ lệ giảm (%)";
                    dgvGiamGia.Columns["Ty_Le_Giam"].DefaultCellStyle.Format = "N2";
                    dgvGiamGia.Columns["Ty_Le_Giam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvGiamGia.Columns["Ty_Le_Giam"].Width = 100;
                }

                if (dgvGiamGia.Columns["So_Tien_Giam"] != null)
                {
                    dgvGiamGia.Columns["So_Tien_Giam"].HeaderText = "Số tiền giảm (VND)";
                    dgvGiamGia.Columns["So_Tien_Giam"].DefaultCellStyle.Format = "N0";
                    dgvGiamGia.Columns["So_Tien_Giam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvGiamGia.Columns["So_Tien_Giam"].DefaultCellStyle.BackColor = Color.LightGreen;
                    dgvGiamGia.Columns["So_Tien_Giam"].DefaultCellStyle.Font = new Font(dgvGiamGia.Font, FontStyle.Bold);
                    dgvGiamGia.Columns["So_Tien_Giam"].Width = 130;
                }

                if (dgvGiamGia.Columns["Nguoi_Tao"] != null)
                {
                    dgvGiamGia.Columns["Nguoi_Tao"].HeaderText = "Người tạo";
                    dgvGiamGia.Columns["Nguoi_Tao"].Width = 120;
                }

                if (dgvGiamGia.Columns["Tong_Tien"] != null)
                {
                    dgvGiamGia.Columns["Tong_Tien"].HeaderText = "Tổng tiền";
                    dgvGiamGia.Columns["Tong_Tien"].DefaultCellStyle.Format = "N0";
                    dgvGiamGia.Columns["Tong_Tien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvGiamGia.Columns["Tong_Tien"].Width = 120;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Format error: " + ex.Message);
            }
        }

        // ========================================
        // METHOD: TÍNH TỔNG KẾT
        // ========================================
        public void TinhTongKet()
        {
            try
            {
                // Lấy tab đang active
                DataGridView dgv = null;
                string loai = "";

                if (tabControl.SelectedTab == tabChietKhau)
                {
                    dgv = dgvChietKhau;
                    loai = "chiết khấu";
                }
                else if (tabControl.SelectedTab == tabGiamGia)
                {
                    dgv = dgvGiamGia;
                    loai = "khuyến mãi";
                }

                if (dgv == null || dgv.Rows.Count == 0)
                {
                    lblTongKet.Text = $"Không có dữ liệu {loai}";
                    return;
                }

                // Lấy tên nhân viên (nếu filter)
                string tenNhanVien = cmbNhanVien.Text;
                string filterText = tenNhanVien == "-- Tất cả --" ? "" : $" ({tenNhanVien})";

                // Tính tổng
                int soPhieu = dgv.Rows.Count;
                decimal tongGiam = 0;
                decimal tongDoanhThu = 0;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Tổng số tiền giảm
                    if (row.Cells["So_Tien_Giam"].Value != null &&
                        row.Cells["So_Tien_Giam"].Value != DBNull.Value)
                    {
                        tongGiam += Convert.ToDecimal(row.Cells["So_Tien_Giam"].Value);
                    }

                    // Tổng doanh thu
                    if (row.Cells["Tong_Tien"].Value != null &&
                        row.Cells["Tong_Tien"].Value != DBNull.Value)
                    {
                        tongDoanhThu += Convert.ToDecimal(row.Cells["Tong_Tien"].Value);
                    }
                }

                decimal trungBinhGiam = soPhieu > 0 ? tongGiam / soPhieu : 0;
                decimal tyLeGiam = tongDoanhThu > 0 ? (tongGiam / tongDoanhThu * 100) : 0;

                // Hiển thị với icon
                string icon = loai == "chiết khấu" ? "📊" : "🎁";

                lblTongKet.Text = $"{icon} {loai.ToUpper()}{filterText} - " +
                                  $"Tổng số phiếu: {soPhieu} | " +
                                  $"Tổng giảm: {tongGiam:N0} VND | " +
                                  $"TB/phiếu: {trungBinhGiam:N0} VND | " +
                                  $"Tỷ lệ giảm: {tyLeGiam:F2}% | " +
                                  $"Tổng doanh thu: {tongDoanhThu:N0} VND";
            }
            catch (Exception ex)
            {
                lblTongKet.Text = "Lỗi tính tổng: " + ex.Message;
            }
        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Xác định tab đang active
                DataTable dt = null;
                string reportPath = "";
                string dataSetName = "";

                if (tabControl.SelectedTab == tabChietKhau)
                {
                    // Tab Chiết khấu - Dùng DataTable gốc
                    dt = dtChietKhauGoc;
                    reportPath = Application.StartupPath + @"\Report\rptChietKhau.rdlc";
                    dataSetName = "dsChietKhau";
                }
                else if (tabControl.SelectedTab == tabGiamGia)
                {
                    // Tab Khuyến mãi - Dùng DataTable gốc
                    dt = dtKhuyenMaiGoc;
                    reportPath = Application.StartupPath + @"\Report\rptKhuyenMai.rdlc";
                    dataSetName = "dsKhuyenMai";
                }

                // 2. Kiểm tra data
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không có dữ liệu để in!\nVui lòng bấm 'Xem báo cáo' trước.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 3. Tạo form preview report
                Form frmReport = new Form();
                frmReport.Text = "Xem trước báo cáo";
                frmReport.Size = new Size(1000, 700);
                frmReport.StartPosition = FormStartPosition.CenterScreen;

                // 4. Tạo ReportViewer
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Dock = DockStyle.Fill;
                reportViewer.LocalReport.ReportPath = reportPath;

                // 5. Set parameters (thông tin cửa hàng)
                CuaHang cuaHang = ThamSo.LayCuaHang();
                string tenNhanVien = cmbNhanVien.Text; // "-- Tất cả --" hoặc tên NV

                ReportParameter[] parameters = new ReportParameter[]
                {
                      new ReportParameter("TuNgay", dtTuNgay.Value.ToString("dd/MM/yyyy")),
                      new ReportParameter("DenNgay", dtDenNgay.Value.ToString("dd/MM/yyyy")),
                      new ReportParameter("TenCuaHang", cuaHang.TenCuaHang),
                      new ReportParameter("DiaChiCuaHang", cuaHang.DiaChi),
                      new ReportParameter("DienThoaiCuaHang", cuaHang.DienThoai),
                      new ReportParameter("NhanVien", tenNhanVien) // Filter nhân viên
                };
                reportViewer.LocalReport.SetParameters(parameters);

                // 6. Bind data vào report
                ReportDataSource rds = new ReportDataSource(dataSetName, dt);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                // 7. Refresh report
                reportViewer.RefreshReport();

                // 8. Hiển thị form
                frmReport.Controls.Add(reportViewer);
                frmReport.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tạo báo cáo:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật tổng khi chuyển tab
            TinhTongKet();
        }

        private void lblTongKet_Click(object sender, EventArgs e)
        {

        }
        private void LoadNhanVien()
        {
            try
            {
                // Lấy danh sách nhân viên từ Controller
                DataTable dt = fac.DanhsachNguoiDung();

                // Kiểm tra có data không
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Không tìm thấy nhân viên trong hệ thống!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Thêm option "Tất cả" vào đầu
                DataRow rowAll = dt.NewRow();
                rowAll["ID"] = 0;  // ID = 0 nghĩa là "Tất cả"
                rowAll["HO_TEN"] = "-- Tất cả --";

                // Set các field khác để tránh lỗi (nếu có)
                if (dt.Columns.Contains("TEN_DANG_NHAP"))
                    rowAll["TEN_DANG_NHAP"] = "";
                if (dt.Columns.Contains("QUYEN_HAN"))
                    rowAll["QUYEN_HAN"] = "";

                dt.Rows.InsertAt(rowAll, 0);  // Chèn vào vị trí đầu tiên

                // Bind vào ComboBox
                cmbNhanVien.DataSource = dt;
                cmbNhanVien.DisplayMember = "HO_TEN";  // Hiển thị tên
                cmbNhanVien.ValueMember = "ID";        // Giá trị ẩn là ID

                // Chọn "Tất cả" mặc định
                cmbNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải danh sách nhân viên:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void frmThongKeGiamGia_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Set ngày mặc định: Tháng hiện tại
                DateTime now = DateTime.Now;
                dtTuNgay.Value = new DateTime(now.Year, now.Month, 1); // Ngày 1 tháng hiện tại
                dtDenNgay.Value = now; // Hôm nay

                // 2. Load danh sách nhân viên vào ComboBox
                LoadNhanVien();

                // 3. Clear data ban đầu
                dgvChietKhau.DataSource = null;
                dgvGiamGia.DataSource = null;
                lblTongKet.Text = "Chọn khoảng thời gian và bấm 'Xem báo cáo'";

                // 4. Set tab mặc định
                tabControl.SelectedIndex = 0; // Tab Chiết khấu
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi khởi tạo form:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

            }
        }

    }
}
