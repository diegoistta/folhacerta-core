using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FolhaCerta.Business.Service.Interfaces;
using FolhaCerta.Model.Dto;
using FolhaCerta.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    [Route("api")]
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
        [HttpPost("login")]
        public IActionResult login([FromBody]UsuarioDto usuario)
        {
             var service = usuarioService.Autenticar(usuario);
             
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
             
            service.Data.Token = tokenString;
            return Ok(service);
        }

        [HttpPost("salvar-usuario")]
        public IActionResult Registro([FromBody]UsuarioDto usuarioDto)
        {
            try 
            {
                // save 
              var data = usuarioService.Salvar(usuarioDto);

                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

    }
}
