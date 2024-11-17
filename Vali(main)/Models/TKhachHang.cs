using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vali_main_.Models;

public partial class TKhachHang
{
    [Key]
    [Display(Name = "Mã khách hàng")]
    public string MaKhanhHang { get; set; } = null!;
    [Display(Name = "Tên TK")]
    public string? Username { get; set; }
    [Display(Name = "Tên khách hàng")]
    public string? TenKhachHang { get; set; }
    [Display(Name = "Ngày sinh")]
    public DateOnly? NgaySinh { get; set; }
    [Display(Name = "Số điện thoại")]
    public string? SoDienThoai { get; set; }
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }
    [Display(Name = "Loại khách hàng")]
    public byte? LoaiKhachHang { get; set; }
    [Display(Name = "Ảnh đại diện")]
    public string? AnhDaiDien { get; set; }
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
    [Display(Name = "Hóa đơn")]
    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();
    [Display(Name = "Tên TK")]
    public virtual TUser? UsernameNavigation { get; set; }
}
