using Projeto.Core;

namespace Projeto.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AdicionarBanco(this WebApplicationBuilder builder)
        {
            Configuracao.BancoDados.StringConexao = 
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public static void AdicionarConfiguracaoSegredos(this WebApplicationBuilder builder)
        {
            Configuracao.SenhaSegredos.ChaveApi =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveApi") ?? string.Empty;
            Configuracao.SenhaSegredos.JwtChavePrivada =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveJwtPrivado") ?? string.Empty;
            Configuracao.SenhaSegredos.ChaveSenhaSalt =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveSenhaSalt") ?? string.Empty;
        }

        public static void AdicionarConfiguracaoSendgrid(this WebApplicationBuilder builder)
        {
            Configuracao.SendgridApi.ChaveApi =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("ChaveApi") ?? string.Empty;            
            Configuracao.SendgridDestinatario.EmailDestinatarioPadrao =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("EmailDestinatario") ?? string.Empty;            
            Configuracao.SendgridDestinatario.NomeDestinatarioPadrao =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("NomeDestinatario") ?? string.Empty;
        }
    }
}
