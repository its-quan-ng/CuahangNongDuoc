using System;
using System.Data;
using System.Windows.Forms;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Controller
{
    /// <summary>
    /// Controller: Business logic cho Khuyến mãi
    /// YC4: Quản lý và áp dụng khuyến mãi
    /// </summary>
    public class KhuyenMaiController
    {
        KhuyenMaiFactory factory = new KhuyenMaiFactory();
        BindingSource bs = new BindingSource();

        public KhuyenMaiController()
        {
            bs.DataSource = factory.DanhsachKhuyenMai();
        }

        /// <summary>
        /// Hiển thị danh sách khuyến mãi lên DataGridView (dùng cho frmKhuyenMai)
        /// </summary>
        public void HienthiDataGridview(BindingNavigator bn, DataGridView dg)
        {
            bs.DataSource = factory.DanhsachKhuyenMai();
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        /// <summary>
        /// Hiển thị danh sách khuyến mãi đang áp dụng lên ComboBox
        /// Dùng cho frmBanLe/frmBanSi
        /// </summary>
        public void HienthiComboBoxDangApDung(ComboBox cmb)
        {
            DataTable tbl = factory.LayKhuyenMaiDangApDung();
            cmb.DataSource = tbl;
            cmb.DisplayMember = "TEN_KHUYEN_MAI";
            cmb.ValueMember = "ID";
            cmb.SelectedIndex = -1; // Không chọn mặc định
        }

        /// <summary>
        /// Lấy thông tin 1 chương trình khuyến mãi và convert sang BusinessObject
        /// </summary>
        public KhuyenMai LayKhuyenMai(int id)
        {
            DataTable tbl = factory.LayKhuyenMai(id);
            if (tbl.Rows.Count == 0)
                return null;

            DataRow row = tbl.Rows[0];
            KhuyenMai km = new KhuyenMai();

            km.Id = Convert.ToString(row["ID"]);
            km.TenKhuyenMai = Convert.ToString(row["TEN_KHUYEN_MAI"]);
            km.TyLeGiam = Convert.ToDecimal(row["TY_LE_GIAM"]);
            km.DieuKienLoai = Convert.ToString(row["DIEU_KIEN_LOAI"]);
            km.DieuKienGiaTri = Convert.ToDecimal(row["DIEU_KIEN_GIA_TRI"]);
            km.TuNgay = Convert.ToDateTime(row["TU_NGAY"]);
            km.DenNgay = Convert.ToDateTime(row["DEN_NGAY"]);
            km.TrangThai = Convert.ToBoolean(row["TRANG_THAI"]);
            km.GhiChu = row["GHI_CHU"] != DBNull.Value ? Convert.ToString(row["GHI_CHU"]) : "";

            return km;
        }

        /// <summary>
        /// Tạo DataRow mới (dùng cho thêm mới)
        /// </summary>
        public DataRow NewRow()
        {
            return factory.NewRow();
        }

        /// <summary>
        /// Thêm DataRow vào DataTable
        /// </summary>
        public void Add(DataRow row)
        {
            factory.Add(row);
        }

        /// <summary>
        /// Lưu thay đổi vào database
        /// </summary>
        public bool Save()
        {
            return factory.Save();
        }
    }
}
