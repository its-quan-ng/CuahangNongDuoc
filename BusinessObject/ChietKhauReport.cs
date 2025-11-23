using System;

namespace CuahangNongduoc.BusinessObject
{
    /// <summary>
    /// DTO cho báo cáo chiết khấu
    /// Chứa thông tin về các giao dịch bán hàng có chiết khấu
    /// </summary>
    public class ChietKhauReport
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int colSTT { get; set; }

        /// <summary>
        /// Ngày bán hàng
        /// </summary>
        public DateTime colNgayBan { get; set; }

        /// <summary>
        /// Mã phiếu bán
        /// </summary>
        public string colMaPhieu { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string colKhachHang { get; set; }

        /// <summary>
        /// Phần trăm chiết khấu (%)
        /// </summary>
        public decimal colChietKhau { get; set; }

        /// <summary>
        /// Số tiền được giảm (VND)
        /// </summary>
        public decimal colSoTienGiam { get; set; }

        /// <summary>
        /// Người tạo phiếu
        /// </summary>
        public string colNguoiTao { get; set; }

        /// <summary>
        /// Tổng tiền sau khi giảm
        /// </summary>
        public decimal colTongTien { get; set; }
    }
}

