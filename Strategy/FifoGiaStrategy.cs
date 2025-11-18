using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    internal class FifoGiaStrategy : ITinhGiaXuatStrategy
    {
        public long TinhGiaXuat(IList<MaSanPham> danhSachLoXuat)
        {
            if (danhSachLoXuat == null || danhSachLoXuat.Count == 0)
            {
                throw new ArgumentException("Danh sách lô xuất không được rỗng!");
            }

            
            MaSanPham loXuatDauTien = danhSachLoXuat[0];
            long giaXuat = loXuatDauTien.GiaNhap;

            System.Diagnostics.Debug.WriteLine(
                $"FIFO Price: Lấy giá lô đầu tiên (Lô {loXuatDauTien.Id}): {giaXuat:N0}đ"
            );

            return giaXuat;
        }


    }
}
