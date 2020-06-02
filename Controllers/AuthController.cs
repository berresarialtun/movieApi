using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webapi.Helpers;
using webapi.Models;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly apiDbContext db;
        private IConfiguration _config;
        public AuthController(apiDbContext _db, IConfiguration config)
        {
            db = _db;
            _config = config;
        }
   
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await db.Users.AddAsync(user);
                    await db.SaveChangesAsync();
                    return Ok("Kullanıcı kaydedildi");
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User User)
        {
           
            User loginUser = db.Users.Where(x => x.username == User.username && x.password == User.password ).SingleOrDefault() ;
            if (loginUser!=null)
            {
               
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
           
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, loginUser.name.ToString() ),
                    new Claim(JwtRegisteredClaimNames.Jti, loginUser.Id.ToString() )

                };
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credential
                    );

                var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
              
                return Ok(new { encodetoken });
            }
            return BadRequest("hatalı giriş");

        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {

            return Ok("Fdrgdf");

        }

       
    }
}
