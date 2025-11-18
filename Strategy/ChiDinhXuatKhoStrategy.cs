using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    internal class ChiDinhXuatKhoStrategy
    {
        public IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat)
        {
            
            return new List<MaSanPham>();
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
