using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NyousTarde.Contexts;
using NyousTarde.Domains;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace NyousTarde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Chamamos nosso contexto do banco
        NyousContext _context = new NyousContext();

        //Capturar as infos do token do appsetting.json
        // Definimos uma variável para percorrer nossos métodos com as configurações obtidas no appsettings.json
        private IConfiguration _config;

        // Definimos um método construtor para poder passar essas configs
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        private Usuario AuthenticateUser(Usuario login)
        {
            return _context.Usuarios
                  .Include(a => a.IdAcessoNavigation)
                  .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);
        }

        // Criamos nosso método que vai gerar nosso Token
        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
            // a qualquer momento enquanto o Token for ativo
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //Pegar info de acesso por escrito , ex: Padrao ou Administrador 
                new Claim(ClaimTypes.Role, userInfo.IdAcessoNavigation.Tipo)
            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Usamos a anotação "AllowAnonymous" para 
        // ignorar a autenticação neste método, já que é ele quem fará isso
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Usuario login)
        {
            // Definimos logo de cara como não autorizado
            IActionResult response = Unauthorized();

            // Autenticamos o usuário da API
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
