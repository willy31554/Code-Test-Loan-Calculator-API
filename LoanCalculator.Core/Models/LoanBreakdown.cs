namespace LoanCalculator.Core.Models
{

    public class LoanBreakdown
    {
        public double LoanAmount { get; set; }
        public string? PaymentFrequency { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public string? InterestType { get; set; }
        public double InterestRate { get; set; }
        public double ProcessingFees { get; set; }
        public double ExciseDuty { get; set; }
        public double LegalFees { get; set; }
        public double Interest { get; set; }
        public double TakeHomeAmount { get; set; }
    }
}
