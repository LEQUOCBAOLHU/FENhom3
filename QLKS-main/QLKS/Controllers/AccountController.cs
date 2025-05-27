using Microsoft.AspNetCore.Mvc;
using QLKS.Models;
using QLKS.Repository;
using QLKS.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace QLKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }
<<<<<<< HEAD

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "QuanLy")]
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _repository.GetAllAccounts(pageNumber, pageSize);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Lấy danh sách tài khoản thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi lấy danh sách: " + ex.Message,
                    data = (object)null
                });
=======
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi lấy danh sách: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [Authorize(Roles = "QuanLy")]
<<<<<<< HEAD
        [HttpGet("by-name")]
=======
        [HttpGet("{hoTen}")]
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        public async Task<IActionResult> GetByNameNhanVien([FromQuery] string hoTen)
        {
            try
            {
<<<<<<< HEAD
                if (string.IsNullOrEmpty(hoTen))
                {
                    return BadRequest(new
                    {
                        message = "Họ tên không được để trống.",
                        data = (object)null
                    });
                }

=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                var accounts = await _repository.GetByNameNhanVien(hoTen);
                var result = accounts.Select(nv => new Account
                {
                    HoTen = nv.HoTen,
                    MaVaiTro = nv.MaVaiTro,
                    SoDienThoai = nv.SoDienThoai,
                    Email = nv.Email,
                    GioiTinh = nv.GioiTinh,
                    DiaChi = nv.DiaChi,
                    NgaySinh = nv.NgaySinh
<<<<<<< HEAD
                }).ToList();
                return Ok(new
                {
                    message = "Tìm kiếm tài khoản thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi tìm kiếm: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi tìm kiếm: " + ex.Message });
            }
        }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "QuanLy")]
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Account model)
        {
            try
            {
                var nhanVien = new NhanVien
                {
                    HoTen = model.HoTen,
                    Email = model.Email,
                    SoDienThoai = model.SoDienThoai,
                    MaVaiTro = model.MaVaiTro,
                    GioiTinh = model.GioiTinh,
                    DiaChi = model.DiaChi,
                    NgaySinh = model.NgaySinh
                };

                var addedAccount = await _repository.AddAccount(nhanVien);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Thêm tài khoản thành công!",
                    data = new { Email = addedAccount.Email }
                });
=======
                return Ok(new { Message = "Thêm tài khoản thành công!", Email = addedAccount.Email });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message ?? ex.Message;
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Lỗi khi thêm tài khoản: " + ex.Message,
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi thêm tài khoản: " + ex.Message,
                    data = (object)null
                });
            }
        }

=======
                return BadRequest(new { Message = "Lỗi khi thêm tài khoản: " + innerException });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Lỗi khi thêm tài khoản: " + ex.Message });
            }
        }
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        [Authorize(Roles = "QuanLy")]
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateAccount(string email, [FromBody] UpdateAccountDTO model)
        {
            try
            {
                var success = await _repository.UpdateAccount(email, model);
                if (!success)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy tài khoản để cập nhật.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Cập nhật tài khoản thành công!",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi cập nhật tài khoản: " + ex.Message,
                    data = (object)null
                });
            }
        }

        [Authorize(Roles = "QuanLy")]
        [HttpDelete("{email}")]
=======
                    return NotFound(new { Message = "Không tìm thấy tài khoản để cập nhật." });
                }

                return Ok(new { Message = "Cập nhật tài khoản thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Lỗi khi cập nhật tài khoản: " + ex.Message });
            }
        }
        [Authorize(Roles = "QuanLy")]
        [HttpDelete("{email}")]
        
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        public async Task<IActionResult> DeleteAccount(string email)
        {
            try
            {
                var success = await _repository.DeleteAccount(email);
                if (!success)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy tài khoản hoặc tài khoản đã bị vô hiệu hóa.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Vô hiệu hóa tài khoản thành công!",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi vô hiệu hóa tài khoản: " + ex.Message,
                    data = (object)null
                });
=======
                    return NotFound(new { Message = "Không tìm thấy tài khoản hoặc tài khoản đã bị vô hiệu hóa." });
                }

                return Ok(new { Message = "Vô hiệu hóa tài khoản thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi vô hiệu hóa tài khoản: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpPut("restore/{email}")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> RestoreAccount(string email)
        {
            try
            {
                var success = await _repository.RestoreAccount(email);
                if (!success)
                {
<<<<<<< HEAD
                    return NotFound(new
                    {
                        message = "Không tìm thấy tài khoản hoặc tài khoản đã hoạt động.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    message = "Khôi phục tài khoản thành công!",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi khôi phục tài khoản: " + ex.Message,
                    data = (object)null
                });
            }
        }
    }
}
=======
                    return NotFound(new { Message = "Không tìm thấy tài khoản hoặc tài khoản đã hoạt động." });
                }

                return Ok(new { Message = "Khôi phục tài khoản thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi khôi phục tài khoản: " + ex.Message });
            }
        }

    }
}
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
