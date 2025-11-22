using System;

namespace CuahangNongduoc.BusinessObject
{
    /// <summary>
    /// Entity: Chương trình khuyến mãi
    /// YC4: Quản lý khuyến mãi có điều kiện
    /// </summary>
    public class KhuyenMai
    {
        private String m_Id;
        public String Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private String m_TenKhuyenMai;
        public String TenKhuyenMai
        {
            get { return m_TenKhuyenMai; }
            set { m_TenKhuyenMai = value; }
        }

        private decimal m_TyLeGiam;
        public decimal TyLeGiam
        {
            get { return m_TyLeGiam; }
            set { m_TyLeGiam = value; }
        }

        private String m_DieuKienLoai; // "TONG_TIEN" hoặc "SO_LUONG"
        public String DieuKienLoai
        {
            get { return m_DieuKienLoai; }
            set { m_DieuKienLoai = value; }
        }

        private decimal m_DieuKienGiaTri;
        public decimal DieuKienGiaTri
        {
            get { return m_DieuKienGiaTri; }
            set { m_DieuKienGiaTri = value; }
        }

        private DateTime m_TuNgay;
        public DateTime TuNgay
        {
            get { return m_TuNgay; }
            set { m_TuNgay = value; }
        }

        private DateTime m_DenNgay;
        public DateTime DenNgay
        {
            get { return m_DenNgay; }
            set { m_DenNgay = value; }
        }

        private bool m_TrangThai;
        public bool TrangThai
        {
            get { return m_TrangThai; }
            set { m_TrangThai = value; }
        }

        private String m_GhiChu;
        public String GhiChu
        {
            get { return m_GhiChu; }
            set { m_GhiChu = value; }
        }
    }
}
