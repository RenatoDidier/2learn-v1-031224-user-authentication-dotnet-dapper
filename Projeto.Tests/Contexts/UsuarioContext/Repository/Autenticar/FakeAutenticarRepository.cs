using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;

namespace Projeto.Tests.Contexts.UsuarioContext.Repository.Autenticar
{
    public class FakeAutenticarRepository : IRepository
    {
        public Task<Usuario?> ObterUsuarioCompletoPorEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (email.Contains("emailvalido@gmail.com"))
            {
                Senha senha = new("senha123");
                Nome nome = new("Renato", "Didier");
                Email emailUsuario = new(email);

                Usuario usuario = new()
                {
                    Id = CriadorStringAleatorio.GerarOitoCracteres(),
                    Email = emailUsuario,
                    Senha = senha,
                    Nome = nome
                };

                return Task.FromResult(usuario);

            }
            else if (email.Contains("emailvalidovalidado@gmail.com"))
            {
                Senha senha = new("senha123");
                Nome nome = new("Renato", "Didier");
                Email emailUsuario = new(email);

                Usuario usuario = new()
                {
                    Id = CriadorStringAleatorio.GerarOitoCracteres(),
                    Email = emailUsuario,
                    Senha = senha,
                    Nome = nome
                };

                usuario.Email.Validacao.ValidacaoRealizada = DateTime.Now;

                return Task.FromResult(usuario);
            }
            else
            {
                return Task.FromResult<Usuario?>(null);
            }
        }
    }
}
