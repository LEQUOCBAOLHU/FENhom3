using Microsoft.EntityFrameworkCore;
using QLKS.Data;
using QLKS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS.Repository
{
    public interface ILoaiPhongRepository
    {
<<<<<<< HEAD
        Task<PagedLoaiPhongResponse> GetAllAsync(int pageNumber, int pageSize);
        Task<LoaiPhongMD> GetByIdAsync(int maLoaiPhong);
        Task<LoaiPhongMD> AddLoaiPhongAsync(LoaiPhongVM loaiPhongVM);
        Task<bool> EditLoaiPhongAsync(int maLoaiPhong, LoaiPhongVM loaiPhongVM);
        Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong);
=======
        PagedLoaiPhongResponse GetAll(int pageNumber, int pageSize);
        JsonResult GetById(int maLoaiPhong);
        JsonResult AddLoaiPhong(LoaiPhongVM loaiPhongVM);
        JsonResult EditLoaiPhong(int maLoaiPhong, LoaiPhongVM loaiPhongVM);
        JsonResult DeleteLoaiPhong(int maLoaiPhong);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
    }

    public class LoaiPhongRepository : ILoaiPhongRepository
    {
        private readonly DataQlks112Nhom3Context _context;

        public LoaiPhongRepository(DataQlks112Nhom3Context context)
        {
            _context = context;
        }
<<<<<<< HEAD
        public async Task<PagedLoaiPhongResponse> GetAllAsync(int pageNumber, int pageSize)
=======

        public PagedLoaiPhongResponse GetAll(int pageNumber, int pageSize)
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.LoaiPhongs.AsQueryable();

<<<<<<< HEAD
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var loaiPhongs = await query
=======
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var loaiPhongs = query
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(lp => new LoaiPhongMD
                {
                    MaLoaiPhong = lp.MaLoaiPhong,
                    TenLoaiPhong = lp.TenLoaiPhong,
                    GiaCoBan = lp.GiaCoBan,
                    SoNguoiToiDa = lp.SoNguoiToiDa
                })
<<<<<<< HEAD
                .ToListAsync();
=======
                .ToList();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return new PagedLoaiPhongResponse
            {
                LoaiPhongs = loaiPhongs,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

<<<<<<< HEAD
        public async Task<LoaiPhongMD> GetByIdAsync(int maLoaiPhong)
        {
            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return null; // Trả về null để controller xử lý lỗi
            }

            return new LoaiPhongMD
=======
        public JsonResult GetById(int maLoaiPhong)
        {
            var loaiPhong = _context.LoaiPhongs
                .FirstOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return new JsonResult("Không tìm thấy loại phòng")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var loaiPhongMD = new LoaiPhongMD
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            {
                MaLoaiPhong = loaiPhong.MaLoaiPhong,
                TenLoaiPhong = loaiPhong.TenLoaiPhong,
                GiaCoBan = loaiPhong.GiaCoBan,
                SoNguoiToiDa = loaiPhong.SoNguoiToiDa
            };
<<<<<<< HEAD
        }

        public async Task<LoaiPhongMD> AddLoaiPhongAsync(LoaiPhongVM loaiPhongVM)
        {
            // Kiểm tra trùng TenLoaiPhong
            var check = await _context.LoaiPhongs
                .FirstOrDefaultAsync(lp => lp.TenLoaiPhong == loaiPhongVM.TenLoaiPhong);
            if (check != null)
            {
                throw new ArgumentException("Loại phòng đã tồn tại");
=======

            return new JsonResult(loaiPhongMD);
        }

        public JsonResult AddLoaiPhong(LoaiPhongVM loaiPhongVM)
        {
            // Kiểm tra trùng TenLoaiPhong
            var check = _context.LoaiPhongs
                .FirstOrDefault(lp => lp.TenLoaiPhong == loaiPhongVM.TenLoaiPhong);
            if (check != null)
            {
                return new JsonResult("Loại phòng đã tồn tại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }

            var loaiPhong = new LoaiPhong
            {
                TenLoaiPhong = loaiPhongVM.TenLoaiPhong,
                GiaCoBan = loaiPhongVM.GiaCoBan,
                SoNguoiToiDa = loaiPhongVM.SoNguoiToiDa
            };

            _context.LoaiPhongs.Add(loaiPhong);
<<<<<<< HEAD
            await _context.SaveChangesAsync();

            return new LoaiPhongMD
            {
                MaLoaiPhong = loaiPhong.MaLoaiPhong,
                TenLoaiPhong = loaiPhong.TenLoaiPhong,
                GiaCoBan = loaiPhong.GiaCoBan,
                SoNguoiToiDa = loaiPhong.SoNguoiToiDa
            };
        }

        public async Task<bool> EditLoaiPhongAsync(int maLoaiPhong, LoaiPhongVM loaiPhongVM)
        {
            var loaiPhong = await _context.LoaiPhongs
                .SingleOrDefaultAsync(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return false; // Trả về false để controller xử lý lỗi
            }

            // Kiểm tra trùng TenLoaiPhong với loại phòng khác
            var checkDuplicate = await _context.LoaiPhongs
                .FirstOrDefaultAsync(lp => lp.TenLoaiPhong == loaiPhongVM.TenLoaiPhong && lp.MaLoaiPhong != maLoaiPhong);
            if (checkDuplicate != null)
            {
                throw new ArgumentException("Tên loại phòng đã tồn tại");
=======
            _context.SaveChanges();

            return new JsonResult("Đã thêm loại phòng")
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public JsonResult EditLoaiPhong(int maLoaiPhong, LoaiPhongVM loaiPhongVM)
        {
            var loaiPhong = _context.LoaiPhongs
                .SingleOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return new JsonResult("Không tìm thấy loại phòng cần sửa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            // Kiểm tra trùng TenLoaiPhong với loại phòng khác
            var checkDuplicate = _context.LoaiPhongs
                .FirstOrDefault(lp => lp.TenLoaiPhong == loaiPhongVM.TenLoaiPhong && lp.MaLoaiPhong != maLoaiPhong);
            if (checkDuplicate != null)
            {
                return new JsonResult("Tên loại phòng đã tồn tại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }

            loaiPhong.TenLoaiPhong = loaiPhongVM.TenLoaiPhong;
            loaiPhong.GiaCoBan = loaiPhongVM.GiaCoBan;
            loaiPhong.SoNguoiToiDa = loaiPhongVM.SoNguoiToiDa;

<<<<<<< HEAD
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLoaiPhongAsync(int maLoaiPhong)
        {
            var loaiPhong = await _context.LoaiPhongs
                .SingleOrDefaultAsync(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return false; // Trả về false để controller xử lý lỗi
            }

            // Kiểm tra xem loại phòng có đang được sử dụng bởi phòng nào không
            var phongUsingLoaiPhong = await _context.Phongs
                .AnyAsync(p => p.MaLoaiPhong == maLoaiPhong);
            if (phongUsingLoaiPhong)
            {
                throw new ArgumentException("Không thể xóa loại phòng vì đang được sử dụng bởi ít nhất một phòng");
            }

            _context.LoaiPhongs.Remove(loaiPhong);
            await _context.SaveChangesAsync();
            return true;
        }
    
}
=======
            _context.SaveChanges();

            return new JsonResult("Đã chỉnh sửa loại phòng")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        public JsonResult DeleteLoaiPhong(int maLoaiPhong)
        {
            var loaiPhong = _context.LoaiPhongs
                .SingleOrDefault(lp => lp.MaLoaiPhong == maLoaiPhong);

            if (loaiPhong == null)
            {
                return new JsonResult("Không tìm thấy loại phòng cần xóa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            // Kiểm tra xem loại phòng có đang được sử dụng bởi phòng nào không
            var phongUsingLoaiPhong = _context.Phongs
                .Any(p => p.MaLoaiPhong == maLoaiPhong);
            if (phongUsingLoaiPhong)
            {
                return new JsonResult("Không thể xóa loại phòng vì đang được sử dụng bởi ít nhất một phòng")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            _context.LoaiPhongs.Remove(loaiPhong);
            _context.SaveChanges();

            return new JsonResult("Đã xóa loại phòng")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
}