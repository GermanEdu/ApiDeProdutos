﻿using ApiDeProdutos.Services;
using ApiDeProdutos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;
        public UsuarioController(IConfiguration configuration, IAuthenticate authentication)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<UserToken>> CriarUsuario([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "As senhas não conferem");
                return BadRequest(ModelState);
            }
            var result = await _authentication.RegisterUser(model.Email, model.Password);
            if (result)
            {
                return Ok($"Usuário {model.Email} criado com sucesso");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido.");
                return BadRequest(ModelState);
            }
        }
        [HttpPost("LogarUsuario")]
        public async Task<ActionResult<UserToken>> LogarUsuario([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido.");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuToken", "token de teste"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}