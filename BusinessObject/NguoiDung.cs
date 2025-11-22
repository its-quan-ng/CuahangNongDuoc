using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

                                                                       

namespace CuahangNongduoc.BusinessObject
    {
        public class NguoiDung
        {
            public NguoiDung() { }

            public NguoiDung(int id, String tenDangNhap)
            {
                m_Id = id;
                m_TenDangNhap = tenDangNhap;
            }

            public NguoiDung(int id, String tenDangNhap, String hoTen, String quyenHan,String trangThai)
            {
                m_Id = id;
                m_TenDangNhap = tenDangNhap;
                m_HoTen = hoTen;
                m_QuyenHan = quyenHan;
            m_TrangThai= trangThai;
            }

            private int m_Id;
            public int Id
            {
                get { return m_Id; }
                set { m_Id = value; }
            }

            private String m_TenDangNhap;

            public String TenDangNhap
            {
                get { return m_TenDangNhap; }
                set { m_TenDangNhap = value; }
            }

            private String m_MatKhau;

            public String MatKhau
            {
                get { return m_MatKhau; }
                set { m_MatKhau = value; }
            }

            private String m_HoTen;

            public String HoTen
            {
                get { return m_HoTen; }
                set { m_HoTen = value; }
            }

            private String m_SoDienThoai;

            public String SoDienThoai
            {
                get { return m_SoDienThoai; }
                set { m_SoDienThoai = value; }
            }

            private String m_QuyenHan;

            public String QuyenHan
            {
                get { return m_QuyenHan; }
                set { m_QuyenHan = value; }
            }
        private String m_TrangThai;
        public String TrangThai
        {
            get { return m_TrangThai;}
            set { m_TrangThai = value;}
        }
        }
    }


