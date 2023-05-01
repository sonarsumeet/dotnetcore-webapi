using NetCoreWebAPI.Models;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.DataAccess
{
    public class UsersDB : IUsers
    {
        private readonly ApplicationDataContext _applicationDBContext;
        public UsersDB(ApplicationDataContext applicationDBContext)
        {
            this._applicationDBContext = applicationDBContext;
        }
        public async Task<bool> CreateUsers(Users objUsers)
        {

            try
            {
                 await _applicationDBContext.AddAsync(objUsers);
                 await _applicationDBContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public string LoginUser(LoginModel objLoginModel)
        {
            try
            {
                var loginDetails = _applicationDBContext.Users.Where(x => x.UserName == objLoginModel.UserName).FirstOrDefault();
                if (loginDetails == null)
                    loginDetails.UserName = "";

                return loginDetails.UserName;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
