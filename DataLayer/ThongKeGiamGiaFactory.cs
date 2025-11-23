using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.DataLayer
{
    public class ThongKeGiamGiaFactory
    {
        
          public DataTable ThongKeChietKhau(DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();

            SqlCommand cmd = new SqlCommand(@"
                  SELECT
                      ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) AS [STT],
                      CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) AS [Ngay_Ban],
                      PB.ID AS [Ma_Phieu],
                      ISNULL(KH.HO_TEN, N'(Không có)') AS [Khach_Hang],
                      PB.CHIET_KHAU AS [Chiet_Khau_Percent],
                      (PB.TONG_TIEN * PB.CHIET_KHAU / 100) AS [So_Tien_Giam],
                      ISNULL(ND.HO_TEN, N'(Phiếu cũ)') AS [Nguoi_Tao],
                      PB.TONG_TIEN AS [Tong_Tien]
                  FROM [dbo].[PHIEU_BAN] PB
                  LEFT JOIN [dbo].[KHACH_HANG] KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN [dbo].[NGUOI_DUNG] ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE PB.NGAY_BAN BETWEEN @tu_ngay AND @den_ngay
                      AND PB.CHIET_KHAU > 0
                  ORDER BY PB.NGAY_BAN DESC, PB.ID DESC
              ");

            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay;

            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Thống kê chiết khấu - THEO NHÂN VIÊN cụ thể
        /// </summary>
        /// <param name="idNguoiDung">ID nhân viên</param>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable với columns giống ThongKeChietKhau(), chỉ filter thêm theo nhân viên</returns>
        public DataTable ThongKeChietKhauTheoNhanVien(int idNguoiDung, DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();

            SqlCommand cmd = new SqlCommand(@"
                  SELECT
                      ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) AS [STT],
                      CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) AS [Ngay_Ban],
                      PB.ID AS [Ma_Phieu],
                      ISNULL(KH.HO_TEN, N'(Không có)') AS [Khach_Hang],
                      PB.CHIET_KHAU AS [Chiet_Khau_Percent],
                      (PB.TONG_TIEN * PB.CHIET_KHAU / 100) AS [So_Tien_Giam],
                      ND.HO_TEN AS [Nguoi_Tao],
                      PB.TONG_TIEN AS [Tong_Tien]
                  FROM [dbo].[PHIEU_BAN] PB
                  LEFT JOIN [dbo].[KHACH_HANG] KH ON PB.ID_KHACH_HANG = KH.ID
                  INNER JOIN [dbo].[NGUOI_DUNG] ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE PB.NGAY_BAN BETWEEN @tu_ngay AND @den_ngay
                      AND PB.CHIET_KHAU > 0
                      AND PB.ID_NGUOI_DUNG = @id_nguoi_dung
                  ORDER BY PB.NGAY_BAN DESC, PB.ID DESC
              ");

            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay;
            cmd.Parameters.Add("@id_nguoi_dung", SqlDbType.Int).Value = idNguoiDung;

            ds.Load(cmd);
            return ds;
        }

        
          public DataTable ThongKeKhuyenMai(DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();

            SqlCommand cmd = new SqlCommand(@"
                  SELECT
                      ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) AS [STT],
                      CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) AS [Ngay_Ban],
                      PB.ID AS [Ma_Phieu],
                      ISNULL(KH.HO_TEN, N'(Không có)') AS [Khach_Hang],
                      KM.TEN_KHUYEN_MAI AS [Chuong_Trinh],
                      KM.TY_LE_GIAM AS [Ty_Le_Giam],
                      (PB.TONG_TIEN * KM.TY_LE_GIAM / 100) AS [So_Tien_Giam],
                      ISNULL(ND.HO_TEN, N'(Phiếu cũ)') AS [Nguoi_Tao],
                      PB.TONG_TIEN AS [Tong_Tien]
                  FROM [dbo].[PHIEU_BAN] PB
                  INNER JOIN [dbo].[KHUYEN_MAI] KM ON PB.ID_KHUYEN_MAI = KM.ID
                  LEFT JOIN [dbo].[KHACH_HANG] KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN [dbo].[NGUOI_DUNG] ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE PB.NGAY_BAN BETWEEN @tu_ngay AND @den_ngay
                      AND PB.ID_KHUYEN_MAI IS NOT NULL
                  ORDER BY PB.NGAY_BAN DESC, PB.ID DESC
              ");

            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay;

            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Thống kê khuyến mãi - THEO NHÂN VIÊN cụ thể
        /// </summary>
        /// <param name="idNguoiDung">ID nhân viên</param>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable với columns giống ThongKeKhuyenMai(), chỉ filter thêm theo nhân viên</returns>
        public DataTable ThongKeKhuyenMaiTheoNhanVien(int idNguoiDung, DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();

            SqlCommand cmd = new SqlCommand(@"
                  SELECT
                      ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) AS [STT],
                      CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) AS [Ngay_Ban],
                      PB.ID AS [Ma_Phieu],
                      ISNULL(KH.HO_TEN, N'(Không có)') AS [Khach_Hang],
                      KM.TEN_KHUYEN_MAI AS [Chuong_Trinh],
                      KM.TY_LE_GIAM AS [Ty_Le_Giam],
                      (PB.TONG_TIEN * KM.TY_LE_GIAM / 100) AS [So_Tien_Giam],
                      ND.HO_TEN AS [Nguoi_Tao],
                      PB.TONG_TIEN AS [Tong_Tien]
                  FROM [dbo].[PHIEU_BAN] PB
                  INNER JOIN [dbo].[KHUYEN_MAI] KM ON PB.ID_KHUYEN_MAI = KM.ID
                  LEFT JOIN [dbo].[KHACH_HANG] KH ON PB.ID_KHACH_HANG = KH.ID
                  INNER JOIN [dbo].[NGUOI_DUNG] ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE PB.NGAY_BAN BETWEEN @tu_ngay AND @den_ngay
                      AND PB.ID_KHUYEN_MAI IS NOT NULL
                      AND PB.ID_NGUOI_DUNG = @id_nguoi_dung
                  ORDER BY PB.NGAY_BAN DESC, PB.ID DESC
              ");

            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay;
            cmd.Parameters.Add("@id_nguoi_dung", SqlDbType.Int).Value = idNguoiDung;

            ds.Load(cmd);
            return ds;
        }

    }
}
