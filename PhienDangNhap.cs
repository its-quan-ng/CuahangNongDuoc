using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc
{
    internal class PhienDangNhap
    {
        // Khóa đồng bộ (đảm bảo Thread Safety khi tạo instance)
        private static readonly object _lock = new object();

        // 1. Instance duy nhất của lớp (Singleton)
        private static PhienDangNhap _instance = null;

        // ========== PRIVATE FIELDS (Đã bỏ static) ==========
        private int m_IdNguoiDung = -1;
        private String m_TenDangNhap = String.Empty;
        private String m_HoTen = String.Empty;
        private String m_QuyenHan = String.Empty;
        private bool m_DaDangNhap = false;

        // 2. Constructor Private: Ngăn việc tạo đối tượng từ bên ngoài
        private PhienDangNhap() { }

        // 3. PUBLIC STATIC ACCESSOR (Điểm truy cập duy nhất)
        
        /// Lấy instance duy nhất của PhienDangNhap (Singleton)
       
        public static PhienDangNhap Instance
        {
            get
            {
                // Double-Checked Locking để tối ưu hiệu năng
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PhienDangNhap();
                        }
                    }
                }
                return _instance;
            }
        }

        //  PUBLIC PROPERTIES (Đã bỏ static) 

       
        /// ID người dùng
       
        public int IdNguoiDung => m_IdNguoiDung;

        
        /// Tên đăng nhập
        
        public String TenDangNhap => m_TenDangNhap;

        
        /// Họ tên đầy đủ
        
        public String HoTen => m_HoTen;

        
        /// Quyền hạn: "admin" hoặc "user"
     
        public String QuyenHan => m_QuyenHan;

       
        /// Đã đăng nhập chưa
        
        public bool DaDangNhap => m_DaDangNhap;

        
        /// Là Admin không (Kiểm tra phân quyền)
        
        public bool LaAdmin
        {
            get { return m_QuyenHan.Trim().ToLower() == "admin"; }
        }

        //  PUBLIC METHODS (Đã bỏ static) 

        
        /// Lưu thông tin đăng nhập
        
        public void DangNhap(int idNguoiDung, String tenDangNhap, String hoTen, String quyenHan)
        {
            m_IdNguoiDung = idNguoiDung;
            m_TenDangNhap = tenDangNhap;
            m_HoTen = hoTen;
            m_QuyenHan = quyenHan;
            m_DaDangNhap = true;

            // Ghi log
            System.Diagnostics.Debug.WriteLine($"PhienDangNhap.DangNhap: {tenDangNhap} ({hoTen}) - Quyền: {quyenHan}");
        }

        
        /// Xóa thông tin đăng nhập (Đăng xuất)
      
        public void DangXuat()
        {
            m_IdNguoiDung = -1;
            m_TenDangNhap = String.Empty;
            m_HoTen = String.Empty;
            m_QuyenHan = String.Empty;
            m_DaDangNhap = false;

            // Ghi log
            System.Diagnostics.Debug.WriteLine("PhienDangNhap.DangXuat: Đã xóa phiên");
        }

       
        /// Lấy tên hiển thị
       
        public String LayTenHienThi()
        {
            if (m_DaDangNhap)
            {
                // Trả về: Họ tên (Tên đăng nhập)
                return String.Format("{0} ({1})", m_HoTen, m_TenDangNhap);
            }
            return "Chưa đăng nhập";
        }
    }

}
