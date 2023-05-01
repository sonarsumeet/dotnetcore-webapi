﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public DateTime RefreshTokenCreate { get; set; }
    }
}
