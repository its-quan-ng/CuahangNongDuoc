using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuNhapFactory
    {
        DataService m_Ds = new DataService();

        public PhieuNhapFactory()
        {
            m_Ds.TableName = "PHIEU_NHAP";
        }

        public void LoadSchema()
        {
           SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_NHAP WHERE ID=-1");
            m_Ds.Load(cmd);

        }

        public DataTable DanhsachPhieuNhap()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_NHAP");
            ds.Load(cmd);

            return ds;
        }

        public DataTable TimPhieuNhap(int maNCC, DateTime dt)
        {
            DataService ds = new DataService();
            String sql = "SELECT * FROM PHIEU_NHAP WHERE NGAY_NHAP = @ngay AND ID_NHA_CUNG_CAP = @ncc";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@ncc", SqlDbType.Int).Value = maNCC;
            
            ds.Load(cmd);

            return ds;
        }


        public DataTable LayPhieuNhap(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_NHAP WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }


        
        public DataRow NewRow()
        {
            // Đảm bảo đã load schema (các cột của bảng PHIEU_NHAP) trước khi tạo dòng mới
            if (m_Ds.Columns.Count == 0)
            {
                LoadSchema();
            }
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
