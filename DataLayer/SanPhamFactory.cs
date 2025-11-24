using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class SanPhamFactory
    {
        DataService m_Ds = new DataService();

        public SanPhamFactory()
            {
            m_Ds.TableName = "SAN_PHAM";
        }

        public DataTable DanhsachSanPham()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM");
            ds.Load(cmd);

            return ds;
        }

        public DataTable TimMaSanPham(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);

            return ds;
        }
        
        public void TimMaSanPhamLoad(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            m_Ds.Load(cmd);
        }
        
        public DataTable TimTenSanPham(String ten)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE TEN_SAN_PHAM LIKE '%' + @ten + '%'");
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
            ds.Load(cmd);

            return ds;
        }
        
        public void TimTenSanPhamLoad(String ten)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE TEN_SAN_PHAM LIKE '%' + @ten + '%'");
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
            m_Ds.Load(cmd);
        }


        public DataTable LaySanPham(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }

        public DataTable LaySoLuongTon()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH , SP.SO_LUONG , SUM(MA.SO_LUONG) AS SO_LUONG_TON "
                + " FROM SAN_PHAM SP INNER JOIN MA_SAN_PHAM MA ON SP.ID = MA.ID_SAN_PHAM "
                + " GROUP BY SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, SP.SO_LUONG");
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Lấy báo cáo biến động tồn kho theo khoảng thời gian
        /// </summary>
        public DataTable LayBienDongTon(DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(@"
                SELECT
                    SP.ID,
                    SP.TEN_SAN_PHAM,
                    SP.DON_GIA_NHAP,
                    SP.GIA_BAN_SI,
                    SP.GIA_BAN_LE,
                    SP.ID_DON_VI_TINH,
                    ISNULL(SUM(MA.SO_LUONG), 0) AS TON_HIEN_TAI,
                    ISNULL(NHAP.SO_LUONG_NHAP, 0) AS NHAP_TRONG_KY,
                    ISNULL(XUAT.SO_LUONG_XUAT, 0) AS XUAT_TRONG_KY
                FROM SAN_PHAM SP
                LEFT JOIN MA_SAN_PHAM MA ON SP.ID = MA.ID_SAN_PHAM
                LEFT JOIN (
                    SELECT MSP.ID_SAN_PHAM, SUM(MSP.SO_LUONG) AS SO_LUONG_NHAP
                    FROM MA_SAN_PHAM MSP
                    INNER JOIN PHIEU_NHAP PN ON MSP.ID_PHIEU_NHAP = PN.ID
                    WHERE PN.NGAY_NHAP >= @TuNgay AND PN.NGAY_NHAP <= @DenNgay
                    GROUP BY MSP.ID_SAN_PHAM
                ) NHAP ON SP.ID = NHAP.ID_SAN_PHAM
                LEFT JOIN (
                    SELECT MSP.ID_SAN_PHAM, SUM(CTPB.SO_LUONG) AS SO_LUONG_XUAT
                    FROM CHI_TIET_PHIEU_BAN CTPB
                    INNER JOIN PHIEU_BAN PB ON CTPB.ID_PHIEU_BAN = PB.ID
                    INNER JOIN MA_SAN_PHAM MSP ON CTPB.ID_MA_SAN_PHAM = MSP.ID
                    WHERE PB.NGAY_BAN >= @TuNgay AND PB.NGAY_BAN <= @DenNgay
                    GROUP BY MSP.ID_SAN_PHAM
                ) XUAT ON SP.ID = XUAT.ID_SAN_PHAM
                GROUP BY SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH,
                         NHAP.SO_LUONG_NHAP, XUAT.SO_LUONG_XUAT
                HAVING ISNULL(NHAP.SO_LUONG_NHAP, 0) > 0 OR ISNULL(XUAT.SO_LUONG_XUAT, 0) > 0 OR ISNULL(SUM(MA.SO_LUONG), 0) > 0
                ORDER BY SP.TEN_SAN_PHAM
            ");
            cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = tuNgay.Date;
            cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = denNgay.Date.AddDays(1).AddSeconds(-1); // 23:59:59
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM");
            m_Ds.Load(cmd);
        }

        /// <summary>
        /// Get m_Ds để binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return m_Ds;
        }

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
