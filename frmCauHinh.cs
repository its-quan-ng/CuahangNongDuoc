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
    public partial class frmCauHinh : Form
    {
        public frmCauHinh()
        {
            InitializeComponent();
        }


        private void frmCauHinh_Load(object sender, EventArgs e)
        {
            try
            {
                // Đọc cấu hình từ database
                String phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

                // Debug: Log giá trị đọc được
                System.Diagnostics.Debug.WriteLine($"[frmCauHinh] Load - PhuongPhapXuatKho: '{phuongPhapXuatKho}'");

                // Reset tất cả radio trước khi set
                radFIFO.Checked = false;
                radChiDinh.Checked = false;

                if (phuongPhapXuatKho == "FIFO")
                {
                    radFIFO.Checked = true;
                }
                else if (phuongPhapXuatKho == "CHI_DINH")
                {
                    radChiDinh.Checked = true;
                }

                String phuongPhapTinhGia = ThamSo.PhuongPhapTinhGiaXuat;

                // Debug: Log giá trị đọc được
                System.Diagnostics.Debug.WriteLine($"[frmCauHinh] Load - PhuongPhapTinhGia: '{phuongPhapTinhGia}'");

                // Reset tất cả radio trước khi set
                radAverage.Checked = false;
                radFIFOGia.Checked = false;

                if (phuongPhapTinhGia == "AVERAGE")
                {
                    radAverage.Checked = true;
                }
                else if (phuongPhapTinhGia == "FIFO")
                {
                    radFIFOGia.Checked = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi đọc cấu hình:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (radFIFO.Checked)
                {
                    ThamSo.PhuongPhapXuatKho = "FIFO";
                }
                else if (radChiDinh.Checked)
                {
                    ThamSo.PhuongPhapXuatKho = "CHI_DINH";
                }
                string savedValue = ThamSo.PhuongPhapXuatKho;

                if (radAverage.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "AVERAGE";
                }
                else if (radFIFOGia.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "FIFO";
                }

                string savedGia = ThamSo.PhuongPhapTinhGiaXuat;

                string tenXuatKho = savedValue == "FIFO" ? "FIFO (Nhập trước xuất trước)" : "Chỉ định (Chọn lô thủ công)";
                string tenTinhGia = savedGia == "FIFO" ? "Giá FIFO (Giá lô đầu tiên)" : "Giá bình quân gia quyền";

                string message = "Lưu cấu hình thành công!\n\n" +
                                $"Phương pháp xuất kho: {tenXuatKho}\n" +
                                $"Phương pháp tính giá: {tenTinhGia}";

                MessageBox.Show(
                    message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Refresh UI cho tất cả form đang mở
                RefreshAllOpenForms();

                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    "Lỗi validation:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lưu cấu hình:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close( );
        }
        private void RefreshAllOpenForms()
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form is frmBanLe)
                    {
                        ((frmBanLe)form).RefreshConfigUI();
                    }
                    else if (form is frmBanSi)
                    {
                        ((frmBanSi)form).RefreshConfigUI();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RefreshAllOpenForms Error: {ex.Message}");
            }
        }
    }
}
