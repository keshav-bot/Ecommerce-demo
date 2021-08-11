using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KKTraders.Services
{
    public class UserService: IUserService
    {
        private readonly IHttpContextAccessor httpContectAccessor;

        public UserService(IHttpContextAccessor  httpContectAccessor)
        {
            this.httpContectAccessor = httpContectAccessor;
        }
        public string getUserId()
        {
           var user= httpContectAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return user;
        }

        public bool IsAuthenticated()
        {
            return httpContectAccessor.HttpContext.User.Identity.IsAuthenticated;
            
        }
    }
}
