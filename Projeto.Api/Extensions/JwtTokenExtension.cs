using Microsoft.IdentityModel.Tokens;
using Projeto.Core;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto.Api.Extensions
{
    public static class JwtTokenExtension
    {
        public static string GerarTokenJwt(DadosUsuarioResponse data)
        {
            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes(Configuracao.SenhaSegredos.JwtChavePrivada);

            SigningCredentials credentials = new(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GeradorChavesToken(data),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credentials
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }
        public static ClaimsIdentity GeradorChavesToken(DadosUsuarioResponse usuario)
        {
            ClaimsIdentity ci = new();

            ci.AddClaim(new Claim("Id", usuario.Id));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Name, usuario.Email));
            ci.AddClaim(new Claim(ClaimTypes.Role, usuario.Credencial));

            return ci;

        }

    }
}
