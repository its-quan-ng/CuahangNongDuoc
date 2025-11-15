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
            // Commit dữ liệu đang nhập trên lưới về DataTable
            bindingNavigatorPositionItem.Focus();
            if (bindingNavigator.BindingSource != null)
            {
                bindingNavigator.BindingSource.EndEdit();
            }
            dataGridView.EndEdit();

            // Không cho phép bất kỳ bản ghi nào có Tên đơn vị trống
            if (bindingNavigator.BindingSource != null)
            {
                foreach (object item in bindingNavigator.BindingSource)
                {
                    DataRowView rowView = item as DataRowView;
                    if (rowView == null) continue;

                    string tenDonVi = Convert.ToString(rowView["TEN_DON_VI"]).Trim();
                    if (string.IsNullOrWhiteSpace(tenDonVi))
                    {
                        MessageBox.Show("Tên đơn vị không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Tìm dòng tương ứng trên DataGridView để focus lại ô tên đơn vị
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            DataRowView r = dataGridView.Rows[i].DataBoundItem as DataRowView;
                            if (r == rowView)
                            {
                                dataGridView.CurrentCell = dataGridView.Rows[i].Cells["colTenDV"];
                                dataGridView.BeginEdit(true);
                                break;
                            }
                        }

                        return;
                    }
                }

                // Kiểm tra trùng tên đơn vị (không phân biệt hoa/thường)
                var nameCounts = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
                foreach (object item in bindingNavigator.BindingSource)
                {
                    DataRowView rowView = item as DataRowView;
                    if (rowView == null) continue;

                    string tenDonVi = Convert.ToString(rowView["TEN_DON_VI"]).Trim();
                    if (string.IsNullOrWhiteSpace(tenDonVi)) continue; // đã kiểm tra ở vòng trên

                    if (!nameCounts.ContainsKey(tenDonVi))
                        nameCounts[tenDonVi] = 0;
                    nameCounts[tenDonVi]++;
                }

                string tenTrung = null;
                foreach (var kv in nameCounts)
                {
                    if (kv.Value > 1)
                    {
                        tenTrung = kv.Key;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(tenTrung))
                {
                    MessageBox.Show("Tên đơn vị '" + tenTrung + "' đã bị trùng. Vui lòng sửa lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Focus vào dòng đầu tiên có tên trùng (ngoại trừ dòng đầu tiên nếu muốn xử lý khác)
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        var cellValue = Convert.ToString(dataGridView.Rows[i].Cells["colTenDV"].Value).Trim();
                        if (string.Equals(cellValue, tenTrung, StringComparison.CurrentCultureIgnoreCase))
                        {
                            dataGridView.CurrentCell = dataGridView.Rows[i].Cells["colTenDV"];
                            dataGridView.BeginEdit(true);
                            break;
                        }
                    }

                    return;
                }
            }

            // Ghi xuống database; nếu không có dòng thay đổi thì cũng không báo lỗi gây hiểu nhầm
            try
            {
                ctrl.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu đơn vị tính.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}