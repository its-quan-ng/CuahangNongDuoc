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
    public partial class frmDonViTinh : Form
    {
        DonViTinhController ctrl = new DonViTinhController();
        public frmDonViTinh()
        {
            InitializeComponent();

            // Xử lý lỗi dữ liệu trên lưới để tránh treo ô nhập khi nhập ký tự đặc biệt (vd: @@)
            this.dataGridView.DataError += dataGridView_DataError;
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Chặn exception làm treo ô nhập, đồng thời báo lỗi thân thiện
            e.ThrowException = false;
            e.Cancel = false;

            string message = "Dữ liệu không hợp lệ trong ô đang nhập. Vui lòng kiểm tra lại.";
            if (e.Exception != null && !string.IsNullOrEmpty(e.Exception.Message))
            {
                // Nếu cần có thêm thông tin kỹ thuật thì nối thêm, còn không thì có thể bỏ phần này
                message += "\nChi tiết: " + e.Exception.Message;
            }

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigator.BindingSource.EndEdit();

            if (ctrl.Save())
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}