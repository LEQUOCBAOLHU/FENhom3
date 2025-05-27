using System;
using System.Collections.Generic;

namespace QLKS.Data;

public partial class PhuThu
{
    public int MaPhuThu { get; set; }

    public int? MaLoaiPhong { get; set; }

<<<<<<< HEAD
    public decimal? GiaPhuThuTheoNgay { get; set; }

    public decimal? GiaPhuThuTheoGio { get; set; }
=======
    public decimal? PhuThuNguoiThem { get; set; }

    public decimal TyLePhuThu { get; set; }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

    public virtual LoaiPhong? MaLoaiPhongNavigation { get; set; }
}
