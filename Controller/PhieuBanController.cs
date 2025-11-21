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

        public void HienthiPhieuBan(BindingNavigator bn,ComboBox cmb, TextBox txt, DateTimePicker dt, NumericUpDown numTongTien, NumericUpDown numDatra, NumericUpDown numConNo, NumericUpDown numChiPhiVanChuyen, NumericUpDown numChiPhiDichVu)
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

    }
}
