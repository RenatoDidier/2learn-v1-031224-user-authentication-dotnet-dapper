using Flunt.Notifications;
using Projeto.Core.Contexts.CompartilhadoContext.UseCases;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar
{
    public class Response : ResponsePadrao
    {
        public Response(string mensagem, int status, IEnumerable<Notification>? notificacoes)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = notificacoes;
        }
        public Response(string mensagem, DadosUsuarioResponse dados)
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
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Credencial { get; set; } = string.Empty;
    }
}
