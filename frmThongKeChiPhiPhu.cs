using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc
{
    public partial class frmThongKeChiPhiPhu : Form
    {
        public frmThongKeChiPhiPhu()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Thiết lập form
            this.Text = "Thống kê chi phí phụ";
            this.WindowState = FormWindowState.Maximized;

            // Thiết lập DateTimePicker
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Value = DateTime.Now;

            // Thiết lập labels
            label3.Text = "Từ ngày:";
            label4.Text = "Đến ngày:";

            // Thiết lập buttons
            btnXemBC.Text = "Xem báo cáo";
            
            btnInBC.Text = "In báo cáo";
            
            btnThoat.Text = "Đóng";

            // Thiết lập TabControl
            tabPage1.Text = "Chi phí vận chuyển";
            tabPage2.Text = "Chi phí dịch vụ";

            // Thiết lập DataGridView
            SetupDataGridView(dgvChiPhiVC, "Chi phí VC");
            SetupDataGridView(dgvChiPhiDV, "Chi phí DV");

            // Ẩn các control không dùng
            numTongPhieu.Visible = false;
            numTongCP.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }

        private void SetupDataGridView(DataGridView dgv, string headerText)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;

            // Xóa các cột cũ
            dgv.Columns.Clear();

            // Thêm các cột mới
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayBan",
                HeaderText = "Ngày bán",
                DataPropertyName = "NgayBan",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhieu",
                HeaderText = "Mã phiếu",
                DataPropertyName = "MaPhieu",
                Width = 100
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKhachHang",
                HeaderText = "Khách hàng",
                DataPropertyName = "KhachHang",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colChiPhiVanChuyen",
                HeaderText = headerText,
                DataPropertyName = "ChiPhiVanChuyen",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTongTien",
                HeaderText = "Tổng tiền",
                DataPropertyName = "TongTien",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void btnXemBC_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra ngày hợp lệ
                if (dtpTuNgay.Value > dtpDenNgay.Value)
                {
                    MessageBox.Show(
                        "Ngày bắt đầu không được lớn hơn ngày kết thúc!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Load dữ liệu cho tab đang active
                if (tabControl.SelectedTab == tabPage1)
                {
                    LoadChiPhiVanChuyen();
                }
                else if (tabControl.SelectedTab == tabPage2)
                {
                    LoadChiPhiDichVu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadChiPhiVanChuyen()
        {
            try
            {
                // Lấy dữ liệu từ database
                // Lấy dữ liệu từ Controller
                ThongKeController ctrl = new ThongKeController();
                DataTable dt = ctrl.LayChiPhiVanChuyen(dtpTuNgay.Value, dtpDenNgay.Value);

                // Bind vào DataGridView
                dgvChiPhiVC.DataSource = dt;

                // Hiển thị thống kê
                if (dt.Rows.Count > 0)
                {
                    long tongChiPhi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        tongChiPhi += Convert.ToInt64(row["ChiPhiVanChuyen"]);
                    }

                    label1.Visible = true;
                    label2.Visible = true;
                    numTongPhieu.Visible = true;
                    numTongCP.Visible = true;

                    label1.Text = "Tổng số phiếu:";
                    label2.Text = "Tổng chi phí VC:";
                    numTongPhieu.Value = dt.Rows.Count;
                    numTongCP.Value = tongChiPhi;
                    numTongPhieu.ReadOnly = true;
                    numTongCP.ReadOnly = true;
                }
                else
                {
                    // Ẩn các controls khi không có dữ liệu
                    label1.Visible = false;
                    label2.Visible = false;
                    numTongPhieu.Visible = false;
                    numTongCP.Visible = false;
                    
                    MessageBox.Show(
                        "Không có dữ liệu trong khoảng thời gian này!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu chi phí vận chuyển:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadChiPhiDichVu()
        {
            try
            {
                // Lấy dữ liệu từ database
                // Lấy dữ liệu từ Controller
                ThongKeController ctrl = new ThongKeController();
                DataTable dt = ctrl.LayChiPhiDichVu(dtpTuNgay.Value, dtpDenNgay.Value);

                // Bind vào DataGridView
                dgvChiPhiDV.DataSource = dt;

                // Hiển thị thống kê
                if (dt.Rows.Count > 0)
                {
                    long tongChiPhi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        tongChiPhi += Convert.ToInt64(row["ChiPhiVanChuyen"]);
                    }

                    label1.Visible = true;
                    label2.Visible = true;
                    numTongPhieu.Visible = true;
                    numTongCP.Visible = true;

                    label1.Text = "Tổng số phiếu:";
                    label2.Text = "Tổng chi phí DV:";
                    numTongPhieu.Value = dt.Rows.Count;
                    numTongCP.Value = tongChiPhi;
                    numTongPhieu.ReadOnly = true;
                    numTongCP.ReadOnly = true;
                }
                else
                {
                    // Ẩn các controls khi không có dữ liệu
                    label1.Visible = false;
                    label2.Visible = false;
                    numTongPhieu.Visible = false;
                    numTongCP.Visible = false;
                    
                    MessageBox.Show(
                        "Không có dữ liệu trong khoảng thời gian này!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu chi phí dịch vụ:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Xác định tab đang active
                DataTable dt = null;
                string reportPath = "";
                string dataSetName = "";

                if (tabControl.SelectedTab == tabPage1)
                {
                    // Chi phí vận chuyển
                    dt = dgvChiPhiVC.DataSource as DataTable;
                    reportPath = Application.StartupPath + @"\Report\rptChiPhiVanChuyen.rdlc";
                    dataSetName = "dsChiPhiVC";
                }
                else if (tabControl.SelectedTab == tabPage2)
                {
                    // Chi phí dịch vụ
                    dt = dgvChiPhiDV.DataSource as DataTable;
                    reportPath = Application.StartupPath + @"\Report\rptChiPhiDichVu.rdlc";
                    dataSetName = "dsChiPhiDV";
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

                // 3. Kiểm tra file report có tồn tại không
                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show(
                        "Không tìm thấy file báo cáo:\n" + reportPath,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // 4. Tạo form preview report
                Form frmReport = new Form();
                frmReport.Text = "Xem trước báo cáo";
                frmReport.Size = new Size(1000, 700);
                frmReport.StartPosition = FormStartPosition.CenterScreen;
                frmReport.WindowState = FormWindowState.Maximized;

                // 5. Tạo ReportViewer
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Dock = DockStyle.Fill;
                reportViewer.LocalReport.ReportPath = reportPath;

                // 6. Set parameters (thông tin cửa hàng)
                CuaHang cuaHang = ThamSo.LayCuaHang();
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("TuNgay", dtpTuNgay.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("DenNgay", dtpDenNgay.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("TenCuaHang", cuaHang != null ? cuaHang.TenCuaHang : ""),
                    new ReportParameter("DiaChiCuaHang", cuaHang != null ? cuaHang.DiaChi : ""),
                    new ReportParameter("DienThoaiCuaHang", cuaHang != null ? cuaHang.DienThoai : "")
                };
                reportViewer.LocalReport.SetParameters(parameters);

                // 7. Bind data vào report
                ReportDataSource rds = new ReportDataSource(dataSetName, dt);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                // 8. Refresh report
                reportViewer.RefreshReport();

                // 9. Hiển thị form
                frmReport.Controls.Add(reportViewer);
                frmReport.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tạo báo cáo:\n" + ex.Message + "\n\nStack trace:\n" + ex.StackTrace,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThongKeChiPhiPhu_Load(object sender, EventArgs e)
        {

        }


    }
}
