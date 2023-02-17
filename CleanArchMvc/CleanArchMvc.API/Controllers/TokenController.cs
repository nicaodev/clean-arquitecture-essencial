using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            //Para teste
            //usuario@localhost
            //Numsey#2023"

            var result = await _authenticate.Authenticate(loginModel.Email, loginModel.Password);

            if (result)
            {
                return GenerateToken(loginModel);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)] // Não mostra mais o endpoint(swagger etc)
        public async Task<ActionResult> CreateUser([FromBody] LoginModel loginModel)
        {
            var resulto = await _authenticate.RegisterUser(loginModel.Email, loginModel.Password);

            if (resulto)
            {
                return Ok($"User {loginModel.Email} was created!!!");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return BadRequest(ModelState);
            }
        }
        private UserToken GenerateToken(LoginModel userInfo)
        {
            //Declaracoes do user

            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuValor", "qualquer-coisa-que-quiser-por-ex"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Gerando chave privada para assinar Token

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //Gerando a assinatura Digital

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            // Definindo o tempo de expiração do token

            var exp = DateTime.UtcNow.AddMinutes(10);

            //Gerando token

            JwtSecurityToken token = new JwtSecurityToken(
                // Emissor
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: exp,
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = exp
            };
        }


    }
}
