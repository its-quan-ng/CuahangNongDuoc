using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void SetupEvents()
        {
            // 1. Thêm event Click cho button Đăng nhập
            btnDangNhap.Click += btnDangNhap_Click;

            // 2. Thêm event KeyDown (để bắt Enter Key) cho các field
            txtDangNhap.KeyDown += TxtField_KeyDown;
            txtMatKhau.KeyDown += TxtField_KeyDown;
        }
        private void TxtField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Logic: username → password → login
                if (sender == txtDangNhap)
                {
                    txtMatKhau.Focus();
                }
                else if (sender == txtMatKhau)
                {
                    ThucHienDangNhap();
                }

                // Chặn tiếng beep khi nhấn Enter trong TextBox
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ThucHienDangNhap();
        }
        private void ThucHienDangNhap()
        {
            string tenDangNhap = txtDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text; // Không trim mật khẩu

            // Validate Rỗng
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    txtDangNhap.Focus();
                }
                else
                {
                    txtMatKhau.Focus();
                }
                return;
            }

            // Gọi Controller để xử lý Đăng nhập (Giả định NguoiDungController đã tồn tại)
            NguoiDung nguoiDung = NguoiDungController.Instance.DangNhap(tenDangNhap, matKhau);

            if (nguoiDung != null)
            {
                // Đăng nhập thành công: Lưu vào PhienDangNhap
                PhienDangNhap.Instance.DangNhap(
                    nguoiDung.Id,
                    nguoiDung.TenDangNhap,
                    nguoiDung.HoTen,
                    nguoiDung.QuyenHan
                );

                MessageBox.Show($"Đăng nhập thành công! Chào mừng {nguoiDung.HoTen}.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở frmMain và đóng frmDangNhap
                frmMain mainForm = new frmMain();
                mainForm.Show();
                this.Hide(); // Ẩn form hiện tại
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không đúng. Vui lòng thử lại.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtDangNhap.Focus();
            }
        }
    }

}
