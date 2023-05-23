using LoanCalculator.Core.Models;

namespace LoanCalculator.Application.Services
{
    public interface ILoanCalculatorService
    {
        LoanBreakdown CalculateLoanBreakdown(LoanParameters parameters);
    }
}
