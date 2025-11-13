using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc.Controller
{
    public class NguoiDungController
    {
        NguoiDungFactory factory = new NguoiDungFactory();

        /// <summary>
        /// Hiển thị danh sách người dùng lên DataGridView
        /// </summary>
        public void HienthiNguoiDungDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn)
        {
            factory.LoadData();

            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            DataTable tbl = factory.GetDataTable();

            bs.DataSource = tbl;
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        /// <summary>
        /// Hiển thị người dùng lên ComboBox (nếu cần)
        /// </summary>
        public void HienthiAutoComboBox(System.Windows.Forms.ComboBox cmb)
        {
            cmb.DataSource = factory.DanhsachNguoiDung();
            cmb.DisplayMember = "HO_TEN";
            cmb.ValueMember = "ID";
        }

        /// <summary>
        /// Get DataTable từ Factory để refresh binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return factory.GetDataTable();
        }

        /// <summary>
        /// Tìm kiếm người dùng theo họ tên
        /// </summary>
        public void TimHoTen(String hoTen)
        {
            factory.TimHoTenLoad(hoTen);
        }

        /// <summary>
        /// Lấy thông tin người dùng theo ID (convert sang BusinessObject)
        /// </summary>
        public NguoiDung LayNguoiDung(int id)
        {
            DataTable tbl = factory.LayNguoiDung(id);
            NguoiDung nd = new NguoiDung();

            if (tbl.Rows.Count > 0)
            {
                nd.Id = Convert.ToInt32(tbl.Rows[0]["ID"]);
                nd.TenDangNhap = Convert.ToString(tbl.Rows[0]["TEN_DANG_NHAP"]);
                nd.HoTen = Convert.ToString(tbl.Rows[0]["HO_TEN"]);
                nd.SoDienThoai = Convert.ToString(tbl.Rows[0]["SO_DIEN_THOAI"]);
                nd.QuyenHan = Convert.ToString(tbl.Rows[0]["QUYEN_HAN"]);
                // KHÔNG lấy mật khẩu ra BusinessObject vì lý do bảo mật
            }
            return nd;
        }

        /// <summary>
        /// Lấy danh sách người dùng (convert sang BusinessObject list)
        /// </summary>
        public IList<NguoiDung> LayDanhSachNguoiDung()
        {
            DataTable tbl = factory.DanhsachNguoiDung();
            IList<NguoiDung> ds = new List<NguoiDung>();

            foreach (DataRow row in tbl.Rows)
            {
                NguoiDung nd = new NguoiDung();
                nd.Id = Convert.ToInt32(row["ID"]);
                nd.TenDangNhap = Convert.ToString(row["TEN_DANG_NHAP"]);
                nd.HoTen = Convert.ToString(row["HO_TEN"]);
                nd.SoDienThoai = Convert.ToString(row["SO_DIEN_THOAI"]);
                nd.QuyenHan = Convert.ToString(row["QUYEN_HAN"]);
                ds.Add(nd);
            }
            return ds;
        }

        /// <summary>
        /// Đăng nhập - kiểm tra username và password
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhau">Mật khẩu chưa mã hóa</param>
        /// <returns>NguoiDung object nếu đăng nhập thành công, null nếu thất bại</returns>
        public NguoiDung DangNhap(String tenDangNhap, String matKhau)
        {
            // Mã hóa mật khẩu thành MD5
            string matKhauMaHoa = MaHoaMD5(matKhau);

            // Kiểm tra trong database
            DataTable tbl = factory.DangNhap(tenDangNhap, matKhauMaHoa);

            if (tbl.Rows.Count > 0)
            {
                // Đăng nhập thành công, trả về thông tin user
                NguoiDung nd = new NguoiDung();
                nd.Id = Convert.ToInt32(tbl.Rows[0]["ID"]);
                nd.TenDangNhap = Convert.ToString(tbl.Rows[0]["TEN_DANG_NHAP"]);
                nd.HoTen = Convert.ToString(tbl.Rows[0]["HO_TEN"]);
                nd.SoDienThoai = Convert.ToString(tbl.Rows[0]["SO_DIEN_THOAI"]);
                nd.QuyenHan = Convert.ToString(tbl.Rows[0]["QUYEN_HAN"]);
                return nd;
            }

            // Đăng nhập thất bại
            return null;
        }

        /// <summary>
        /// Kiểm tra xem username đã tồn tại chưa
        /// </summary>
        public bool KiemTraUsernameExists(String tenDangNhap)
        {
            DataTable tbl = factory.LayNguoiDungByUsername(tenDangNhap);
            return tbl.Rows.Count > 0;
        }

        /// <summary>
        /// Mã hóa mật khẩu bằng MD5
        /// </summary>
        public static string MaHoaMD5(String matKhau)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhau);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public DataRow NewRow()
        {
            return factory.NewRow();
        }

        public void Add(DataRow row)
        {
            factory.Add(row);
        }

        public bool Save()
        {
            return factory.Save();
        }
    }
}
