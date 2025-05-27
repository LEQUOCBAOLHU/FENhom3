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
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuRepository _dichVuRepository;

        public DichVuController(IDichVuRepository dichVuRepository)
        {
            _dichVuRepository = dichVuRepository;
        }

<<<<<<< HEAD
        [HttpGet]
=======
        [HttpGet()]
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "NhanVien")]
        public async Task<IActionResult> GetAllDichVu([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var dichVus = await _dichVuRepository.GetAllDichVu(pageNumber, pageSize);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Lấy danh sách dịch vụ thành công!",
                    data = dichVus
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi lấy danh sách dịch vụ: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                return Ok(dichVus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách dịch vụ: " + ex.Message });
            }
        }


>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [HttpGet("search")]
        [Authorize(Roles = "NhanVien")]
        public async Task<IActionResult> GetDichVuByName([FromQuery] string tenDichVu)
        {
            try
            {
                if (string.IsNullOrEmpty(tenDichVu))
                {
<<<<<<< HEAD
                    return BadRequest(new
                    {
                        message = "Tên dịch vụ không được để trống.",
                        data = (object)null
                    });
=======
                    return BadRequest(new { Message = "Tên dịch vụ không được để trống." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                }

                var dichVus = await _dichVuRepository.GetDichVuByName(tenDichVu);
                if (dichVus == null || !dichVus.Any())
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy dịch vụ nào với tên này.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Tìm kiếm dịch vụ thành công!",
                    data = dichVus
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi tìm dịch vụ: " + ex.Message,
                    data = (object)null
                });
=======
                    return NotFound(new { Message = "Không tìm thấy dịch vụ nào với tên này." });
                }

                return Ok(dichVus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tìm dịch vụ: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpPost]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> AddDichVu([FromBody] DichVuVM model)
        {
            try
            {
                var dichVuVM = await _dichVuRepository.AddDichVu(model);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Thêm dịch vụ thành công!",
                    data = dichVuVM
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
                    message = "Lỗi khi thêm dịch vụ: " + ex.Message,
                    data = (object)null
                });
=======
                return Ok(new { Message = "Thêm dịch vụ thành công!", Data = dichVuVM });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi thêm dịch vụ: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpPut("{tenDichVu}")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> UpdateDichVu(string tenDichVu, [FromBody] DichVuVM model)
        {
            try
            {
                var result = await _dichVuRepository.UpdateDichVu(tenDichVu, model);
                if (!result)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy dịch vụ để cập nhật.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Cập nhật dịch vụ thành công!",
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
                    message = "Lỗi khi cập nhật dịch vụ: " + ex.Message,
                    data = (object)null
                });
=======
                    return NotFound(new { Message = "Không tìm thấy dịch vụ để cập nhật." });
                }

                return Ok(new { Message = "Cập nhật dịch vụ thành công!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi cập nhật dịch vụ: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpDelete("{tenDichVu}")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> DeleteDichVu(string tenDichVu)
        {
            try
            {
                var result = await _dichVuRepository.DeleteDichVu(tenDichVu);
                if (!result)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy dịch vụ để xóa.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Xóa dịch vụ thành công!",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi xóa dịch vụ: " + ex.Message,
                    data = (object)null
                });
            }
        }
    }
}
=======
                    return NotFound(new { Message = "Không tìm thấy dịch vụ để xóa." });
                }

                return Ok(new { Message = "Xóa dịch vụ thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi xóa dịch vụ: " + ex.Message });
            }
        }
    }
}
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
