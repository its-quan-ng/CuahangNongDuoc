using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuThanhToanFactory
    {
        DataService m_Ds = new DataService();

        public PhieuThanhToanFactory()
        {
            m_Ds.TableName = "PHIEU_THANH_TOAN";
        }

        public DataTable DanhsachPhieuThanhToan()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_THANH_TOAN ");
            ds.Load(cmd);

            return ds;
        }
        public DataTable TimPhieuThanhToan(int kh, DateTime ngay)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG=@kh AND NGAY_THANH_TOAN = @ngay");
            cmd.Parameters.Add("@kh", SqlDbType.Int).Value = kh;
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngay;

            ds.Load(cmd);

            return ds;
        }
      
        public DataTable LayPhieuThanhToan(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_THANH_TOAN WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }


        public static long LayTongTien(int kh, int thang, int nam)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT SUM(TONG_TIEN) FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG = @kh AND MONTH(NGAY_THANH_TOAN)=@thang AND YEAR(NGAY_THANH_TOAN)= @nam");
            cmd.Parameters.Add("@kh", SqlDbType.Int).Value = kh;
            cmd.Parameters.Add("@thang", SqlDbType.Int).Value = thang;
            cmd.Parameters.Add("@nam", SqlDbType.Int).Value = nam;

            object obj = ds.ExecuteScalar(cmd);
            
            if (obj == null)
                return 0;
            else
                return Convert.ToInt64(obj);
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
