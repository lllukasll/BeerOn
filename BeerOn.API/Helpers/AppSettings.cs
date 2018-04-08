using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOn.API.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string AccessExpireMinutes { get; set; }

        public string RefreshExpireMinutes { get; set; }
    }
}
