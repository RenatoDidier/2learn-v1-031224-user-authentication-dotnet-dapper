namespace Projeto.Shared
{
    public class Configuracao
    {
        public static ConfiguracaoSecrets Segredos { get; set; } = new();
        public class ConfiguracaoSecrets
        {
            public string ChaveApi { get; set; } = string.Empty;
            public string JwtChavePrivada { get; set; } = string.Empty;
            public string ChaveSenha { get; set; } = string.Empty;
        }
    }
}
