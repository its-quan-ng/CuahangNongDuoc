using System;
using System.Collections.Generic;
using System.Text;

namespace CuahangNongduoc.BusinessObject
{
    public class ChiTietPhieuBan
    {
        private PhieuBan m_PhieuBan;

        public PhieuBan PhieuBan
        {
            get { return m_PhieuBan; }
            set { m_PhieuBan = value; }
        }
        private MaSanPham m_MaSP;

        public MaSanPham MaSanPham
        {
            get { return m_MaSP; }
            set { m_MaSP = value; }
        }
        private int m_SoLuong;

        public int SoLuong
        {
            get { return m_SoLuong; }
            set { m_SoLuong = value; }
        }
        private long m_DonGia;

        public long DonGia
        {
            get { return m_DonGia; }
            set { m_DonGia = value; }
        }
        private long m_ThanhTien;

        public long ThanhTien
        {
            get { return m_ThanhTien; }
            set { m_ThanhTien = value; }
        }

        // Properties for RDLC Report
        public String TenSanPham
        {
            get
            {
                if (m_MaSP == null || m_MaSP.SanPham == null)
                    return "";
                return m_MaSP.SanPham.TenSanPham ?? "";
            }
        }

        public String MaSo
        {
            get { return m_MaSP != null ? m_MaSP.Id : ""; }
        }

        public DateTime NgayHetHan
        {
            get { return m_MaSP != null ? m_MaSP.NgayHetHan : DateTime.MinValue; }
        }

    }

    public class ChiTietPhieuBan_rptSoLuongBan
    {
        private PhieuBan m_PhieuBan;

        public PhieuBan PhieuBan
        {
            get { return m_PhieuBan; }
            set { m_PhieuBan = value; }
        }

        private MaSanPham m_MaSP;

        public MaSanPham MaSanPham
        {
            get { return m_MaSP; }
            set { m_MaSP = value; }
        }

        private String m_MaSP_id;
        public String MaSanPham_id
        {
            get { return m_MaSP_id; }
            set { m_MaSP_id = value; }
        }

        private String m_MaSP_tenSP;
        public String TenSanPham
        {
            get { return m_MaSP_tenSP; }
            set { m_MaSP_tenSP = value; }
        }

        private int m_SoLuong;

        public int SoLuong
        {
            get { return m_SoLuong; }
            set { m_SoLuong = value; }
        }
        private long m_DonGia;

        public long DonGia
        {
            get { return m_DonGia; }
            set { m_DonGia = value; }
        }
        private long m_ThanhTien;

        public long ThanhTien
        {
            get { return m_ThanhTien; }
            set { m_ThanhTien = value; }
        }

    }
}
