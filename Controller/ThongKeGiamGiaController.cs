using CuahangNongduoc.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Controller
{
    internal class ThongKeGiamGiaController
    {
        ThongKeGiamGiaFactory factory = new ThongKeGiamGiaFactory();

        // ========================================
        // TAB 1: CHIẾT KHẤU (%)
        // ========================================

        /// <summary>
        /// Thống kê chiết khấu - TẤT CẢ nhân viên
        /// </summary>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable chứa danh sách phiếu có chiết khấu</returns>
        public DataTable ThongKeChietKhau(DateTime tuNgay, DateTime denNgay)
        {
            return factory.ThongKeChietKhau(tuNgay, denNgay);
        }

        /// <summary>
        /// Thống kê chiết khấu - THEO NHÂN VIÊN cụ thể
        /// </summary>
        /// <param name="idNguoiDung">ID nhân viên</param>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable chứa danh sách phiếu của nhân viên đó</returns>
        public DataTable ThongKeChietKhauTheoNhanVien(int idNguoiDung, DateTime tuNgay, DateTime denNgay)
        {
            return factory.ThongKeChietKhauTheoNhanVien(idNguoiDung, tuNgay, denNgay);
        }

        // ========================================
        // TAB 2: KHUYẾN MÃI (Chương trình)
        // ========================================

        /// <summary>
        /// Thống kê khuyến mãi - TẤT CẢ nhân viên
        /// </summary>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable chứa danh sách phiếu có áp dụng chương trình KM</returns>
        public DataTable ThongKeKhuyenMai(DateTime tuNgay, DateTime denNgay)
        {
            return factory.ThongKeKhuyenMai(tuNgay, denNgay);
        }

        /// <summary>
        /// Thống kê khuyến mãi - THEO NHÂN VIÊN cụ thể
        /// </summary>
        /// <param name="idNguoiDung">ID nhân viên</param>
        /// <param name="tuNgay">Từ ngày</param>
        /// <param name="denNgay">Đến ngày</param>
        /// <returns>DataTable chứa danh sách phiếu của nhân viên đó</returns>
        public DataTable ThongKeKhuyenMaiTheoNhanVien(int idNguoiDung, DateTime tuNgay, DateTime denNgay)
        {
            return factory.ThongKeKhuyenMaiTheoNhanVien(idNguoiDung, tuNgay, denNgay);
        }

    }
}
