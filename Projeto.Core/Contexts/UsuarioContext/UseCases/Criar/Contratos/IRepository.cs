using Projeto.Core.Contexts.UsuarioContext.Models;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos
{
    public interface IRepository
    {
        Task<bool> ExisteUsuarioAsync(string email, CancellationToken cancellationToken);
        Task<bool> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
    }
}
