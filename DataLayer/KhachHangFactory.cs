using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class KhachHangFactory
    {
        DataService m_Ds = new DataService();

        public KhachHangFactory()
        {
            m_Ds.TableName = "KHACH_HANG";
        }

        public DataTable DanhsachKhachHang(bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = @loai");
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }
        public DataTable TimHoTen(String hoten, bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }

        public DataTable TimDiaChi(String diachi, bool loai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            ds.Load(cmd);

            return ds;
        }

        public DataTable DanhsachKhachHang()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG");
            ds.Load(cmd);

            return ds;
        }

        public DataTable LayKhachHang(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE ID = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save (cho khách hàng lẻ)
        /// </summary>
        public void LoadDataKhachHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = 0");
            m_Ds.Load(cmd);
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save (cho đại lý)
        /// </summary>
        public void LoadDataDaiLy()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = 1");
            m_Ds.Load(cmd);
        }

        /// <summary>
        /// Get m_Ds để binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return m_Ds;
        }

        /// <summary>
        /// Tìm theo họ tên và load vào m_Ds để binding
        /// </summary>
        public void TimHoTenLoad(String hoten, bool loai)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
        }

        /// <summary>
        /// Tìm theo địa chỉ và load vào m_Ds để binding
        /// </summary>
        public void TimDiaChiLoad(String diachi, bool loai)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
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
        /// Kiểm tra xem đại lý có được sử dụng trong các bảng khác không
        /// </summary>
        public bool KiemTraLienKet(int idKhachHang)
        {
            DataService ds = new DataService();

            // Kiểm tra trong PHIEU_BAN
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_BAN WHERE ID_KHACH_HANG = @id");
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count1 = ds.ExecuteScalar(cmd1);
            if (count1 != null && Convert.ToInt32(count1) > 0)
                return true;

            // Kiểm tra trong DU_NO_KH
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM DU_NO_KH WHERE ID_KHACH_HANG = @id");
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count2 = ds.ExecuteScalar(cmd2);
            if (count2 != null && Convert.ToInt32(count2) > 0)
                return true;

            // Kiểm tra trong PHIEU_THANH_TOAN
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG = @id");
            cmd3.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count3 = ds.ExecuteScalar(cmd3);
            if (count3 != null && Convert.ToInt32(count3) > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Lấy danh sách các bảng có liên kết với đại lý
        /// </summary>
        public List<string> LayDanhSachBangLienKet(int idKhachHang)
        {
            List<string> danhSachBang = new List<string>();
            DataService ds = new DataService();

            // Kiểm tra trong PHIEU_BAN
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_BAN WHERE ID_KHACH_HANG = @id");
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count1 = ds.ExecuteScalar(cmd1);
            if (count1 != null && Convert.ToInt32(count1) > 0)
                danhSachBang.Add("Phiếu bán");

            // Kiểm tra trong DU_NO_KH
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM DU_NO_KH WHERE ID_KHACH_HANG = @id");
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count2 = ds.ExecuteScalar(cmd2);
            if (count2 != null && Convert.ToInt32(count2) > 0)
                danhSachBang.Add("Dư nợ khách hàng");

            // Kiểm tra trong PHIEU_THANH_TOAN
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM PHIEU_THANH_TOAN WHERE ID_KHACH_HANG = @id");
            cmd3.Parameters.Add("@id", SqlDbType.Int).Value = idKhachHang;
            object count3 = ds.ExecuteScalar(cmd3);
            if (count3 != null && Convert.ToInt32(count3) > 0)
                danhSachBang.Add("Phiếu thanh toán");

            return danhSachBang;
        }
    }
}
