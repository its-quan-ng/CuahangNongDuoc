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
            // this.WindowState = FormWindowState.Maximized; // Đã tắt để form hiển thị đúng kích thước thiết kế

            // Thiết lập DateTimePicker
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Value = DateTime.Now;


            numTongCP.Enabled = false;
            numTongPhieu.Enabled = false;
      
            // Thiết lập DataGridView
            SetupDataGridView(dgvChiPhiVC, "Chi phí VC");
            SetupDataGridView(dgvChiPhiDV, "Chi phí DV");

          

            // Thiết lập Maximum cho NumericUpDown để tránh lỗi khi giá trị lớn
            numTongCP.Maximum = 999999999999; // 999 tỷ
            numTongCP.Minimum = 0;
            numTongCP.DecimalPlaces = 0;
            numTongCP.ThousandsSeparator = true;
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

            // Thêm cột STT (Số thứ tự) - không bind vào data, sẽ tính toán tự động
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSTT",
                HeaderText = "STT",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                ReadOnly = true
            });

            // Thêm các cột mới
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayBan",
                HeaderText = "Ngày bán",
                DataPropertyName = "Ngay_Ban",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaPhieu",
                HeaderText = "Mã phiếu",
                DataPropertyName = "Ma_Phieu",
                Width = 100
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKhachHang",
                HeaderText = "Khách hàng",
                DataPropertyName = "Khach_Hang",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colChiPhiVanChuyen",
                HeaderText = headerText,
                DataPropertyName = headerText == "Chi phí VC" ? "Chi_Phi_VC" : "Chi_Phi_DV",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Event để tự động đếm STT
            dgv.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var cell = dgv.Rows[e.RowIndex].Cells["colSTT"];
                    if (cell != null)
                    {
                        cell.Value = (e.RowIndex + 1).ToString();
                    }
                }
            };
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

                // Kiểm tra null
                if (dt == null)
                {
                    MessageBox.Show(
                        "Lỗi: Không thể lấy dữ liệu từ database!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Bind vào DataGridView
                dgvChiPhiVC.DataSource = dt;

                // Hiển thị thống kê
                if (dt != null && dt.Rows.Count > 0)
                {
                    long tongChiPhi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Chi_Phi_VC"] != DBNull.Value)
                        {
                            tongChiPhi += Convert.ToInt64(row["Chi_Phi_VC"]);
                        }
                    }


                    numTongPhieu.Value = dt.Rows.Count;
                    // Đảm bảo Maximum đủ lớn trước khi set Value
                    if (tongChiPhi > (decimal)numTongCP.Maximum)
                    {
                        numTongCP.Maximum = (decimal)tongChiPhi;
                    }
                    numTongCP.Value = Math.Min((decimal)tongChiPhi, numTongCP.Maximum);
                    numTongPhieu.ReadOnly = true;
                    numTongCP.ReadOnly = true;
                }
                else
                {
               
                    
                    string msg = string.Format(
                        "Không có dữ liệu chi phí vận chuyển từ ngày {0} đến ngày {1}!\n\nVui lòng kiểm tra:\n- Có phiếu bán trong khoảng thời gian này không?\n- Các phiếu bán có chi phí vận chuyển > 0 không?",
                        dtpTuNgay.Value.ToString("dd/MM/yyyy"),
                        dtpDenNgay.Value.ToString("dd/MM/yyyy")
                    );
                    
                    MessageBox.Show(
                        msg,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu chi phí vận chuyển:\n" + ex.Message + "\n\nChi tiết:\n" + ex.StackTrace,
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

                // Kiểm tra null
                if (dt == null)
                {
                    MessageBox.Show(
                        "Lỗi: Không thể lấy dữ liệu từ database!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Bind vào DataGridView
                dgvChiPhiDV.DataSource = dt;

                // Hiển thị thống kê
                if (dt != null && dt.Rows.Count > 0)
                {
                    long tongChiPhi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Chi_Phi_DV"] != DBNull.Value)
                        {
                            tongChiPhi += Convert.ToInt64(row["Chi_Phi_DV"]);
                        }
                    }


                    numTongPhieu.Value = dt.Rows.Count;
                    // Đảm bảo Maximum đủ lớn trước khi set Value
                    if (tongChiPhi > (decimal)numTongCP.Maximum)
                    {
                        numTongCP.Maximum = (decimal)tongChiPhi;
                    }
                    numTongCP.Value = Math.Min((decimal)tongChiPhi, numTongCP.Maximum);
                    numTongPhieu.ReadOnly = true;
                    numTongCP.ReadOnly = true;
                }
                else
                {
                    
                    
                    string msg = string.Format(
                        "Không có dữ liệu chi phí dịch vụ từ ngày {0} đến ngày {1}!\n\nVui lòng kiểm tra:\n- Có phiếu bán trong khoảng thời gian này không?\n- Các phiếu bán có chi phí dịch vụ > 0 không?",
                        dtpTuNgay.Value.ToString("dd/MM/yyyy"),
                        dtpDenNgay.Value.ToString("dd/MM/yyyy")
                    );
                    
                    MessageBox.Show(
                        msg,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu chi phí dịch vụ:\n" + ex.Message + "\n\nChi tiết:\n" + ex.StackTrace,
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

                // Kiểm tra file tồn tại
                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show(
                        "Không tìm thấy file báo cáo tại:\n" + reportPath,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // 4. Tạo ReportViewer
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Dock = DockStyle.Fill;

                reportViewer.LocalReport.ReportPath = reportPath;

                // 5. Set parameters (thông tin cửa hàng)
                CuaHang cuaHang = ThamSo.LayCuaHang();

                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("TuNgay", dtpTuNgay.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("DenNgay", dtpDenNgay.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("TenCuaHang", cuaHang.TenCuaHang ?? ""),
                    new ReportParameter("DiaChiCuaHang", cuaHang.DiaChi ?? ""),
                    new ReportParameter("DienThoaiCuaHang", cuaHang.DienThoai ?? "")
                };

                reportViewer.LocalReport.SetParameters(parameters);

                // 6. Bind data vào report
                ReportDataSource rds = new ReportDataSource(dataSetName, dt);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                // 7. Refresh report
                reportViewer.RefreshReport();

                // 9. Hiển thị form
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThongKeChiPhiPhu_Load(object sender, EventArgs e)
        {
            // Đăng ký event khi chuyển tab để cập nhật thống kê
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi chuyển tab, cập nhật thống kê nếu đã có dữ liệu
            UpdateStatisticsForCurrentTab();
        }

        private void UpdateStatisticsForCurrentTab()
        {
            try
            {
                DataTable dt = null;
                string labelText = "";
                string columnName = "";

                if (tabControl.SelectedTab == tabPage1)
                {
                    // Tab chi phí vận chuyển
                    dt = dgvChiPhiVC.DataSource as DataTable;
                    labelText = "Tổng chi phí VC:";
                    columnName = "Chi_Phi_VC";
                }
                else if (tabControl.SelectedTab == tabPage2)
                {
                    // Tab chi phí dịch vụ
                    dt = dgvChiPhiDV.DataSource as DataTable;
                    labelText = "Tổng chi phí DV:";
                    columnName = "Chi_Phi_DV";
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    long tongChiPhi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[columnName] != DBNull.Value)
                        {
                            tongChiPhi += Convert.ToInt64(row[columnName]);
                        }
                    }

                    
                    // Đảm bảo Maximum đủ lớn trước khi set Value
                    if (tongChiPhi > (decimal)numTongCP.Maximum)
                    {
                        numTongCP.Maximum = (decimal)tongChiPhi;
                    }
                    numTongCP.Value = Math.Min((decimal)tongChiPhi, numTongCP.Maximum);
                    numTongPhieu.ReadOnly = true;
                    numTongCP.ReadOnly = true;
                }
              
            }
            catch (Exception ex)
            {
                // Xử lý lỗi im lặng khi chuyển tab
                System.Diagnostics.Debug.WriteLine("UpdateStatisticsForCurrentTab Error: " + ex.Message);
            }
        }

    }
}
