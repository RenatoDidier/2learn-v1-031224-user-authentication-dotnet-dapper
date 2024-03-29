using System.Security.Claims;

namespace Projeto.Api.Extensions
{
    public static class ClaimsTokenExtension
    {
        public static string Id(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? string.Empty;

        public static string Name(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;

        public static string Email(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;

        public static string Credencial(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
