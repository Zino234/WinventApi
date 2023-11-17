using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winvent.Application.Interface;
using Winvent.Application.Services;
using Winvent.Domain.Models;
using WinventApi.DTO.TitheDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TitheController : ControllerBase
    {
        private readonly ITitheService _titheService;
        private readonly IOfficerService _officerService;
        private readonly ILogger<OfferingController> _logger;
        public TitheController(ITitheService titheService, IOfficerService officerService, ILogger<OfferingController> logger)
        {
            _titheService = titheService;
            _officerService = officerService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddTithe")]
        public async Task<ActionResult<DefaultResponse<Tithe>>> AddTithe(AddTitheDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<Tithe>();

            try
            {
                var officer = await _officerService.GetOfficerById(dto.OfficerId);
                var tit = new Tithe
                {
                    TitheName = dto.TitheName,
                    TitheCollectedBy = officer.OfficerFirstname,
                    TitheCollectedAt = dto.TitheCollectedAt,
                    TitheGivenBy = dto.TitheGivenBy,
                    TitheAmount = dto.TitheAmount,
                    officer = officer,
                    TitheCreatedAt = DateTime.Now,
                    ServiceType = dto.ServiceType

                };
                await _titheService.AddTithe(tit);
                response.Status = true;
                response.Data = tit;
                response.ResponseMessage = "Tithe Added Successfully";
                response.ResponseCode = "00";
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
        [Route("GetAllTithes")]
        public async Task<ActionResult<DefaultResponse<List<Tithe>>>> GetAllTithes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new DefaultResponse<List<Tithe>>();
            try
            {
                var res = await _titheService.GetAllTithes();
                response.Status = true;
                response.ResponseMessage = res.Count > 0 ? "Tithes found" : "No tithe found";
                response.Data = res;
                response.ResponseCode = "00";
                return Ok(res);
                
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
        [Route("GetTithe/{Id}")]
        public async Task<ActionResult<DefaultResponse<Tithe>>> GetTithe([FromRoute] Guid Id)
        {
            DefaultResponse<Tithe> response = new();
            try
            {
                var singleTithe = await _titheService.GetTitheById(Id);
                if (singleTithe == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No tithe found";
                    return StatusCode(404, response);
                }

                response.Data = singleTithe;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Tithe Found";
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
        [Route("UpdateTithe/ {id}")]
        public async Task<ActionResult<DefaultResponse<Tithe>>> UpdateTithe([FromBody]UpdateTitheDto dto, [FromRoute] Guid id)
        {
            var response = new DefaultResponse<Tithe>();

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

               
                var tit = await _titheService.GetTitheById(id);
                if (tit == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No Tithe Found";
                    return StatusCode(404, response);
                }

                tit.TitheAmount = dto.TitheAmount;
                tit.TitheName = dto.TitheName;
                tit.TitheGivenBy = dto.TitheGivenBy;
                tit.TitheCollectedAt = dto.TitheCollectedAt;
                tit.ServiceType = dto.ServiceType;


                 await _titheService.UpdateTithe(tit);

                response.Data = tit;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = $"updated Successfull";
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
        [Route("DeleteTithe/{Id}")]
        public async Task<ActionResult<DefaultResponse<Tithe>>> DeleteTithe([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<Officer>();
            try
            {

                var getTithe =  await _titheService.GetTitheById(Id);
                if (getTithe == null)
                {

                    response.Status = false;
                    response.ResponseMessage = "No Tithe  found";
                    return StatusCode(404, response);
                }

                await _titheService.DeleteTitheById(Id);
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
