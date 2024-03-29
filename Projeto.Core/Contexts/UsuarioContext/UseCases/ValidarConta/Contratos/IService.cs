using Projeto.Core.Contexts.UsuarioContext.Models;
using System.Reflection.Metadata;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos
{
    public interface IService
    {
        Task<bool> EnviarCodigoVerificacaoEmailAsync(Usuario usuario, CancellationToken cancellationToken);
    }
}
