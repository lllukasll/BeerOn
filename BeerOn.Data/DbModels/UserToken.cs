﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOn.Data.DbModels
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
