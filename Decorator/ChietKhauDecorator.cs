using System;

namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Concrete Decorator: Áp dụng chiết khấu % trên TỔNG HÀNG
    /// YC4: Chiết khấu nhân viên
    /// </summary>
    public class ChietKhauDecorator : TongTienDecorator
    {
        private readonly decimal m_ChietKhau; // %
        private readonly decimal m_TongHangGoc; // Tổng hàng ban đầu (để tính CK)

        public ChietKhauDecorator(
            ITongTienComponent component,
            decimal chietKhau,
            decimal tongHangGoc)
            : base(component)
        {
            if (chietKhau < 0 || chietKhau > 100)
            {
                throw new ArgumentException("Chiết khấu phải từ 0-100%!");
            }

            m_ChietKhau = chietKhau;
            m_TongHangGoc = tongHangGoc;
        }

        public override decimal TinhTongTien()
        {
            decimal tongTruoc = m_Component.TinhTongTien();

            if (m_ChietKhau == 0)
            {
                return tongTruoc; // Không có chiết khấu
            }

            // Tính tiền chiết khấu (áp dụng trên TỔNG HÀNG GỐC, không tính phí)
            decimal tienChietKhau = m_TongHangGoc * m_ChietKhau / 100;

            // Trừ chiết khấu
            decimal tongSau = tongTruoc - tienChietKhau;

            return tongSau;
        }

        /// <summary>
        /// Helper method: Tính số tiền chiết khấu (để UI hiển thị)
        /// </summary>
        public decimal LayTienChietKhau()
        {
            if (m_ChietKhau == 0)
                return 0;

            return m_TongHangGoc * m_ChietKhau / 100;
        }
    }
}
