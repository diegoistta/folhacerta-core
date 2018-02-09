using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FolhaCerta.Business.Dto;
using AutoMapper;
using FolhaCerta.Business.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using FolhaCerta.WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net;




namespace FolhaCerta.WebApi.Controllers
{   
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
         private readonly IUsuarioService usuarioService;
         private readonly AppSettings appSettings;
       
        public UsuarioController(IUsuarioService usuarioService, IOptions<AppSettings> appSettings)
        {
            this.usuarioService = usuarioService;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("autenticacao")]
        public IActionResult Login([FromBody]UsuarioDto usuarioDto)
        {
             var service = usuarioService.Authenticate(usuarioDto);
             
            if (service.Error)
            {
                return BadRequest(service);
            };
                
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, service.Data.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            var data = new {
                Id = service.Data.Id,
                Login = service.Data.Login,
                Nome = service.Data.Nome,
                Sobrenome = service.Data.Sobrenome,
                Token = tokenString
            };

           
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios =  this.usuarioService.GetAll();
            return Ok(usuarios);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registro([FromBody]UsuarioDto usuarioDto)
        {
            try 
            {
                // save 
              var service = usuarioService.Create(usuarioDto);

                if (service.Error)
                {
                    return BadRequest(service);
                }

                return Ok(service);
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            } 
        }
    }
}
