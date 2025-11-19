using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc.DataLayer
{
    public class ChiTietPhieuBanFactory
    {
        DataService m_Ds = new DataService();

        public ChiTietPhieuBanFactory()
        {
            m_Ds.TableName = "CHI_TIET_PHIEU_BAN";
        }

        public DataTable LayChiTietPhieuBan(int idPhieuBan)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT CT.*, SP.TEN_SAN_PHAM FROM CHI_TIET_PHIEU_BAN CT " +
                "INNER JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID " +
                "INNER JOIN SAN_PHAM SP ON MSP.ID_SAN_PHAM = SP.ID " +
                "WHERE CT.ID_PHIEU_BAN = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            ds.Load(cmd);
            return ds;
        }

        public DataTable LayChiTietPhieuBan(DateTime dtNgayBan)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT CT.* FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " WHERE PB.NGAY_BAN = @ngayban");
            cmd.Parameters.Add("@ngayban", SqlDbType.DateTime).Value = dtNgayBan;
            ds.Load(cmd);
            return ds;
        }

        public DataTable LayChiTietPhieuBan(int thang, int nam)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT CT.* FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " WHERE MONTH(PB.NGAY_BAN) = @thang AND YEAR(PB.NGAY_BAN)= @nam");
            cmd.Parameters.Add("@thang", SqlDbType.Int).Value = thang;
            cmd.Parameters.Add("@nam", SqlDbType.Int).Value = nam;
            ds.Load(cmd);
            return ds;
        }

        
        
        public DataRow NewRow()
        {
            // Đảm bảo DataTable có schema trước khi tạo row mới
            if (m_Ds.Columns.Count == 0)
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 0 * FROM CHI_TIET_PHIEU_BAN");
                m_Ds.Load(cmd);
            }
            return m_Ds.NewRow();
        }
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
       public bool Save()
        {
            // Xử lý các dòng thêm mới
            foreach (DataRow row in m_Ds.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    // Trừ số lượng khi thêm mới (bán hàng)
                    CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(Convert.ToString(row["ID_MA_SAN_PHAM"]), -Convert.ToInt32(row["SO_LUONG"]));
                }
            }
            
            // Xử lý các dòng bị xóa (cần lấy từ GetChanges vì các dòng đã xóa không có trong Rows)
            DataTable deletedRows = m_Ds.GetChanges(DataRowState.Deleted);
            if (deletedRows != null)
            {
                foreach (DataRow row in deletedRows.Rows)
                {
                    // Cộng lại số lượng khi xóa (hủy bán hàng)
                    CuahangNongduoc.DataLayer.MaSanPhamFactory.CapNhatSoLuong(Convert.ToString(row["ID_MA_SAN_PHAM", DataRowVersion.Original]), Convert.ToInt32(row["SO_LUONG", DataRowVersion.Original]));
                }
            }
            
            // Xóa cột TEN_SAN_PHAM trước khi lưu (vì cột này không có trong DB)
            if (m_Ds.Columns.Contains("TEN_SAN_PHAM"))
            {
                m_Ds.Columns.Remove("TEN_SAN_PHAM");
            }
            
            return m_Ds.ExecuteNoneQuery() > 0;
        }

        /// <summary>
        /// Load data vào m_Ds để binding và save
        /// </summary>
        public void LoadData(int idPhieuBan)
        {
            // Chỉ load từ bảng CHI_TIET_PHIEU_BAN để có thể save được
            SqlCommand cmd = new SqlCommand("SELECT * FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
            m_Ds.Load(cmd);
            
            // Thêm cột TEN_SAN_PHAM để hiển thị (không lưu vào DB)
            if (!m_Ds.Columns.Contains("TEN_SAN_PHAM"))
            {
                DataColumn col = new DataColumn("TEN_SAN_PHAM", typeof(string));
                col.ReadOnly = false; // Cho phép gán giá trị
                col.AllowDBNull = true;
                m_Ds.Columns.Add(col);
            }
            
            // Điền tên sản phẩm cho từng dòng
            foreach (DataRow row in m_Ds.Rows)
            {
                string idMaSanPham = Convert.ToString(row["ID_MA_SAN_PHAM"]);
                DataService ds = new DataService();
                SqlCommand cmdTen = new SqlCommand(
                    "SELECT SP.TEN_SAN_PHAM FROM MA_SAN_PHAM MSP " +
                    "INNER JOIN SAN_PHAM SP ON MSP.ID_SAN_PHAM = SP.ID " +
                    "WHERE MSP.ID = @idMaSP");
                cmdTen.Parameters.Add("@idMaSP", SqlDbType.NVarChar).Value = idMaSanPham;
                object tenSP = ds.ExecuteScalar(cmdTen);
                row["TEN_SAN_PHAM"] = tenSP != null ? tenSP.ToString() : "";
            }
        }

        /// <summary>
        /// Get m_Ds để binding
        /// </summary>
        public DataTable GetDataTable()
        {
            // Đảm bảo DataTable có schema trước khi trả về
            // Chỉ load từ bảng CHI_TIET_PHIEU_BAN để có thể save được
            if (m_Ds.Columns.Count == 0)
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 0 * FROM CHI_TIET_PHIEU_BAN");
                m_Ds.Load(cmd);
                
                // Thêm cột TEN_SAN_PHAM để hiển thị (không lưu vào DB)
                if (!m_Ds.Columns.Contains("TEN_SAN_PHAM"))
                {
                    DataColumn col = new DataColumn("TEN_SAN_PHAM", typeof(string));
                    m_Ds.Columns.Add(col);
                }
            }
            return m_Ds;
        }
    }
}
