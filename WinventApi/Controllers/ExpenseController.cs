using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winvent.Application.Interface;
using Winvent.Domain.Models;
using WinventApi.DTO.ExpenseDto;
using WinventApi.Response;

namespace WinventApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IOfficerService _officerService;
        private readonly ILogger<ExpenseController> _logger;
        public ExpenseController(IExpenseService expenseService, IOfficerService officerService, ILogger<ExpenseController> logger)
        {
            _expenseService = expenseService;
            _officerService = officerService;
            _logger = logger;
        }


        [HttpPost]
        [Route("AddExpense")]
        public async Task<ActionResult<DefaultResponse<Expense>>> AddExpense(AddExpenseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new DefaultResponse<Expense>();
            try
            {
                var officer = await _officerService.GetOfficerById(dto.OfficerId);
                var res = new Expense
                {
                    ExpenseAmount = dto.ExpenseAmount,
                    ExpenseDescription = dto.ExpenseDescription,
                    ExpenseDoneAt = dto.ExpenseDoneAt,
                    ExpenseTitle = dto.ExpenseTitle,
                    ExpenseDoneBy = officer.OfficerFirstname,
                    ServiceType = dto.ServiceType,
                    officer = officer
                };

                await _expenseService.AddExpense(res);
                response.ResponseCode = "00";
                response.ResponseMessage = "Expense Added";
                response.Data = res;
                response.Status = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ResponseCode = "99";
                response.ResponseMessage = "Something went wrong";
                response.Status = false;
                return StatusCode(500, response);
            }
      
        }


        [HttpGet]
        [Route("GetAllExpenses")]
        public async Task<ActionResult<DefaultResponse<List<Expense>>>> GetAllExpenses()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<List<Expense>>();
            try
            {
              var exp =   await _expenseService.GetAllExpenses();
                if (exp == null)
                {
                    response.ResponseMessage = "No expense Found";
                    return StatusCode(404, response);
                }
                response.ResponseMessage = exp.Count > 0 ? "Expense found" : "No expense found";
                response.Data = exp;
                response.ResponseCode = "00";
                response.Status = true;
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                response.ResponseMessage = "Something Went Wrong";
                response.ResponseCode = "99";
                response.Status = false;
                return StatusCode(500, response);

            }
        }



        [HttpGet]
        [Route("GetExpense/{Id}")]
        public async Task<ActionResult<DefaultResponse<Expense>>> GetExpense([FromRoute] Guid Id)
        {
            DefaultResponse<Expense> response = new();
            try
            {
                var singleExpense = await _expenseService.GetExpenseById(Id);
                if (singleExpense == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No expense found";
                    return StatusCode(404, response);
                }

                response.Data = singleExpense;
                response.Status = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Exènse Found";
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
        [Route("UpdateExpense/{Id}")]
        public async Task<ActionResult<DefaultResponse<Expense>>> UpdateExpense([FromBody] UpdateExpenseDto dto, [FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<Expense>();     
            try
            {
                var res = await _expenseService.GetExpenseById(Id);
                if (res == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No Expense Found";
                    return StatusCode(404, response);
                }

                res.ExpenseAmount = dto.ExpenseAmount;
                res.ExpenseTitle = dto.ExpenseTitle;
                res.ExpenseDoneAt = dto.ExpenseDoneAt;
                res.ExpenseDescription = dto.ExpenseDescription;
                res.ServiceType = dto.ServiceType;
               await _expenseService.UpdateExpense(res);
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
        [Route("DeleteExpense/{Id}")]
        public async Task<ActionResult<DefaultResponse<Expense>>> DeleteExpense([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<Expense>();
            try
            {
                var getExpense = await _expenseService.GetExpenseById(Id);
                if(getExpense == null)
                {
                    response.Status = false;
                    response.ResponseMessage = "No Expense  found";
                    return StatusCode(404, response);
                }

                await _expenseService.DeleteExpenseById(Id);
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
                response.ResponseMessage = "Something went wrong";
                return StatusCode(500, response);
            }
        }
    }
}
