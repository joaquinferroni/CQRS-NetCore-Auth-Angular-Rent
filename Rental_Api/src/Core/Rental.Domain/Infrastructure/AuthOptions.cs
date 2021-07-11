namespace Rental.Domain.Infrastructure
{
    public class AuthOptions
    {
        public static readonly string SECTION = "Auth";
        public string Secret { get; set; }
        public double ExpireHours { get; set; }
    }
}