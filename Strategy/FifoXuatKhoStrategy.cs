using System;
using System.Collections.Generic;
using System.Data;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc.Strategy
{
    
    public class FifoXuatKhoStrategy : IXuatKhoStrategy
    {
       
        public IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat)
        {
            if (soLuongCanXuat <= 0)
            {
                return new List<MaSanPham>();
            }

            var factory = new MaSanPhamFactory();
            
            DataTable tblLo = factory.DanhsachMaSanPham(idSanPham);

            IList<MaSanPham> ds = new List<MaSanPham>();
            var ctrlSanPham = new SanPhamController();

            int soLuongConLai = soLuongCanXuat;

            foreach (DataRow row in tblLo.Rows)
            {
                if (soLuongConLai <= 0)
                    break;

                int soLuongTonLo = Convert.ToInt32(row["SO_LUONG"]);
                if (soLuongTonLo <= 0)
                    continue;

                int soLuongXuatTuLo = Math.Min(soLuongTonLo, soLuongConLai);

                var msp = new MaSanPham
                {
                    Id = Convert.ToString(row["ID"]),
                    SoLuong = soLuongXuatTuLo,
                    GiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]),
                    NgayNhap = Convert.ToDateTime(row["NGAY_NHAP"]),
                    NgaySanXuat = Convert.ToDateTime(row["NGAY_SAN_XUAT"]),
                    NgayHetHan = Convert.ToDateTime(row["NGAY_HET_HAN"]),
                    SanPham = ctrlSanPham.LaySanPham(Convert.ToInt32(row["ID_SAN_PHAM"]))
                };

                ds.Add(msp);

                soLuongConLai -= soLuongXuatTuLo;
            }

            return ds;
        }
    }
}
