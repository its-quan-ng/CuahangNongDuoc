using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;
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
            txtTenDangNhap.Focus();
        }
       
        private NguoiDungController ctrl = new NguoiDungController();

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Focus();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        { // VALIDATION: Kiểm tra rỗng
            if (String.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtTenDangNhap.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhau.Focus();
                return;
            }

            // ĐĂNG NHẬP
            try
            {
                // Gọi Controller để kiểm tra
                NguoiDung nguoiDung = ctrl.DangNhap(
                    txtTenDangNhap.Text.Trim(),
                    txtMatKhau.Text
                );

                if (nguoiDung != null)
                {
                    // ✅ ĐĂNG NHẬP THÀNH CÔNG

                    // Lưu vào PhienDangNhap (Singleton)
                    PhienDangNhap.DangNhap(
                        nguoiDung.Id,
                        nguoiDung.TenDangNhap,
                        nguoiDung.HoTen,
                        nguoiDung.QuyenHan
                    );

                    // Hiển thị thông báo
                    MessageBox.Show(
                        String.Format("Xin chào {0}!", nguoiDung.HoTen),
                        "Đăng Nhập Thành Công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Mở form chính
                    this.Hide();
                    frmMain frmChinh = new frmMain();
                    frmChinh.ShowDialog();

                    // Sau khi đóng frmMain → Đóng form đăng nhập
                    this.Close();
                }
                else
                {
                    // ❌ ĐĂNG NHẬP THẤT BẠI
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không đúng!",
                        "Lỗi Đăng Nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    // Clear mật khẩu, focus lại username
                    txtMatKhau.Clear();
                    txtTenDangNhap.SelectAll();
                    txtTenDangNhap.Focus();
                }
            }
            catch (Exception ex)
            {
                // LỖI DATABASE
                MessageBox.Show(
                    "Lỗi kết nối cơ sở dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                System.Diagnostics.Debug.WriteLine("frmDangNhap Error: " + ex.ToString());
            }

        }
        private void ThucHienDangNhap()
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text; // Không trim mật khẩu

            // Validate Rỗng
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    txtTenDangNhap.Focus();
                }
                else
                {
                    txtMatKhau.Focus();
                }
                return;
            }

            // Gọi Controller để xử lý Đăng nhập (Giả định NguoiDungController đã tồn tại)
           NguoiDung nguoiDung = ctrl.DangNhap(tenDangNhap, matKhau);

            if (nguoiDung != null)
            {
                // Đăng nhập thành công: Lưu vào PhienDangNhap
                PhienDangNhap.DangNhap(
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
                txtTenDangNhap.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Bạn có chắc muốn thoát?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        /// <summary>
        /// Enter trong txtTenDangNhap → Focus sang txtMatKhau
        /// </summary>
        private void txtTenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtMatKhau.Focus();
                    e.Handled = true;
                }
            }


        }

        private void btnThoat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap.PerformClick();
                e.Handled = true;
            }
        }

    }
}


