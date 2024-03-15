using MediatR;
using Projeto.Core.Contexts.UsuarioContext.Enum;

namespace Projeto.Core.Contexts.UsuarioContext
{
    public record Request(
            string PrimeiroNome,
            string UltimoSobrenome,
            string Email,
            string Senha,
            CredencialEnum Credencial
        ) : IRequest<Response>;
}
