using System;
using System.Collections.Generic;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.Strategy
{
    public interface IXuatKhoStrategy
    {
        IList<MaSanPham> ChonLoXuat(int idSanPham, int soLuongCanXuat);
    }
}
