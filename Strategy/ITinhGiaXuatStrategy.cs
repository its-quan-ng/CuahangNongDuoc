using CuahangNongduoc.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuahangNongduoc.Strategy
{
    internal interface ITinhGiaXuatStrategy
    {
        
        long TinhGiaXuat(IList<MaSanPham> danhSachLoXuat);

    }
}
