using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winvent.Application.Interface;
using Winvent.Application.Services;
using Winvent.Domain.Models;
using WinventApi.DTO.TransportSeedDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportSeedController : ControllerBase
    {
        private readonly ITransportSeedService _transportSeed;
        private readonly IOfficerService _officerService;
        private readonly ILogger<TransportSeedController> _logger;
        public TransportSeedController(ITransportSeedService transportSeedService, IOfficerService officerService, ILogger<TransportSeedController> logger)
        {
            _logger = logger;
            _officerService = officerService;
            _transportSeed = transportSeedService;
        }

        [HttpPost]
        [Route("AddTransportSeed")]
        public async Task<ActionResult<DefaultResponse<TransportSeed>>> AddTransportSeed([FromBody] AddTransportSeedDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new DefaultResponse<TransportSeed>();
            try
            {
                var officer = await _officerService.GetOfficerById(dto.OfficerId);
                var res = new TransportSeed
                {
                    TransportSeedAmount = dto.TransportSeedAmount,
                    TransportSeedCollectedAt = dto.TransportSeedCollectedAt,
                    TransportSeedGivenBy = dto.TransportSeedGivenBy,
                    TransportSeedCollectedBy = officer.OfficerFirstname,
                    TransportSeedCreatedAt = DateTime.Now,
                    TransportSeedName = dto.TransportSeedName,
                    officer = officer,
                    ServiceType = dto.ServiceType,
                };

                await _transportSeed.AddTransportSeed(res);
                response.Status = true;
                response.Data = res;
                response.ResponseCode = "00";
                response.ResponseMessage = "TransportSeed Added Successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Status = false;
                response.ResponseCode = "99";
                response.ResponseMessage = "something went wrong";
                return StatusCode(500, response);
            }
        }


        [HttpGet]
        [Route("GetAllTransportSeeds")]
        public async Task<ActionResult<DefaultResponse<List<TransportSeed>>>> GetAllTransportSeeds()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<List<TransportSeed>>();
            try
            {
                var res = await _transportSeed.GetAllTransportSeeds();
                response.Status = true;
                response.ResponseCode = "00";
                response.Data = res;
                response.ResponseMessage = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Status = false;
                response.ResponseCode = "99";
                response.ResponseMessage = "something went wrong";
                return StatusCode(500, response);

            }
        }



        [HttpGet]
        [Route("GetTransportSeed/{Id}")]
        public async Task<ActionResult<DefaultResponse<TransportSeed>>> GetExpense([FromRoute] Guid Id)
        {
            DefaultResponse<TransportSeed> response = new();
            try
            {
                var singleTransportSeed = await _transportSeed.GetTransportSeedById(Id);
                if (singleTransportSeed == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No transportSeed found";
                    return StatusCode(404, response);
                }

                response.Data = singleTransportSeed;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "TransportSeed Found";
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
        [Route("UpdateTransportSeed/{Id}")]
        public async Task<ActionResult<DefaultResponse<TransportSeed>>> UpdateTransportSeed([FromBody] UpdateTransportSeedDto dto, [FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var response = new DefaultResponse<TransportSeed>();
            try
            {
                var res = await _transportSeed.GetTransportSeedById(Id);
                if (res == null)
                {
                    response.ResponseMessage = "No TransportSeed Found";
                    response.Status = false;
                    return StatusCode(404, response);
                }

                res.TransportSeedName = dto.TransportSeedName;
                res.TransportSeedGivenBy = dto.TransportSeedGivenBy;
                res.TransportSeedCollectedAt = dto.TransportSeedCollectedAt;
                res.TransportSeedAmount = dto.TransportSeedAmount;
                await _transportSeed.UpdateTransportSeed(res);
                res.ServiceType = dto.ServiceType;
                response.Status = true;
                response.ResponseCode = "00";
                response.Data = res;
                response.ResponseMessage = "Updated Successfully";
                return Ok(response);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                response.Status = false;
                response.ResponseCode = "99";
                response.ResponseMessage = "something went wrong";
                return StatusCode(500,response);
            }
        }


        [HttpDelete]
        [Route("DeleteTransportSeed/{Id}")]
        public async Task<ActionResult<DefaultResponse<TransportSeed>>> DeleteTransportseed([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<Officer>();
            try
            {

                var getTransportSeed = _transportSeed.GetTransportSeedById(Id);
                if (getTransportSeed == null)
                {

                    response.Status = false;
                    response.ResponseMessage = "No TransportSeed  found";
                    return StatusCode(404, response);
                }

                await _transportSeed.DeleteTransportSeedById(Id);
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
