using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmSoLuongTon : Form
    {
        public frmSoLuongTon()
        {
            InitializeComponent();
        }

        private void frmSoLuongTon_Load(object sender, EventArgs e)
        {
            // Set giá trị mặc định cho DateTimePicker
            dtTuNgay.Value = DateTime.Now.AddMonths(-1); // Mặc định: 1 tháng trước
            dtDenNgay.Value = DateTime.Now;

            // Load data mặc định (1 tháng gần nhất)
            LoadBienDongTon();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            // Validate
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("'Từ ngày' phải nhỏ hơn hoặc bằng 'Đến ngày'!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtTuNgay.Focus();
                return;
            }

            LoadBienDongTon();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xem tất cả từ đầu đến hiện tại
            dtTuNgay.Value = new DateTime(2000, 1, 1); // Từ năm 2000
            dtDenNgay.Value = DateTime.Now;
            LoadBienDongTon();
        }

        private void LoadBienDongTon()
        {
            try
            {
                DateTime tuNgay = dtTuNgay.Value.Date;
                DateTime denNgay = dtDenNgay.Value.Date;

                IList<CuahangNongduoc.BusinessObject.SoLuongTon> data =
                    CuahangNongduoc.Controller.SanPhamController.LayBienDongTon(tuNgay, denNgay);

                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian này!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Vẫn bind empty list để clear report
                    this.SoLuongTonBindingSource.DataSource = new List<CuahangNongduoc.BusinessObject.SoLuongTon>();
                    this.reportViewer.RefreshReport();
                    return;
                }

                // Process data để đảm bảo không có null
                IList<CuahangNongduoc.BusinessObject.SoLuongTon> processedData =
                    new List<CuahangNongduoc.BusinessObject.SoLuongTon>();

                foreach (var item in data)
                {
                    if (item != null)
                    {
                        var sanPham = item.SanPham;

                        if (sanPham == null)
                        {
                            sanPham = new CuahangNongduoc.BusinessObject.SanPham();
                            item.SanPham = sanPham;
                        }

                        if (string.IsNullOrEmpty(sanPham.Id))
                        {
                            sanPham.Id = "";
                        }
                        if (string.IsNullOrEmpty(sanPham.TenSanPham))
                        {
                            sanPham.TenSanPham = "";
                        }

                        if (sanPham.DonViTinh == null)
                        {
                            sanPham.DonViTinh = new CuahangNongduoc.BusinessObject.DonViTinh();
                            sanPham.DonViTinh.Ten = "";
                        }
                        else if (string.IsNullOrEmpty(sanPham.DonViTinh.Ten))
                        {
                            sanPham.DonViTinh.Ten = "";
                        }

                        processedData.Add(item);
                    }
                }

                this.SoLuongTonBindingSource.DataSource = processedData;
                this.reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
