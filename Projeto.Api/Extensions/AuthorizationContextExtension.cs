namespace Projeto.Api.Extensions
{
    public static class AuthorizationContextExtension
    {
        public static void AdicionarAutorizacao(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(
                option =>
                {
                    option.AddPolicy("Convidado", p => p.RequireRole("Convidado", "Usuario", "Administrador"));
                    option.AddPolicy("Usuario", p => p.RequireRole("Usuario", "Administrador"));
                    option.AddPolicy("Administrador", p => p.RequireRole("Administrador"));
                }
            );
        }
    }
}
