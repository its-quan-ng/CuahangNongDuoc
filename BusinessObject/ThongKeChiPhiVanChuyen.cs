using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.BusinessObject
{
    public class ThongKeChiPhiVanChuyen
    {
        // Ngày bán
        private DateTime m_NgayBan;
        public DateTime NgayBan
        {
            get { return m_NgayBan; }
            set { m_NgayBan = value; }
        }

        // Mã phiếu bán
        private string m_MaPhieu;
        public string MaPhieu
        {
            get { return m_MaPhieu; }
            set { m_MaPhieu = value; }
        }

        // Tên khách hàng
        private string m_KhachHang;
        public string KhachHang
        {
            get { return m_KhachHang; }
            set { m_KhachHang = value; }
        }

        // Chi phí vận chuyển
        private long m_ChiPhiVanChuyen;
        public long ChiPhiVanChuyen
        {
            get { return m_ChiPhiVanChuyen; }
            set { m_ChiPhiVanChuyen = value; }
        }

        // Tổng tiền hóa đơn
        private long m_TongTien;
        public long TongTien
        {
            get { return m_TongTien; }
            set { m_TongTien = value; }
        }

        // Constructor
        public ThongKeChiPhiVanChuyen()
        {
        }

        public ThongKeChiPhiVanChuyen(DateTime ngayBan, string maPhieu, string khachHang,
                                       long chiPhiVC, long tongTien)
        {
            m_NgayBan = ngayBan;
            m_MaPhieu = maPhieu;
            m_KhachHang = khachHang;
            m_ChiPhiVanChuyen = chiPhiVC;
            m_TongTien = tongTien;
        }
    }

}
