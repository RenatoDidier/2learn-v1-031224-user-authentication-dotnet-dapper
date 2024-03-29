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
                    u.[SenhaHash] AS HashSenha,
                    u.[PrimeiroNome],
                    u.[UltimoSobrenome],
                    u.[Email] AS Endereco,
                    u.[CodigoValidacao] AS Codigo,
                    u.[ValidacaoRealizada],
                    u.[LimiteValidacao],
                    c.[Titulo] AS Titulo
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
                var resultado = await _connection.QueryAsync<Usuario, Senha, Nome, Email, Validacao, Credencial, Usuario?>(
                        sqlQuery,
                        (usuario, senha, nome, email, validacao, credencial) =>
                        {
                            if (usuario != null)
                            {
                                usuario.Senha = senha;
                                usuario.Nome = nome;
                                usuario.Email = email;
                                usuario.Email.Validacao = validacao;
                                usuario.Credenciais = new List<Credencial>();

                                usuario.Credenciais.Add(credencial);
                            }
                            return usuario;
                        },
                        parametros,
                        splitOn: "HashSenha, PrimeiroNome, Endereco, Codigo, Titulo"

                    );
                if (resultado != null)
                {
                    Usuario resultadoFinal = new();

                    for (var i = 0; i < resultado.Count(); i ++)
                    {
                        if (i == 0)
                        {
                            resultadoFinal.Id = resultado.FirstOrDefault().Id;
                            resultadoFinal.Senha = resultado.FirstOrDefault().Senha;
                            resultadoFinal.Nome = resultado.FirstOrDefault().Nome;
                            resultadoFinal.Email = resultado.FirstOrDefault().Email;
                            resultadoFinal.Credenciais.AddRange(resultado.FirstOrDefault().Credenciais);
                        } else
                        {
                            resultadoFinal.Credenciais.AddRange(resultado.Skip(i).FirstOrDefault().Credenciais);
                        }
                    }
                    return resultadoFinal;
                }
                return resultado.FirstOrDefault();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }
    }
}
