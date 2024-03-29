using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos;

namespace Projeto.Tests.Contexts.UsuarioContext.Repository.Criar
{
    public class FakeCriarRepository : IRepository
    {
        public Task<bool> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {

            return Task.FromResult(true);
        }

        public Task<bool> ExisteUsuarioAsync(string email, CancellationToken cancellationToken)
        {
            if (email.Equals("rendfv@gmail.com"))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
