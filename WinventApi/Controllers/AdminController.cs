using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Winvent.Application.Interface;
using Winvent.Domain.Models;
using WinventApi.DTO.AdminDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        public AdminController(IAdminService adminService , ILogger<AdminController> logger, IConfiguration configuration )
        {
            _adminService = adminService;
            _logger = logger;
            _configuration = configuration;

        }


        [HttpPost]
        [Route("AdminLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<DefaultResponse<string>>> AdminLogin([FromBody]AdminLoginDto dto)
        {
            var addi = new Admin()
            {
                AdminUsername = dto.AdminUsername,
                AdminPassword = dto.AdminPassword
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DefaultResponse<string> response = new();
            try
            {
                var user = await _adminService.AdminLogin(addi);
                if (addi == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "no admin found";
                    return StatusCode(404, response);
                }
                response.Data = "Success";
                response.Status = true;
                var token = CreateToken(addi);
                response.ResponseCode = "00";
                response.ResponseMessage = "Logged in successfully:" + token;
                return Ok(response);
            }
            

            catch (Exception ex)
            {

                _logger.LogError("", ex);
                response.Status = false;
                response.ResponseCode = "99";
                response.ResponseMessage = "An Error Occured";
                return StatusCode(500, response);
            }
        }
        private string CreateToken(Admin add)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, add.AdminUsername),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials, claims: claims);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
