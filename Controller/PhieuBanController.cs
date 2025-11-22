using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{
    
    public class PhieuBanController
    {
        PhieuBanFactory factory = new PhieuBanFactory();

        BindingSource bs = new BindingSource();


        public PhieuBanController()
        {
            bs.DataSource = factory.LayPhieuBan(-1);
        }
        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Update()
        {
            bs.MoveNext();
            factory.Save();
        }
        public void Save()
        {
            factory.Save();
        }

        public void Refresh(bool isPhieuLe)
        {
            int currentPosition = bs.Position;

            if (isPhieuLe)
                bs.DataSource = factory.DanhsachPhieuBanLe();
            else
                bs.DataSource = factory.DanhsachPhieuBanSi();

            if (currentPosition < bs.Count)
                bs.Position = currentPosition;
        }

        public void HienthiPhieuBanLe(BindingNavigator bn, DataGridView dg)
        {

            bs.DataSource = factory.DanhsachPhieuBanLe();
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public void HienthiPhieuBanSi(BindingNavigator bn, DataGridView dg)
        {

            bs.DataSource = factory.DanhsachPhieuBanSi();
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public void HienthiPhieuBan(BindingNavigator bn,ComboBox cmb, TextBox txt, DateTimePicker dt, NumericUpDown numTongTien, NumericUpDown numDatra, NumericUpDown numConNo, NumericUpDown numChiPhiVanChuyen, NumericUpDown numChiPhiDichVu, NumericUpDown numChietKhau)
        {

            bn.BindingSource = bs;

            txt.DataBindings.Clear();
            txt.DataBindings.Add("Text", bs, "ID");

            cmb.DataBindings.Clear();
            cmb.DataBindings.Add("SelectedValue", bs, "ID_KHACH_HANG");

            dt.DataBindings.Clear();
            dt.DataBindings.Add("Value", bs, "NGAY_BAN");

            numTongTien.DataBindings.Clear();
            Binding bindingTongTien = new Binding("Value", bs, "TONG_TIEN");
            bindingTongTien.Format += new ConvertEventHandler(Binding_Format);
            numTongTien.DataBindings.Add(bindingTongTien);

            numDatra.DataBindings.Clear();
            Binding bindingDaTra = new Binding("Value", bs, "DA_TRA");
            bindingDaTra.Format += new ConvertEventHandler(Binding_Format);
            numDatra.DataBindings.Add(bindingDaTra);

            numConNo.DataBindings.Clear();
            Binding bindingConNo = new Binding("Value", bs, "CON_NO");
            bindingConNo.Format += new ConvertEventHandler(Binding_Format);
            numConNo.DataBindings.Add(bindingConNo);

            numChiPhiVanChuyen.DataBindings.Clear();
            Binding bindingChiPhiVanChuyen = new Binding("Value", bs, "CHI_PHI_VAN_CHUYEN");
            bindingChiPhiVanChuyen.Format += new ConvertEventHandler(Binding_Format);
            numChiPhiVanChuyen.DataBindings.Add(bindingChiPhiVanChuyen);

            numChiPhiDichVu.DataBindings.Clear();
            Binding bindingChiPhiDichVu = new Binding("Value", bs, "CHI_PHI_DICH_VU");
            bindingChiPhiDichVu.Format += new ConvertEventHandler(Binding_Format);
            numChiPhiDichVu.DataBindings.Add(bindingChiPhiDichVu);

            numChietKhau.DataBindings.Clear();
            Binding bindingChietKhau = new Binding("Value", bs, "CHIET_KHAU");
            bindingChietKhau.Format += new ConvertEventHandler(Binding_Format);
            numChietKhau.DataBindings.Add(bindingChietKhau);

        }

        // Hàm xử lý chuyển đổi DBNull thành 0
        private void Binding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value == DBNull.Value || e.Value == null)
            {
                e.Value = 0;
            }
        }

        public PhieuBan LayPhieuBan(int id)
        {
            DataTable tbl = factory.LayPhieuBan(id);
            PhieuBan ph = null;
            if (tbl.Rows.Count > 0)
            {

                ph = new PhieuBan();
                ph.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                
                ph.NgayBan = Convert.ToDateTime(tbl.Rows[0]["NGAY_BAN"]);
                ph.TongTien = Convert.ToInt64(tbl.Rows[0]["TONG_TIEN"]);
                ph.DaTra = Convert.ToInt64(tbl.Rows[0]["DA_TRA"]);
                ph.ConNo = Convert.ToInt64(tbl.Rows[0]["CON_NO"]);
                KhachHangController ctrlKH = new KhachHangController();
                ph.KhachHang = ctrlKH.LayKhachHang(Convert.ToInt32(tbl.Rows[0]["ID_KHACH_HANG"]));
                ChiTietPhieuBanController ctrl = new ChiTietPhieuBanController();
                ph.ChiTiet = ctrl.ChiTietPhieuBan(Convert.ToInt32(ph.Id));
            }
            return ph;
        }

        public void TimPhieuBan(int maKH, DateTime dt)
        {
            factory.TimPhieuBan(maKH, dt);

        }

        /// <summary>
        /// Kiểm tra xem phiếu bán có được sử dụng trong các bảng khác không
        /// </summary>
        public bool KiemTraLienKet(int idPhieuBan)
        {
            return factory.KiemTraLienKet(idPhieuBan);
        }

        /// <summary>
        /// Lấy danh sách các bảng có liên kết với phiếu bán
        /// </summary>
        public List<string> LayDanhSachBangLienKet(int idPhieuBan)
        {
            return factory.LayDanhSachBangLienKet(idPhieuBan);
        }

        // =============================================
        // YC4: CHIẾT KHẤU VÀ KHUYẾN MÃI
        // =============================================

        /// <summary>
        /// Tính tổng tiền hóa đơn sử dụng Decorator Pattern
        /// </summary>
        /// <param name="tongHang">Tổng tiền hàng (SUM THANH_TIEN)</param>
        /// <param name="chiPhiVC">Chi phí vận chuyển</param>
        /// <param name="chiPhiDV">Chi phí dịch vụ</param>
        /// <param name="chietKhau">% chiết khấu (0-100)</param>
        /// <param name="idKhuyenMai">ID khuyến mãi (nullable)</param>
        /// <param name="soLuongSanPham">Tổng số lượng sản phẩm</param>
        /// <returns>Tổng tiền cuối cùng</returns>
        public decimal TinhTongTien(
            decimal tongHang,
            decimal chiPhiVC,
            decimal chiPhiDV,
            decimal chietKhau,
            int? idKhuyenMai,
            int soLuongSanPham)
        {
            try
            {
                // Bước 1: Tạo base component
                Decorator.ITongTienComponent component = new Decorator.TongTienBase(tongHang);

                // Bước 2: Decorate chi phí (nếu có)
                if (chiPhiVC > 0 || chiPhiDV > 0)
                    component = new Decorator.ChiPhiDecorator(component, chiPhiVC, chiPhiDV);

                // Bước 3: Decorate chiết khấu (nếu có)
                if (chietKhau > 0)
                    component = new Decorator.ChietKhauDecorator(component, chietKhau, tongHang);

                // Bước 4: Decorate khuyến mãi (nếu có)
                if (idKhuyenMai.HasValue)
                {
                    KhuyenMaiController ctrlKM = new KhuyenMaiController();
                    KhuyenMai km = ctrlKM.LayKhuyenMai(idKhuyenMai.Value);

                    if (km != null)
                    {
                        component = new Decorator.KhuyenMaiDecorator(component, km, tongHang, soLuongSanPham);
                    }
                }

                return component.TinhTongTien();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi tính tổng tiền: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Tính số tiền chiết khấu (để UI hiển thị)
        /// </summary>
        public decimal TinhTienChietKhau(decimal tongHang, decimal chietKhau)
        {
            if (chietKhau == 0)
                return 0;

            return tongHang * chietKhau / 100;
        }

        /// <summary>
        /// Kiểm tra điều kiện khuyến mãi (không throw exception)
        /// </summary>
        /// <param name="idKhuyenMai">ID khuyến mãi</param>
        /// <param name="tongHang">Tổng tiền hàng</param>
        /// <param name="soLuongSanPham">Tổng số lượng sản phẩm</param>
        /// <param name="errorMessage">Output: Message lỗi nếu không đủ điều kiện</param>
        /// <returns>True nếu đủ điều kiện, False nếu không đủ</returns>
        public bool KiemTraDieuKienKhuyenMai(
            int idKhuyenMai,
            decimal tongHang,
            int soLuongSanPham,
            out string errorMessage)
        {
            errorMessage = "";

            KhuyenMaiController ctrlKM = new KhuyenMaiController();
            KhuyenMai km = ctrlKM.LayKhuyenMai(idKhuyenMai);

            if (km == null)
            {
                errorMessage = "Không tìm thấy khuyến mãi!";
                return false;
            }

            Specification.IKhuyenMaiSpecification spec;

            if (km.DieuKienLoai == "TONG_TIEN")
            {
                spec = new Specification.TongTienToiThieuSpecification(km.DieuKienGiaTri);
            }
            else if (km.DieuKienLoai == "SO_LUONG")
            {
                spec = new Specification.SoLuongToiThieuSpecification((int)km.DieuKienGiaTri);
            }
            else
            {
                errorMessage = "Loại điều kiện không hợp lệ!";
                return false;
            }

            bool isValid = spec.IsSatisfiedBy(tongHang, soLuongSanPham);
            if (!isValid)
            {
                errorMessage = spec.GetErrorMessage(tongHang, soLuongSanPham);
            }

            return isValid;
        }

        /// <summary>
        /// Tính tổng tiền giảm (Chiết khấu + Khuyến mãi)
        /// Dùng để hiển thị trên UI
        /// </summary>
        public decimal TinhTongTienGiam(
            decimal tongHang,
            decimal chietKhau,
            int? idKhuyenMai,
            int soLuongSanPham)
        {
            decimal tongGiam = 0;

            // Tiền chiết khấu
            if (chietKhau > 0)
            {
                tongGiam += tongHang * chietKhau / 100;
            }

            // Tiền khuyến mãi
            if (idKhuyenMai.HasValue)
            {
                KhuyenMaiController ctrlKM = new KhuyenMaiController();
                KhuyenMai km = ctrlKM.LayKhuyenMai(idKhuyenMai.Value);

                if (km != null)
                {
                    string error;
                    if (KiemTraDieuKienKhuyenMai(idKhuyenMai.Value, tongHang, soLuongSanPham, out error))
                    {
                        tongGiam += tongHang * km.TyLeGiam / 100;
                    }
                }
            }

            return tongGiam;
        }

    }
}
