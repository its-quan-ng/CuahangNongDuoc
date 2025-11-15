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

        // Danh sách đơn vị tính dùng chung m_Ds để có thể Save() lại SQL Server
        public DonViTinhFactory()
        {
            m_Ds.TableName = "DON_VI_TINH";
        }

        public DataTable DanhsachDVT()
        {
            // Sắp xếp theo ID để các đơn vị tính mới (ID lớn hơn) nằm dưới các đơn vị cũ
            SqlCommand cmd = new SqlCommand("SELECT * FROM DON_VI_TINH ORDER BY ID");
            m_Ds.Load(cmd);

            // Thiết lập tự sinh ID trên DataTable để tránh lỗi "Column 'ID' does not allow nulls" khi thêm dòng mới
            if (m_Ds.Columns.Contains("ID"))
            {
                DataColumn colId = m_Ds.Columns["ID"];
                colId.AutoIncrement = true;

                // Tính giá trị ID nhỏ nhất hiện có, sau đó seed = minID - 1 để không bị trùng
                int seed = -1;
                if (m_Ds.Rows.Count > 0)
                {
                    object minObj = m_Ds.Compute("MIN(ID)", string.Empty);
                    if (minObj != DBNull.Value)
                    {
                        int minId = Convert.ToInt32(minObj);
                        seed = minId - 1;
                    }
                }

                colId.AutoIncrementSeed = seed;
                colId.AutoIncrementStep = -1;
            }

            return m_Ds;
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
