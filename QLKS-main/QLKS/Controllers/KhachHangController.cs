using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLKS.Data;
using QLKS.Models;
using QLKS.Repository;

namespace QLKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangRepository _khachHangRepository;

        public KhachHangController(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }

        [Authorize(Roles = "NhanVien")]
        [HttpGet]
        public async Task<IActionResult> GetAllKhachHang([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var khachHangs = await _khachHangRepository.GetAllKhachHang(pageNumber, pageSize);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Lấy danh sách khách hàng thành công!",
                    data = khachHangs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi lấy danh sách khách hàng: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                return Ok(khachHangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách khách hàng: " + ex.Message });
            }
        }


>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "NhanVien")]
        [HttpGet("{hoTen}")]
        public async Task<IActionResult> GetKhachHangByName([FromQuery] string hoTen)
        {
            try
            {
                if (string.IsNullOrEmpty(hoTen))
                {
<<<<<<< HEAD
                    return BadRequest(new
                    {
                        message = "Họ tên không được để trống.",
                        data = (object)null
                    });
=======
                    return BadRequest(new { Message = "Họ tên không được để trống." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                }

                var khachHangs = await _khachHangRepository.GetKhachHangByName(hoTen);
                if (khachHangs == null || !khachHangs.Any())
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy khách hàng nào với tên này.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Tìm kiếm khách hàng thành công!",
                    data = khachHangs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi tìm khách hàng: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                    return NotFound(new { Message = "Không tìm thấy khách hàng nào với tên này." });
                }

                return Ok(khachHangs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tìm khách hàng: " + ex.Message });
            }
        }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "NhanVien")]
        [HttpPost]
        public async Task<IActionResult> AddKhachHang([FromBody] KhachHangVM model)
        {
            try
            {
                var khachHangVM = await _khachHangRepository.AddKhachHang(model);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Thêm khách hàng thành công!",
                    data = khachHangVM
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi thêm khách hàng: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                return Ok(new { Message = "Thêm khách hàng thành công!", Data = khachHangVM });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi thêm khách hàng: " + ex.Message });
            }
        }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "NhanVien")]
        [HttpPut("{hoTen}")]
        public async Task<IActionResult> UpdateKhachHang(string hoTen, [FromBody] KhachHangVM model)
        {
            try
            {
                var result = await _khachHangRepository.UpdateKhachHang(hoTen, model);
                if (!result)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy khách hàng để cập nhật.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Cập nhật khách hàng thành công!",
                    data = (object)null
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi cập nhật khách hàng: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                    return NotFound(new { Message = "Không tìm thấy khách hàng để cập nhật." });
                }

                return Ok(new { Message = "Cập nhật khách hàng thành công!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật khách hàng: " + ex.Message });
            }
        }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "QuanLy")]
        [HttpDelete("{hoTen}")]
        public async Task<IActionResult> DeleteKhachHang(string hoTen)
        {
            try
            {
                var result = await _khachHangRepository.DeleteKhachHang(hoTen);
                if (!result)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy khách hàng để xóa.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Xóa khách hàng thành công!",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi xóa khách hàng: " + ex.Message,
                    data = (object)null
                });
            }
        }
    }
}
=======
                    return NotFound(new { Message = "Không tìm thấy khách hàng để xóa." });
                }

                return Ok(new { Message = "Xóa khách hàng thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa khách hàng: " + ex.Message });
            }
        }
    }
}
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
