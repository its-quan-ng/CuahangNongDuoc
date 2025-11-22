namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Component interface cho Decorator Pattern
    /// YC4: Tính tổng tiền hóa đơn
    /// </summary>
    public interface ITongTienComponent
    {
        /// <summary>
        /// Tính tổng tiền
        /// </summary>
        decimal TinhTongTien();
    }
}
