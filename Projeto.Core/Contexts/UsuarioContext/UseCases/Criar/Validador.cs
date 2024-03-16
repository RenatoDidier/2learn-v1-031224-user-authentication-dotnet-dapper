using Flunt.Notifications;
using Flunt.Validations;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Criar
{
    public class Validador
    {
        public static Contract<Notification> GarantirRequisicao(CriarUsuarioRequest requisicao)
            => new Contract<Notification>()
                .Requires()
                .IsLowerOrEqualsThan(requisicao.PrimeiroNome, 100, "PrimeiroNome", "O nome precisa ter menos que 100 caracteres")
                .IsGreaterOrEqualsThan(requisicao.PrimeiroNome, 6, "PrimeiroNome", "O nome precisa ter, pelo menos 6 caracteres")
                .IsLowerOrEqualsThan(requisicao.UltimoSobrenome, 100, "UltimoNome", "O sobrenome precisa ter menos que 100 caracteres")
                .IsGreaterOrEqualsThan(requisicao.UltimoSobrenome, 6, "UltimoNome", "O sobrenome precisa ter, pelo menos 6 caracteres")
                .IsLowerOrEqualsThan(requisicao.Senha, 25, "Senha", "A senha pode ter, no máximo, 25 caracteres")
                .IsGreaterOrEqualsThan(requisicao.Senha, 8, "Senha", "A senha precisa ter, no mínimo, 8 caracteres")
                .IsEmail(requisicao.Email, "E-mail", "E-mail inválido");
    }
}
