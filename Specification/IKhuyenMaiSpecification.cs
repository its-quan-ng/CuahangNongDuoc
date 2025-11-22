namespace CuahangNongduoc.Specification
{
    /// <summary>
    /// Specification Pattern: Interface kiểm tra điều kiện khuyến mãi
    /// YC4: Validate điều kiện tổng tiền hoặc số lượng
    /// </summary>
    public interface IKhuyenMaiSpecification
    {
        /// <summary>
        /// Kiểm tra điều kiện có thỏa mãn không
        /// </summary>
        /// <param name="tongTienHang">Tổng tiền hàng (chưa cộng chi phí)</param>
        /// <param name="soLuongSanPham">Tổng số lượng sản phẩm</param>
        /// <returns>True nếu đủ điều kiện, False nếu không đủ</returns>
        bool IsSatisfiedBy(decimal tongTienHang, int soLuongSanPham);

        /// <summary>
        /// Lấy thông báo lỗi nếu không thỏa điều kiện
        /// </summary>
        string GetErrorMessage(decimal tongTienHang, int soLuongSanPham);
    }
}
