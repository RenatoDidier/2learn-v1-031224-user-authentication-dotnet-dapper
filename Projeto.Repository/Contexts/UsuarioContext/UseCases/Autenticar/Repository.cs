using Dapper;
using Projeto.Core.Contexts.UsuarioContext.Enum;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;
using System.Data.SqlClient;

namespace Projeto.Repository.Contexts.UsuarioContext.UseCases.Autenticar
{
    public class Repository : IRepository
    {
        private readonly SqlConnection _connection;
        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<Usuario?> ObterUsuarioCompletoPorEmailAsync(string email, CancellationToken cancellationToken)
        {

            var parametros = new
            {
                email
            };

            var sqlQuery = @"
                SELECT 
                    u.[Id],
                    c.[Titulo] AS Credencial,
                    u.[SenhaHash] AS HashSenha,
                    u.[PrimeiroNome],
                    u.[UltimoSobrenome],
                    u.[Email] AS Endereco,
                    u.[ValidacaoRealizada]
                FROM
                    [usuario] u
                INNER JOIN
                    [usuario_credenciais] uc ON u.[Id] = uc.[UsuarioId]
                INNER JOIN
                    [credenciais] c ON uc.[credenciaisId] = c.[Id]
                WHERE
                    u.[Email] = @Email
            ";

            try
            {
                var resultado = await _connection.QueryAsync<Usuario, Senha, Nome, Email, Validacao, Usuario?>(
                        sqlQuery,
                        (usuario, senha, nome, email, validacao) =>
                        {
                            if (usuario != null)
                            {
                                usuario.Senha = senha;
                                usuario.Nome = nome;
                                usuario.Email = email;
                                usuario.Email.Validacao = validacao;
                            }
                            return usuario;
                        },
                        parametros,
                        splitOn: "HashSenha, PrimeiroNome, Endereco, ValidacaoRealizada"

                    );

                return resultado.FirstOrDefault();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }
    }
}
