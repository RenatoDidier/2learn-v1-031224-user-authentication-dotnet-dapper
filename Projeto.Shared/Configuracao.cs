using static Projeto.Shared.Configuracao.EmailSendgridConfiguracao;

namespace Projeto.Shared
{
    public class Configuracao
    {
        public static SendgridApiConfiguracao SendgridApi { get; set; } = new();
        public static EmailSendgridConfiguracao SendgridDestinatario { get; set; } = new();
        public static ConfiguracaoBancoDados BancoDados { get; set; } = new();
        public static ConfiguracaoSenhaSegredos SenhaSegredos { get; set; } = new();
        public class ConfiguracaoSenhaSegredos
        {
            public string ChaveApi { get; set; } = string.Empty;
            public string JwtChavePrivada { get; set; } = string.Empty;
            public string ChaveSenhaSalt { get; set; } = string.Empty;
        }
        public class EmailSendgridConfiguracao
        {
            public string EmailDestinatarioPadrao { get; set; } = string.Empty;
            public string NomeDestinatarioPadrao { get; set; } = string.Empty;
        }
        public class SendgridApiConfiguracao
        {
            public string ChaveApi { get; set; } = string.Empty;
        }
        public class ConfiguracaoBancoDados
        {
            public string StringConexao { get; set; } = string.Empty;
        }
    }
}
