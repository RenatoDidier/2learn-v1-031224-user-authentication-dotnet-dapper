using Projeto.Core.Contexts.UsuarioContext.Models;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos
{
    public interface IRepository
    {
        Task<Usuario?> ObterUsuarioPorEmailAsync(string emailRequisicao, CancellationToken cancellationToken);
        Task<bool> ValidarContaBanco(string email, CancellationToken cancellationToken);
        Task<string?> GerarNovoCodigoValidacao(string email, CancellationToken cancellationToken);
    }
}
