using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class KhachHangFactory
    {
        DataService m_Ds = new DataService();

        public DataTable DanhsachKhachHang(bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = @loai");
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }
        public DataTable TimHoTen(String hoten, bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }

        public DataTable TimDiaChi(String diachi, bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }

        public DataTable DanhsachKhachHang()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG");
            ds.Load(cmd);

            return ds;
        }

        public DataTable LayKhachHang(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
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
