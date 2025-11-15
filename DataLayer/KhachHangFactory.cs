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

        // Các hàm danh sách/tìm kiếm sử dụng chung m_Ds để binding và Save()
        public KhachHangFactory()
        {
            m_Ds.TableName = "KHACH_HANG";
        }

        public DataTable DanhsachKhachHang(bool loai)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE LOAI_KH = @loai");
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable TimHoTen(String hoten, bool loai)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE HO_TEN LIKE '%' + @hoten + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoten;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable TimDiaChi(String diachi, bool loai)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG WHERE DIA_CHI LIKE '%' + @diachi + '%' AND LOAI_KH = @loai");
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@loai", SqlDbType.Bit).Value = loai;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable DanhsachKhachHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KHACH_HANG");
            m_Ds.Load(cmd);
            return m_Ds;
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
    }
}
