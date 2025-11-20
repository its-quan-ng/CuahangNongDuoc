using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class frmLyDoChi : Form
    {
        CuahangNongduoc.Controller.LyDoChiController ctrl = new CuahangNongduoc.Controller.LyDoChiController();

        public frmLyDoChi()
        {
            InitializeComponent();
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Ly Do Chi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Ly Do Chi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bindingNavigator.BindingSource.RemoveCurrent();
            }
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            ctrl.HienthiDataGridview(dataGridView, bindingNavigator);

            // Vô hiệu hóa AddNewItem mặc định để tránh lỗi, tuy nhiên logic Save sẽ xử lý hết
            bindingNavigator.AddNewItem = null;

            // Đăng ký event cho nút Thêm
            bindingNavigatorAddNewItem.Click += bindingNavigatorAddNewItem_Click;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            // Thêm dòng mới thông qua BindingSource
            // Việc gán ID sẽ được thực hiện khi Lưu để đảm bảo chính xác nhất
            bindingNavigator.BindingSource.AddNew();
            bindingNavigator.BindingSource.MoveLast();
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            bindingNavigatorPositionItem.Focus();
            bindingNavigator.BindingSource.EndEdit();

            // LOGIC QUAN TRỌNG: Tự động gán ID cho các dòng mới trước khi lưu
            // Điều này xử lý cả trường hợp nhấn nút Thêm và nhập trực tiếp vào Grid
            try
            {
                DataTable dt = ctrl.GetDataTable();

                // 1. Tìm ID lớn nhất hiện có
                int maxId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    // Bỏ qua các dòng đã xóa
                    if (row.RowState != DataRowState.Deleted && row["ID"] != DBNull.Value)
                    {
                        int currentId = Convert.ToInt32(row["ID"]);
                        if (currentId > maxId)
                        {
                            maxId = currentId;
                        }
                    }
                }

                // 2. Gán ID cho các dòng mới (Added) mà chưa có ID
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        if (row["ID"] == DBNull.Value)
                        {
                            maxId++; // Tăng ID lên 1
                            row["ID"] = maxId;
                        }
                    }
                }

                // 3. Thực hiện lưu
                if (ctrl.Save())
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}