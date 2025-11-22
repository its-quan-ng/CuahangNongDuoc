using System;

namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Concrete Component: Tổng tiền hàng cơ bản (BASE)
    /// </summary>
    public class TongTienBase : ITongTienComponent
    {
        private readonly decimal m_TongTienHang;

        public TongTienBase(decimal tongTienHang)
        {
            if (tongTienHang < 0)
            {
                throw new ArgumentException("Tổng tiền hàng không thể âm!");
            }

            m_TongTienHang = tongTienHang;
        }

        public decimal TinhTongTien()
        {
            return m_TongTienHang;
        }
    }
}
