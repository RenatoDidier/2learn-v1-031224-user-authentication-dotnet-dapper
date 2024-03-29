using MediatR;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar
{
    public record AutenticarUsuarioRequest(string Email, string Senha) : IRequest<AutenticarUsuarioResponse>;
}
