using System;
using System.Collections.Generic;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Strategy
{
    /// <summary>
    /// Định nghĩa chiến lược chọn các lô hàng để xuất kho.
    /// </summary>
    public interface IXuatKhoStrategy
    {
        /// <summary>
        /// Chọn các lô hàng để xuất kho theo thuật toán cụ thể.
        /// </summary>
        /// <param name="idSanPham">ID sản phẩm cần xuất.</param>
        /// <param name="soLuongCanXuat">Số lượng cần xuất.</param>
        /// <returns>Danh sách các mã sản phẩm được chọn, theo thứ tự xuất.</returns>
        IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat);
    }
}
