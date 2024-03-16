using MediatR;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public record ValidarUsuarioRequest (
            string Email,
            string CodigoVerificacao
        ) : IRequest<ValidarUsuarioResponse>;
}
