using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace CuahangNongduoc.DataLayer
{
    public class LyDoChiFactory
    {
        DataService m_Ds = new DataService();

        public LyDoChiFactory()
        {
            m_Ds.TableName = "LY_DO_CHI";
        }

        public DataTable DanhsachLyDo()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM LY_DO_CHI");
            ds.Load(cmd);

            return ds;
        }

        public DataTable LayLyDoChi(long id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM LY_DO_CHI WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            ds.Load(cmd);

            return ds;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM LY_DO_CHI");
            m_Ds.Load(cmd);
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
