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
