using Microsoft.EntityFrameworkCore;
using QLKS.Data;
using QLKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS.Repository
{
    public interface IDatPhongRepository
    {
        Task<PagedDatPhongResponse> GetAllVMAsync(int pageNumber, int pageSize);
        Task<DatPhongVM> GetByIdVMAsync(int maDatPhong);
        Task AddVMAsync(List<CreateDatPhongVM> datPhongVMs, List<int> maKhList);
        Task UpdateVMAsync(int maDatPhong, UpdateDatPhongVM datPhongVM);
        Task<bool> DeleteByMaDatPhongAsync(int maDatPhong);
        Task UpdateDatPhongTrangThaiByMaPhongAsync(string maPhong, string trangThai);
    }

    public class DatPhongRepository : IDatPhongRepository
    {
        private readonly DataQlks112Nhom3Context _context;
<<<<<<< HEAD
        private readonly TimeSpan _cleanupBuffer = TimeSpan.FromHours(2); // Khoảng dọn dẹp: 2 tiếng
=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

        public DatPhongRepository(DataQlks112Nhom3Context context)
        {
            _context = context;
        }

        public async Task<PagedDatPhongResponse> GetAllVMAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.DatPhongs
                .Where(dp => dp.IsActive == true)
                .Include(dp => dp.MaPhongNavigation)
<<<<<<< HEAD
                .Include(dp => dp.MaKhNavigation)
                .Include(dp => dp.SuDungDichVus); // Thêm Include để tải SuDungDichVus
=======
                .Include(dp => dp.MaKhNavigation);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var datPhongs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new List<DatPhongVM>();
            foreach (var dp in datPhongs)
            {
                var datPhongVM = MapToVM(dp);
                datPhongVM.DanhSachKhachHang = await _context.KhachHangs
                    .Where(kh => kh.MaDatPhong == dp.MaDatPhong && kh.IsActive == true)
                    .Select(kh => new TenKhachHangVM
                    {
                        HoTen = kh.HoTen
                    })
                    .ToListAsync();

                datPhongVM.DanhSachDichVu = await _context.SuDungDichVus
                    .Where(sddv => sddv.MaDatPhong == dp.MaDatPhong && sddv.IsActive == true)
                    .Join(_context.DichVus,
                          sddv => sddv.MaDichVu,
                          dv => dv.MaDichVu,
                          (sddv, dv) => new SuDungDichVuVM
                          {
                              MaSuDung = sddv.MaSuDung,
                              MaDatPhong = sddv.MaDatPhong ?? 0,
                              MaDichVu = sddv.MaDichVu ?? 0,
                              TenDichVu = dv.TenDichVu,
                              SoLuong = sddv.SoLuong,
                              ThanhTien = sddv.ThanhTien ?? 0
                          })
                    .ToListAsync();

                result.Add(datPhongVM);
            }

            return new PagedDatPhongResponse
            {
                DatPhongs = result,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<DatPhongVM> GetByIdVMAsync(int maDatPhong)
        {
            var datPhong = await _context.DatPhongs
                .Where(dp => dp.MaDatPhong == maDatPhong && dp.IsActive == true)
                .Include(dp => dp.MaPhongNavigation)
                .Include(dp => dp.MaKhNavigation)
                .FirstOrDefaultAsync();

            if (datPhong == null)
                return null;

            var datPhongVM = MapToVM(datPhong);
            datPhongVM.DanhSachKhachHang = await _context.KhachHangs
                .Where(kh => kh.MaDatPhong == datPhong.MaDatPhong && kh.IsActive == true)
                .Select(kh => new TenKhachHangVM
                {
                    HoTen = kh.HoTen
                })
                .ToListAsync();

            datPhongVM.DanhSachDichVu = await _context.SuDungDichVus
                .Where(sddv => sddv.MaDatPhong == datPhong.MaDatPhong && sddv.IsActive == true)
                .Join(_context.DichVus,
                      sddv => sddv.MaDichVu,
                      dv => dv.MaDichVu,
                      (sddv, dv) => new SuDungDichVuVM
                      {
                          MaSuDung = sddv.MaSuDung,
                          MaDatPhong = sddv.MaDatPhong ?? 0,
                          MaDichVu = sddv.MaDichVu ?? 0,
                          TenDichVu = dv.TenDichVu,
                          SoLuong = sddv.SoLuong,
                          ThanhTien = sddv.ThanhTien ?? 0
                      })
                .ToListAsync();

<<<<<<< HEAD
            // Gán SoLuongDichVuSuDung dựa trên DanhSachDichVu
            datPhongVM.SoLuongDichVuSuDung = datPhongVM.DanhSachDichVu?.Count ?? 0;

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            return datPhongVM;
        }

        public async Task AddVMAsync(List<CreateDatPhongVM> datPhongVMs, List<int> maKhList)
        {
            if (datPhongVMs == null || !datPhongVMs.Any())
                throw new ArgumentException("Danh sách đặt phòng không được để trống.");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
<<<<<<< HEAD
                    Console.WriteLine($"Bắt đầu thêm {datPhongVMs.Count} đặt phòng với maKhList: {string.Join(", ", maKhList)}");

                    if (maKhList == null || !maKhList.Any())
                        throw new ArgumentException("Danh sách khách hàng không được để trống.");

=======
                    // Kiểm tra maKhList và maKh đại diện
                    if (maKhList == null || !maKhList.Any())
                        throw new ArgumentException("Danh sách khách hàng không được để trống.");

                    // Kiểm tra các khách hàng trong danh sách có hợp lệ không
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                    var khachHangs = await _context.KhachHangs
                        .Where(kh => maKhList.Contains(kh.MaKh) && kh.IsActive == true)
                        .ToListAsync();

<<<<<<< HEAD
                    Console.WriteLine($"Tìm thấy {khachHangs.Count} khách hàng hợp lệ.");

                    if (khachHangs.Count != maKhList.Count)
                        throw new ArgumentException("Một hoặc nhiều khách hàng trong danh sách không tồn tại hoặc đã bị ẩn.");
=======
                    if (khachHangs.Count != maKhList.Count)
                    {
                        throw new ArgumentException("Một hoặc nhiều khách hàng trong danh sách không tồn tại hoặc đã bị ẩn.");
                    }

                    // Kiểm tra xem các khách hàng đã được liên kết với đặt phòng khác chưa
                    var khachHangsDaLienKet = khachHangs.Where(kh => kh.MaDatPhong != null).ToList();
                    if (khachHangsDaLienKet.Any())
                    {
                        throw new ArgumentException($"Khách hàng {string.Join(", ", khachHangsDaLienKet.Select(kh => kh.HoTen))} đã được liên kết với đặt phòng khác.");
                    }

                    // Kiểm tra maKh đại diện có nằm trong maKhList không
                    var datPhongVM = datPhongVMs.First();
                    if (datPhongVM.MaKh.HasValue && !maKhList.Contains(datPhongVM.MaKh.Value))
                    {
                        throw new ArgumentException("Khách hàng đại diện phải nằm trong danh sách khách hàng được chọn.");
                    }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

                    var datPhongs = datPhongVMs.Select(datPhongVM => new DatPhong
                    {
                        MaNv = datPhongVM.MaNv,
<<<<<<< HEAD
                        MaKh = datPhongVM.MaKh, // Lưu khách hàng đại diện
=======
                        MaKh = datPhongVM.MaKh, // Lưu khách hàng đại diện do người dùng chọn
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                        MaPhong = datPhongVM.MaPhong,
                        NgayDat = datPhongVM.NgayDat ?? DateOnly.FromDateTime(DateTime.Now),
                        NgayNhanPhong = datPhongVM.NgayNhanPhong,
                        NgayTraPhong = datPhongVM.NgayTraPhong,
                        SoNguoiO = datPhongVM.SoNguoiO,
<<<<<<< HEAD
                        TrangThai = datPhongVM.TrangThai?.Trim() ?? "Đã đặt",
                        IsActive = true
                    }).ToList();

                    foreach (var datPhong in datPhongs)
                    {
                        Console.WriteLine($"Xử lý DatPhong: MaPhong={datPhong.MaPhong}, NgayNhanPhong={datPhong.NgayNhanPhong}, NgayTraPhong={datPhong.NgayTraPhong}");

                        // Kiểm tra phòng bảo trì
                        var phong = await _context.Phongs.FindAsync(datPhong.MaPhong);
                        if (phong?.TrangThai == "Bảo trì" && datPhong.TrangThai != "Hủy")
                            throw new ArgumentException($"Phòng {datPhong.MaPhong} đang bảo trì, không thể đặt trừ trạng thái 'Hủy'.");

                        // Kiểm tra xung đột thời gian
                        if (await CheckBookingConflictAsync(datPhong.MaPhong, datPhong.NgayNhanPhong.Value, datPhong.NgayTraPhong.Value))
                            throw new ArgumentException($"Phòng {datPhong.MaPhong} đã được đặt trong khoảng thời gian từ {datPhong.NgayNhanPhong} đến {datPhong.NgayTraPhong} hoặc nằm trong khoảng dọn dẹp 2 tiếng.");

                        await ValidateSingleDatPhong(datPhong);

                        // Thêm bản ghi
                        _context.DatPhongs.Add(datPhong);
                        await _context.SaveChangesAsync();
                        Console.WriteLine($"Đã lưu DatPhong với MaDatPhong={datPhong.MaDatPhong}");

                        // Cập nhật MaDatPhong cho khách hàng
                        var khachHangsToUpdate = await _context.KhachHangs
                            .Where(kh => maKhList.Contains(kh.MaKh) && kh.IsActive == true)
                            .ToListAsync();
=======
                        TrangThai = datPhongVM.TrangThai?.Trim() ?? "Đang sử dụng",
                        IsActive = true
                    }).ToList();

                    await ValidateDatPhong(datPhongs);

                    foreach (var datPhong in datPhongs)
                    {
                        var sqlInsert = @"
                            INSERT INTO dbo.DatPhong (MaNv, MaKh, MaPhong, NgayDat, NgayNhanPhong, NgayTraPhong, SoNguoiO, TrangThai, IsActive)
                            VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});";

                        object maNvParam = datPhong.MaNv.HasValue ? (object)datPhong.MaNv.Value : DBNull.Value;
                        object maKhParam = datPhong.MaKh.HasValue ? (object)datPhong.MaKh.Value : DBNull.Value;
                        object maPhongParam = datPhong.MaPhong ?? (object)DBNull.Value;
                        object ngayDatParam = datPhong.NgayDat.HasValue ? (object)datPhong.NgayDat.Value : DBNull.Value;
                        object trangThaiParam = datPhong.TrangThai ?? (object)DBNull.Value;

                        await _context.Database.ExecuteSqlRawAsync(sqlInsert,
                            maNvParam,
                            maKhParam,
                            maPhongParam,
                            ngayDatParam,
                            datPhong.NgayNhanPhong,
                            datPhong.NgayTraPhong,
                            datPhong.SoNguoiO,
                            trangThaiParam,
                            datPhong.IsActive);

                        // Lấy MaDatPhong vừa tạo
                        datPhong.MaDatPhong = _context.DatPhongs
                            .OrderByDescending(dp => dp.MaDatPhong)
                            .Select(dp => dp.MaDatPhong)
                            .First();

                        // Cập nhật MaDatPhong cho tất cả khách hàng trong maKhList
                        var khachHangsToUpdate = await _context.KhachHangs
                            .Where(kh => maKhList.Contains(kh.MaKh) && kh.IsActive == true)
                            .ToListAsync();

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                        foreach (var khachHang in khachHangsToUpdate)
                        {
                            khachHang.MaDatPhong = datPhong.MaDatPhong;
                            _context.KhachHangs.Update(khachHang);
<<<<<<< HEAD
                            Console.WriteLine($"Cập nhật MaDatPhong={datPhong.MaDatPhong} cho KhachHang MaKh={khachHang.MaKh}");
                        }
                        await _context.SaveChangesAsync();

                        // Cập nhật trạng thái phòng
                        await UpdatePhongStatusAsync(datPhong.MaPhong);
                        Console.WriteLine($"Đã cập nhật trạng thái phòng {datPhong.MaPhong}");
                    }

                    await transaction.CommitAsync();
                    Console.WriteLine("Giao dịch hoàn tất.");
=======
                        }
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
<<<<<<< HEAD
                    Console.WriteLine($"Lỗi khi thêm dữ liệu: {ex.Message} - Inner: {ex.InnerException?.Message}");
=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                    throw new Exception($"Lỗi khi thêm dữ liệu: {ex.Message}", ex);
                }
            }
        }

        public async Task UpdateVMAsync(int maDatPhong, UpdateDatPhongVM datPhongVM)
        {
            var existingDatPhong = await _context.DatPhongs
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong && dp.IsActive == true);

            if (existingDatPhong == null)
                throw new ArgumentException("Đặt phòng không tồn tại hoặc đã bị ẩn.");

<<<<<<< HEAD
            var originalMaPhong = existingDatPhong.MaPhong;
            var originalNgayNhanPhong = existingDatPhong.NgayNhanPhong;
            var originalNgayTraPhong = existingDatPhong.NgayTraPhong;

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            existingDatPhong.MaNv = datPhongVM.MaNv ?? existingDatPhong.MaNv;
            existingDatPhong.MaKh = datPhongVM.MaKh ?? existingDatPhong.MaKh;
            existingDatPhong.MaPhong = datPhongVM.MaPhong ?? existingDatPhong.MaPhong;
            existingDatPhong.NgayDat = datPhongVM.NgayDat ?? existingDatPhong.NgayDat;
            existingDatPhong.NgayNhanPhong = datPhongVM.NgayNhanPhong ?? existingDatPhong.NgayNhanPhong;
            existingDatPhong.NgayTraPhong = datPhongVM.NgayTraPhong ?? existingDatPhong.NgayTraPhong;
            existingDatPhong.SoNguoiO = datPhongVM.SoNguoiO;
            existingDatPhong.TrangThai = datPhongVM.TrangThai?.Trim() ?? existingDatPhong.TrangThai;

            await ValidateSingleDatPhong(existingDatPhong);

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
<<<<<<< HEAD
                    // Cập nhật bản ghi DatPhong
                    _context.DatPhongs.Update(existingDatPhong);
                    await _context.SaveChangesAsync();

                    // Kiểm tra xung đột nếu thay đổi thời gian hoặc phòng
                    if (existingDatPhong.MaPhong != originalMaPhong ||
                        existingDatPhong.NgayNhanPhong != originalNgayNhanPhong ||
                        existingDatPhong.NgayTraPhong != originalNgayTraPhong)
                    {
                        if (await CheckBookingConflictAsync(existingDatPhong.MaPhong, existingDatPhong.NgayNhanPhong.Value, existingDatPhong.NgayTraPhong.Value, maDatPhong))
                            throw new ArgumentException($"Phòng {existingDatPhong.MaPhong} đã được đặt trong khoảng thời gian từ {existingDatPhong.NgayNhanPhong} đến {existingDatPhong.NgayTraPhong} hoặc nằm trong khoảng dọn dẹp 2 tiếng.");
                    }

                    // Xử lý danh sách khách hàng mới nếu có
                    if (datPhongVM.MaKhList != null && datPhongVM.MaKhList.Any())
                    {
                        var khachHangs = await _context.KhachHangs
                            .Where(kh => datPhongVM.MaKhList.Contains(kh.MaKh) && kh.IsActive == true)
                            .ToListAsync();

                        if (khachHangs.Count != datPhongVM.MaKhList.Count)
                            throw new ArgumentException("Một hoặc nhiều khách hàng trong danh sách không tồn tại hoặc đã bị ẩn.");

                        foreach (var khachHang in khachHangs)
                        {
                            khachHang.MaDatPhong = maDatPhong; // Gán MaDatPhong cho khách hàng mới
                            _context.KhachHangs.Update(khachHang);
                        }
                        await _context.SaveChangesAsync();
                    }

                    // Cập nhật trạng thái phòng
                    await UpdatePhongStatusAsync(existingDatPhong.MaPhong);
=======
                    var sql = @"
                        UPDATE dbo.DatPhong
                        SET MaNv = {0}, MaKh = {1}, MaPhong = {2}, NgayDat = {3}, NgayNhanPhong = {4}, NgayTraPhong = {5}, SoNguoiO = {6}, TrangThai = {7}
                        WHERE MaDatPhong = {8};";

                    object maNvParam = existingDatPhong.MaNv.HasValue ? (object)existingDatPhong.MaNv.Value : DBNull.Value;
                    object maKhParam = existingDatPhong.MaKh.HasValue ? (object)existingDatPhong.MaKh.Value : DBNull.Value;

                    await _context.Database.ExecuteSqlRawAsync(sql,
                        maNvParam,
                        maKhParam,
                        existingDatPhong.MaPhong,
                        existingDatPhong.NgayDat,
                        existingDatPhong.NgayNhanPhong,
                        existingDatPhong.NgayTraPhong,
                        existingDatPhong.SoNguoiO,
                        existingDatPhong.TrangThai,
                        maDatPhong);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Lỗi khi cập nhật dữ liệu: {ex.Message}", ex);
                }
            }
        }

        public async Task<bool> DeleteByMaDatPhongAsync(int maDatPhong)
        {
            var datPhong = await _context.DatPhongs
                .FirstOrDefaultAsync(dp => dp.MaDatPhong == maDatPhong && dp.IsActive == true);

            if (datPhong == null)
                return false;

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
<<<<<<< HEAD
                    datPhong.IsActive = false;
                    _context.DatPhongs.Update(datPhong);
                    await _context.SaveChangesAsync();

                    await UpdatePhongStatusAsync(datPhong.MaPhong);
=======
                    var suDungDichVus = await _context.SuDungDichVus
                        .Where(sddv => sddv.MaDatPhong == maDatPhong && sddv.IsActive == true)
                        .ToListAsync();
                    if (suDungDichVus.Any())
                    {
                        await _context.Database.ExecuteSqlRawAsync(
                            "UPDATE dbo.SuDungDichVu SET IsActive = 0 WHERE MaDatPhong = {0}", maDatPhong);
                    }

                    var chiTietHoaDons = await _context.ChiTietHoaDons
                        .Where(cthd => cthd.MaDatPhong == maDatPhong)
                        .ToListAsync();
                    if (chiTietHoaDons.Any())
                    {
                        await _context.Database.ExecuteSqlRawAsync(
                            "DELETE FROM dbo.ChiTietHoaDon WHERE MaDatPhong = {0}", maDatPhong);
                    }

                    // Cập nhật MaDatPhong = null cho các khách hàng liên quan
                    var khachHangs = await _context.KhachHangs
                        .Where(kh => kh.MaDatPhong == maDatPhong && kh.IsActive == true)
                        .ToListAsync();
                    foreach (var khachHang in khachHangs)
                    {
                        khachHang.MaDatPhong = null;
                        _context.KhachHangs.Update(khachHang);
                    }
                    await _context.SaveChangesAsync();

                    await _context.Database.ExecuteSqlRawAsync(
                        "UPDATE dbo.DatPhong SET IsActive = 0 WHERE MaDatPhong = {0}", maDatPhong);

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Lỗi khi xóa dữ liệu: {ex.Message}", ex);
                }
            }
<<<<<<< HEAD
=======

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            return true;
        }

        public async Task UpdateDatPhongTrangThaiByMaPhongAsync(string maPhong, string trangThai)
        {
            if (string.IsNullOrEmpty(maPhong))
                throw new ArgumentException("Mã phòng không được để trống.");

            if (string.IsNullOrEmpty(trangThai))
                throw new ArgumentException("Trạng thái không được để trống.");

            trangThai = trangThai.Trim();

            var validTrangThaiDatPhong = new[] { "Đang sử dụng", "Hủy", "Hoàn thành", "Đã đặt" };
            if (!validTrangThaiDatPhong.Contains(trangThai, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Trạng thái không hợp lệ. Chỉ cho phép: Đang sử dụng, Hủy, Hoàn thành, Đã đặt.");

<<<<<<< HEAD
=======
            var phong = await _context.Phongs.FirstOrDefaultAsync(p => p.MaPhong == maPhong);
            if (phong == null)
                throw new ArgumentException($"Phòng {maPhong} không tồn tại.");

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            var relatedBookings = await _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong && dp.IsActive == true && dp.TrangThai != "Hủy")
                .ToListAsync();

            if (!relatedBookings.Any())
                throw new ArgumentException($"Không tìm thấy đặt phòng hợp lệ nào cho phòng {maPhong} để cập nhật trạng thái.");

<<<<<<< HEAD
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var booking in relatedBookings)
                    {
                        booking.TrangThai = trangThai;
                        _context.DatPhongs.Update(booking);
                    }
                    await _context.SaveChangesAsync();

                    // Cập nhật trạng thái phòng
                    await UpdatePhongStatusAsync(maPhong);

                    await transaction.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Lỗi khi cập nhật trạng thái đặt phòng: {ex.InnerException?.Message}", ex);
                }
            }
        }

        private async Task<bool> CheckBookingConflictAsync(string maPhong, DateTime ngayNhanPhong, DateTime ngayTraPhong, int? maDatPhongToExclude = null)
        {
            if (ngayNhanPhong >= ngayTraPhong)
                return true; // Xung đột: Thời gian không hợp lệ

            var cleanupStart = ngayNhanPhong - _cleanupBuffer;
            var cleanupEnd = ngayTraPhong + _cleanupBuffer;

            var existingBookings = await _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong
                          && dp.IsActive
                          && dp.TrangThai != "Hủy"
                          && dp.TrangThai != "Hoàn thành"
                          && (maDatPhongToExclude == null || dp.MaDatPhong != maDatPhongToExclude))
                .ToListAsync();

            foreach (var booking in existingBookings)
            {
                var bookingCleanupStart = booking.NgayNhanPhong - _cleanupBuffer;
                var bookingCleanupEnd = booking.NgayTraPhong + _cleanupBuffer;

                if (
                    // Xung đột trực tiếp
                    (ngayNhanPhong < booking.NgayTraPhong && ngayTraPhong > booking.NgayNhanPhong) ||
                    // Đặt phòng mới bao quanh đặt phòng hiện có
                    (ngayNhanPhong <= booking.NgayNhanPhong && ngayTraPhong >= booking.NgayTraPhong) ||
                    // Đặt phòng hiện có bao quanh đặt phòng mới
                    (booking.NgayNhanPhong <= ngayNhanPhong && booking.NgayTraPhong >= ngayTraPhong) ||
                    // Xung đột với khoảng dọn dẹp
                    (ngayNhanPhong < bookingCleanupEnd && ngayTraPhong > bookingCleanupStart) ||
                    (cleanupStart < booking.NgayTraPhong && cleanupEnd > booking.NgayNhanPhong)
                )
                {
                    return true; // Có xung đột
                }
            }
            return false; // Không có xung đột
        }

        private async Task UpdatePhongStatusAsync(string maPhong)
        {
            var phong = await _context.Phongs
                .FirstOrDefaultAsync(p => p.MaPhong == maPhong);

            if (phong == null || phong.TrangThai == "Bảo trì")
                return; // Không cập nhật nếu phòng không tồn tại hoặc đang bảo trì

            var currentTime = DateTime.Now; // 17:14 PM +07, 21/5/2025

            // Kiểm tra đặt phòng hiện tại (Đang sử dụng)
            var currentBooking = await _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong
                          && dp.IsActive
                          && dp.TrangThai == "Đang sử dụng"
                          && currentTime >= dp.NgayNhanPhong
                          && currentTime <= dp.NgayTraPhong)
                .OrderBy(dp => dp.NgayNhanPhong)
                .FirstOrDefaultAsync();

            if (currentBooking != null)
            {
                phong.TrangThai = "Đang sử dụng";
                await _context.SaveChangesAsync();
                return;
            }

            // Kiểm tra đặt phòng trong tương lai (Đã đặt)
            var nextBooking = await _context.DatPhongs
                .Where(dp => dp.MaPhong == maPhong
                          && dp.IsActive
                          && dp.TrangThai == "Đã đặt"
                          && dp.NgayNhanPhong > currentTime)
                .OrderBy(dp => dp.NgayNhanPhong)
                .FirstOrDefaultAsync();

            if (nextBooking != null)
            {
                var tomorrow = currentTime.Date.AddDays(1);
                if (nextBooking.NgayNhanPhong.HasValue && nextBooking.NgayNhanPhong.Value.Date == tomorrow)
                {
                    phong.TrangThai = "Đã đặt";
                }
                else
                {
                    phong.TrangThai = "Trống";
                }
            }
            else
            {
                phong.TrangThai = "Trống";
            }

            await _context.SaveChangesAsync();
=======
            foreach (var booking in relatedBookings)
            {
                booking.TrangThai = trangThai;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Lỗi khi cập nhật trạng thái đặt phòng: {ex.InnerException?.Message}", ex);
            }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        private async Task ValidateDatPhong(List<DatPhong> datPhongs)
        {
            if (datPhongs == null || !datPhongs.Any())
                throw new ArgumentException("Danh sách đặt phòng không được để trống.");

            var duplicatePhongs = datPhongs.GroupBy(dp => dp.MaPhong)
                                           .Where(g => g.Count() > 1)
                                           .Select(g => g.Key)
                                           .ToList();
            if (duplicatePhongs.Any())
                throw new ArgumentException($"Các phòng sau bị trùng lặp trong danh sách đặt: {string.Join(", ", duplicatePhongs)}");

            foreach (var datPhong in datPhongs)
            {
                await ValidateSingleDatPhong(datPhong);
            }
        }

        private async Task ValidateSingleDatPhong(DatPhong datPhong)
        {
            if (string.IsNullOrEmpty(datPhong.MaPhong))
                throw new ArgumentException("Mã phòng không được để trống.");

            if (datPhong.NgayNhanPhong == null || datPhong.NgayTraPhong == null)
                throw new ArgumentException("Ngày nhận phòng và ngày trả phòng không được để trống.");

            if (datPhong.NgayNhanPhong > datPhong.NgayTraPhong)
                throw new ArgumentException("Ngày nhận phòng phải trước ngày trả phòng.");

            if (datPhong.SoNguoiO <= 0)
                throw new ArgumentException("Số người ở phải lớn hơn 0.");

            var phong = await _context.Phongs.FindAsync(datPhong.MaPhong);
            if (phong == null)
                throw new ArgumentException($"Phòng {datPhong.MaPhong} không tồn tại.");

<<<<<<< HEAD
            if (phong.TrangThai == "Bảo trì")
                throw new ArgumentException($"Phòng {datPhong.MaPhong} đang bảo trì, không thể đặt.");

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            if (datPhong.MaKh.HasValue)
            {
                var khachHang = await _context.KhachHangs.FindAsync(datPhong.MaKh);
                if (khachHang == null)
                    throw new ArgumentException("Khách hàng không tồn tại.");
            }

            if (datPhong.MaNv.HasValue)
            {
                var nhanVien = await _context.NhanViens.FindAsync(datPhong.MaNv);
                if (nhanVien == null)
                    throw new ArgumentException("Nhân viên không tồn tại.");
            }

            var validTrangThai = new[] { "Đang sử dụng", "Hủy", "Hoàn thành", "Đã đặt" };
            if (!string.IsNullOrEmpty(datPhong.TrangThai))
            {
                datPhong.TrangThai = datPhong.TrangThai.Trim();
                if (!validTrangThai.Contains(datPhong.TrangThai, StringComparer.OrdinalIgnoreCase))
                    throw new ArgumentException("Trạng thái không hợp lệ. Chỉ cho phép: Đang sử dụng, Hủy, Đã đặt, Hoàn thành.");
            }
        }

        private DatPhongVM MapToVM(DatPhong datPhong)
        {
            return new DatPhongVM
            {
                MaDatPhong = datPhong.MaDatPhong,
                MaNv = datPhong.MaNv,
                MaKh = datPhong.MaKh,
                TenKhachHang = datPhong.MaKhNavigation?.HoTen,
                MaPhong = datPhong.MaPhong,
                NgayDat = datPhong.NgayDat ?? DateOnly.FromDateTime(DateTime.Now),
                NgayNhanPhong = datPhong.NgayNhanPhong,
                NgayTraPhong = datPhong.NgayTraPhong,
                SoNguoiO = datPhong.SoNguoiO,
                PhuThu = datPhong.PhuThu,
                TrangThai = datPhong.TrangThai,
                TongTienPhong = datPhong.TongTienPhong,
                SoLuongDichVuSuDung = datPhong.SuDungDichVus?.Count ?? 0,
                DanhSachKhachHang = new List<TenKhachHangVM>()
            };
        }
    }
}