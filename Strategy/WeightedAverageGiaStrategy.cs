using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
   
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

                    
                    System.Diagnostics.Debug.WriteLine(
                        $"Lô {maSp.Id}: {maSp.SoLuong} × {maSp.GiaNhap:N0}đ = {thanhTien:N0}đ"
                    );
                }

                
                long giaXuatTrungBinh = tongThanhTien / tongSoLuong;

                System.Diagnostics.Debug.WriteLine(
                    $"→ Giá xuất TB: {tongThanhTien:N0}đ / {tongSoLuong} = {giaXuatTrungBinh:N0}đ"
                );

                return giaXuatTrungBinh;
            }

        }

    }

