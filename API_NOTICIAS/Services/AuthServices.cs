using API_NOTICIAS.Constans;
using API_NOTICIAS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_NOTICIAS.Services
{
    public class AuthServices
    {
        /// <summary>
        /// Classe para a criação do JWT.
        /// </summary>
        
        //Método que gera um JWT para a Role de User
        public static object GenerateTokenUser(Usuario usuario)
        {
            List<Claim> lstClaims = new List<Claim>();

            lstClaims.Add(new Claim(ClaimTypes.Role, Claims.User));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(Constans.Key.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(lstClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Método que ger aum JWT para a Role de Admin
        public static object GenerateTokenAdmin(Usuario usuario)
        {
            List<Claim> lstClaims = new List<Claim>();

            lstClaims.Add(new Claim(ClaimTypes.Role, Claims.Admin));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(Constans.Key.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(lstClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

