using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Curso.ComercioElectronico.HttpApi
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase {

        private readonly JwtConfiguration jwtConfiguration;
        private readonly IUsuarioAppService usuarioAppService;
        private readonly UserManager userManager;

        public TokenController(
            IUsuarioAppService usuarioAppService,
            UserManager userManager,
            IOptions<JwtConfiguration> options) {
            this.jwtConfiguration = options.Value;
            this.usuarioAppService = usuarioAppService;
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {

            var user = await usuarioAppService.GetByUserAsync(input.UserName);

            //1. Validar User.
            if (user == null)
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }

            if (!(await userManager.CheckPasswordAsync(input.UserName, input.Password)))
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }


            //2. Generar claims
            //create claims details based on the user information
            var claims = new List<Claim>();

         
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
            claims.Add(new Claim("UserName", input.UserName));
           
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);
 

           var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }

        
    }

    public class UserInput
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
