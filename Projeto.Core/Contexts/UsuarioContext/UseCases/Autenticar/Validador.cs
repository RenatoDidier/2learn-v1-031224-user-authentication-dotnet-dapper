using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar
{
    public class Validador
    {
        public static Contract<Notification> GarantirRequisicao(AutenticarUsuarioRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido")
                .IsGreaterOrEqualsThan(requisicao.Senha, 8, "Senha", "Senha inválida")
                .IsLowerOrEqualsThan(requisicao.Senha, 20, "Senha", "Senha inválida");
    }
}
