using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    /// <summary>
    /// Strategy: CHỈ ĐỊNH (Manual Selection)
    /// User tự chọn lô trong form (cmbMaSanPham) - KHÔNG dùng strategy để chọn tự động
    /// Class này chỉ để hoàn thiện Strategy Pattern cho báo cáo đồ án
    /// </summary>
    public class ChiDinhXuatKhoStrategy : IXuatKhoStrategy
    {
        public IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat)
        {
            throw new NotImplementedException(
                "Mode CHỈ ĐỊNH: User tự chọn lô trong form. Method này không được sử dụng."
            );
        }
        public bool ValidateDanhSachLoChon(IList<MaSanPham> danhSachLoUserChon, int soLuongCanXuat)
        {
            int tongSoLuong = 0;
            foreach (var maSp in danhSachLoUserChon)
            {
                tongSoLuong += maSp.SoLuong;
            }

            if (tongSoLuong != soLuongCanXuat)
            {
                throw new InvalidOperationException(
                    $"Số lượng không khớp! Cần xuất: {soLuongCanXuat}, Đã chọn: {tongSoLuong}"
                );
            }

            return true;
        }
    }
}
