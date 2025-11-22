using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class PhieuBanFactory
    {
        DataService m_Ds = new DataService();

        public PhieuBanFactory()
        {
            m_Ds.TableName = "PHIEU_BAN";
        }

        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_BAN WHERE ID=-1");
            m_Ds.Load(cmd);
        }

        public DataTable TimPhieuBan(int idKh, DateTime dt)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_BAN WHERE NGAY_BAN = @ngay AND ID_KHACH_HANG=@kh");
            cmd.Parameters.Add("@ngay", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@kh", SqlDbType.Int).Value = idKh;

            ds.Load(cmd);

            return ds;
        }

        public DataTable DanhsachPhieuBanLe()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT PB.*, KM.TEN_KHUYEN_MAI
                  FROM PHIEU_BAN PB
                  INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN KHUYEN_MAI KM ON PB.ID_KHUYEN_MAI = KM.ID
                  WHERE KH.LOAI_KH = 0
                  ORDER BY PB.ID DESC");
            ds.Load(cmd);

            return ds;
        }
        public DataTable DanhsachPhieuBanSi()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT PB.*, KM.TEN_KHUYEN_MAI
                  FROM PHIEU_BAN PB
                  INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN KHUYEN_MAI KM ON PB.ID_KHUYEN_MAI = KM.ID
                  WHERE KH.LOAI_KH = 1
                  ORDER BY PB.ID DESC");
            ds.Load(cmd);

            return ds;
        }


        public DataTable LayPhieuBan(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEU_BAN WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }


        public DataTable LayChiTietPhieuBan(int idPhieuBan)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            ds.Load(cmd);
            return ds;
        }

        public static long LayConNo(int kh, int thang, int nam)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT SUM(CON_NO) FROM PHIEU_BAN WHERE ID_KHACH_HANG = @kh AND MONTH(NGAY_BAN)=@thang AND YEAR(NGAY_BAN)= @nam");
            cmd.Parameters.Add("@kh", SqlDbType.Int).Value = kh;
            cmd.Parameters.Add("@thang", SqlDbType.Int).Value = thang;
            cmd.Parameters.Add("@nam", SqlDbType.Int).Value = nam;

            object obj = ds.ExecuteScalar(cmd);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public static int LaySoPhieu()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM PHIEU_BAN");
            
            object obj = ds.ExecuteScalar(cmd);
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }
        
        public DataRow NewRow()
        {
            // Ensure schema is loaded before creating a new row
            if (m_Ds.Columns.Count == 0)
            {
                LoadData();
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

        /// <summary>
        /// Kiểm tra xem phiếu bán có được sử dụng trong các bảng khác không
        /// </summary>
        public bool KiemTraLienKet(int idPhieuBan)
        {
            DataService ds = new DataService();

            // Kiểm tra trong CHI_TIET_PHIEU_BAN
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count1 = ds.ExecuteScalar(cmd1);
            if (count1 != null && Convert.ToInt32(count1) > 0)
                return true;

            // Kiểm tra trong DU_NO_KH (nếu có liên kết qua PHIEU_BAN)
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM DU_NO_KH WHERE ID_PHIEU_BAN = @id");
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count2 = ds.ExecuteScalar(cmd2);
            if (count2 != null && Convert.ToInt32(count2) > 0)
                return true;

            // Kiểm tra trong PHIEU_THANH_TOAN (nếu có liên kết qua PHIEU_BAN)
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_THANH_TOAN WHERE ID_PHIEU_BAN = @id");
            cmd3.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count3 = ds.ExecuteScalar(cmd3);
            if (count3 != null && Convert.ToInt32(count3) > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Lấy danh sách các bảng có liên kết với phiếu bán
        /// </summary>
        public List<string> LayDanhSachBangLienKet(int idPhieuBan)
        {
            List<string> danhSachBang = new List<string>();
            DataService ds = new DataService();

            // Kiểm tra trong CHI_TIET_PHIEU_BAN
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count1 = ds.ExecuteScalar(cmd1);
            if (count1 != null && Convert.ToInt32(count1) > 0)
                danhSachBang.Add("Chi tiết phiếu bán");

            // Kiểm tra trong DU_NO_KH
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM DU_NO_KH WHERE ID_PHIEU_BAN = @id");
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count2 = ds.ExecuteScalar(cmd2);
            if (count2 != null && Convert.ToInt32(count2) > 0)
                danhSachBang.Add("Dư nợ khách hàng");

            // Kiểm tra trong PHIEU_THANH_TOAN
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_THANH_TOAN WHERE ID_PHIEU_BAN = @id");
            cmd3.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            object count3 = ds.ExecuteScalar(cmd3);
            if (count3 != null && Convert.ToInt32(count3) > 0)
                danhSachBang.Add("Phiếu thanh toán");

            return danhSachBang;
        }
    }
}
