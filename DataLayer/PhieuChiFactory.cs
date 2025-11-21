using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuChiFactory
    {
        DataService m_Ds = new DataService();

        public PhieuChiFactory()
        {
            m_Ds.TableName = "PHIEU_CHI";
        }

        public DataTable TimPhieuChi(int lydo, DateTime ngay)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_CHI WHERE ID_LY_DO_CHI = @lydo AND NGAY_CHI = @ngay");
            cmd.Parameters.Add("@lydo", SqlDbType.Int).Value = lydo;
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = ngay;

            ds.Load(cmd);

            return ds;
        }

        public DataTable DanhsachPhieuChi()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_CHI ");
            ds.Load(cmd);

            return ds;
        }
      
        public DataTable LayPhieuChi(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_CHI WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }


        public static long LayTongTien(int lydo, int thang, int nam)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT SUM(TONG_TIEN) FROM PHIEU_CHI WHERE ID_LY_DO_CHI = @lydo AND MONTH(NGAY_CHI)=@thang AND YEAR(NGAY_CHI)= @nam");
            cmd.Parameters.Add("@lydo", SqlDbType.Int).Value = lydo;
            cmd.Parameters.Add("@thang", SqlDbType.Int).Value = thang;
            cmd.Parameters.Add("@nam", SqlDbType.Int).Value = nam;

            object obj = ds.ExecuteScalar(cmd);
            
            if (obj == null)
                return 0;
            else
                return Convert.ToInt64(obj);
        }
        
        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_CHI");
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

        /// <summary>
        /// Kiểm tra xem phiếu chi có được sử dụng trong các bảng khác không
        /// </summary>
        public bool KiemTraLienKet(int idPhieuChi)
        {
            // Hiện tại chưa có bảng nào tham chiếu đến PHIEU_CHI
            // Giữ phương thức này để dễ mở rộng trong tương lai
            return false;
        }

        /// <summary>
        /// Lấy danh sách các bảng có liên kết với phiếu chi
        /// </summary>
        public List<string> LayDanhSachBangLienKet(int idPhieuChi)
        {
            List<string> danhSachBang = new List<string>();
            // Hiện tại chưa có bảng nào tham chiếu đến PHIEU_CHI
            return danhSachBang;
        }
    }
}
