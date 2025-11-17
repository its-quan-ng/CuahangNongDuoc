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
        { // Kiểm tra rỗng
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
        private void btnDangNhap_KeyPress(object sender, KeyPressEventArgs e)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMatKhau.PasswordChar = '\0';   // hiện dãy ký tự thật
            }
            else
            {
                txtMatKhau.PasswordChar = '*';    // che lại bằng *
            }
        }

    }
}


