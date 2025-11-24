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
                @"SELECT PB.*, KM.TEN_KHUYEN_MAI, ND.HO_TEN AS NGUOI_LAP
                  FROM PHIEU_BAN PB
                  INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN KHUYEN_MAI KM ON PB.ID_KHUYEN_MAI = KM.ID
                  LEFT JOIN NGUOI_DUNG ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE KH.LOAI_KH = 0
                  ORDER BY PB.ID DESC");
            ds.Load(cmd);

            return ds;
        }
        public DataTable DanhsachPhieuBanSi()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT PB.*, KM.TEN_KHUYEN_MAI, ND.HO_TEN AS NGUOI_LAP
                  FROM PHIEU_BAN PB
                  INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                  LEFT JOIN KHUYEN_MAI KM ON PB.ID_KHUYEN_MAI = KM.ID
                  LEFT JOIN NGUOI_DUNG ND ON PB.ID_NGUOI_DUNG = ND.ID
                  WHERE KH.LOAI_KH = 1
                  ORDER BY PB.ID DESC");
            ds.Load(cmd);

            return ds;
        }

        /// <summary>
        /// Load danh sách phiếu bán lẻ vào m_Ds để có thể Save (cho form danh sách phiếu)
        /// </summary>
        public void LoadDanhsachPhieuBanLe()
        {
            // Query chỉ từ bảng PHIEU_BAN (KHÔNG JOIN) để có thể save
            SqlCommand cmd = new SqlCommand(
                @"SELECT *
                  FROM PHIEU_BAN
                  WHERE ID_KHACH_HANG IN (SELECT ID FROM KHACH_HANG WHERE LOAI_KH = 0)
                  ORDER BY ID DESC");
            m_Ds.Load(cmd);

            // Thêm columns hiển thị (không lưu vào DB)
            if (!m_Ds.Columns.Contains("TEN_KHUYEN_MAI"))
            {
                m_Ds.Columns.Add("TEN_KHUYEN_MAI", typeof(string));
            }
            if (!m_Ds.Columns.Contains("NGUOI_LAP"))
            {
                m_Ds.Columns.Add("NGUOI_LAP", typeof(string));
            }

            // Populate data cho columns hiển thị
            foreach (DataRow row in m_Ds.Rows)
            {
                // Lấy tên khuyến mãi
                if (row["ID_KHUYEN_MAI"] != DBNull.Value)
                {
                    int idKM = Convert.ToInt32(row["ID_KHUYEN_MAI"]);
                    DataService dsKM = new DataService();
                    SqlCommand cmdKM = new SqlCommand("SELECT TEN_KHUYEN_MAI FROM KHUYEN_MAI WHERE ID = @id");
                    cmdKM.Parameters.Add("@id", SqlDbType.Int).Value = idKM;
                    object tenKM = dsKM.ExecuteScalar(cmdKM);
                    row["TEN_KHUYEN_MAI"] = tenKM != null ? tenKM.ToString() : "";
                }

                // Lấy tên người lập
                if (row["ID_NGUOI_DUNG"] != DBNull.Value)
                {
                    int idND = Convert.ToInt32(row["ID_NGUOI_DUNG"]);
                    DataService dsND = new DataService();
                    SqlCommand cmdND = new SqlCommand("SELECT HO_TEN FROM NGUOI_DUNG WHERE ID = @id");
                    cmdND.Parameters.Add("@id", SqlDbType.Int).Value = idND;
                    object tenND = dsND.ExecuteScalar(cmdND);
                    row["NGUOI_LAP"] = tenND != null ? tenND.ToString() : "";
                }
            }
        }

        /// <summary>
        /// Load danh sách phiếu bán sỉ vào m_Ds để có thể Save (cho form danh sách phiếu)
        /// </summary>
        public void LoadDanhsachPhieuBanSi()
        {
            // Query chỉ từ bảng PHIEU_BAN (KHÔNG JOIN) để có thể save
            SqlCommand cmd = new SqlCommand(
                @"SELECT *
                  FROM PHIEU_BAN
                  WHERE ID_KHACH_HANG IN (SELECT ID FROM KHACH_HANG WHERE LOAI_KH = 1)
                  ORDER BY ID DESC");
            m_Ds.Load(cmd);

            // Thêm columns hiển thị (không lưu vào DB)
            if (!m_Ds.Columns.Contains("TEN_KHUYEN_MAI"))
            {
                m_Ds.Columns.Add("TEN_KHUYEN_MAI", typeof(string));
            }
            if (!m_Ds.Columns.Contains("NGUOI_LAP"))
            {
                m_Ds.Columns.Add("NGUOI_LAP", typeof(string));
            }

            // Populate data cho columns hiển thị
            foreach (DataRow row in m_Ds.Rows)
            {
                // Lấy tên khuyến mãi
                if (row["ID_KHUYEN_MAI"] != DBNull.Value)
                {
                    int idKM = Convert.ToInt32(row["ID_KHUYEN_MAI"]);
                    DataService dsKM = new DataService();
                    SqlCommand cmdKM = new SqlCommand("SELECT TEN_KHUYEN_MAI FROM KHUYEN_MAI WHERE ID = @id");
                    cmdKM.Parameters.Add("@id", SqlDbType.Int).Value = idKM;
                    object tenKM = dsKM.ExecuteScalar(cmdKM);
                    row["TEN_KHUYEN_MAI"] = tenKM != null ? tenKM.ToString() : "";
                }

                // Lấy tên người lập
                if (row["ID_NGUOI_DUNG"] != DBNull.Value)
                {
                    int idND = Convert.ToInt32(row["ID_NGUOI_DUNG"]);
                    DataService dsND = new DataService();
                    SqlCommand cmdND = new SqlCommand("SELECT HO_TEN FROM NGUOI_DUNG WHERE ID = @id");
                    cmdND.Parameters.Add("@id", SqlDbType.Int).Value = idND;
                    object tenND = dsND.ExecuteScalar(cmdND);
                    row["NGUOI_LAP"] = tenND != null ? tenND.ToString() : "";
                }
            }
        }

        /// <summary>
        /// Get m_Ds để binding (cho form danh sách phiếu)
        /// </summary>
        public DataTable GetDataTable()
        {
            return m_Ds;
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
        /// Lấy danh sách phiếu bán có chiết khấu theo khoảng thời gian và nhân viên
        /// </summary>
        /// <param name="tuNgay">Ngày bắt đầu</param>
        /// <param name="denNgay">Ngày kết thúc</param>
        /// <param name="maNguoiDung">ID nhân viên (null = tất cả)</param>
        /// <returns>DataTable chứa thông tin phiếu bán có chiết khấu</returns>
        public DataTable LayPhieuBanCoChietKhau(DateTime tuNgay, DateTime denNgay, int? maNguoiDung)
        {
            DataService ds = new DataService();
            
            string query = @"
                SELECT 
                    PB.ID,
                    PB.NGAY_BAN,
                    PB.MA_PHIEU,
                    KH.TEN_KHACH_HANG,
                    PB.CHIET_KHAU,
                    PB.TONG_TIEN,
                    ND.HO_TEN AS NGUOI_LAP,
                    -- Tính số tiền giảm = (Tổng hàng * Chiết khấu / 100)
                    CASE 
                        WHEN PB.CHIET_KHAU IS NOT NULL AND PB.CHIET_KHAU > 0 
                        THEN (SELECT SUM(THANH_TIEN) FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = PB.ID) * PB.CHIET_KHAU / 100
                        ELSE 0
                    END AS SO_TIEN_GIAM
                FROM PHIEU_BAN PB
                INNER JOIN KHACH_HANG KH ON PB.ID_KHACH_HANG = KH.ID
                LEFT JOIN NGUOI_DUNG ND ON PB.ID_NGUOI_DUNG = ND.ID
                WHERE PB.NGAY_BAN >= @tuNgay 
                    AND PB.NGAY_BAN <= @denNgay
                    AND (PB.CHIET_KHAU IS NOT NULL AND PB.CHIET_KHAU > 0)";

            if (maNguoiDung.HasValue)
            {
                query += " AND PB.ID_NGUOI_DUNG = @maNguoiDung";
            }

            query += " ORDER BY PB.NGAY_BAN DESC, PB.ID DESC";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.Add("@tuNgay", SqlDbType.DateTime).Value = tuNgay;
            cmd.Parameters.Add("@denNgay", SqlDbType.DateTime).Value = denNgay;
            
            if (maNguoiDung.HasValue)
            {
                cmd.Parameters.Add("@maNguoiDung", SqlDbType.Int).Value = maNguoiDung.Value;
            }

            ds.Load(cmd);
            return ds;
        }

        /// <summary>
        /// Kiểm tra xem phiếu bán có được sử dụng trong các bảng khác không
        /// Hiện tại: DU_NO_KH và PHIEU_THANH_TOAN không có FK đến PHIEU_BAN → Luôn return false
        /// </summary>
        public bool KiemTraLienKet(int idPhieuBan)
        {
            // Database hiện tại KHÔNG có foreign key từ bảng nào đến PHIEU_BAN
            // CHI_TIET_PHIEU_BAN sẽ được xóa thủ công trong DeletePhieuBan()
            return false;
        }

        /// <summary>
        /// Lấy danh sách các bảng có liên kết với phiếu bán
        /// </summary>
        public List<string> LayDanhSachBangLienKet(int idPhieuBan)
        {
            // Database hiện tại KHÔNG có foreign key từ bảng nào đến PHIEU_BAN
            return new List<string>();
        }

        /// <summary>
        /// Xóa phiếu bán và chi tiết phiếu bán (cascade delete thủ công)
        /// </summary>
        public bool DeletePhieuBan(int idPhieuBan)
        {
            DataService ds = new DataService();
            try
            {
                // Xóa CHI_TIET_PHIEU_BAN trước (FK constraint)
                SqlCommand cmd1 = new SqlCommand("DELETE FROM CHI_TIET_PHIEU_BAN WHERE ID_PHIEU_BAN = @id");
                cmd1.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
                ds.ExecuteNoneQuery(cmd1);

                // Sau đó xóa PHIEU_BAN
                SqlCommand cmd2 = new SqlCommand("DELETE FROM PHIEU_BAN WHERE ID = @id");
                cmd2.Parameters.Add("@id", SqlDbType.Int).Value = idPhieuBan;
                int result = ds.ExecuteNoneQuery(cmd2);

                return result > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeletePhieuBan Error: {ex.Message}");
                return false;
            }
        }
    }
}
