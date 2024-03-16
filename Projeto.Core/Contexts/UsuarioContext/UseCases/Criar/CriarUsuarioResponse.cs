using Flunt.Notifications;
using Projeto.Core.Contexts.CompartilhadoContext.UseCases;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Criar
{
    public class CriarUsuarioResponse : ResponsePadrao
    {
        public CriarUsuarioResponse(string mensagem, int status, IEnumerable<Notification>? notificacoes = null)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = notificacoes;
        }

        public CriarUsuarioResponse(string mensagem, RespostaUsuario dados)
        {
            Mensagem = mensagem;
            Status = 201;
            DadosRetorno = dados;
            Notificacoes = null;
        }
        public RespostaUsuario? DadosRetorno { get; set; }


    }
    public record RespostaUsuario(string Id, string Nome, string Email);
}
