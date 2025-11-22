using System;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Specification;

namespace CuahangNongduoc.Decorator
{
    /// <summary>
    /// Concrete Decorator: Áp dụng khuyến mãi % (CÓ VALIDATE ĐIỀU KIỆN)
    /// YC4: Khuyến mãi cửa hàng
    /// </summary>
    public class KhuyenMaiDecorator : TongTienDecorator
    {
        private readonly KhuyenMai m_KhuyenMai;
        private readonly decimal m_TongHangGoc;
        private readonly int m_SoLuongSanPham;
        private readonly IKhuyenMaiSpecification m_Specification;

        public KhuyenMaiDecorator(
            ITongTienComponent component,
            KhuyenMai khuyenMai,
            decimal tongHangGoc,
            int soLuongSanPham)
            : base(component)
        {
            if (khuyenMai == null)
                throw new ArgumentNullException("Khuyến mãi không thể null!");

            m_KhuyenMai = khuyenMai;
            m_TongHangGoc = tongHangGoc;
            m_SoLuongSanPham = soLuongSanPham;

            // Tạo Specification theo loại điều kiện
            if (khuyenMai.DieuKienLoai == "TONG_TIEN")
            {
                m_Specification = new TongTienToiThieuSpecification(khuyenMai.DieuKienGiaTri);
            }
            else if (khuyenMai.DieuKienLoai == "SO_LUONG")
            {
                m_Specification = new SoLuongToiThieuSpecification((int)khuyenMai.DieuKienGiaTri);
            }
            else
            {
                throw new ArgumentException("Loại điều kiện không hợp lệ: " + khuyenMai.DieuKienLoai);
            }
        }

        public override decimal TinhTongTien()
        {
            decimal tongTruoc = m_Component.TinhTongTien();

            // Validate điều kiện
            if (!m_Specification.IsSatisfiedBy(m_TongHangGoc, m_SoLuongSanPham))
            {
                throw new InvalidOperationException(
                    m_Specification.GetErrorMessage(m_TongHangGoc, m_SoLuongSanPham)
                );
            }

            // Tính tiền khuyến mãi (áp dụng trên tổng hàng gốc)
            decimal tienKhuyenMai = m_TongHangGoc * m_KhuyenMai.TyLeGiam / 100;

            return tongTruoc - tienKhuyenMai;
        }

        /// <summary>
        /// Helper: Tính số tiền khuyến mãi (để UI hiển thị)
        /// </summary>
        public decimal LayTienKhuyenMai()
        {
            if (!m_Specification.IsSatisfiedBy(m_TongHangGoc, m_SoLuongSanPham))
                return 0;

            return m_TongHangGoc * m_KhuyenMai.TyLeGiam / 100;
        }

        /// <summary>
        /// Kiểm tra điều kiện có thỏa mãn không (không throw exception)
        /// </summary>
        public bool KiemTraDieuKien()
        {
            return m_Specification.IsSatisfiedBy(m_TongHangGoc, m_SoLuongSanPham);
        }

        /// <summary>
        /// Lấy error message nếu không thỏa điều kiện
        /// </summary>
        public string LayErrorMessage()
        {
            return m_Specification.GetErrorMessage(m_TongHangGoc, m_SoLuongSanPham);
        }
    }
}
