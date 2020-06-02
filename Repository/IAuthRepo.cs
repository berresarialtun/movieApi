using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Repository
{
    public interface IAuthRepo
    {
        public Action Login(User user);
    }
}
