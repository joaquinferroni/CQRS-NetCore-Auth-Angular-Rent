using System;
using System.Collections.Generic;
using System.Text;

namespace Rental.Domain.Infrastructure
{
    public class LiveLimitOptions
    {
        public static readonly string SECTION = "LiveLimitOptions";
        public  CustomerLimitDefaultValue  CustomerLimitDefaultValue{ get; set; }
    }
}
