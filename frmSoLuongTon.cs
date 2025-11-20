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
            try
            {
                IList<CuahangNongduoc.BusinessObject.SoLuongTon> data = CuahangNongduoc.Controller.SanPhamController.LaySoLuongTon();
                
                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                IList<CuahangNongduoc.BusinessObject.SoLuongTon> processedData = new List<CuahangNongduoc.BusinessObject.SoLuongTon>();
                
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
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}