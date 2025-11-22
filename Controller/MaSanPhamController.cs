using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.Strategy;

namespace CuahangNongduoc.Controller
{
    public class MaSanPhamController
    {
        MaSanPhamFactory factory = new MaSanPhamFactory();
        
        public DataRow NewRow()
        {
            factory.LoadSchema();  // Factory tự check nếu đã load rồi
            return factory.NewRow();
        }

        public DataTable GetCurrentDataTable()
        {
            return factory.GetCurrentDataTable();
        }

        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public bool Save()
        {
            return factory.Save();
        }

        public SanPham LaySanPham(String idMaSanPham)
        {
            MaSanPhamFactory f = new MaSanPhamFactory();
            DataTable tbl = f.LaySanPham(idMaSanPham);
            SanPham sp = null;
            DonViTinhController ctrlDVT = new DonViTinhController();
            if (tbl.Rows.Count > 0)
            {
                sp =  new  SanPham();
                sp.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                sp.TenSanPham = Convert.ToString(tbl.Rows[0]["TEN_SAN_PHAM"]);
                sp.SoLuong = Convert.ToInt32(tbl.Rows[0]["SO_LUONG"]);
                sp.DonGiaNhap = Convert.ToInt64(tbl.Rows[0]["DON_GIA_NHAP"]);
                sp.GiaBanLe = Convert.ToInt64(tbl.Rows[0]["GIA_BAN_LE"]);
                sp.GiaBanSi = Convert.ToInt64(tbl.Rows[0]["GIA_BAN_SI"]);
                sp.DonViTinh = ctrlDVT.LayDVT(Convert.ToInt32(tbl.Rows[0]["ID_DON_VI_TINH"]));
            }
            return sp;

        }



        public MaSanPham LayMaSanPham(String idMaSanPham)
        {
            MaSanPhamFactory f = new MaSanPhamFactory();
            DataTable tbl = f.LayMaSanPham(idMaSanPham);
            MaSanPham sp = null;
            SanPhamController ctrlSanPham = new SanPhamController();
            if (tbl.Rows.Count > 0)
            {
                sp = new MaSanPham();
                sp.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                sp.SoLuong = Convert.ToInt32(tbl.Rows[0]["SO_LUONG"]);
                sp.GiaNhap = Convert.ToInt64(tbl.Rows[0]["DON_GIA_NHAP"]);
                sp.NgayNhap = Convert.ToDateTime(tbl.Rows[0]["NGAY_NHAP"]);
                sp.NgaySanXuat = Convert.ToDateTime(tbl.Rows[0]["NGAY_SAN_XUAT"]);
                sp.NgayHetHan = Convert.ToDateTime(tbl.Rows[0]["NGAY_HET_HAN"]);
                sp.SanPham = ctrlSanPham.LaySanPham(Convert.ToInt32(tbl.Rows[0]["ID_SAN_PHAM"]));
            }
            return sp;
        }


        public static IList<MaSanPham_DBO> LayMaSanPhamHetHan(DateTime dt)
        {
            IList<MaSanPham_DBO> ds = new List<MaSanPham_DBO>();
            MaSanPhamFactory f = new MaSanPhamFactory();
            DataTable tbl = f.DanhsachMaSanPhamHetHan(dt);

            MaSanPham_DBO sp = null;
            SanPhamController ctrlSanPham = new SanPhamController();

            foreach  ( DataRow row in tbl.Rows)
            {
                sp = new MaSanPham_DBO();
                sp.Id = Convert.ToString(row["ID"]);
                sp.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                sp.GiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]);
                sp.NgayNhap = Convert.ToDateTime(row["NGAY_NHAP"]);
                sp.NgaySanXuat = Convert.ToDateTime(row["NGAY_SAN_XUAT"]);
                sp.NgayHetHan = Convert.ToDateTime(row["NGAY_HET_HAN"]);
                sp.SanPham = ctrlSanPham.LaySanPham(Convert.ToInt32(row["ID_SAN_PHAM"]));
                if (row.Table.Columns.Contains("TEN_SAN_PHAM"))
                {
                    sp.TenSanPham = Convert.ToString(row["TEN_SAN_PHAM"]);
                }
                ds.Add(sp);
            }
            return ds;

        }

        public void HienThiAutoComboBox(int sp, ComboBox cmb)
        {
            cmb.DataSource = factory.DanhsachMaSanPham(sp);
            cmb.DisplayMember = "ID";
            cmb.ValueMember = "ID";
        }

        public void HienThiDataGridViewComboBox(DataGridViewComboBoxColumn cmb)
        {
            cmb.DataSource = factory.DanhsachMaSanPham();
            cmb.DisplayMember = "ID";
            cmb.ValueMember = "ID";
            cmb.DataPropertyName = "ID_MA_SAN_PHAM";
            cmb.HeaderText = "Mã sản phẩm";
        }

        public void HienThiChiTietPhieuNhap(int id, DataGridView dg)
        {
            
            dg.DataSource = factory.DanhsachChiTiet(id);
        }
        public IList<MaSanPham> ChiTietPhieuNhap(int id)
        {
            SanPhamController ctrlSanPham = new SanPhamController();
            IList<MaSanPham> ds = new List<MaSanPham>();
            DataTable tbl = factory.DanhsachChiTiet(id);
            foreach (DataRow row in tbl.Rows)
            {
                MaSanPham sp = new MaSanPham();
                sp = new MaSanPham();
                sp.Id = Convert.ToString(row["ID"]);
                sp.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                sp.GiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]);

                sp.ThanhTien = sp.SoLuong * sp.GiaNhap;
                sp.NgayNhap = Convert.ToDateTime(row["NGAY_NHAP"]);
                sp.NgaySanXuat = Convert.ToDateTime(row["NGAY_SAN_XUAT"]);
                sp.NgayHetHan = Convert.ToDateTime(row["NGAY_HET_HAN"]);
                sp.SanPham = ctrlSanPham.LaySanPham(Convert.ToInt32(row["ID_SAN_PHAM"]));
                ds.Add(sp);
            }
            return ds;
        }

        // ═══════════════════════════════════════════════════════════════
        // TASK 4: LOGIC FIFO CƠ BẢN (Wrapper methods)
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Chọn lô tự động theo FIFO (FEFO - hết hạn sớm nhất)
        /// Wrapper method cho Strategy Pattern
        /// </summary>
        public IList<MaSanPham> ChonLoFIFO(int idSanPham, int soLuongCan)
        {
            // Gọi Strategy Pattern
            IXuatKhoStrategy strategy = new FifoXuatKhoStrategy();
            return strategy.ChonLoXuat(idSanPham, soLuongCan);
        }

        /// <summary>
        /// Tính giá xuất cho 1 sản phẩm (tất cả lô còn hàng)
        /// Sử dụng để hiển thị giá trong form (txtGiaBQGQ)
        /// </summary>
        public long TinhGiaXuat(int idSanPham)
        {
            if (idSanPham <= 0)
            {
                return 0;
            }

            try
            {
                MaSanPhamFactory factory = new MaSanPhamFactory();
                DataTable tblLo = factory.LayDanhSachLoConHang(idSanPham);

                if (tblLo == null || tblLo.Rows.Count == 0)
                {
                    return 0;
                }

                // Chuyển DataTable → IList<MaSanPham>
                IList<MaSanPham> danhSachLo = new List<MaSanPham>();

                foreach (DataRow row in tblLo.Rows)
                {
                    MaSanPham msp = new MaSanPham
                    {
                        Id = Convert.ToString(row["ID"]),
                        SoLuong = Convert.ToInt32(row["SO_LUONG"]),
                        GiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]),
                        NgayNhap = Convert.ToDateTime(row["NGAY_NHAP"]),
                        NgayHetHan = Convert.ToDateTime(row["NGAY_HET_HAN"])
                    };
                    danhSachLo.Add(msp);
                }

                // Gọi Strategy tính giá
                long giaXuat = TinhGiaTheoConfig(danhSachLo);
                return giaXuat;
            }
            catch (Exception ex)
            {
                return 0;  
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // STRATEGY PATTERN: XUẤT KHO VÀ TÍNH GIÁ
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Chọn lô xuất theo cấu hình (FIFO tự động hoặc CHI_DINH thủ công)
        /// Sử dụng Strategy Pattern
        /// </summary>
        /// <param name="idSanPham">ID sản phẩm cần xuất</param>
        /// <param name="soLuongCan">Số lượng cần xuất</param>
        /// <returns>Danh sách lô đã chọn (mỗi lô chứa ID, số lượng, giá nhập...)</returns>
        public IList<MaSanPham> ChonLoTheoConfig(int idSanPham, int soLuongCan)
        {
            if (idSanPham <= 0)
            {
                throw new ArgumentException("ID sản phẩm không hợp lệ!");
            }

            if (soLuongCan <= 0)
            {
                throw new ArgumentException("Số lượng cần xuất phải lớn hơn 0!");
            }

            string phuongPhap = ThamSo.PhuongPhapXuatKho;

            if (phuongPhap == "FIFO")
            {
                IXuatKhoStrategy strategy = new FifoXuatKhoStrategy();
                return strategy.ChonLoXuat(idSanPham, soLuongCan);
            }
            else
            {
                throw new NotImplementedException(
                    "Mode CHỈ ĐỊNH: User tự chọn lô trong form. Method này không được gọi."
                );
            }
        }

        /// <summary>
        /// Tính giá xuất (COGS) theo cấu hình (AVERAGE hoặc FIFO)
        /// Sử dụng Strategy Pattern
        /// </summary>
        /// <param name="danhSachLo">Danh sách lô đã xuất</param>
        /// <returns>Giá xuất trung bình (đơn vị: đồng)</returns>
        public long TinhGiaTheoConfig(IList<MaSanPham> danhSachLo)
        {
            if (danhSachLo == null || danhSachLo.Count == 0)
            {
                throw new ArgumentException("Danh sách lô không được rỗng!");
            }

            string phuongPhap = ThamSo.PhuongPhapTinhGiaXuat;

            if (phuongPhap == "AVERAGE")
            {
                ITinhGiaXuatStrategy strategy = new WeightedAverageGiaStrategy();
                return strategy.TinhGiaXuat(danhSachLo);
            }
            else
            {
                ITinhGiaXuatStrategy strategy = new FifoGiaStrategy();
                return strategy.TinhGiaXuat(danhSachLo);
            }
        }

    }
}
