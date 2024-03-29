using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;

namespace Projeto.Tests.Contexts.UsuarioContext.Repository.Criar
{
    public class FakeService : IService
    {
        public Task<bool> EnviarCodigoVerificacaoEmailAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
