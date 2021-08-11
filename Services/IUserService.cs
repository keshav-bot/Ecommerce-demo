using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Services
{
    public interface IUserService
    {
        public string getUserId();
        public bool IsAuthenticated();
    }
}
