using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginAPIController : Controller
    {
        private readonly IConfiguration configuration;

        public LoginAPIController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("Login")]
        [HttpPost]
        public ClinicaModels.Token Index(ClinicaModels.Token tokenrequest)
        {
            ClinicaModels.Token tokenresult = new ClinicaModels.Token();
            if(tokenrequest.token == "qfwneklfqnwke")
            {
                string applicationName = "ClinicaAPI";
                tokenresult.expirationTime = DateTime.Now.AddMinutes(30);
                tokenresult.token = CustomTokenJWT(applicationName, tokenresult.expirationTime);
            }
            return tokenresult;
        }

        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)

        {

            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName),
                new Claim("Name", "nombrepesrsona")
            };

            var _Payload = new JwtPayload(

                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: token_expiration
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}
