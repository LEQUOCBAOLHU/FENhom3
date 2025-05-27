using QLKS.Data;

namespace QLKS.Models
{
    public class PhongVM
    {
        public int? MaLoaiPhong { get; set; }
        public string? TenPhong { get; set; }
        public string? TrangThai { get; set; }
<<<<<<< HEAD
        public string? TenLoaiPhong { get; set; } 
        public decimal GiaCoBan { get; set; }    
        public int SoNguoiToiDa { get; set; } 
=======
        public string? TenLoaiPhong { get; set; } // THAY ĐỔI: Thêm TenLoaiPhong
        public decimal GiaCoBan { get; set; }     // THAY ĐỔI: Thêm GiaCoBan
        public int SoNguoiToiDa { get; set; }     // THAY ĐỔI: Thêm SoNguoiToiDa
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    }

    public class PhongMD : PhongVM
    {
        public string MaPhong { get; set; } = null!;
    }
<<<<<<< HEAD

    public class PhongAddVM
    {
        public string MaPhong { get; set; } = null!;
        public int MaLoaiPhong { get; set; }
        public string TenPhong { get; set; } = null!;
    }

    public class PhongEditVM
    {
        public int MaLoaiPhong { get; set; }
        public string TenPhong { get; set; } = null!;
    }

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    public class PagedPhongResponse
    {
        public List<PhongMD> Phongs { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}