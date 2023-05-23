using LoanCalculator.Core.Models;

namespace LoanCalculator.Application.Services
{
    public class LoanCalculatorService : ILoanCalculatorService
    {
        public LoanBreakdown CalculateLoanBreakdown(LoanParameters parameters)
        {
            // Constants for fees
            const double ProcessingFeePercentage = 0.03;
            const double ExciseDutyPercentage = 0.2;
            const double LegalFees = 10000;

            // Calculate charges
            double processingFees = parameters.LoanAmount * ProcessingFeePercentage;
            double exciseDuty = processingFees * ExciseDutyPercentage;

            // Calculate interest based on interest type
            double interestRate;
            if (parameters.InterestType == "Flat Rate")
                interestRate = parameters.BankInterestRateFlat;
            else if (parameters.InterestType == "Reducing Balance")
                interestRate = parameters.BankInterestRateReducing;
            else
                throw new Exception("Invalid interest type.");

            double interest = CalculateInterest(parameters.LoanAmount, interestRate, parameters.LoanPeriod, parameters.PaymentFrequency);

            // Calculate take-home amount
            double takeHomeAmount = parameters.LoanAmount - (processingFees + exciseDuty + LegalFees + interest);

            // Build breakdown
            LoanBreakdown breakdown = new LoanBreakdown
            {
                LoanAmount = parameters.LoanAmount,
                PaymentFrequency = parameters.PaymentFrequency,
                LoanPeriod = parameters.LoanPeriod,
                StartDate = parameters.StartDate,
                InterestType = parameters.InterestType,
                InterestRate = interestRate,
                ProcessingFees = processingFees,
                ExciseDuty = exciseDuty,
                LegalFees = LegalFees,
                Interest = interest,
                TakeHomeAmount = takeHomeAmount
            };

            return breakdown;
        }

        private double CalculateInterest(double loanAmount, double interestRate, int loanPeriod, string paymentFrequency)
        {
            double totalInterest = 0;
            double effectiveInterestRate = interestRate / 100;

            switch (paymentFrequency)
            {
                case "Annually":
                    totalInterest = loanAmount * effectiveInterestRate * loanPeriod;
                    break;
                case "Quarterly":
                    int numPaymentsPerYear = 4;
                    int numPayments = loanPeriod * numPaymentsPerYear;
                    double quarterlyInterestRate = effectiveInterestRate / numPaymentsPerYear;
                    totalInterest = loanAmount * quarterlyInterestRate * numPayments;
                    break;
                case "Monthly":
                    int numPaymentsPerMonth = 12;
                    int numPaymentsMonthly = loanPeriod * numPaymentsPerMonth;
                    double monthlyInterestRate = effectiveInterestRate / numPaymentsMonthly;
                    totalInterest = loanAmount * monthlyInterestRate * numPaymentsMonthly;
                    break;
                case "Every 6 Months":
                    int numPaymentsPerHalfYear = 2;
                    int numPaymentsHalfYear = loanPeriod * numPaymentsPerHalfYear;
                    double halfYearInterestRate = effectiveInterestRate / numPaymentsPerHalfYear;
                    totalInterest = loanAmount * halfYearInterestRate * numPaymentsHalfYear;
                    break;
                default:
                    throw new Exception("Invalid payment frequency.");
            }

            return totalInterest;
        }
    }
}
