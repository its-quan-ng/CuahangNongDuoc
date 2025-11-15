using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    public partial class frmSanPham : Form
    {
        SanPhamController ctrl = new SanPhamController();
        DonViTinhController ctrlDVT = new DonViTinhController();

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            // Cho phép NumericUpDown nhận được các giá trị âm từ CSDL (nếu dữ liệu cũ bị sai),
            // sau đó ta sẽ kiểm tra lại khi lưu bằng hàm KiemTraSanPhamHopLe().
            // Nếu không đặt Minimum nhỏ hơn, khi binding gặp giá trị âm sẽ phát sinh
            // System.ArgumentOutOfRangeException: 'Value' must be between 'Minimum' and 'Maximum'.
            numSoLuong.Minimum = -1000000;
            numDonGiaNhap.Minimum = -1000000000;
            numGiaBanSi.Minimum = -1000000000;
            numGiaBanLe.Minimum = -1000000000;

            ctrlDVT.HienthiAutoComboBox(cmbDVT);
            dataGridView.Columns.Add(ctrlDVT.HienthiDataGridViewComboBoxColumn());
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator,
                 txtMaSanPham, txtTenSanPham, cmbDVT, numSoLuong, numDonGiaNhap, numGiaBanSi, numGiaBanLe);
        }


        private void toolLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trước khi lưu xuống database
            if (!KiemTraSanPhamHopLe())
            {
                return;
            }

            bindingNavigatorPositionItem.Focus();

            // Đảm bảo các control commit giá trị về DataTable trước khi lưu
            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.EndEdit();
            }
            dataGridView.EndEdit();

            try
            {
                bool ketQua = ctrl.Save();
                if (!ketQua)
                {
                    // Không có dòng nào được cập nhật (ExecuteNoneQuery trả về 0)
                    MessageBox.Show(
                        "Lưu sản phẩm thất bại. Không có dữ liệu nào được cập nhật.",
                        "Sản phẩm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log cho developer
                System.Diagnostics.Debug.WriteLine("Lưu sản phẩm lỗi: " + ex.Message);

                // Thông báo cho người dùng
                MessageBox.Show(
                    "Đã xảy ra lỗi khi lưu sản phẩm.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Hàm validate dữ liệu sản phẩm hiện tại (row đang được binding lên các control)
        private bool KiemTraSanPhamHopLe()
        {
            // Tên sản phẩm bắt buộc nhập
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return false;
            }

            // Bắt buộc chọn đơn vị tính
            if (cmbDVT.SelectedValue == null)
            {
                MessageBox.Show("Bạn phải chọn đơn vị tính.", "Sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDVT.Focus();
                return false;
            }

            // Giá và số lượng không được âm
            if (numSoLuong.Value < 0)
            {
                MessageBox.Show("Số lượng không được âm.", "Sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoLuong.Focus();
                return false;
            }

            if (numDonGiaNhap.Value < 0 || numGiaBanSi.Value < 0 || numGiaBanLe.Value < 0)
            {
                MessageBox.Show("Đơn giá nhập, giá bán sỉ, giá bán lẻ không được âm.", "Sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (numDonGiaNhap.Value < 0) numDonGiaNhap.Focus();
                else if (numGiaBanSi.Value < 0) numGiaBanSi.Focus();
                else numGiaBanLe.Focus();
                return false;
            }

            return true;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            DataRow row = ctrl.NewRow();
            long maso = ThamSo.SanPham;
            ThamSo.SanPham = maso + 1;
            row["ID"] = maso;
            row["TEN_SAN_PHAM"] = "";
            row["SO_LUONG"] = 0;
            row["DON_GIA_NHAP"] = 0;
            row["GIA_BAN_SI"] = 0;
            row["GIA_BAN_LE"] = 0;
            ctrl.Add(row);
            bindingNavigator.BindingSource.MoveLast();

        }


        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "San Pham", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void btnThemDVT_Click(object sender, EventArgs e)
        {
            frmDonViTinh DVT = new frmDonViTinh();
            DVT.ShowDialog();
            ctrlDVT.HienthiAutoComboBox(cmbDVT);
        }


        private void toolTimMaSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = true;
            toolTimTenSanPham.Checked = false;
            toolTimSanPham.Text = "";

        }

        private void mnuTimTenSanPham_Click(object sender, EventArgs e)
        {
            toolTimMaSanPham.Checked = false;
            toolTimTenSanPham.Checked = true;
            toolTimSanPham.Text = "";
        }

        private void toolTimSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimSanPham();
            }
        }

        private void toolTimSanPham_Leave(object sender, EventArgs e)
        {
            TimSanPham();
        }

        void TimSanPham()
        {
            if (toolTimMaSanPham.Checked == true)
            {
                if (!string.IsNullOrEmpty(toolTimSanPham.Text))
                {
                    ctrl.TimMaSanPham(Convert.ToInt32(toolTimSanPham.Text));
                    if (bindingNavigator.BindingSource != null)
                    {
                        bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(toolTimSanPham.Text))
                {
                    ctrl.TimTenSanPham(toolTimSanPham.Text);
                    if (bindingNavigator.BindingSource != null)
                    {
                        bindingNavigator.BindingSource.DataSource = ctrl.GetDataTable();
                    }
                }
            }
        }

        private void toolTimSanPham_Enter(object sender, EventArgs e)
        {
            toolTimSanPham.Text = "";
            toolTimSanPham.ForeColor = Color.Black;
        }



    }
}