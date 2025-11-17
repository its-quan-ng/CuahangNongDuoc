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
                String phuongPhapXuatKho = ThamSo.PhuongPhapXuatKho;

                if (phuongPhapXuatKho == "FIFO")
                {
                    radFIFO.Checked = true;
                }
                else if (phuongPhapXuatKho == "CHI_DINH")
                {
                    radChiDinh.Checked = true;
                }

                String phuongPhapTinhGia = ThamSo.PhuongPhapTinhGiaXuat;

                if (phuongPhapTinhGia == "AVERAGE")
                {
                    radAverage.Checked = true;
                }
                else if (phuongPhapTinhGia == "FIFO")
                {
                    radFIFOGia.Checked = true;
                }

                chkTuDongPhanLo.Checked = ThamSo.TuDongPhanLo;
                chkHienThiLo.Checked = ThamSo.HienThiLoPhieuXuat;

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

                if (radAverage.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "AVERAGE";
                }
                else if (radFIFOGia.Checked)
                {
                    ThamSo.PhuongPhapTinhGiaXuat = "FIFO";
                }

                ThamSo.TuDongPhanLo = chkTuDongPhanLo.Checked;
                ThamSo.HienThiLoPhieuXuat = chkHienThiLo.Checked;

                MessageBox.Show(
                    "Lưu cấu hình thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

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
    }
}
