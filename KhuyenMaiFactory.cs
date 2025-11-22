using System;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    /// <summary>
    /// Factory: Data access cho table KHUYEN_MAI
    /// YC4: CRUD chương trình khuyến mãi
    /// </summary>
    public class KhuyenMaiFactory
    {
        DataService m_Ds = new DataService();

        public KhuyenMaiFactory()
        {
            m_Ds.TableName = "KHUYEN_MAI";
        }

        /// <summary>
        /// Load schema để NewRow() hoạt động
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHUYEN_MAI WHERE ID = -1");
            m_Ds.Load(cmd);

            // Set AutoIncrement cho ID column
            if (m_Ds.Columns.Contains("ID"))
            {
                m_Ds.Columns["ID"].AutoIncrement = true;
                m_Ds.Columns["ID"].AutoIncrementSeed = -1;
                m_Ds.Columns["ID"].AutoIncrementStep = -1;
                m_Ds.Columns["ID"].ReadOnly = true;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả chương trình khuyến mãi
        /// </summary>
        public DataTable DanhsachKhuyenMai()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHUYEN_MAI ORDER BY TU_NGAY DESC");
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Lấy danh sách khuyến mãi đang áp dụng
        /// Điều kiện: TRANG_THAI = 1 VÀ hôm nay nằm trong khoảng TU_NGAY - DEN_NGAY
        /// </summary>
        public DataTable LayKhuyenMaiDangApDung()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT * FROM KHUYEN_MAI
                  WHERE TRANG_THAI = 1
                    AND GETDATE() BETWEEN TU_NGAY AND DEN_NGAY
                  ORDER BY TEN_KHUYEN_MAI");
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Lấy thông tin 1 chương trình khuyến mãi theo ID
        /// </summary>
        public DataTable LayKhuyenMai(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHUYEN_MAI WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Tạo DataRow mới để thêm vào DataTable
        /// </summary>
        public DataRow NewRow()
        {
            // Load schema nếu chưa load
            if (m_Ds.Columns.Count == 0)
                LoadData();

            return m_Ds.NewRow();
        }

        /// <summary>
        /// Thêm DataRow vào DataTable (chưa lưu DB)
        /// </summary>
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }

        /// <summary>
        /// Lưu thay đổi vào database (INSERT/UPDATE/DELETE)
        /// </summary>
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
