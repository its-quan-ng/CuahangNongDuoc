using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class NhaCungCapFactory
    {
        DataService m_Ds = new DataService();

        public NhaCungCapFactory()
        {
            m_Ds.TableName = "NHA_CUNG_CAP";
        }

        public DataTable DanhsachNCC()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP");
            ds.Load(cmd);

            return ds;
        }
        public DataTable TimDiaChi(String diachi)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP WHERE DIA_CHI LIKE '%' + @diachi + '%' ");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            ds.Load(cmd);

            return ds;
        }
        public DataTable TimHoTen(String hoten)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP WHERE HO_TEN LIKE '%' + @hoten + '%' ");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            ds.Load(cmd);

            return ds;
        }

        public DataTable LayNCC(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP");
            m_Ds.Load(cmd);

            // Set AutoIncrement cho ID column (IDENTITY trong database)
            if (m_Ds.Columns.Contains("ID"))
            {
                m_Ds.Columns["ID"].AutoIncrement = true;
                m_Ds.Columns["ID"].AutoIncrementSeed = -1;
                m_Ds.Columns["ID"].AutoIncrementStep = -1;
                m_Ds.Columns["ID"].ReadOnly = true;
            }
        }

        /// <summary>
        /// Get m_Ds để binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return m_Ds;
        }

        /// <summary>
        /// Tìm theo địa chỉ và load vào m_Ds để binding
        /// </summary>
        public void TimDiaChiLoad(String diachi)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP WHERE DIA_CHI LIKE '%' + @diachi + '%'");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            m_Ds.Load(cmd);
        }

        /// <summary>
        /// Tìm theo họ tên và load vào m_Ds để binding
        /// </summary>
        public void TimHoTenLoad(String hoten)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM NHA_CUNG_CAP WHERE HO_TEN LIKE '%' + @hoten + '%'");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            m_Ds.Load(cmd);
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

        /// <summary>
        /// Kiểm tra xem nhà cung cấp đã được sử dụng ở bảng khác chưa
        /// </summary>
        /// <param name="nccId">ID nhà cung cấp</param>
        /// <returns>true nếu đã có dữ liệu liên kết</returns>
        public bool HasLinkedRecords(int nccId)
        {
            DataService ds = new DataService();
            DataService.OpenConnection();

            // Hiện tại chỉ có bảng PHIEU_NHAP tham chiếu trực tiếp tới NHA_CUNG_CAP
            SqlCommand cmd = new SqlCommand(@"
                SELECT CASE 
                    WHEN EXISTS (SELECT 1 FROM PHIEU_NHAP WHERE ID_NHA_CUNG_CAP = @id) 
                    THEN 1 ELSE 0 END");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = nccId;

            object result = ds.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
            {
                return false;
            }

            return Convert.ToInt32(result) > 0;
        }
    }
}
