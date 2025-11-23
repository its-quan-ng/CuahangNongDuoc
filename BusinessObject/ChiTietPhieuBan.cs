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
                try
                {
                    if (m_MaSP == null)
                        return "[MaSP NULL]";
                    if (m_MaSP.SanPham == null)
                        return "[SanPham NULL]";
                    return m_MaSP.SanPham.TenSanPham ?? "[TenSP NULL]";
                }
                catch (Exception ex)
                {
                    return $"[ERROR: {ex.Message}]";
                }
            }
        }

        public String MaSo
        {
            get
            {
                try
                {
                    return m_MaSP != null ? m_MaSP.Id : "";
                }
                catch
                {
                    return "";
                }
            }
        }

        public DateTime NgayHetHan
        {
            get
            {
                try
                {
                    return m_MaSP != null ? m_MaSP.NgayHetHan : DateTime.MinValue;
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

    }
}
