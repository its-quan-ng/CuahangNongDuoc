using System;
using System.Data;
using System.Windows.Forms;
using CuahangNongduoc.Controller;

namespace CuahangNongduoc
{
    /// <summary>
    /// Form quản lý khuyến mãi (CRUD)
    /// YC4: Thêm/Sửa/Xóa chương trình khuyến mãi
    /// </summary>
    public partial class frmKhuyenMai : Form
    {
        KhuyenMaiController ctrl = new KhuyenMaiController();

        public frmKhuyenMai()
        {
            InitializeComponent();
        }

        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            // Load danh sách khuyến mãi
            ctrl.HienthiDataGridview(bindingNavigator, dataGridView);

            // Binding controls với BindingSource
            BindingSource bs = bindingNavigator.BindingSource;

            txtTenKhuyenMai.DataBindings.Clear();
            txtTenKhuyenMai.DataBindings.Add("Text", bs, "TEN_KHUYEN_MAI");

            numTyLeGiam.DataBindings.Clear();
            numTyLeGiam.DataBindings.Add("Value", bs, "TY_LE_GIAM");

            dtTuNgay.DataBindings.Clear();
            dtTuNgay.DataBindings.Add("Value", bs, "TU_NGAY");

            dtDenNgay.DataBindings.Clear();
            dtDenNgay.DataBindings.Add("Value", bs, "DEN_NGAY");

            rtxtGhiChu.DataBindings.Clear();
            rtxtGhiChu.DataBindings.Add("Text", bs, "GHI_CHU");

            // Load ComboBox Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Vô hiệu hóa");
            cboTrangThai.SelectedIndex = 0;

            // Set mặc định RadioButton
            radTongTien.Checked = true;
            txtTongTien.Enabled = true;
            numSoLuong.Enabled = false;

            // Đăng ký events
            radTongTien.CheckedChanged += radTongTien_CheckedChanged;
            radSoLuong.CheckedChanged += radSoLuong_CheckedChanged;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            bs.CurrentChanged += BindingSource_CurrentChanged;
        }

        // Event: BindingSource thay đổi → Update RadioButton + cboTrangThai
        private void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)sender;
            if (bs.Current == null)
                return;

            try
            {
                DataRowView rowView = (DataRowView)bs.Current;

                // Update RadioButton + TextBox/Numeric theo DIEU_KIEN_LOAI
                string loai = rowView["DIEU_KIEN_LOAI"].ToString();
                decimal giaTri = Convert.ToDecimal(rowView["DIEU_KIEN_GIA_TRI"]);

                if (loai == "TONG_TIEN")
                {
                    radTongTien.Checked = true;
                    txtTongTien.Text = giaTri.ToString("N0");
                }
                else if (loai == "SO_LUONG")
                {
                    radSoLuong.Checked = true;
                    numSoLuong.Value = giaTri;
                }

                // Update ComboBox Trạng thái
                bool trangThai = Convert.ToBoolean(rowView["TRANG_THAI"]);
                cboTrangThai.SelectedIndex = trangThai ? 0 : 1;
            }
            catch
            {
            }
        }

        private void radTongTien_CheckedChanged(object sender, EventArgs e)
        {
            if (radTongTien.Checked)
            {
                txtTongTien.Enabled = true;
                numSoLuong.Enabled = false;
                numSoLuong.Value = 0;
            }
        }

        private void radSoLuong_CheckedChanged(object sender, EventArgs e)
        {
            if (radSoLuong.Checked)
            {
                txtTongTien.Enabled = false;
                txtTongTien.Text = "";
                numSoLuong.Enabled = true;
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
                return;

            try
            {
                DataRowView rowView = (DataRowView)dataGridView.CurrentRow.DataBoundItem;
                if (rowView == null)
                    return;

                string loai = rowView["DIEU_KIEN_LOAI"].ToString();
                decimal giaTri = Convert.ToDecimal(rowView["DIEU_KIEN_GIA_TRI"]);

                if (loai == "TONG_TIEN")
                {
                    radTongTien.Checked = true;
                    txtTongTien.Text = giaTri.ToString("N0");
                }
                else if (loai == "SO_LUONG")
                {
                    radSoLuong.Checked = true;
                    numSoLuong.Value = giaTri;
                }

                bool trangThai = Convert.ToBoolean(rowView["TRANG_THAI"]);
                cboTrangThai.SelectedIndex = trangThai ? 0 : 1;
            }
            catch
            {
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                DataRowView rowView = (DataRowView)row.DataBoundItem;
                if (rowView == null)
                    return;

                // Format cột Điều kiện
                if (dataGridView.Columns[e.ColumnIndex].Name == "colDieuKien")
                {
                    string loai = rowView["DIEU_KIEN_LOAI"].ToString();
                    decimal giaTri = Convert.ToDecimal(rowView["DIEU_KIEN_GIA_TRI"]);

                    if (loai == "TONG_TIEN")
                    {
                        e.Value = $"Tổng tiền: {giaTri:N0} đ";
                    }
                    else if (loai == "SO_LUONG")
                    {
                        e.Value = $"Số lượng: {giaTri:N0} SP";
                    }

                    e.FormattingApplied = true;
                }

                // Format cột Trạng thái
                if (dataGridView.Columns[e.ColumnIndex].Name == "colTinhTrang")
                {
                    bool trangThai = Convert.ToBoolean(rowView["TRANG_THAI"]);
                    e.Value = trangThai ? "Hoạt động" : "Vô hiệu hóa";
                    e.FormattingApplied = true;
                }
            }
            catch
            {
            }
        }

        private void toolLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lưu data từ RadioButton + ComboBox vào current row
                SyncCurrentRowFromUI();

                if (dataGridView.CurrentCell != null)
                {
                    dataGridView.EndEdit();
                }
                bindingNavigator.Focus();
                bindingNavigator.BindingSource.EndEdit();

                DataTable dt = (DataTable)bindingNavigator.BindingSource.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        if (row["TEN_KHUYEN_MAI"] == DBNull.Value || string.IsNullOrWhiteSpace(row["TEN_KHUYEN_MAI"].ToString()))
                        {
                            MessageBox.Show("Vui lòng nhập Tên chương trình!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (row["TY_LE_GIAM"] == DBNull.Value)
                        {
                            MessageBox.Show("Vui lòng nhập Tỷ lệ khuyến mãi!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        decimal tyLeGiam = Convert.ToDecimal(row["TY_LE_GIAM"]);
                        if (tyLeGiam <= 0 || tyLeGiam > 100)
                        {
                            MessageBox.Show("Tỷ lệ khuyến mãi phải từ 0-100%!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (row["DIEU_KIEN_LOAI"] == DBNull.Value || string.IsNullOrWhiteSpace(row["DIEU_KIEN_LOAI"].ToString()))
                        {
                            MessageBox.Show("Vui lòng chọn Điều kiện áp dụng!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string loai = row["DIEU_KIEN_LOAI"].ToString();
                        if (loai != "TONG_TIEN" && loai != "SO_LUONG")
                        {
                            MessageBox.Show("Loại điều kiện không hợp lệ!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (row["DIEU_KIEN_GIA_TRI"] == DBNull.Value)
                        {
                            MessageBox.Show("Vui lòng nhập Giá trị điều kiện!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        decimal giaTri = Convert.ToDecimal(row["DIEU_KIEN_GIA_TRI"]);
                        if (giaTri <= 0)
                        {
                            MessageBox.Show("Giá trị điều kiện phải > 0!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (row["TU_NGAY"] == DBNull.Value || row["DEN_NGAY"] == DBNull.Value)
                        {
                            MessageBox.Show("Vui lòng nhập Từ ngày và Đến ngày!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DateTime tuNgay = Convert.ToDateTime(row["TU_NGAY"]);
                        DateTime denNgay = Convert.ToDateTime(row["DEN_NGAY"]);
                        if (denNgay < tuNgay)
                        {
                            MessageBox.Show("Đến ngày phải >= Từ ngày!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (row["TRANG_THAI"] == DBNull.Value)
                        {
                            row["TRANG_THAI"] = true;
                        }
                    }
                }

                if (ctrl.Save())
                {
                    MessageBox.Show("Lưu thành công!", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ctrl.HienthiDataGridview(bindingNavigator, dataGridView);
                }
                else
                {
                    MessageBox.Show("Không có thay đổi để lưu.", "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Đồng bộ data từ RadioButton + ComboBox vào current row
        /// Gọi trước khi Save
        /// </summary>
        private void SyncCurrentRowFromUI()
        {
            BindingSource bs = bindingNavigator.BindingSource;
            if (bs.Current == null)
                return;

            try
            {
                DataRowView rowView = (DataRowView)bs.Current;
                DataRow row = rowView.Row;

                // Lưu điều kiện áp dụng
                if (radTongTien.Checked)
                {
                    row["DIEU_KIEN_LOAI"] = "TONG_TIEN";

                    if (!string.IsNullOrWhiteSpace(txtTongTien.Text))
                    {
                        decimal giaTri = decimal.Parse(txtTongTien.Text.Replace(",", ""));
                        row["DIEU_KIEN_GIA_TRI"] = giaTri;
                    }
                }
                else if (radSoLuong.Checked)
                {
                    row["DIEU_KIEN_LOAI"] = "SO_LUONG";
                    row["DIEU_KIEN_GIA_TRI"] = numSoLuong.Value;
                }

                // Lưu trạng thái
                row["TRANG_THAI"] = cboTrangThai.SelectedIndex == 0; // 0: Hoạt động = true, 1: Vô hiệu hóa = false
            }
            catch
            {
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo row mới
                DataRow row = ctrl.NewRow();

                // Gán giá trị mặc định
                row["TEN_KHUYEN_MAI"] = "";
                row["TY_LE_GIAM"] = 0;
                row["TU_NGAY"] = DateTime.Now.Date;
                row["DEN_NGAY"] = DateTime.Now.Date.AddMonths(1);
                row["TRANG_THAI"] = true;
                row["GHI_CHU"] = "";

                // Mặc định: Tổng tiền >= 100,000đ
                row["DIEU_KIEN_LOAI"] = "TONG_TIEN";
                row["DIEU_KIEN_GIA_TRI"] = 100000;

                // Thêm vào DataTable
                ctrl.Add(row);

                // Refresh DataGridView
                DataTable dt = ctrl.GetCurrentDataTable();
                bindingNavigator.BindingSource.DataSource = dt;
                bindingNavigator.BindingSource.MoveLast();

                // Focus vào textbox đầu tiên để user nhập liệu
                txtTenKhuyenMai.Focus();
                txtTenKhuyenMai.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message,
                    "Khuyến mãi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = bindingNavigator.BindingSource;
                if (bs.Current == null)
                {
                    MessageBox.Show("Vui lòng chọn chương trình khuyến mãi cần xóa!",
                        "Khuyến mãi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DataRowView rowView = (DataRowView)bs.Current;
                string tenKM = rowView["TEN_KHUYEN_MAI"].ToString();

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa chương trình '{tenKM}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa row
                    rowView.Row.Delete();

                    // Lưu thay đổi
                    if (ctrl.Save())
                    {
                        MessageBox.Show("Xóa thành công!",
                            "Khuyến mãi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Refresh DataGridView
                        ctrl.HienthiDataGridview(bindingNavigator, dataGridView);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa chương trình này!",
                            "Khuyến mãi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message,
                    "Khuyến mãi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void toolThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
