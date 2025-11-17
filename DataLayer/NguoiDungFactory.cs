using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    
       
namespace CuahangNongduoc.DataLayer
    {
        public class NguoiDungFactory
        {
            DataService m_Ds = new DataService();

            public NguoiDungFactory()
            {
                m_Ds.TableName = "NGUOI_DUNG";
            }

            /// <summary>
            /// Lấy danh sách tất cả người dùng
            /// </summary>
            public DataTable DanhsachNguoiDung()
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG ORDER BY ID");
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Lấy thông tin người dùng theo ID
            /// </summary>
            public DataTable LayNguoiDung(int id)
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE ID = @id");
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Lấy thông tin người dùng theo tên đăng nhập
            /// </summary>
            public DataTable LayNguoiDungByUsername(String tenDangNhap)
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE TEN_DANG_NHAP = @tendangnhap");
                cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = tenDangNhap;
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Kiểm tra đăng nhập - trả về thông tin user nếu đúng
            /// </summary>
            public DataTable DangNhap(String tenDangNhap, String matKhauMaHoa)
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE TEN_DANG_NHAP = @tendangnhap AND MAT_KHAU = @matkhau");
                cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = tenDangNhap;
                cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = matKhauMaHoa;
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Lấy danh sách người dùng theo quyền hạn
            /// </summary>
            public DataTable DanhsachNguoiDungTheoQuyen(String quyenHan)
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE QUYEN_HAN = @quyenhan ORDER BY ID");
                cmd.Parameters.Add("@quyenhan", SqlDbType.VarChar).Value = quyenHan;
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Tìm kiếm người dùng theo họ tên
            /// </summary>
            public DataTable TimHoTen(String hoTen)
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE HO_TEN LIKE '%' + @hoten + '%' ORDER BY ID");
                cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoTen;
                ds.Load(cmd);
                return ds;
            }

            /// <summary>
            /// Load data vào m_Ds để binding và save
            /// </summary>
            public void LoadData()
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG ORDER BY ID");
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
            public void TimHoTenLoad(String hoTen)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOI_DUNG WHERE HO_TEN LIKE '%' + @hoten + '%' ORDER BY ID");
                cmd.Parameters.Add("@hoten", SqlDbType.NVarChar).Value = hoTen;
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

