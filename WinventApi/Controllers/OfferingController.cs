using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Winvent.Application.Interface;
using Winvent.Application.Services;
using Winvent.Domain.Models;
using WinventApi.DTO.ExpenseDto;
using WinventApi.DTO.OfferingDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferingController : ControllerBase
    {
        private readonly IOfferingService _offeringService;
        private readonly IOfficerService _officerService;
        private readonly ILogger<OfferingController> _logger;
        public OfferingController(IOfferingService offeringService, IOfficerService officerService, ILogger<OfferingController> logger)
        {
            _offeringService = offeringService;
            _officerService = officerService;
            _logger = logger;

        }


        [HttpPost]
        [Route("AddOffering")]
        public async Task<ActionResult<DefaultResponse<Offering>>> AddOffering([FromBody]AddOfferingDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DefaultResponse<Offering> response = new DefaultResponse<Offering>();

            try
            {
                //var officer = await _officerService.Officers.FindAsync(dto.OfficerId);
                var officer = await _officerService.GetOfficerById(dto.OfficerId);
                var off = new Offering()
                {
                    OfferingAmount = dto.OfferingAmount,
                    OfferingCollectedAt = dto.OfferingCollectedAt,
                    OfficerId = dto.OfficerId,
                    Officer=officer,
                    OfferingcreatedAt = DateTime.Now,
                    OfferingCollectedBy = officer.OfficerFirstname,
                    ServiceType = dto.ServiceType,

                };
                await _offeringService.CreateOffering(off);
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Offering Added Successfully";
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


        [HttpGet]
        [Route("GetAllOfferings")]
        public async Task<ActionResult<DefaultResponse<List<Offering>>>> GetAllOfferings()
        {
            DefaultResponse<List<Offering>> response = new();
            try
            {
                var allOfferings = await _offeringService.GetAllOfferings();
                response.Status = allOfferings.Count > 0;
                response.Data = allOfferings;
                response.ResponseCode = "00";
                response.ResponseMessage = allOfferings.Count > 0 ? "Offering found" : "No offering found";

                return Ok(allOfferings);


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
        [Route("GetOffering/{Id}")]
        public async Task<ActionResult<DefaultResponse<Offering>>> GetOffering([FromRoute] Guid Id)
        {
            DefaultResponse<Offering> response = new();
            try
            {
                var singleOffering = await _offeringService.GetOfferingById(Id);
                if (singleOffering == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No offering found";
                    return StatusCode(404, response);
                }

                response.Data = singleOffering;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Offering Found";
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

        [HttpPut]
        [Route("UpdateOffering/{Id}")]
        public async Task<ActionResult<DefaultResponse<Offering>>> UpdateOffering([FromBody] UpdateOfferingDto dto, [FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<Offering>();
            try
            {
                var res = await _offeringService.GetOfferingById(Id);
                if (res == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No Offering Found";
                    return StatusCode(404, response);
                }

                res.OfferingAmount = dto.OfferingAmount;
                res.OfferingCollectedAt = dto.OfferingCollectedAt;
                res.ServiceType = dto.ServiceType;

                await _offeringService.UpdateOffering(res);
                response.Data = res;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Updated successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                response.Status = false;
                response.ResponseCode = "99";
                response.ResponseMessage = "Something went wrong";
                return StatusCode(500, response);
            }

        }



        [HttpDelete]
        [Route("DeleteOffering/{Id}")]
        public async Task<ActionResult<DefaultResponse<Offering>>> DeleteOffering([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<Offering>();
            var getOffering = await _offeringService.GetOfferingById(Id);

            try
            {
               
                if (getOffering == null)
                {

                    response.Status = false;
                    response.ResponseMessage = "No Offering  found";
                    return StatusCode(404, response);
                }

                await _offeringService.DeleteOfferingById(Id);
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

    }
}
