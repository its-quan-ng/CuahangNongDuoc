using System;

namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Concrete Decorator: Cộng thêm chi phí vận chuyển và dịch vụ
    /// YC3 + YC4
    /// </summary>
    public class ChiPhiDecorator : TongTienDecorator
    {
        private readonly decimal m_ChiPhiVanChuyen;
        private readonly decimal m_ChiPhiDichVu;

        public ChiPhiDecorator(
            ITongTienComponent component,
            decimal chiPhiVanChuyen,
            decimal chiPhiDichVu)
            : base(component)
        {
            if (chiPhiVanChuyen < 0)
            {
                throw new ArgumentException("Chi phí vận chuyển không thể âm!");
            }

            if (chiPhiDichVu < 0)
            {
                throw new ArgumentException("Chi phí dịch vụ không thể âm!");
            }

            m_ChiPhiVanChuyen = chiPhiVanChuyen;
            m_ChiPhiDichVu = chiPhiDichVu;
        }

        public override decimal TinhTongTien()
        {
            decimal tongTruoc = m_Component.TinhTongTien();
            decimal tongSau = tongTruoc + m_ChiPhiVanChuyen + m_ChiPhiDichVu;
            return tongSau;
        }
    }
}
