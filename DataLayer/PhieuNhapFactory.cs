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

        public DataTable GetDataTable()
        {
            return m_Ds;
        }

        public void LoadSchema()
        {
            // Chỉ load 1 lần - nếu đã có columns thì return
            if (m_Ds.Columns.Count > 0)
                return;

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

        public DataTable TimPhieuNhap(int maNCC, DateTime tuNgay, DateTime denNgay)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_NHAP WHERE NGAY_NHAP BETWEEN @tu_ngay AND @den_ngay AND ID_NHA_CUNG_CAP = @ncc");
            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay.Date;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay.Date;
            cmd.Parameters.Add("@ncc", SqlDbType.Int).Value = maNCC;

            ds.Load(cmd);

            return ds;
        }


        public void TimPhieuNhapLoad(int maNCC, DateTime tuNgay, DateTime denNgay)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_NHAP WHERE NGAY_NHAP BETWEEN @tu_ngay AND @den_ngay AND ID_NHA_CUNG_CAP = @ncc");
            cmd.Parameters.Add("@tu_ngay", SqlDbType.DateTime).Value = tuNgay.Date;
            cmd.Parameters.Add("@den_ngay", SqlDbType.DateTime).Value = denNgay.Date;
            cmd.Parameters.Add("@ncc", SqlDbType.Int).Value = maNCC;

            m_Ds.Load(cmd);
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
