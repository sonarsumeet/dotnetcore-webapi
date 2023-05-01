using NetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Repository
{
    public interface IUsers
    {
        public Task<bool> CreateUsers(Users objUsers);

        public string LoginUser(LoginModel objLoginModel);
    }
}
