using System;
using System.Collections.Generic;

namespace QLKS.Data;

public partial class DatPhong
{
    public int MaDatPhong { get; set; }

    public int? MaNv { get; set; }

    public int? MaKh { get; set; }

<<<<<<< HEAD
    public string MaPhong { get; set; } = null!;
=======
    public string? MaPhong { get; set; }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

    public DateOnly? NgayDat { get; set; }

    public DateTime? NgayNhanPhong { get; set; }

    public DateTime? NgayTraPhong { get; set; }

    public int SoNguoiO { get; set; }

    public decimal? PhuThu { get; set; }

    public string? TrangThai { get; set; }

    public decimal? TongTienPhong { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }

<<<<<<< HEAD
    public virtual Phong MaPhongNavigation { get; set; } = null!;
=======
    public virtual Phong? MaPhongNavigation { get; set; }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

    public virtual ICollection<SuDungDichVu> SuDungDichVus { get; set; } = new List<SuDungDichVu>();
}
