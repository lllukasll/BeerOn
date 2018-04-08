using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOn.Data.ModelsDto
{
    public class LoginResponseData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
