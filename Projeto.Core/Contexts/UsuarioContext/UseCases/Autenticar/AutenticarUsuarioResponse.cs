using Flunt.Notifications;
using Projeto.Core.Contexts.CompartilhadoContext.UseCases;
using Projeto.Core.Contexts.UsuarioContext.Enum;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar
{
    public class AutenticarUsuarioResponse : ResponsePadrao
    {
        public AutenticarUsuarioResponse(string mensagem, int status, IEnumerable<Notification>? notificacoes)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = notificacoes;
            Dados = null;
        }

        public AutenticarUsuarioResponse(string mensagem, int status)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = null;
            Dados = null;
        }
        public AutenticarUsuarioResponse(string mensagem, DadosUsuarioResponse dados)
        {
            Mensagem = mensagem;
            Notificacoes = null;
            Status = 201;
            Dados = dados;
        }
        public DadosUsuarioResponse? Dados { get; set; }
    }
    public class DadosUsuarioResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Credencial { get; set; } = string.Empty;
    }
}
