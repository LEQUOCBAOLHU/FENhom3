using System;
using System.Collections.Generic;

namespace QLKS.Data;

public partial class LoaiPhong
{
    public int MaLoaiPhong { get; set; }

    public string TenLoaiPhong { get; set; } = null!;

<<<<<<< HEAD
    public int SoNguoiToiDa { get; set; }

    public decimal GiaCoBan { get; set; }

    public decimal? GiaPhongNgay { get; set; }
=======
    public decimal GiaCoBan { get; set; }

    public int SoNguoiToiDa { get; set; }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();

    public virtual ICollection<PhuThu> PhuThus { get; set; } = new List<PhuThu>();
}
