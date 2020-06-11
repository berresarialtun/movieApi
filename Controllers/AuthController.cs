using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webapi.Models;
using webapi.Repository;
using webapi.ViewModel;

namespace webapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly apiDbContext db;
        private IConfiguration config;
        private readonly IMapper mapper;
        public AuthController(apiDbContext _db, IConfiguration _config, IMapper _mapper)
        {
            db = _db;
            config = _config;
            mapper = _mapper;
        }
   
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterVM registerUser)
        {
            User user = mapper.Map< User >(registerUser);
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Ok("Kullanıcı kaydedildi");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

            }
            return BadRequest("Kullanıcı kaydedilemedi.");
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginVM loginUser)
        {     
            var user = db.Users.Where(x => x.username == loginUser.username && x.password == loginUser.password ).SingleOrDefault() ;
            if (user != null)
            {
               
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
           
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.name.ToString() ),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString() )

                };
                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credential
                );

                var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
                //loginUser.userToken = encodetoken;
                return Ok(encodetoken+"Giriş başarılı");
            }
            return BadRequest("hatalı giriş");

        }
       
    }
}
