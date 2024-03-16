using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public class Validador
    {
        public static Contract<Notification> GarantirRequisicao(ValidarUsuarioRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido")
                .IsTrue(requisicao.CodigoVerificacao.Length == 6, "Código validação", "Código Inválido");
    }
}
