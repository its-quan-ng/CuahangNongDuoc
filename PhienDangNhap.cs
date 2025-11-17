using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc
{
    public static class PhienDangNhap
    {
        private static int m_IdNguoiDung = -1;
        private static String m_TenDangNhap = String.Empty;
        private static String m_HoTen = String.Empty;
        private static String m_QuyenHan = String.Empty;
        private static bool m_DaDangNhap = false;

        /// <summary>
        /// ID người dùng
        /// </summary>
        public static int IdNguoiDung
        {
            get { return m_IdNguoiDung; }
        }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public static String TenDangNhap
        {
            get { return m_TenDangNhap; }
        }

        /// <summary>
        /// Họ tên đầy đủ
        /// </summary>
        public static String HoTen
        {
            get { return m_HoTen; }
        }

        /// <summary>
        /// Quyền hạn: "admin" hoặc "user"
        /// </summary>
        public static String QuyenHan
        {
            get { return m_QuyenHan; }
        }

        /// <summary>
        /// Đã đăng nhập chưa
        /// </summary>
        public static bool DaDangNhap
        {
            get { return m_DaDangNhap; }
        }

        /// <summary>
        /// Là Admin không
        /// </summary>
        public static bool LaAdmin
        {
            get { return m_QuyenHan.ToLower() == "admin"; }
        }

        /// <summary>
        /// Lưu thông tin đăng nhập
        /// </summary>
        public static void DangNhap(int idNguoiDung, String tenDangNhap, String hoTen, String quyenHan)
        {
            m_IdNguoiDung = idNguoiDung;
            m_TenDangNhap = tenDangNhap;
            m_HoTen = hoTen;
            m_QuyenHan = quyenHan;
            m_DaDangNhap = true;
 
          }

        /// <summary>
        /// Xóa thông tin đăng nhập
        /// </summary>
        public static void DangXuat()
        {
            m_IdNguoiDung = -1;
            m_TenDangNhap = String.Empty;
            m_HoTen = String.Empty;
            m_QuyenHan = String.Empty;
            m_DaDangNhap = false;

            System.Diagnostics.Debug.WriteLine("PhienDangNhap.DangXuat: Đã xóa phiên");
        }

        /// <summary>
        /// Lấy tên hiển thị
        /// </summary>
        public static String LayTenHienThi()
        {
            if (m_DaDangNhap)
            {
                return String.Format("{0} ({1})", m_HoTen, m_TenDangNhap);
            }
            return "Chưa đăng nhập";
        }
    } 
}


    
