using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Vali_main_.Models;

public partial class TChiTietHdb
{
    [Key, Column(Order = 0)]
    public string MaHoaDon { get; set; } = null!;
    [Key, Column(Order = 1)]
    public string MaChiTietSp { get; set; } = null!;
    [Display(Name ="Số lượng bán")]
    public int? SoLuongBan { get; set; }
    [Display(Name = "Đơn giá")]
    public decimal? DonGiaBan { get; set; }
    [Display(Name = "Giảm gián")]
    public double? GiamGia { get; set; }
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
    [Display(Name = "!!!")]
    public virtual TChiTietSanPham MaChiTietSpNavigation { get; set; } = null!;
    [Display(Name = "!!!!!")]
    public virtual THoaDonBan MaHoaDonNavigation { get; set; } = null!;
}
