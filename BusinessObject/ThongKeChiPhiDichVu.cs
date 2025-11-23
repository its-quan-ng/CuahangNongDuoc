
using System;

namespace CuahangNongduoc.BusinessObject
{
    /// <summary>
    /// Chi tiết chi phí dịch vụ từng phiếu bán (YC5)
    /// </summary>
    public class ThongKeChiPhiDichVu
    {
        private DateTime m_NgayBan;
        public DateTime NgayBan
        {
            get { return m_NgayBan; }
            set { m_NgayBan = value; }
        }

        private string m_MaPhieu;
        public string MaPhieu
        {
            get { return m_MaPhieu; }
            set { m_MaPhieu = value; }
        }

        private string m_KhachHang;
        public string KhachHang
        {
            get { return m_KhachHang; }
            set { m_KhachHang = value; }
        }

        private long m_ChiPhiDichVu;
        public long ChiPhiDichVu
        {
            get { return m_ChiPhiDichVu; }
            set { m_ChiPhiDichVu = value; }
        }

        private long m_TongTien;
        public long TongTien
        {
            get { return m_TongTien; }
            set { m_TongTien = value; }
        }

        // Constructor
        public ThongKeChiPhiDichVu()
        {
        }

        public ThongKeChiPhiDichVu(DateTime ngayBan, string maPhieu, string khachHang,
                                    long chiPhiDV, long tongTien)
        {
            m_NgayBan = ngayBan;
            m_MaPhieu = maPhieu;
            m_KhachHang = khachHang;
            m_ChiPhiDichVu = chiPhiDV;
            m_TongTien = tongTien;
        }
    }
}
