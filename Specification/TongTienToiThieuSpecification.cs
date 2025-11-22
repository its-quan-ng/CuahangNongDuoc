using System;

namespace CuahangNongduoc.Specification
{
    /// <summary>
    /// Concrete Specification: Kiểm tra tổng tiền tối thiểu
    /// VD: "Mua từ 2,000,000 đ giảm 15%"
    /// </summary>
    public class TongTienToiThieuSpecification : IKhuyenMaiSpecification
    {
        private readonly decimal m_GiaTriToiThieu;

        public TongTienToiThieuSpecification(decimal giaTriToiThieu)
        {
            if (giaTriToiThieu < 0)
            {
                throw new ArgumentException("Giá trị tối thiểu không thể âm!");
            }

            m_GiaTriToiThieu = giaTriToiThieu;
        }

        /// <summary>
        /// Kiểm tra: Tổng tiền hàng >= Giá trị tối thiểu
        /// </summary>
        public bool IsSatisfiedBy(decimal tongTienHang, int soLuongSanPham)
        {
            return tongTienHang >= m_GiaTriToiThieu;
        }

        /// <summary>
        /// Message lỗi: Hiển thị số tiền còn thiếu
        /// </summary>
        public string GetErrorMessage(decimal tongTienHang, int soLuongSanPham)
        {
            if (tongTienHang >= m_GiaTriToiThieu)
                return ""; // Không có lỗi

            decimal conThieu = m_GiaTriToiThieu - tongTienHang;

            return $"Chương trình yêu cầu mua từ {m_GiaTriToiThieu:N0} đ.\n" +
                   $"Hiện tại: {tongTienHang:N0} đ\n" +
                   $"Còn thiếu: {conThieu:N0} đ";
        }
    }
}
