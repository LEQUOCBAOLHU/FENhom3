using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLKS.Repository;
<<<<<<< HEAD
using System;
using System.Threading.Tasks;
=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

namespace QLKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    [Authorize]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _thongKeRepository;

=======
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _thongKeRepository;
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        public ThongKeController(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        [Authorize(Roles = "QuanLy")]
        [HttpGet("ngay")]
        public async Task<IActionResult> ThongKeTheoNgay([FromQuery] DateTime ngay)
        {
            if (ngay == default)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Ngày không được để trống.",
                    data = (object)null
                });
            }

            try
            {
                var result = await _thongKeRepository.ThongKeTheoNgay(ngay);
                return Ok(new
                {
                    message = "Thống kê theo ngày thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi thống kê theo ngày: " + ex.Message,
                    data = (object)null
                });
            }
=======
                return BadRequest(new { Message = "Ngày không được để trống." });
            }

            var result = await _thongKeRepository.ThongKeTheoNgay(ngay);
            return Ok(result);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        [Authorize(Roles = "QuanLy")]
        [HttpGet("khoang-thoi-gian")]
        public async Task<IActionResult> ThongKeTheoKhoangThoiGian([FromQuery] DateTime tuNgay, [FromQuery] DateTime denNgay)
        {
            if (tuNgay > denNgay)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Ngày bắt đầu không được lớn hơn ngày kết thúc.",
                    data = (object)null
                });
            }

            try
            {
                var result = await _thongKeRepository.ThongKeTheoKhoangThoiGian(tuNgay, denNgay);
                return Ok(new
                {
                    message = "Thống kê theo khoảng thời gian thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi thống kê theo khoảng thời gian: " + ex.Message,
                    data = (object)null
                });
            }
=======
                return BadRequest("Ngày bắt đầu không được lớn hơn ngày kết thúc.");
            }

            var result = await _thongKeRepository.ThongKeTheoKhoangThoiGian(tuNgay, denNgay);
            return Ok(result);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        [Authorize(Roles = "QuanLy")]
        [HttpGet("thang")]
        public async Task<IActionResult> ThongKeTheoThang([FromQuery] int nam, [FromQuery] int thang)
        {
            if (nam < 2000 || nam > DateTime.Now.Year || thang < 1 || thang > 12)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Năm hoặc tháng không hợp lệ.",
                    data = (object)null
                });
            }

            try
            {
                var result = await _thongKeRepository.ThongKeTheoThang(nam, thang);
                return Ok(new
                {
                    message = "Thống kê theo tháng thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi thống kê theo tháng: " + ex.Message,
                    data = (object)null
                });
            }
=======
                return BadRequest(new { Message = "Năm hoặc tháng không hợp lệ." });
            }

            var result = await _thongKeRepository.ThongKeTheoThang(nam, thang);
            return Ok(result);
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        [Authorize(Roles = "QuanLy")]
        [HttpGet("nam")]
        public async Task<IActionResult> ThongKeTheoNam([FromQuery] int nam)
        {
            if (nam < 2000 || nam > DateTime.Now.Year)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Năm không hợp lệ.",
                    data = (object)null
                });
            }

            try
            {
                var result = await _thongKeRepository.ThongKeTheoNam(nam);
                return Ok(new
                {
                    message = "Thống kê theo năm thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi thống kê theo năm: " + ex.Message,
                    data = (object)null
                });
            }
        }
    }
}
=======
                return BadRequest(new { Message = "Năm không hợp lệ." });
            }

            var result = await _thongKeRepository.ThongKeTheoNam(nam);
            return Ok(result);
        }
    }
}

>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
