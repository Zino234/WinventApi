using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Winvent.Application.Interface;
using Winvent.Application.Services;
using Winvent.Domain.Models;
using WinventApi.DTO.OfficerDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficerController : ControllerBase
    {

        private readonly IOfficerService _officerService;
        private readonly ILogger<OfficerController> _logger;
        private readonly IConfiguration _configuration;
        public OfficerController(IOfficerService officerService, ILogger<OfficerController> logger, IConfiguration configuration)
        {
            _officerService = officerService;
            _logger = logger;
            _configuration = configuration;

        }


        [HttpPost]
        [Route("CreateOfficer")]
        public async Task<ActionResult<DefaultResponse<Officer>>> CreateOfficer([FromBody] CreateOfficerDto dto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.OfficerPassword);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DefaultResponse<Officer> response = new();
            try
            {
                var off = new Officer()
                {
                    OfficerFirstname = dto.OfficerFirstname,
                    OfficerLastname = dto.OfficerLastname,
                    OfficerUsername = dto.OfficerUsername,
                    OfficerPhone = dto.OfficerPhone,
                    OfficerEmail = dto.OfficerEmail,
                    OfficerPassword = passwordHash,
                    OfficerCreatedAt = DateTime.Now,
                    OfficerIsDisabled = false
                };

                await _officerService.CreateOfficer(off);
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Officer Added Successfully";
                response.Data = off;
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


        [HttpPost]
        [Route("OfficerLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<DefaultResponse<Officer>>> OfficerLogin([FromBody] OfficerLoginDto dto)
        {


            var off = new Officer()
            {
                OfficerUsername = dto.Username,
                OfficerPassword = dto.Password
            };

            // var res = BCrypt.Net.BCrypt.Verify(dto.Password, off.OfficerPassword);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<Officer>();


            try
            {
                var user = await _officerService.OfficerLogin(off);
                if (off == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "no officer found";
                    return StatusCode(404, response);
                }
                response.Data = off;
                response.Status = true;
                response.ResponseCode = "00";
                var token = CreateToken(off);
                response.ResponseMessage = "Logged in successfully: " + token;
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



        

        [HttpGet]
        [Route("GetAllOfficers")]
        public async Task<ActionResult<DefaultResponse<List<Officer>>>> GetAllOfficers()
        {
            var response = new DefaultResponse<List<Officer>>();
            try
            {
                var res = await _officerService.GetAllOfficers();
                response.Status = true;
                response.ResponseMessage = "Success";
                response.ResponseCode = "00";
                response.Data = res;
                return Ok(response);
            }
            catch (Exception)
            {

                response.Status = false;
                response.ResponseMessage = "Something went wrong";
                response.ResponseCode = "99";
                return StatusCode(500, response);
            }
        }


        [HttpPut]
        [Route("UpdateOfficer/{Id}")]
        public async Task<ActionResult<DefaultResponse<Officer>>> UpdateOfficer([FromBody] UpdateOfficerDto dto, [FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var response = new DefaultResponse<Officer>();
            try
            {
                var res = await _officerService.GetOfficerById(Id);
                if (res == null)
                {
                    response.ResponseMessage = "Not Found";
                    response.ResponseCode = "99";
                    return StatusCode(404, response);
                }

                res.OfficerEmail = dto.OfficerEmail;
                res.OfficerFirstname = dto.OfficerFirstname;
                res.OfficerLastname = dto.OfficerLastname;
                res.OfficerPhone = dto.OfficerPhone;
                res.OfficerUsername = dto.OfficerUsername;
                res.OfficerPassword = dto.OfficerPassword;
                await _officerService.UpdateOfficer(res);
                response.ResponseMessage = "Updated successfully";
                response.Status = true;
                response.ResponseCode = "00";
                response.Data = res;
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseMessage = "Something went wrong";
                response.Status = false;
                response.ResponseCode = "99";
                return StatusCode(500, response);
            }

        }


        [HttpGet]
        [Route("GetOfficer/{Id}")]
        public async Task<ActionResult<DefaultResponse<Officer>>> GetOfficer([FromRoute] Guid Id)
        {
            DefaultResponse<Officer> response = new();
            try
            {
                var singleOfficer = await _officerService.GetOfficerById(Id);
                if (singleOfficer == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No offficer found";
                    return StatusCode(404, response);
                }

                response.Data = singleOfficer;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Officer Found";
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

        [HttpDelete]
        [Route("DeleteOfficer/{Id}")]
        public async Task<ActionResult<DefaultResponse<Officer>>> DeleteOfficer([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<Officer>();
            try
            {

                var getOfficer = await _officerService.GetOfficerById(Id);
                if (getOfficer == null)
                {

                    response.Status = false;
                    response.ResponseMessage = "No Offering  found";
                    return StatusCode(404, response);
                }

                await _officerService.DeleteOfficerById(Id);
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "deleted  successfully";
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


        


        private string CreateToken(Officer office)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, office.OfficerUsername)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials, claims: claims);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }


}
