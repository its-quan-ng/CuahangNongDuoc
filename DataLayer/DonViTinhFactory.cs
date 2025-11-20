using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class DonViTinhFactory
    {
        DataService m_Ds = new DataService();

        public DonViTinhFactory()
        {
            m_Ds.TableName = "DON_VI_TINH";
        }

        public DataTable DanhsachDVT()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM DON_VI_TINH");
            ds.Load(cmd);

            return ds;
        }


        public DataTable LayDVT(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM DON_VI_TINH WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM DON_VI_TINH");
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

        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
