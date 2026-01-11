using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmInPhieuBan : Form
    {
        CuahangNongduoc.BusinessObject.PhieuBan m_PhieuBan;
        public frmInPhieuBan(CuahangNongduoc.BusinessObject.PhieuBan ph)
        {
            InitializeComponent();
            this.reportViewer.LocalReport.SubreportProcessing += new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            m_PhieuBan = ph;
        }

        void LocalReport_SubreportProcessing(object sender, Microsoft.Reporting.WinForms.SubreportProcessingEventArgs e)
        {
            e.DataSources.Clear();
            e.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("CuahangNongduoc_BusinessObject_ChiTietPhieuBan", m_PhieuBan.ChiTiet));
        }

        private void frmInPhieuBan_Load(object sender, EventArgs e)
        {
            Num2Str num = new Num2Str();
            IList<Microsoft.Reporting.WinForms.ReportParameter> param = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            CuahangNongduoc.BusinessObject.CuaHang ch = ThamSo.LayCuaHang();
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("ten_cua_hang", ch.TenCuaHang));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dia_chi", ch.DiaChi));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("dien_thoai", ch.DienThoai));
            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("bang_chu", num.NumberToString(m_PhieuBan.ConNo.ToString())));

            // YC4: Tính tổng tiền giảm (Chiết khấu + Khuyến mãi)
            decimal tongTienGiam = 0;
            try
            {
                // Tính tiền chiết khấu
                decimal tienCK = m_PhieuBan.TongTien * m_PhieuBan.ChietKhau / 100;
                tongTienGiam += tienCK;

                // Tính tiền khuyến mãi (nếu có)
                if (m_PhieuBan.IdKhuyenMai.HasValue)
                {
                    Controller.KhuyenMaiController ctrlKM = new Controller.KhuyenMaiController();
                    BusinessObject.KhuyenMai km = ctrlKM.LayKhuyenMai(m_PhieuBan.IdKhuyenMai.Value);
                    if (km != null)
                    {
                        decimal tienKM = m_PhieuBan.TongTien * km.TyLeGiam / 100;
                        tongTienGiam += tienKM;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi tính tổng tiền giảm: {ex.Message}");
                tongTienGiam = 0;
            }

            param.Add(new Microsoft.Reporting.WinForms.ReportParameter("tong_tien_giam", tongTienGiam.ToString("N0")));

            this.reportViewer.LocalReport.SetParameters(param);
            this.PhieuBanBindingSource.DataSource = m_PhieuBan;
            this.reportViewer.RefreshReport();
        }
    }
}