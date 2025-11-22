using System;

namespace CuahangNongduoc.Specification
{
    /// <summary>
    /// Concrete Specification: Kiểm tra số lượng sản phẩm tối thiểu
    /// VD: "Mua từ 10 sản phẩm giảm 5%"
    /// </summary>
    public class SoLuongToiThieuSpecification : IKhuyenMaiSpecification
    {
        private readonly int m_SoLuongToiThieu;

        public SoLuongToiThieuSpecification(int soLuongToiThieu)
        {
            if (soLuongToiThieu < 0)
            {
                throw new ArgumentException("Số lượng tối thiểu không thể âm!");
            }

            m_SoLuongToiThieu = soLuongToiThieu;
        }

        /// <summary>
        /// Kiểm tra: Số lượng sản phẩm >= Số lượng tối thiểu
        /// </summary>
        public bool IsSatisfiedBy(decimal tongTienHang, int soLuongSanPham)
        {
            return soLuongSanPham >= m_SoLuongToiThieu;
        }

        /// <summary>
        /// Message lỗi: Hiển thị số lượng còn thiếu
        /// </summary>
        public string GetErrorMessage(decimal tongTienHang, int soLuongSanPham)
        {
            if (soLuongSanPham >= m_SoLuongToiThieu)
                return ""; // Không có lỗi

            int conThieu = m_SoLuongToiThieu - soLuongSanPham;

            return $"Chương trình yêu cầu mua từ {m_SoLuongToiThieu} sản phẩm.\n" +
                   $"Hiện tại: {soLuongSanPham} sản phẩm\n" +
                   $"Còn thiếu: {conThieu} sản phẩm";
        }
    }
}
