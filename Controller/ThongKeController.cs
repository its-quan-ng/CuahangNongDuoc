using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc.Controller
{
    public class ThongKeController
    {
        ThongKeFactory factory = new ThongKeFactory();

        public DataTable LayChiPhiVanChuyen(DateTime tuNgay, DateTime denNgay)
        {
            return factory.LayChiPhiVanChuyen(tuNgay, denNgay);
        }

        public DataTable LayChiPhiDichVu(DateTime tuNgay, DateTime denNgay)
        {
            return factory.LayChiPhiDichVu(tuNgay, denNgay);
        }
    }
}
