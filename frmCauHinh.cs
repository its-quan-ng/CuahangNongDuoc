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
                // ========== ĐỌC PHƯƠNG PHÁP XUẤT KHO ==========
                String phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

                if (phuongPhapXuatKho == "FIFO")
                {
                    radFIFO.Checked = true;
                }
                else if (phuongPhapXuatKho == "CHI_DINH")
                {
                    radChiDinh.Checked = true;
                }

                // ========== ĐỌC PHƯƠNG PHÁP TÍNH GIÁ ==========
                String phuongPhapTinhGia = ThamSo.PhuongPhapTinhGiaXuat;

                if (phuongPhapTinhGia == "AVERAGE")
                {
                    radAverage.Checked = true;
                }
                else if (phuongPhapTinhGia == "FIFO")
                {
                    radFIFOGia.Checked = true;
                }

                // ========== ĐỌC CHECKBOX ==========
                chkTuDongPhanLo.Checked = ThamSo.TuDongPhanLo;
                chkHienThiLo.Checked = ThamSo.HienThiLoPhieuXuat;

                System.Diagnostics.Debug.WriteLine("frmCauHinhKho: Đọc cấu hình thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi đọc cấu hình:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                System.Diagnostics.Debug.WriteLine("frmCauHinhKho Load Error: " + ex.ToString());
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // ========== LƯU PHƯƠNG PHÁP XUẤT KHO ==========
                if (radFIFO.Checked)
                {
                    ThamSo.PhuongPhapXuatKho = "FIFO";
                }
                else if (radChiDinh.Checked)
                {
                    ThamSo.PhuongPhapXuatKho = "CHI_DINH";
                }

                // ========== LƯU PHƯƠNG PHÁP TÍNH GIÁ ==========
                if (radAverage.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "AVERAGE";
                }
                else if (radFIFOGia.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "FIFO";
                }

                // ========== LƯU CHECKBOX ==========
                ThamSo.TuDongPhanLo = chkTuDongPhanLo.Checked;
                ThamSo.HienThiLoPhieuXuat = chkHienThiLo.Checked;

                System.Diagnostics.Debug.WriteLine("frmCauHinhKho: Lưu cấu hình thành công");

                // Thông báo thành công
                MessageBox.Show(
                    "Lưu cấu hình thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Đóng form
                this.Close();
            }
            catch (ArgumentException ex)
            {
                // Lỗi validation từ ThamSo.cs
                MessageBox.Show(
                    "Lỗi validation:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                // Lỗi database hoặc lỗi khác
                MessageBox.Show(
                    "Lỗi lưu cấu hình:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                System.Diagnostics.Debug.WriteLine("frmCauHinhKho Save Error: " + ex.ToString());
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close( );
        }
    }
}
