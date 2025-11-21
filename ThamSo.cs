using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CuahangNongduoc
{
    public enum Controll
    {
        Normal,
        AddNew,
        Edit
    }
    public class ThamSo
    {
        public static void PreMonth(ref int thangtruoc, ref int namtruoc, int thang, int nam)
        {
            thangtruoc = thang - 1;
            namtruoc = nam;
            if (thangtruoc == 0)
            {
                thangtruoc = 12;
                namtruoc= nam - 1;
            }
        }

        public static bool LaSoNguyen(String so)
        {
            try
            {
                Convert.ToInt64(so);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static long LayMaPhieuNhap()
        {
            DataService ds = new DataService();
            object obj = ds.ExecuteScalar(new SqlCommand("SELECT PHIEU_NHAP FROM THAM_SO"));
            return Convert.ToInt64(obj);
        }
        public static void GanMaPhieuNhap(long id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_NHAP = @id");
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            ds.ExecuteNoneQuery(cmd);
        }

        public static long LayMaPhieuBan()
        {
            DataService ds = new DataService();
            object obj = ds.ExecuteScalar(new SqlCommand("SELECT PHIEU_BAN FROM THAM_SO"));
            return Convert.ToInt64(obj);
        }
        public static void GanMaPhieuBan(long id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_BAN = @id");
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            ds.ExecuteNoneQuery(cmd);
        }

        public static long LayMaPhieuThanhToan()
        {
            DataService ds = new DataService();
            object obj = ds.ExecuteScalar(new SqlCommand("SELECT PHIEU_THANH_TOAN FROM THAM_SO"));
            return Convert.ToInt64(obj);
        }
        public static void GanMaPhieuThanhToan(long id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_THANH_TOAN = @id");
            cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
            ds.ExecuteNoneQuery(cmd);
        }



        public static long SanPham
        {
            get 
            {
                DataService ds = new DataService();
                object obj = ds.ExecuteScalar(new SqlCommand("SELECT SAN_PHAM FROM THAM_SO"));
                return Convert.ToInt64(obj);
            }
            set 
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET SAN_PHAM = @value");
                cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
                ds.ExecuteNoneQuery(cmd);
            }
        }
	
        
        public static CuahangNongduoc.BusinessObject.CuaHang LayCuaHang()
        {
            CuahangNongduoc.BusinessObject.CuaHang ch = new CuahangNongduoc.BusinessObject.CuaHang();
            DataService ds = new DataService();
            ds.Load(new SqlCommand("SELECT TEN_CUA_HANG, DIA_CHI, DIEN_THOAI FROM THAM_SO"));
            if (ds.Rows.Count > 0)
            {
                ch.TenCuaHang = ds.Rows[0]["TEN_CUA_HANG"].ToString();
                ch.DiaChi = ds.Rows[0]["DIA_CHI"].ToString();
                ch.DienThoai = ds.Rows[0]["DIEN_THOAI"].ToString();
            }
            return ch;
        }
        public static void GanCuaHang(String ten_cua_hang, String dia_chi , String dien_thoai)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET TEN_CUA_HANG = @ten_cua_hang, DIA_CHI = @dia_chi, DIEN_THOAI = @dien_thoai ");
            cmd.Parameters.Add("@ten_cua_hang", SqlDbType.NVarChar).Value = ten_cua_hang;
            cmd.Parameters.Add("@dia_chi", SqlDbType.NVarChar).Value = dia_chi;
            cmd.Parameters.Add("@dien_thoai", SqlDbType.NVarChar).Value = dien_thoai;

            ds.ExecuteNoneQuery(cmd);
        }

        

        public static long NhaCungCap
        {
            get
            {
                DataService ds = new DataService();
                object obj = ds.ExecuteScalar(new SqlCommand("SELECT NHA_CUNG_CAP FROM THAM_SO"));
                return Convert.ToInt64(obj);
            }
            set
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET NHA_CUNG_CAP = @value");
                cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
                ds.ExecuteNoneQuery(cmd);
            }
        }

        public static long KhachHang
        {
            get
            {
                DataService ds = new DataService();
                object obj = ds.ExecuteScalar(new SqlCommand("SELECT KHACH_HANG FROM THAM_SO"));
                return Convert.ToInt64(obj);
            }
            set
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET KHACH_HANG = @value");
                cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
                ds.ExecuteNoneQuery(cmd);
            }
        }

        public static long PhieuChi
        {
            get
            {
                DataService ds = new DataService();
                object obj = ds.ExecuteScalar(new SqlCommand("SELECT PHIEU_CHI FROM THAM_SO"));
                return Convert.ToInt64(obj);
            }
            set
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_CHI = @value");
                cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
                ds.ExecuteNoneQuery(cmd);
            }
        }

        public static String PhuongPhapXuatKho
        {
            get
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT PHUONG_PHAP_XUAT_KHO FROM THAM_SO");
                object obj = ds.ExecuteScalar(cmd);

                if (obj != null && obj != DBNull.Value)
                {
                    String giatri = Convert.ToString(obj).ToUpper();

                    // Validate: chỉ chấp nhận FIFO hoặc CHI_DINH
                    if (giatri == "FIFO" || giatri == "CHI_DINH")
                        return giatri;
                }

                // Mặc định: FIFO
                return "FIFO";
            }
            set
            {
                // Validate trước khi lưu
                String giatri = value.ToUpper();
                if (giatri != "FIFO" && giatri != "CHI_DINH")
                {
                    throw new ArgumentException("Phương pháp xuất kho phải là FIFO hoặc CHI_DINH");
                }

                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHUONG_PHAP_XUAT_KHO = @value");
                cmd.Parameters.Add("@value", SqlDbType.VarChar, 20).Value = giatri;
                ds.ExecuteNoneQuery(cmd);
            }
        }

        public static String PhuongPhapTinhGiaXuat
        {
            get
            {
                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("SELECT PHUONG_PHAP_TINH_GIA_XUAT FROM THAM_SO");
                object obj = ds.ExecuteScalar(cmd);

                if (obj != null && obj != DBNull.Value)
                {
                    String giatri = Convert.ToString(obj).ToUpper();

                    // Validate: chỉ chấp nhận AVERAGE hoặc FIFO
                    if (giatri == "AVERAGE" || giatri == "FIFO")
                        return giatri;
                }

                // Mặc định: AVERAGE (Bình quân gia quyền)
                return "AVERAGE";
            }
            set
            {
                // Validate trước khi lưu
                String giatri = value.ToUpper();
                if (giatri != "AVERAGE" && giatri != "FIFO")
                {
                    throw new ArgumentException("Phương pháp tính giá phải là AVERAGE hoặc FIFO");
                }

                DataService ds = new DataService();
                SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHUONG_PHAP_TINH_GIA_XUAT = @value");
                cmd.Parameters.Add("@value", SqlDbType.VarChar, 30).Value = giatri;
                ds.ExecuteNoneQuery(cmd);
            }
        }

        public static string LayTenPhuongPhapTinhGia()
        {
            string phuongPhap = PhuongPhapTinhGiaXuat;
            return phuongPhap == "FIFO" ? "Giá FIFO" : "Giá BQGQ";
        }

        public static string LayTooltipPhuongPhapTinhGia(string loaiTooltip = "label")
        {
            string phuongPhap = PhuongPhapTinhGiaXuat;

            if (phuongPhap == "FIFO")
            {
                if (loaiTooltip == "textbox")
                    return "Giá của lô có ngày hết hạn sớm nhất";
                else
                    return "Giá xuất theo lô hết hạn sớm nhất (FEFO - First Expired First Out)";
            }
            else
            {
                if (loaiTooltip == "textbox")
                    return "Giá trung bình có tính trọng số theo số lượng";
                else
                    return "Giá bình quân gia quyền (Weighted Average)";
            }
        }

    }
}
