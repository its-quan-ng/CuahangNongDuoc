using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{

    public class ChiTietPhieuBanController
    {
        ChiTietPhieuBanFactory factory = new ChiTietPhieuBanFactory();



        public void HienThiChiTiet(DataGridView dgv, int idPhieuBan)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.LayChiTietPhieuBan(idPhieuBan);
            dgv.DataSource = bs;
        }
        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Save()
        {
            factory.Save();
        }

        /// <summary>
        /// Load data vào Factory để binding và save
        /// </summary>
        public void LoadData(int idPhieuBan)
        {
            factory.LoadData(idPhieuBan);
        }

        /// <summary>
        /// Get DataTable từ Factory để refresh binding
        /// </summary>
        public DataTable GetDataTable()
        {
            return factory.GetDataTable();
        }

        public IList<ChiTietPhieuBan> ChiTietPhieuBan(int idPhieuBan)
        {
            IList<ChiTietPhieuBan> ds = new List<ChiTietPhieuBan>();

            DataTable tbl = factory.LayChiTietPhieuBan(idPhieuBan);
            foreach (DataRow row in tbl.Rows)
            {
                MaSanPhamController ctrl = new MaSanPhamController();
                ChiTietPhieuBan ct = new ChiTietPhieuBan();
                ct.DonGia = Convert.ToInt64(row["DON_GIA"]);
                ct.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                ct.ThanhTien = Convert.ToInt64(row["THANH_TIEN"]);
                ct.MaSanPham = ctrl.LayMaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]));

                ds.Add(ct);
            }
            return ds;
        }

        public IList<ChiTietPhieuBan_rptSoLuongBan> ChiTietPhieuBan(DateTime dtTuNgay, DateTime dtDenNgay)
        {
            IList<ChiTietPhieuBan_rptSoLuongBan> ds = new List<ChiTietPhieuBan_rptSoLuongBan>();
            DataTable tbl = factory.LayChiTietPhieuBan(dtTuNgay,dtDenNgay);
            foreach (DataRow row in tbl.Rows)
            {
                MaSanPhamController ctrl = new MaSanPhamController();
                PhieuBanController pb_ctrl = new PhieuBanController();
                ChiTietPhieuBan_rptSoLuongBan ct = new ChiTietPhieuBan_rptSoLuongBan();

                ct.DonGia = Convert.ToInt64(row["DON_GIA"]);
                ct.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                ct.ThanhTien = Convert.ToInt64(row["THANH_TIEN"]);
                ct.MaSanPham = ctrl.LayMaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]));
                ct.PhieuBan = pb_ctrl.LayPhieuBan(Convert.ToInt32(row["ID_PHIEU_BAN"]));
                ct.TenSanPham = Convert.ToString(row["TEN_SAN_PHAM"]);
                if (row.Table.Columns.Contains("ID_MA_SAN_PHAM"))
                {
                    ct.MaSanPham_id = Convert.ToString( row["ID_MA_SAN_PHAM"] );
                }
                ds.Add(ct);
            }
            return ds;
        }
        public IList<ChiTietPhieuBan_rptSoLuongBan> ChiTietPhieuBan(int thang, int nam)
        {
            IList<ChiTietPhieuBan_rptSoLuongBan> ds = new List<ChiTietPhieuBan_rptSoLuongBan>();
            DataTable tbl = factory.LayChiTietPhieuBan(thang, nam);
            foreach (DataRow row in tbl.Rows)
            {
                MaSanPhamController ctrl = new MaSanPhamController();
                PhieuBanController pb_ctrl = new PhieuBanController();
                ChiTietPhieuBan_rptSoLuongBan ct = new ChiTietPhieuBan_rptSoLuongBan();

                ct.DonGia = Convert.ToInt64(row["DON_GIA"]);
                ct.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                ct.ThanhTien = Convert.ToInt64(row["THANH_TIEN"]);
                ct.MaSanPham = ctrl.LayMaSanPham(Convert.ToString(row["ID_MA_SAN_PHAM"]));
                ct.PhieuBan = pb_ctrl.LayPhieuBan(Convert.ToInt32(row["ID_PHIEU_BAN"]));
                ct.TenSanPham = Convert.ToString(row["TEN_SAN_PHAM"]);
                if (row.Table.Columns.Contains("ID_MA_SAN_PHAM"))
                {
                    ct.MaSanPham_id = Convert.ToString(row["ID_MA_SAN_PHAM"]);
                }
                ds.Add(ct);
            }
            return ds;
        }

    }
}
