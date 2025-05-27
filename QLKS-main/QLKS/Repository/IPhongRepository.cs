<<<<<<< HEAD
using QLKS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QLKS.Data;
using Microsoft.EntityFrameworkCore;
=======
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLKS.Data;
using QLKS.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLKS.Models;
using Microsoft.AspNetCore.Mvc;
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

namespace QLKS.Repository
{
    public interface IPhongRepository
    {
<<<<<<< HEAD
        Task<PagedPhongResponse> GetAllAsync(int pageNumber, int pageSize);
        Task<PhongMD> GetByIdAsync(string MaPhong);
        Task<PhongMD> AddPhongAsync(PhongAddVM phongVM);
        Task<bool> EditPhongAsync(string MaPhong, PhongEditVM phongVM);
        Task<bool> DeletePhongAsync(string MaPhong);
        Task<PagedPhongResponse> GetByTrangThaiAsync(string trangThai, int pageNumber, int pageSize);
        Task<bool> UpdateTrangThaiAsync(string maPhong, string trangThai);
        Task<PagedPhongResponse> GetByLoaiPhongAsync(int maLoaiPhong, int pageNumber, int pageSize);
        Task<Dictionary<string, int>> GetRoomStatusStatisticsAsync();
        Task<bool> IsRoomAvailableAsync(string maPhong, DateTime startDate, DateTime endDate);
    }
    public class PhongRepository : IPhongRepository
    {
        private readonly DataQlks112Nhom3Context _context;
        private readonly TimeSpan _cleanupBuffer = TimeSpan.FromHours(2); // Khoảng dọn dẹp: 2 tiếng
=======
        PagedPhongResponse GetAll(int pageNumber, int pageSize);
        JsonResult AddPhong(PhongMD phongVM);
        JsonResult EditPhong(string MaPhong, PhongVM phongVM);
        JsonResult DeletePhong(string MaPhong);
        JsonResult GetById(string MaPhong);
        PagedPhongResponse GetByTrangThai(string trangThai, int pageNumber, int pageSize);
        JsonResult UpdateTrangThai(string maPhong, string trangThai);
        PagedPhongResponse GetByLoaiPhong(int maLoaiPhong, int pageNumber, int pageSize);
        Dictionary<string, int> GetRoomStatusStatistics();
        bool IsRoomAvailable(string maPhong, DateTime startDate, DateTime endDate);
    }

    public class PhongRepository : IPhongRepository
    {
        private readonly DataQlks112Nhom3Context _context;
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

        public PhongRepository(DataQlks112Nhom3Context context)
        {
            _context = context;
        }

<<<<<<< HEAD
        public async Task<PagedPhongResponse> GetAllAsync(int pageNumber, int pageSize)
=======
        public PagedPhongResponse GetAll(int pageNumber, int pageSize)
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Phongs
                .Include(p => p.MaLoaiPhongNavigation);

<<<<<<< HEAD
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = await query
=======
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = query
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PhongMD
                {
                    MaPhong = p.MaPhong,
                    MaLoaiPhong = p.MaLoaiPhong,
                    TenPhong = p.TenPhong,
                    TrangThai = p.TrangThai,
                    TenLoaiPhong = p.MaLoaiPhongNavigation.TenLoaiPhong,
                    GiaCoBan = p.MaLoaiPhongNavigation.GiaCoBan,
                    SoNguoiToiDa = p.MaLoaiPhongNavigation.SoNguoiToiDa
                })
<<<<<<< HEAD
                .ToListAsync();
=======
                .ToList();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return new PagedPhongResponse
            {
                Phongs = phongs,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

<<<<<<< HEAD
        public async Task<PhongMD> GetByIdAsync(string MaPhong)
        {
            var phong = await _context.Phongs
                .Include(p => p.MaLoaiPhongNavigation)
                .FirstOrDefaultAsync(u => u.MaPhong == MaPhong);

            if (phong == null)
            {
                return null; // Trả về null để controller xử lý lỗi
            }

            return new PhongMD
            {
                MaPhong = phong.MaPhong,
                MaLoaiPhong = phong.MaLoaiPhong,
                TenPhong = phong.TenPhong,
                TrangThai = phong.TrangThai,
                TenLoaiPhong = phong.MaLoaiPhongNavigation.TenLoaiPhong,
                GiaCoBan = phong.MaLoaiPhongNavigation.GiaCoBan,
                SoNguoiToiDa = phong.MaLoaiPhongNavigation.SoNguoiToiDa
            };
        }

        public async Task<PhongMD> AddPhongAsync(PhongAddVM phongVM)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(phongVM.MaPhong) ||
                string.IsNullOrWhiteSpace(phongVM.TenPhong))
            {
                throw new ArgumentException("Thông tin phòng không đầy đủ. Vui lòng nhập MaPhong, MaLoaiPhong và TenPhong.");
            }

            // Kiểm tra xem phòng đã tồn tại chưa (dựa trên MaPhong hoặc TenPhong)
            var check = await _context.Phongs
                .FirstOrDefaultAsync(c => c.TenPhong == phongVM.TenPhong || c.MaPhong == phongVM.MaPhong);
            if (check != null)
            {
                throw new ArgumentException("Phòng đã tồn tại");
            }

            // Kiểm tra xem MaLoaiPhong có tồn tại không
            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(lp => lp.MaLoaiPhong == phongVM.MaLoaiPhong);
            if (loaiPhong == null)
            {
                throw new ArgumentException("Loại phòng không tồn tại");
            }

            // Tạo mới phòng với các trường bắt buộc và trạng thái mặc định là "Trống"
            var phong = new Phong
            {
                MaPhong = phongVM.MaPhong,
                MaLoaiPhong = phongVM.MaLoaiPhong,
                TenPhong = phongVM.TenPhong,
                TrangThai = "Trống" // Mặc định trạng thái là "Trống"
            };

            _context.Phongs.Add(phong);
            await _context.SaveChangesAsync();

            return new PhongMD
            {
                MaPhong = phong.MaPhong,
                MaLoaiPhong = phong.MaLoaiPhong,
                TenPhong = phong.TenPhong,
                TrangThai = phong.TrangThai,
                TenLoaiPhong = loaiPhong.TenLoaiPhong,
                GiaCoBan = loaiPhong.GiaCoBan,
                SoNguoiToiDa = loaiPhong.SoNguoiToiDa
            };
        }

        public async Task<bool> EditPhongAsync(string MaPhong, PhongEditVM phongVM)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(phongVM.TenPhong))
            {
                throw new ArgumentException("Thông tin phòng không đầy đủ. Vui lòng nhập MaLoaiPhong và TenPhong.");
            }

            // Kiểm tra xem phòng có tồn tại không
            var phong = await _context.Phongs
                .SingleOrDefaultAsync(l => l.MaPhong == MaPhong);
            if (phong == null)
            {
                return false; // Trả về false để controller xử lý lỗi
            }

            // Kiểm tra xem MaLoaiPhong có tồn tại không
            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(lp => lp.MaLoaiPhong == phongVM.MaLoaiPhong);
            if (loaiPhong == null)
            {
                throw new ArgumentException("Loại phòng không tồn tại");
            }

            // Kiểm tra xem TenPhong có bị trùng với phòng khác không
            var checkDuplicate = await _context.Phongs
                .FirstOrDefaultAsync(c => c.TenPhong == phongVM.TenPhong && c.MaPhong != MaPhong);
            if (checkDuplicate != null)
            {
                throw new ArgumentException("Tên phòng đã tồn tại");
            }

            // Cập nhật các trường cần thiết
            phong.MaLoaiPhong = phongVM.MaLoaiPhong;
            phong.TenPhong = phongVM.TenPhong;
            // Không cập nhật TrangThai, giữ nguyên giá trị hiện tại

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePhongAsync(string MaPhong)
        {
            // Kiểm tra xem phòng có tồn tại không
            var phong = await _context.Phongs
                .SingleOrDefaultAsync(l => l.MaPhong == MaPhong);
            if (phong == null)
            {
                return false; // Trả về false để controller xử lý lỗi
            }

            // Kiểm tra xem phòng có đang được thuê hoặc đặt không
            var currentDateTime = DateTime.Now;
            var activeBookings = await _context.DatPhongs
                .Where(dp => dp.MaPhong == MaPhong
                    && dp.TrangThai != "Hủy"
                    && dp.TrangThai != "Hoàn thành"
                    && currentDateTime >= dp.NgayNhanPhong
                    && currentDateTime <= dp.NgayTraPhong)
                .AnyAsync();

            if (activeBookings)
            {
                throw new ArgumentException("Phòng đang được thuê hoặc đặt. Vui lòng đợi khách trả phòng hoặc chuyển khách sang phòng khác trước khi xóa.");
            }

            // Xóa phòng
            _context.Phongs.Remove(phong);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PagedPhongResponse> GetByTrangThaiAsync(string trangThai, int pageNumber, int pageSize)
=======

        public JsonResult AddPhong(PhongMD phongVM)
        {
            var check = _context.Phongs.FirstOrDefault(c => c.TenPhong == phongVM.TenPhong || c.MaPhong == phongVM.MaPhong);
            if (check != null)
            {
                return new JsonResult("Phong đã tồn tại")
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                var phong = new Phong
                {
                    MaPhong = phongVM.MaPhong,
                    MaLoaiPhong = phongVM.MaLoaiPhong,
                    TenPhong = phongVM.TenPhong,
                    TrangThai = phongVM.TrangThai,
                };
                _context.Phongs.Add(phong);
                _context.SaveChanges();
                return new JsonResult("Đã thêm Phong")
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
        }

        public JsonResult EditPhong(string MaPhong, PhongVM phongVM)
        {
            var phong = _context.Phongs.SingleOrDefault(l => l.MaPhong == MaPhong);
            if (phong == null)
            {
                return new JsonResult("Không tìm thấy dịch vụ cần sửa")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                phong.MaLoaiPhong = phongVM.MaLoaiPhong;
                phong.TenPhong = phongVM.TenPhong;
                phong.TrangThai = phongVM.TrangThai;
                _context.SaveChanges();
                return new JsonResult("Đã chỉnh sửa")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult DeletePhong(string MaPhong)
        {
            var phong = _context.Phongs.SingleOrDefault(l => l.MaPhong == MaPhong);
            if (phong == null)
            {
                return new JsonResult("Không tìm thấy phòng cần xoá")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _context.Phongs.Remove(phong);
                _context.SaveChanges();
                return new JsonResult("Đã xoá")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public JsonResult GetById(string MaPhong)
        {
            var phong = _context.Phongs
                .Include(p => p.MaLoaiPhongNavigation) // THAY ĐỔI: Thêm Include để lấy thông tin từ LoaiPhong
                .FirstOrDefault(u => u.MaPhong == MaPhong);

            if (phong == null)
            {
                return new JsonResult("Không tìm thấy phòng")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                var _phong = new PhongMD
                {
                    MaPhong = phong.MaPhong, // THAY ĐỔI: Thêm MaPhong vào kết quả
                    MaLoaiPhong = phong.MaLoaiPhong,
                    TenPhong = phong.TenPhong,
                    TrangThai = phong.TrangThai,
                    TenLoaiPhong = phong.MaLoaiPhongNavigation.TenLoaiPhong, // THAY ĐỔI: Thêm TenLoaiPhong
                    GiaCoBan = phong.MaLoaiPhongNavigation.GiaCoBan,         // THAY ĐỔI: Thêm GiaCoBan
                    SoNguoiToiDa = phong.MaLoaiPhongNavigation.SoNguoiToiDa // THAY ĐỔI: Thêm SoNguoiToiDa
                };
                return new JsonResult(_phong);
            }
        }

        public PagedPhongResponse GetByTrangThai(string trangThai, int pageNumber, int pageSize)
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Phongs
                .Include(p => p.MaLoaiPhongNavigation)
                .Where(p => p.TrangThai == trangThai);

<<<<<<< HEAD
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = await query
=======
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = query
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PhongMD
                {
                    MaPhong = p.MaPhong,
                    MaLoaiPhong = p.MaLoaiPhong,
                    TenPhong = p.TenPhong,
                    TrangThai = p.TrangThai,
                    TenLoaiPhong = p.MaLoaiPhongNavigation.TenLoaiPhong,
                    GiaCoBan = p.MaLoaiPhongNavigation.GiaCoBan,
                    SoNguoiToiDa = p.MaLoaiPhongNavigation.SoNguoiToiDa
                })
<<<<<<< HEAD
                .ToListAsync();
=======
                .ToList();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return new PagedPhongResponse
            {
                Phongs = phongs,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

<<<<<<< HEAD
        public async Task<bool> UpdateTrangThaiAsync(string maPhong, string trangThai)
        {
            var phong = await _context.Phongs
                .SingleOrDefaultAsync(p => p.MaPhong == maPhong);
            if (phong == null)
            {
                return false; // Trả về false để controller xử lý lỗi
            }

            var validTrangThai = new[] { "Trống", "Đang sử dụng", "Bảo trì" };
            if (!validTrangThai.Contains(trangThai, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Trạng thái không hợp lệ. Chỉ cho phép: Trống, Đang sử dụng, Bảo trì");
            }

            phong.TrangThai = trangThai;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedPhongResponse> GetByLoaiPhongAsync(int maLoaiPhong, int pageNumber, int pageSize)
=======
        public JsonResult UpdateTrangThai(string maPhong, string trangThai)
        {
            var phong = _context.Phongs.SingleOrDefault(p => p.MaPhong == maPhong);
            if (phong == null)
            {
                return new JsonResult("Không tìm thầy phòng cần cập nhật trạng thái")
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            phong.TrangThai = trangThai;
            _context.SaveChanges();
            return new JsonResult("Cập nhật trạng thái phòng thành công")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        public PagedPhongResponse GetByLoaiPhong(int maLoaiPhong, int pageNumber, int pageSize)
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Phongs
                .Include(p => p.MaLoaiPhongNavigation)
                .Where(p => p.MaLoaiPhong == maLoaiPhong);

<<<<<<< HEAD
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = await query
=======
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var phongs = query
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PhongMD
                {
                    MaPhong = p.MaPhong,
                    MaLoaiPhong = p.MaLoaiPhong,
                    TenPhong = p.TenPhong,
                    TrangThai = p.TrangThai,
                    TenLoaiPhong = p.MaLoaiPhongNavigation.TenLoaiPhong,
                    GiaCoBan = p.MaLoaiPhongNavigation.GiaCoBan,
                    SoNguoiToiDa = p.MaLoaiPhongNavigation.SoNguoiToiDa
                })
<<<<<<< HEAD
                .ToListAsync();
=======
                .ToList();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return new PagedPhongResponse
            {
                Phongs = phongs,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

<<<<<<< HEAD
        public async Task<Dictionary<string, int>> GetRoomStatusStatisticsAsync()
        {
            var statistics = await _context.Phongs
                .GroupBy(p => p.TrangThai)
                .Select(g => new { TrangThai = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.TrangThai ?? "Không xác định", x => x.Count);
=======
        public Dictionary<string, int> GetRoomStatusStatistics()
        {
            var statistics = _context.Phongs
                .GroupBy(p => p.TrangThai)
                .Select(g => new { TrangThai = g.Key, Count = g.Count() })
                .ToDictionary(x => x.TrangThai, x => x.Count);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return statistics;
        }

<<<<<<< HEAD
        public async Task<bool> IsRoomAvailableAsync(string maPhong, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                return false; // Thời gian không hợp lệ

            var cleanupStart = startDate - _cleanupBuffer;
            var cleanupEnd = endDate + _cleanupBuffer;

            var conflictingBookings = await _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong
                          && dp.IsActive
                          && dp.TrangThai != "Hủy"
                          && dp.TrangThai != "Hoàn thành"
                          && (
                              (dp.NgayNhanPhong <= endDate && dp.NgayTraPhong >= startDate) ||
                              (dp.NgayNhanPhong <= startDate && dp.NgayTraPhong >= endDate) ||
                              (startDate < dp.NgayTraPhong + _cleanupBuffer && endDate > dp.NgayNhanPhong - _cleanupBuffer)
                          ))
                .AnyAsync();
=======
        public bool IsRoomAvailable(string maPhong, DateTime startDate, DateTime endDate)
        {
            var start = startDate;
            var end = endDate;

            var conflictingBookings = _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong &&
                             (dp.TrangThai == "Đang sử dụng" || dp.TrangThai == "Đã đặt" || dp.TrangThai == "Bảo trì") &&
                             ((dp.NgayNhanPhong <= end && dp.NgayTraPhong >= start) ||
                              (dp.NgayNhanPhong >= start && dp.NgayTraPhong <= end)))
                .Any();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            return !conflictingBookings;
        }
    }
}