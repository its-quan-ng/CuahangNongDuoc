using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.BusinessObject
{
    internal class ThongKeGiamGia
    {
        // 1. Ngày bán
        private DateTime m_NgayBan;
        public DateTime NgayBan
        {
            get { return m_NgayBan; }
            set { m_NgayBan = value; }
        }

        // 2. Mã phiếu
        private string m_MaPhieu;
        public string MaPhieu
        {
            get { return m_MaPhieu; }
            set { m_MaPhieu = value; }
        }

        // 3. Khách hàng
        private string m_KhachHang;
        public string KhachHang
        {
            get { return m_KhachHang; }
            set { m_KhachHang = value; }
        }
        private long m_SoTienGiam;
        public long SoTienGiam
        {
            get { return m_SoTienGiam; }
            set { m_SoTienGiam= value ; }
        }
        // 6. Người tạo
        
        private long m_TongTien;
        public long TongTien
        {
            get { return m_TongTien; }
            set { m_TongTien = value; }
        }
        // 8. (Optional) Tổng sau giảm giá
        private long m_TongSauGiam;
        public long TongSauGiam
        {
            get { return m_TongSauGiam; }
            set { m_TongSauGiam = value; }
        }

        // Constructor
        public ThongKeGiamGia()
        {
        }

        public ThongKeGiamGia(DateTime ngayBan, string maPhieu, string khachHang, string nguoiTao
                             ,long SoTienGiam, long tongTien, long tongSauGiam)
        {
            m_NgayBan = ngayBan;
            m_MaPhieu = maPhieu;
            m_KhachHang = khachHang;
            
            m_SoTienGiam=SoTienGiam ;
            m_TongTien = tongTien;
            m_TongSauGiam = tongSauGiam;
        }


    }
}
