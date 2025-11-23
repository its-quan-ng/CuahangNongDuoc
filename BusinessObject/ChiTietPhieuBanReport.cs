using System;

namespace CuahangNongduoc.BusinessObject
{
    /// <summary>
    /// DTO cho báo cáo số lượng bán - chỉ chứa các thuộc tính cơ bản
    /// Tránh lỗi khi report truy cập vào thuộc tính lồng nhau của đối tượng null
    /// </summary>
    public class ChiTietPhieuBanReport
    {
        public string MaSanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public long DonGia { get; set; }
        public long ThanhTien { get; set; }
    }
}
