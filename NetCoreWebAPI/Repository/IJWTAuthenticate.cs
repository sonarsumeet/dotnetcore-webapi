using NetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Repository
{
    public interface IJWTAuthenticate
    {
        public JWTAuthToken Token();
    }
}
