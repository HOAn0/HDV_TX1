using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vali_main_.Models;

public partial class TChiTietSanPham
{
    [Key]
    [Display(Name = "Mã chi tiết SP")]
    public string MaChiTietSp { get; set; } = null!;

    [Display(Name = "Mã sản phẩm")]
    public string? MaSp { get; set; }
    [Display(Name = "Kích thước")]
    public string? MaKichThuoc { get; set; }
    [Display(Name = "Màu sắc")]
    public string? MaMauSac { get; set; }
    [Display(Name = "Ảnh")]
    public string? AnhDaiDien { get; set; }
    [Display(Name = "Video")]
    public string? Video { get; set; }
    [Display(Name = "Giá nhập")]
    public double? DonGiaBan { get; set; }
    [Display(Name = "Giá bán")]
    public double? GiamGia { get; set; }
    [Display(Name = "Số lượng")]
    public int? Slton { get; set; }
    [Display(Name = "Kích thước")]
    public virtual TKichThuoc? MaKichThuocNavigation { get; set; }
    [Display(Name = "Màu sắc")]
    public virtual TMauSac? MaMauSacNavigation { get; set; }
    [Display(Name = "Sản phẩm")]
    public virtual TDanhMucSp? MaSpNavigation { get; set; }
    [Display(Name = "Ảnh")]
    public virtual ICollection<TAnhChiTietSp> TAnhChiTietSps { get; set; } = new List<TAnhChiTietSp>();
    [Display(Name = "Hóa đơn")]
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();
}
