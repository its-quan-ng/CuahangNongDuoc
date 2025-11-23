using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class ThongKeFactory
    {
        DataService ds = new DataService();

        public DataTable LayChiPhiVanChuyen(DateTime tuNgay, DateTime denNgay)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT 
                    PB.NGAY_BAN as NgayBan,
                    PB.ID as MaPhieu,
                    KH.HO_TEN as KhachHang,
                    ISNULL(PB.CHI_PHI_VAN_CHUYEN, 0) as ChiPhiVanChuyen,
                    PB.TONG_TIEN as TongTien
                FROM PHIEU_BAN PB
                INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                WHERE CAST(PB.NGAY_BAN AS DATE) >= CAST(@tuNgay AS DATE)
                    AND CAST(PB.NGAY_BAN AS DATE) <= CAST(@denNgay AS DATE)
                    AND ISNULL(PB.CHI_PHI_VAN_CHUYEN, 0) > 0
                ORDER BY PB.NGAY_BAN DESC");

            cmd.Parameters.Add("@tuNgay", SqlDbType.DateTime).Value = tuNgay.Date;
            cmd.Parameters.Add("@denNgay", SqlDbType.DateTime).Value = denNgay.Date;

            ds.Load(cmd);
            return ds;
        }

        public DataTable LayChiPhiDichVu(DateTime tuNgay, DateTime denNgay)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT 
                    PB.NGAY_BAN as NgayBan,
                    PB.ID as MaPhieu,
                    KH.HO_TEN as KhachHang,
                    ISNULL(PB.CHI_PHI_DICH_VU, 0) as ChiPhiVanChuyen,
                    PB.TONG_TIEN as TongTien
                FROM PHIEU_BAN PB
                INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                WHERE CAST(PB.NGAY_BAN AS DATE) >= CAST(@tuNgay AS DATE)
                    AND CAST(PB.NGAY_BAN AS DATE) <= CAST(@denNgay AS DATE)
                    AND ISNULL(PB.CHI_PHI_DICH_VU, 0) > 0
                ORDER BY PB.NGAY_BAN DESC");

            cmd.Parameters.Add("@tuNgay", SqlDbType.DateTime).Value = tuNgay.Date;
            cmd.Parameters.Add("@denNgay", SqlDbType.DateTime).Value = denNgay.Date;

            ds.Load(cmd);
            return ds;
        }
    }
}
