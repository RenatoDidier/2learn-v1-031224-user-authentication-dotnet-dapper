using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;

namespace Projeto.Tests.Contexts.UsuarioContext.Repository.Validar
{
    public class FakeValidarRepository : IRepository
    {
        public Task<string?> GerarNovoCodigoValidacao(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult("123456");
        }

        public Task<Usuario?> ObterUsuarioPorEmailAsync(string emailRequisicao, CancellationToken cancellationToken)
        {
            if (emailRequisicao.Contains("emailvalido@gmail.com"))
            {
                Senha senha = new("senha123");
                Nome nome = new("Renato", "Didier");
                Email emailUsuario = new(emailRequisicao);

                Usuario usuario = new()
                {
                    Id = CriadorStringAleatorio.GerarOitoCracteres(),
                    Email = emailUsuario,
                    Senha = senha,
                    Nome = nome
                };

                return Task.FromResult(usuario);

            }
            else if (emailRequisicao.Contains("emailvalidoexpirado@gmail.com"))
            {
                Senha senha = new("senha123");
                Nome nome = new("Renato", "Didier");
                Email emailUsuario = new(emailRequisicao);

                Usuario usuario = new()
                {
                    Id = CriadorStringAleatorio.GerarOitoCracteres(),
                    Email = emailUsuario,
                    Senha = senha,
                    Nome = nome
                };

                var horarioAtual = DateTime.Now;

                usuario.Email.Validacao.LimiteValidacao = horarioAtual.AddHours(-5);

                return Task.FromResult(usuario);
            }
            else if (emailRequisicao.Contains("emailvalidovalidado@gmail.com"))
            {
                Senha senha = new("senha123");
                Nome nome = new("Renato", "Didier");
                Email emailUsuario = new(emailRequisicao);

                Usuario usuario = new()
                {
                    Id = CriadorStringAleatorio.GerarOitoCracteres(),
                    Email = emailUsuario,
                    Senha = senha,
                    Nome = nome
                };

                usuario.Email.Validacao.ValidacaoRealizada = DateTime.UtcNow;
                usuario.Email.Validacao.Codigo = "000000";


                return Task.FromResult(usuario);
            }
            else
            {
                return Task.FromResult<Usuario?>(null);
            }
        }

        public Task<bool> ValidarContaBanco(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
