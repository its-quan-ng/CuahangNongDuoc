using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Controller;


namespace CuahangNongduoc
{
    public partial class frmDunoKhachhang : Form
    {
        public frmDunoKhachhang()
        {
            InitializeComponent();
        }
        DuNoKhachHangController ctrl = new DuNoKhachHangController();
        KhachHangController ctrlKH = new KhachHangController();

        // Biến cờ để theo dõi trạng thái dữ liệu
        private bool daTongHopChuaLuu = false;

        private void frmDunoKhachhang_Load(object sender, EventArgs e)
        {

            this.toolThang.SelectedIndex = DateTime.Now.Month - 1;
            this.toolNam.Text = DateTime.Now.Year.ToString();
            ctrlKH.HienthiKhachHangChungDataGridviewComboBox(colKhachHang);

        }


        private void toolNam_Validating(object sender, CancelEventArgs e)
        {
            bool ok = true;
            try
            {
                int nam = Convert.ToInt32(toolNam.Text);
                int namHienTai = DateTime.Now.Year;
                if (nam < 2000 || nam > namHienTai + 5)
                {
                    ok = false;
                }
            }
            catch
            {
                ok = false;
            }
            if (!ok)
            {
                MessageBox.Show("Năm không hợp lệ! Vui lòng nhập năm từ 2000 đến " + (DateTime.Now.Year + 5),
                    "Tổng hợp dư nợ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void toolTongHop_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu đã tổng hợp trước đó mà chưa lưu
            if (daTongHopChuaLuu)
            {
                DialogResult result = MessageBox.Show("Dữ liệu lần tổng hợp trước chưa được lưu.\nBạn có muốn lưu lại không?",
                    "Cảnh báo chưa lưu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Người dùng chọn Lưu
                    if (ctrl.Save())
                    {
                        MessageBox.Show("Lưu dữ liệu cũ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        daTongHopChuaLuu = false;
                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Dừng lại, không tổng hợp mới
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // Người dùng chọn Hủy -> Không làm gì cả
                    return;
                }
                // Nếu chọn No -> Tiếp tục tổng hợp mới (bỏ qua dữ liệu cũ)
            }

            dataGridView.DataSource = null;
            toolProgress.Visible = true;
            ctrl.Tonghop(toolThang.SelectedIndex + 1, Convert.ToInt32(toolNam.Text), toolProgress, dataGridView, bindingNavigator);
            toolProgress.Visible = false;

            // Đánh dấu là đã tổng hợp nhưng chưa lưu
            daTongHopChuaLuu = true;
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            toolThang.Focus();
            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Đánh dấu đã lưu
                daTongHopChuaLuu = false;
            }
            else
            {
                MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            // Kiểm tra khi thoát
            if (daTongHopChuaLuu)
            {
                DialogResult result = MessageBox.Show("Dữ liệu chưa được lưu. Bạn có muốn lưu trước khi thoát không?",
                    "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (ctrl.Save())
                    {
                        MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Không đóng form nếu lưu lỗi
                    }
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
                // Cancel -> Không đóng form
            }
            else
            {
                this.Close();
            }
        }

        private void toolIn_Click(object sender, EventArgs e)
        {
            if (bindingNavigator.BindingSource == null || bindingNavigator.BindingSource.Current == null)
            {
                return;
            }

            DataRowView row = (DataRowView)bindingNavigator.BindingSource.Current;
            KhachHangController ctrlKH = new KhachHangController();
            DuNoKhachHang dn = new DuNoKhachHang();

            dn.Thang = Convert.ToInt32(row["THANG"]);
            dn.Nam = Convert.ToInt32(row["NAM"]);
            dn.DauKy = Convert.ToInt64(row["DAU_KY"]);
            dn.PhatSinh = Convert.ToInt64(row["PHAT_SINH"]);
            dn.DaTra = Convert.ToInt64(row["DA_TRA"]);
            dn.CuoiKy = Convert.ToInt64(row["CUOI_KY"]);
            dn.KhachHang = ctrlKH.LayKhachHang(Convert.ToInt32(row["ID_KHACH_HANG"]));

            frmInDunoKhachHang InDuNo = new frmInDunoKhachHang(dn);
            InDuNo.Show();
        }
    }
}