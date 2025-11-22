using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    /// <summary>
    /// Strategy: BÌNH QUÂN GIA QUYỀN (Weighted Average)
    /// Tính giá xuất = SUM(số lượng × giá nhập) / SUM(số lượng)
    /// Yêu cầu: "bình quân gia quyền"
    /// </summary>
    public class WeightedAverageGiaStrategy : ITinhGiaXuatStrategy
    {
            public long TinhGiaXuat(IList<MaSanPham> danhSachLoXuat)
            {
                if (danhSachLoXuat == null || danhSachLoXuat.Count == 0)
                {
                    throw new ArgumentException("Danh sách lô xuất không được rỗng!");
                }

                long tongThanhTien = 0;  
                int tongSoLuong = 0;    

                foreach (var maSp in danhSachLoXuat)
                {
                    long thanhTien = maSp.GiaNhap * maSp.SoLuong;
                    tongThanhTien += thanhTien;
                    tongSoLuong += maSp.SoLuong;

                }
                long giaXuatTrungBinh = tongThanhTien / tongSoLuong;
                return giaXuatTrungBinh;
            }

        }

    }

