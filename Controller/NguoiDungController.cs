


using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



   

namespace CuahangNongduoc.Controller
    {
        public class NguoiDungController
        {




            NguoiDungFactory factory = new NguoiDungFactory();

        /// <summary>
        /// Hiển thị danh sách người dùng lên DataGridView với data binding
        /// </summary>
        public void HienthiDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn,
            System.Windows.Forms.TextBox txtMaNguoiDung, System.Windows.Forms.TextBox txtTenNguoiDung,
            System.Windows.Forms.TextBox txtTaiKhoan, System.Windows.Forms.TextBox txtMatKhau,
            System.Windows.Forms.TextBox txtSDT,
                System.Windows.Forms.ComboBox cmbQuyenHan, System.Windows.Forms.ComboBox cmbTrangThai)
            {
                factory.LoadData();

                System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
                bs.DataSource = factory.GetDataTable();

                // Bind textboxes
                txtMaNguoiDung.DataBindings.Clear();
                txtMaNguoiDung.DataBindings.Add("Text", bs, "ID");

                txtTenNguoiDung.DataBindings.Clear();
                txtTenNguoiDung.DataBindings.Add("Text", bs, "HO_TEN");

                txtTaiKhoan.DataBindings.Clear();
                txtTaiKhoan.DataBindings.Add("Text", bs, "TEN_DANG_NHAP");

                txtMatKhau.DataBindings.Clear();
                txtMatKhau.DataBindings.Add("Text", bs, "MAT_KHAU");
            
                txtSDT.DataBindings.Clear();
                txtSDT.DataBindings.Add("Text", bs, "SO_DIEN_THOAI");


            // Bind comboboxes
            cmbQuyenHan.DataBindings.Clear();
                cmbQuyenHan.DataBindings.Add("Text", bs, "QUYEN_HAN");

                cmbTrangThai.DataBindings.Clear();
                cmbTrangThai.DataBindings.Add("Text", bs, "TRANG_THAI");

                // Bind navigator and datagridview
                bn.BindingSource = bs;
                dg.DataSource = bs;
            }
        public void HienthiAutoComboBoxTrangThai(System.Windows.Forms.ComboBox cmb)
        {
            DataTable tbl = factory.LayDanhSachTrangThai();
            cmb.DataSource = tbl;
            cmb.DisplayMember = "TRANG_THAI";
            cmb.ValueMember = "TRANG_THAI";
        }

        /// <summary>
        /// Hiển thị danh sách quyền hạn lên ComboBox
        /// </summary>
        public void HienthiAutoComboBoxQuyenHan(System.Windows.Forms.ComboBox cmb)
            {
                DataTable tbl = factory.LayDanhSachQuyenHan();
                cmb.DataSource = tbl;
                cmb.DisplayMember = "QUYEN_HAN";
                cmb.ValueMember = "QUYEN_HAN";
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
            /// Tìm kiếm người dùng theo tài khoản hoặc họ tên
            /// </summary>
            public void TimTheoTaiKhoanHoacTen(String tuKhoa)
            {
                factory.TimTheoTaiKhoanHoacTen(tuKhoa);
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
                    nd.TrangThai = Convert.ToString(row["TRANG_THAI"]);
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


