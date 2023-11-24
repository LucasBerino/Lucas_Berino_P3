using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infraestructura.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Servicios.ContactosService;

namespace Lucas_Berino_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=1234;Database=LucasBerinoP3;";

        private readonly IConfiguration _configuracion;
        private UsuarioService UsuarioService;

        public AuthController(IConfiguration configuration)
        {
            _configuracion = configuration;
            UsuarioService = new UsuarioService(connectionString);
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] LoginModel login)
        {
            var usuario = UsuarioService.obtenerUser(login.UserName);

            if (usuario == null || usuario.contrasena != login.Password)
            {
                return Unauthorized();
            }

            var token = GenerateJWT(usuario);
            return Ok(new { jwt = token });
        }

        private object GenerateJWT(UsuarioModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracion["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.nombre_usuario),
                new Claim(JwtRegisteredClaimNames.Name, user.persona?.nombre ?? ""),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.persona?.apellido ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, user.persona?.email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuracion["Jwt:Issuer"],
                audience: _configuracion["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddSeconds(320),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ValidUser(LoginModel login)
        {
            var usuario = UsuarioService.obtenerUser(login.UserName);
            return usuario != null && usuario.contrasena == login.Password;
        }

        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
