using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    /// <summary>
    /// Strategy: FIFO COSTING (First In First Out)
    /// Tính giá xuất = Giá nhập của lô đầu tiên (ORDER BY NGAY_NHAP ASC)
    /// Yêu cầu: "nhập trước xuất trước" (cho giá xuất)
    /// </summary>
    public class FifoGiaStrategy : ITinhGiaXuatStrategy
    {
        public long TinhGiaXuat(IList<MaSanPham> danhSachLoXuat)
        {
            if (danhSachLoXuat == null || danhSachLoXuat.Count == 0)
            {
                throw new ArgumentException("Danh sách lô xuất không được rỗng!");
            }

            MaSanPham loXuatDauTien = danhSachLoXuat[0];
            long giaXuat = loXuatDauTien.GiaNhap;

            return giaXuat;
        }
    }
}
