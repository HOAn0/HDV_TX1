using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vali_main_.Models;

public partial class TDanhMucSp
{
    [Key]
    [Display(Name = "Mã sản phẩm")]
        
    public string MaSp { get; set; } = null!;
    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
    [MaxLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
    public string? TenSp { get; set; }

    [Display(Name = "Mã chất liệu")]
    public string? MaChatLieu { get; set; }
    [Display(Name = "Mã chất liệu")]
    public string? NganLapTop { get; set; }
    [Display(Name = "Model")]
    public string? Model { get; set; }
    [Display(Name = "Cân nặng")]
    public double? CanNang { get; set; }
    [Display(Name = "Độ nới")]
    public double? DoNoi { get; set; }
    [Display(Name = "Mã hãng SX")]
    public string? MaHangSx { get; set; }
    [Display(Name = "Mã nước SX")]
    public string? MaNuocSx { get; set; }
    [Display(Name = "Mã đặc tính")]
    public string? MaDacTinh { get; set; }
    [Display(Name = "Website")]
    public string? Website { get; set; }
    [Display(Name = "Thời gian bảo hành")]
    public double? ThoiGianBaoHanh { get; set; }
    [Display(Name = "Giới thiệu")]
    public string? GioiThieuSp { get; set; }
    [Display(Name = "Chiết khấu")]
    public double? ChietKhau { get; set; }
    [Display(Name = "Mã loại")]
    public string? MaLoai { get; set; }
    [Display(Name = "Mã đối tượng")]
    public string? MaDt { get; set; }
    [Display(Name = "Ảnh")]
    public string? AnhDaiDien { get; set; }
    [Display(Name = "Giá nhỏ nhất")]
    public decimal? GiaNhoNhat { get; set; }
    [Display(Name = "Giá lớn nhất")]
    public decimal? GiaLonNhat { get; set; }
    [Display(Name = "Chất liệu")]
    public virtual TChatLieu? MaChatLieuNavigation { get; set; }
    [Display(Name = "Đối tượng")]
    public virtual TLoaiDt? MaDtNavigation { get; set; }
    [Display(Name = "Hãng SX")]
    public virtual THangSx? MaHangSxNavigation { get; set; }
    [Display(Name = "Loại")]
    public virtual TLoaiSp? MaLoaiNavigation { get; set; }
    [Display(Name = "Nước SX")]
    public virtual TQuocGium? MaNuocSxNavigation { get; set; }
    [Display(Name = "Ảnh SP")]
    public virtual ICollection<TAnhSp> TAnhSps { get; set; } = new List<TAnhSp>();
    [Display(Name = "Chi Tiết SP")]
    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
