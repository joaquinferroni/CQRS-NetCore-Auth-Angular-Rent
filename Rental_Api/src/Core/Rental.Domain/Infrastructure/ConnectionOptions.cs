using System;
using System.Collections.Generic;
using System.Text;

namespace Rental.Domain.Infrastructure
{
    public class ConnectionOptions
    {
        public static readonly string SECTION = "ConnectionStrings";
        public string Asidb { get; set; }
        public string LimitPre { get; set; }
        public string LimitLive { get; set; }
        public string RedisCache { get; set; }
    }
}
