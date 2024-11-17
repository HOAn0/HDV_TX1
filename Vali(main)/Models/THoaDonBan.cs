/*using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Vali_main_.Models;

public partial class THoaDonBan
{
    [Key]
    [Display(Name ="Mã hóa đơn")]
    [Required(ErrorMessage = "Mã hóa đơn là bắt buộc.")]
    [MaxLength(20, ErrorMessage = "Mã hóa đơn không được vượt quá 20 ký tự.")]
    public string MaHoaDon { get; set; } = null!;
    [Display(Name = "Ngày xuất")]
    [Required(ErrorMessage = "Ngày xuất hóa đơn là bắt buộc.")]
    public DateTime NgayHoaDon { get; set; }
    [Display(Name = "Mã khách hàng")]
    public string? MaKhachHang { get; set; }
    [Display(Name = "Mã nhân viên")]
    public string? MaNhanVien { get; set; }
    [Display(Name = "Tổng tiền")]
    public decimal TongTienHd { get; set; }
    [Display(Name = "Giảm giá")]
    public double? GiamGiaHd { get; set; }
    
    [Display(Name = "Phương thức thanh toán")]
    public byte? PhuongThucThanhToan { get; set; }

    [Display(Name = "Mã số thuế")]
    public string? MaSoThue { get; set; }
    [Display(Name = "Thông tin thuế")]
    public string? ThongTinThue { get; set; }
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
    [Display(Name = "Tên khách hàng")]
    public virtual TKhachHang? MaKhachHangNavigation { get; set; }
    [Display(Name = "Tên nhân viên")]
    public virtual TNhanVien? MaNhanVienNavigation { get; set; }
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();
}*/
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Vali_main_.Models;

public partial class THoaDonBan
{
    [Key]
    [Display(Name = "Mã hóa đơn")]
    public string MaHoaDon { get; set; } = null!;
    [Display(Name = "Ngày xuất")]
    public DateTime? NgayHoaDon { get; set; }
    [Display(Name = "Mã khách hàng")]
    public string? MaKhachHang { get; set; }
    [Display(Name = "Mã nhân viên")]
    public string? MaNhanVien { get; set; }
    [Display(Name = "Tổng tiền")]
    public decimal? TongTienHd { get; set; }
    [Display(Name = "Giảm giá")]
    public double? GiamGiaHd { get; set; }
    [Display(Name = "Phương thức thanh toán")]
    public byte? PhuongThucThanhToan { get; set; }
    [Display(Name = "Mã số thuế")]
    public string? MaSoThue { get; set; }
    [Display(Name = "Thông tin thuế")]
    public string? ThongTinThue { get; set; }
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
    [Display(Name = "Tên khách hàng")]
    public virtual TKhachHang? MaKhachHangNavigation { get; set; }
    [Display(Name = "Tên nhân viên")]
    public virtual TNhanVien? MaNhanVienNavigation { get; set; }
    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

   /* public enum PhuongThucThanhToanEnum
    {
        [Display(Name = "Tiền mặt")]
        TienMat = 1,

        [Display(Name = "Thẻ")]
        The = 2,

        [Display(Name = "Online")]
        Online = 3
    }*/

}
