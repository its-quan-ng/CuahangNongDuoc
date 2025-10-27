using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class MaSanPhanFactory
    {
        DataService m_Ds = new DataService();

        public void LoadSchema()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID = '-1'");
            m_Ds.Load(cmd);
        }

        public DataTable DanhsachMaSanPham(int sp)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM=@id AND SO_LUONG > 0");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = sp;
            ds.Load(cmd);

            return ds;
        }
        public DataTable DanhsachChiTiet(int sp)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE ID_PHIEU_NHAP=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = sp;
            ds.Load(cmd);

            return ds;
        }

        public DataTable LaySanPham(String idMaSanPham)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT SP.* FROM SAN_PHAM SP INNER JOIN MA_SAN_PHAM MSP ON SP.ID = MSP.ID_SAN_PHAM WHERE MSP.ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = idMaSanPham;
            ds.Load(cmd);
            return ds;
        }

        public DataTable LayMaSanPham(String idMaSanPham)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM MSP WHERE MSP.ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = idMaSanPham;
            ds.Load(cmd);
            return ds;
        }

        public DataTable DanhsachMaSanPhamHetHan(DateTime dt)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0 AND NGAY_HET_HAN <= @ngay");
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = dt;
            ds.Load(cmd);

            return ds;
        }

        public DataTable DanhsachMaSanPham()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM MA_SAN_PHAM WHERE SO_LUONG > 0");
            ds.Load(cmd);

            return ds;
        }

        public static void CapNhatSoLuong(String masp, int so_luong)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("UPDATE MA_SAN_PHAM SET SO_LUONG = SO_LUONG + @so WHERE ID = @id");
            cmd.Parameters.Add("@so", SqlDbType.Int).Value = so_luong;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = masp;
            ds.ExecuteNoneQuery(cmd);
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
