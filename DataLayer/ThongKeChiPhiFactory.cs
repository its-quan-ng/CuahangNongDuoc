using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class ThongKeChiPhiFactory
    {
        DataService ds = new DataService();

        public DataTable LayChiPhiVanChuyen(DateTime tuNgay, DateTime denNgay)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT
                    ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) as STT,
                    CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) as Ngay_Ban,
                    PB.ID as Ma_Phieu,
                    KH.HO_TEN as Khach_Hang,
                    ISNULL(PB.CHI_PHI_VAN_CHUYEN, 0) as Chi_Phi_VC,
                    PB.TONG_TIEN as Tong_Tien_Phieu
                FROM PHIEU_BAN PB
                INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                WHERE CONVERT(DATE, PB.NGAY_BAN) >= @tuNgay
                    AND CONVERT(DATE, PB.NGAY_BAN) <= @denNgay
                    AND ISNULL(PB.CHI_PHI_VAN_CHUYEN, 0) > 0
                ORDER BY PB.NGAY_BAN DESC");

            cmd.Parameters.Add("@tuNgay", SqlDbType.Date).Value = tuNgay.Date;
            cmd.Parameters.Add("@denNgay", SqlDbType.Date).Value = denNgay.Date;

            ds.Load(cmd);
            return ds;
        }

        public DataTable LayChiPhiDichVu(DateTime tuNgay, DateTime denNgay)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT
                    ROW_NUMBER() OVER (ORDER BY PB.NGAY_BAN DESC) as STT,
                    CONVERT(VARCHAR(10), PB.NGAY_BAN, 103) as Ngay_Ban,
                    PB.ID as Ma_Phieu,
                    KH.HO_TEN as Khach_Hang,
                    ISNULL(PB.CHI_PHI_DICH_VU, 0) as Chi_Phi_DV,
                    PB.TONG_TIEN as Tong_Tien_Phieu
                FROM PHIEU_BAN PB
                INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                WHERE CONVERT(DATE, PB.NGAY_BAN) >= @tuNgay
                    AND CONVERT(DATE, PB.NGAY_BAN) <= @denNgay
                    AND ISNULL(PB.CHI_PHI_DICH_VU, 0) > 0
                ORDER BY PB.NGAY_BAN DESC");

            cmd.Parameters.Add("@tuNgay", SqlDbType.Date).Value = tuNgay.Date;
            cmd.Parameters.Add("@denNgay", SqlDbType.Date).Value = denNgay.Date;

            ds.Load(cmd);
            return ds;
        }
    }
}
