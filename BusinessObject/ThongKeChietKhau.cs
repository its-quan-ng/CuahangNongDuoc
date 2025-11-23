using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.BusinessObject
{
    public class ThongKeChietKhau
    {
        // Properties matching RDLC field names exactly

        // 1. STT (Row number)
        private long m_STT;
        public long STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }

        // 2. Ngày bán (as string formatted)
        private string m_NgayBan_Str;
        public string Ngay_Ban
        {
            get { return m_NgayBan_Str; }
            set { m_NgayBan_Str = value; }
        }

        // 3. Mã phiếu
        private int m_MaPhieu_Int;
        public int Ma_Phieu
        {
            get { return m_MaPhieu_Int; }
            set { m_MaPhieu_Int = value; }
        }

        // 4. Khách hàng
        private string m_KhachHang_Str;
        public string Khach_Hang
        {
            get { return m_KhachHang_Str; }
            set { m_KhachHang_Str = value; }
        }

        // 5. Chiết khấu (%)
        private decimal m_ChietKhauPercent;
        public decimal Chiet_Khau_Percent
        {
            get { return m_ChietKhauPercent; }
            set { m_ChietKhauPercent = value; }
        }

        // 6. Số tiền giảm (VND)
        private decimal m_SoTienGiam;
        public decimal So_Tien_Giam
        {
            get { return m_SoTienGiam; }
            set { m_SoTienGiam = value; }
        }

        // 7. Người tạo
        private string m_NguoiTao_Str;
        public string Nguoi_Tao
        {
            get { return m_NguoiTao_Str; }
            set { m_NguoiTao_Str = value; }
        }

        // 8. Tổng tiền
        private long m_TongTien_Long;
        public long Tong_Tien
        {
            get { return m_TongTien_Long; }
            set { m_TongTien_Long = value; }
        }

        // ===== Legacy properties (for backward compatibility) =====

        // 1. Ngày bán
        private DateTime m_NgayBan;
        public DateTime NgayBan
        {
            get { return m_NgayBan; }
            set { m_NgayBan = value; }
        }

        // 2. Mã phiếu
        private string m_MaPhieu;
        public string MaPhieu
        {
            get { return m_MaPhieu; }
            set { m_MaPhieu = value; }
        }

        // 3. Khách hàng
        private string m_KhachHang;
        public string KhachHang
        {
            get { return m_KhachHang; }
            set { m_KhachHang = value; }
        }

        // 4. % Chiết khấu
        private decimal m_ChietKhau;
        public decimal ChietKhau
        {
            get { return m_ChietKhau; }
            set { m_ChietKhau = value; }
        }

        // 6. Người tạo
        private string m_NguoiTao;
        public string NguoiTao
        {
            get { return m_NguoiTao; }
            set { m_NguoiTao = value; }
        }

        // 7. Tổng tiền (trước chiết khấu)
        private long m_TongTien;
        public long TongTien
        {
            get { return m_TongTien; }
            set { m_TongTien = value; }
        }

        // 8. (Optional) Tổng sau chiết khấu
        private long m_TongSauGiam;
        public long TongSauGiam
        {
            get { return m_TongSauGiam; }
            set { m_TongSauGiam = value; }
        }

        // Constructor
        public ThongKeChietKhau()
        {
        }

        public ThongKeChietKhau(DateTime ngayBan, string maPhieu, string khachHang,
                                decimal chietKhau, decimal soTienGiam, string nguoiTao,
                                long tongTien, long tongSauGiam)
        {
            m_NgayBan = ngayBan;
            m_MaPhieu = maPhieu;
            m_KhachHang = khachHang;
            m_ChietKhau = chietKhau;
            m_SoTienGiam = soTienGiam;
            m_NguoiTao = nguoiTao;
            m_TongTien = tongTien;
            m_TongSauGiam = tongSauGiam;
        }



    }
}
