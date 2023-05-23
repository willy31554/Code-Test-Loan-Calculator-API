namespace LoanCalculator.Core.Models
{
    public class LoanParameters
    {


        public double LoanAmount { get; set; }
        public string? PaymentFrequency { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime StartDate { get; set; }
        public string? InterestType { get; set; }
        public double BankInterestRateFlat { get; set; }
        public double BankInterestRateReducing { get; set; }
    }


}
