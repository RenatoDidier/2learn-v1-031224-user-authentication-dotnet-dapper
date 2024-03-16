using Flunt.Notifications;
using Projeto.Core.Contexts.CompartilhadoContext.UseCases;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public class ValidarUsuarioResponse : ResponsePadrao
    {
        public ValidarUsuarioResponse(string mensagem, int status, IEnumerable<Notification> notificacoes)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = notificacoes;
        }
        public ValidarUsuarioResponse(string mensagem, int status)
        {
            Mensagem = mensagem;
            Status = status;
            Notificacoes = null;
        }

        public ValidarUsuarioResponse(string mensagem)
        {
            Mensagem = mensagem;
            Status = 201;
            Notificacoes = null;
        }
    }
}
