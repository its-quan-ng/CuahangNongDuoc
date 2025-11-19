using System;
using System.Collections.Generic;
using System.Data;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc.Strategy
{
    /// <summary>
    /// Strategy: FEFO (First Expired First Out)
    /// Tự động chọn lô theo ngày hết hạn sớm nhất
    /// Yêu cầu: "hệ thống tự tính và phân lô theo ngày hết hạn trước sẽ xuất trước"
    /// </summary>
    public class FifoXuatKhoStrategy : IXuatKhoStrategy
    {
        /// <summary>
        /// Chọn lô xuất theo FEFO (ORDER BY NGAY_HET_HAN ASC)
        /// </summary>
        public IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat)
        {
            if (soLuongCanXuat <= 0)
            {
                throw new ArgumentException("Số lượng cần xuất phải lớn hơn 0!");
            }

            var factory = new MaSanPhamFactory();

            // ⭐ QUAN TRỌNG: Dùng LayDanhSachLoConHang() - đã có ORDER BY NGAY_HET_HAN ASC
            DataTable tblLo = factory.LayDanhSachLoConHang(idSanPham);

            if (tblLo == null || tblLo.Rows.Count == 0)
            {
                throw new InvalidOperationException(
                    $"Sản phẩm ID {idSanPham} chưa có lô nào trong kho!\n" +
                    $"Vui lòng nhập hàng trước khi bán."
                );
            }

            IList<MaSanPham> ds = new List<MaSanPham>();

            int soLuongConLai = soLuongCanXuat;

            foreach (DataRow row in tblLo.Rows)
            {
                if (soLuongConLai <= 0)
                    break;

                int soLuongTonLo = Convert.ToInt32(row["SO_LUONG"]);
                if (soLuongTonLo <= 0)
                    continue;

                // Lấy số lượng từ lô này (không vượt quá số lượng tồn)
                int soLuongXuatTuLo = Math.Min(soLuongTonLo, soLuongConLai);

                var msp = new MaSanPham
                {
                    Id = Convert.ToString(row["ID"]),
                    SoLuong = soLuongXuatTuLo,
                    GiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]),
                    NgayNhap = Convert.ToDateTime(row["NGAY_NHAP"]),
                    NgaySanXuat = Convert.ToDateTime(row["NGAY_SAN_XUAT"]),
                    NgayHetHan = Convert.ToDateTime(row["NGAY_HET_HAN"])
                };

                ds.Add(msp);

                System.Diagnostics.Debug.WriteLine(
                    $"[FEFO] Chọn lô {msp.Id}: {soLuongXuatTuLo} sản phẩm (HSD: {msp.NgayHetHan:dd/MM/yyyy})"
                );

                soLuongConLai -= soLuongXuatTuLo;
            }

            // Kiểm tra có đủ hàng không
            if (soLuongConLai > 0)
            {
                int soLuongCoSan = soLuongCanXuat - soLuongConLai;
                throw new InvalidOperationException(
                    $"Không đủ hàng trong kho!\n" +
                    $"Cần: {soLuongCanXuat}\n" +
                    $"Có sẵn: {soLuongCoSan}\n" +
                    $"Thiếu: {soLuongConLai}"
                );
            }

            System.Diagnostics.Debug.WriteLine($"[FEFO] Đã chọn {ds.Count} lô, tổng {soLuongCanXuat} sản phẩm");

            return ds;
        }
    }
}
