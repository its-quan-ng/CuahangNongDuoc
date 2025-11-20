using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    public class SoLuongTon
    {
        private SanPham  m_SanPham;

        public SanPham  SanPham
        {
            get { return m_SanPham; }
            set { m_SanPham = value; }
        }
        private int m_SoLuongTon;

        public int SoLuong
        {
            get { return m_SoLuongTon; }
            set { m_SoLuongTon = value; }
        }

        public String MaSanPham
        {
            get { return m_SanPham != null ? m_SanPham.Id : ""; }
        }

        public String TenSanPham
        {
            get { return m_SanPham != null ? m_SanPham.TenSanPham : ""; }
        }

        public long DonGiaNhap
        {
            get { return m_SanPham != null ? m_SanPham.DonGiaNhap : 0; }
        }

        public long GiaBanLe
        {
            get { return m_SanPham != null ? m_SanPham.GiaBanLe : 0; }
        }

        public long GiaBanSi
        {
            get { return m_SanPham != null ? m_SanPham.GiaBanSi : 0; }
        }

        public String DonViTinh
        {
            get { return m_SanPham != null && m_SanPham.DonViTinh != null ? m_SanPham.DonViTinh.Ten : ""; }
        }
    }
}
