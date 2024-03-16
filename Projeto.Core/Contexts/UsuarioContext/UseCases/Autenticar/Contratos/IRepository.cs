using Projeto.Core.Contexts.UsuarioContext.Models;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos
{
    public interface IRepository
    {
        Task<Usuario> ObterUsuarioPorEmailAsync(string email, CancellationToken cancellationToken);
    }
}
