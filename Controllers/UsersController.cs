using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.ViewModel;

namespace webapi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly apiDbContext db;
        private readonly IMapper mapper;
        public UsersController(apiDbContext _db,IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("info")]
        public IActionResult Info()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            var username=claims[0].Value;
            UserVM userVm = mapper.Map<UserVM>(db.Users.Where(x => x.username == username).FirstOrDefault());
            return Ok(userVm);

        }

        [HttpPut]
        [Authorize]
        [Route("info")]
        public IActionResult Info([FromBody] UserVM userVM)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claims = identity.Claims.ToList();
            var username = claims[0].Value;
            try
            {
                User findUser = db.Users.Where(x => x.username == username).FirstOrDefault();
                findUser.username = userVM.username;
                findUser.name = userVM.name;
                findUser.surname = userVM.surname;
                findUser.password = userVM.password;
                findUser.email = userVM.email;
                db.SaveChanges();
                return Ok(findUser);
            }
            catch(Exception err) {
                return BadRequest(err.Message);
            }
     
          

            
        }

    }
}
