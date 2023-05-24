using LoanCalculator.Application.Services;
using LoanCalculator.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LoanCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanCalculatorService _loanCalculatorService;

        public LoanController(ILoanCalculatorService loanCalculatorService)
        {
            _loanCalculatorService = loanCalculatorService;
        }

        [HttpPost]
        public IActionResult CalculateLoanBreakdown([FromBody] LoanParameters parameters)
        {
            try
            {
                LoanBreakdown breakdown = _loanCalculatorService.CalculateLoanBreakdown(parameters);
                return Ok(breakdown);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
