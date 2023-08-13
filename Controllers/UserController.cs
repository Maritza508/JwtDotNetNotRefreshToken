using DemoJWTNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoJWTNetCore.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object objRequest)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(objRequest.ToString());
            string user = data.nombre.ToString();
            string password = data.password.ToString();

            Usuario usuario = Usuario.DB().Where(p => p.nombre == user && p.password == password).FirstOrDefault();

            if (usuario == null)
            {
                return new
                {
                    succes = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.idUsuario.ToString()),
                new Claim("usuario", usuario.nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: singIn
                );

            return new
            {
                succes = true,
                message = "exitoo!!",
                resultToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
