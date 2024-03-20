using MediatR;
using Projeto.Core.Contexts.UsuarioContext.Enum;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Criar
{
    public record CriarUsuarioRequest(
            string PrimeiroNome,
            string UltimoSobrenome,
            string Email,
            string Senha,
            CredencialEnum Credencial,
            CredencialEnum[] Credenciais
        ) : IRequest<CriarUsuarioResponse>;
}
