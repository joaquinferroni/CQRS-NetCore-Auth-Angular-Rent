namespace Rental.Domain.Infrastructure
{
    public class CustomerLimitDefaultValue 
    {
        public static readonly string SECTION = "CustomerLimitDefaultValue";
        public int MaxBetPercentage { get; set; }
        public int MaxWinPercentage { get; set; }
    }
}