using Microsoft.AspNetCore.Mvc;
using QLKS.Models;
using QLKS.Repository;
using QLKS.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace QLKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly INhanVienRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(INhanVienRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("users")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                var existingUser = await _repository.GetNhanVienByEmail(model.Email);
                if (existingUser == null)
<<<<<<< HEAD
                    return BadRequest(new
                    {
                        message = "Email chưa được thêm vào hệ thống. Vui lòng dùng API AddAccount trước.",
                        data = (object)null
                    });

                if (!existingUser.IsActive)
                    return BadRequest(new
                    {
                        message = "Tài khoản đã bị vô hiệu hóa, không thể đăng ký.",
                        data = (object)null
                    });

                if (existingUser.MatKhau != null && existingUser.MatKhau.Length > 0)
                    return BadRequest(new
                    {
                        message = "Tài khoản đã được đăng ký trước đó.",
                        data = (object)null
                    });
=======
                    return BadRequest(new { Message = "Email chưa được thêm vào hệ thống. Vui lòng dùng API AddAccount trước." });

                if (!existingUser.IsActive)
                    return BadRequest(new { Message = "Tài khoản đã bị vô hiệu hóa, không thể đăng ký." });

                if (existingUser.MatKhau != null && existingUser.MatKhau.Length > 0)
                    return BadRequest(new { Message = "Tài khoản đã được đăng ký trước đó." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.MatKhau);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(hashedPassword);

                var nhanVienToUpdate = new NhanVien
                {
                    Email = model.Email,
                    MatKhau = passwordBytes
                };

                var updatedNhanVien = await _repository.Register(nhanVienToUpdate);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Đăng ký thành công!",
                    data = new { MaNv = updatedNhanVien.MaNv }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi đăng ký: " + ex.Message,
                    data = (object)null
                });
=======
                return Ok(new { Message = "Đăng ký thành công!", MaNv = updatedNhanVien.MaNv });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi khi đăng ký: " + ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var (nhanVien, token, refreshToken) = await _repository.Login(model.Email, model.MatKhau);
            if (nhanVien == null)
            {
                var existingUser = await _repository.GetNhanVienByEmail(model.Email);
                if (existingUser != null && !existingUser.IsActive)
                {
<<<<<<< HEAD
                    return Unauthorized(new
                    {
                        message = "Tài khoản đã bị vô hiệu hóa.",
                        data = (object)null
                    });
                }
                return Unauthorized(new
                {
                    message = "Email hoặc mật khẩu không đúng.",
                    data = (object)null
                });
=======
                    return Unauthorized(new { Message = "Tài khoản đã bị vô hiệu hóa." });
                }
                return Unauthorized(new { Message = "Email hoặc mật khẩu không đúng." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }

            var response = new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
<<<<<<< HEAD
                MaNv = nhanVien.MaNv,
=======
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
                HoTen = nhanVien.HoTen,
                Email = nhanVien.Email
            };

<<<<<<< HEAD
            return Ok(new
            {
                message = "Đăng nhập thành công!",
                data = response
            });
        }

        [HttpPost("tokens/refresh")]
        [AllowAnonymous]
=======
            return Ok(response);
        }

        [HttpPost("tokens/refresh")]
        [AllowAnonymous] // Cho phép gọi mà không cần JWT
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            try
            {
                var (newToken, newRefreshToken) = await _repository.RefreshToken(model.Token, model.RefreshToken);
<<<<<<< HEAD
                return Ok(new
                {
                    message = "Làm mới token thành công!",
                    data = new { Token = newToken, RefreshToken = newRefreshToken }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    data = (object)null
                });
=======
                return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var success = await _repository.RevokeToken(token);
            if (!success)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Không thể đăng xuất. Token không hợp lệ.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                message = "Đăng xuất thành công!",
                data = (object)null
            });
=======
                return BadRequest(new { Message = "Không thể đăng xuất. Token không hợp lệ." });
            }

            return Ok(new { Message = "Đăng xuất thành công!" });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        [HttpPost("password/reset")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var success = await _repository.ForgotPassword(model.Email);
            if (!success)
            {
                var existingUser = await _repository.GetNhanVienByEmail(model.Email);
                if (existingUser != null && !existingUser.IsActive)
                {
<<<<<<< HEAD
                    return BadRequest(new
                    {
                        message = "Tài khoản đã bị vô hiệu hóa, không thể đặt lại mật khẩu.",
                        data = (object)null
                    });
                }
                return BadRequest(new
                {
                    message = "Email không tồn tại hoặc không thể tạo mật khẩu mới.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                message = "Mật khẩu mới đã được gửi qua email.",
                data = (object)null
            });
=======
                    return BadRequest(new { Message = "Tài khoản đã bị vô hiệu hóa, không thể đặt lại mật khẩu." });
                }
                return BadRequest(new { Message = "Email không tồn tại hoặc không thể tạo mật khẩu mới." });
            }

            return Ok(new { Message = "Mật khẩu mới đã được gửi qua email." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
        }

        [HttpPost("password")]
        [Authorize(Roles = "QuanLy")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var (nhanVien, token, refreshToken) = await _repository.Login(model.Email, model.OldPassword);
            if (nhanVien == null)
            {
                var existingUser = await _repository.GetNhanVienByEmail(model.Email);
                if (existingUser != null && !existingUser.IsActive)
                {
<<<<<<< HEAD
                    return Unauthorized(new
                    {
                        message = "Tài khoản đã bị vô hiệu hóa.",
                        data = (object)null
                    });
                }
                return Unauthorized(new
                {
                    message = "Email hoặc mật khẩu cũ không đúng.",
                    data = (object)null
                });
=======
                    return Unauthorized(new { Message = "Tài khoản đã bị vô hiệu hóa." });
                }
                return Unauthorized(new { Message = "Email hoặc mật khẩu cũ không đúng." });
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(hashedPassword);

            var success = await _repository.UpdatePassword(model.Email, passwordBytes);
            if (!success)
            {
<<<<<<< HEAD
                return BadRequest(new
                {
                    message = "Không thể đổi mật khẩu.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                message = "Đổi mật khẩu thành công!",
                data = (object)null
            });
        }
    }
}
=======
                return BadRequest(new { Message = "Không thể đổi mật khẩu." });
            }

            return Ok(new { Message = "Đổi mật khẩu thành công!" });
        }
    }
}
>>>>>>> df739cd28c6e6f45fd775af0122f6c41a50ab98c
