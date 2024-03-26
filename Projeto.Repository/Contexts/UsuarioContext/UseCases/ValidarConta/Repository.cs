using Dapper;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;
using System.Data.SqlClient;

namespace Projeto.Repository.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public class Repository : IRepository
    {
        private readonly string PRC_VALIDAR_USUARIO_EMAIL = "PRC_VALIDAR_USUARIO_EMAIL";
        private readonly SqlConnection _connection;
        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<string?> GerarNovoCodigoValidacao(string email, CancellationToken cancellationToken)
        {
            var novaValidacao = new Validacao();

            var parametros = new
            {
                Email = email,
                CodigoValidacao = novaValidacao.Codigo,
                novaValidacao.LimiteValidacao
            };

            var sqlQuery = @"
                UPDATE
                    [usuario]
                SET
                    [CodigoValidacao] = @CodigoValidacao, 
                    [LimiteValidacao] = @LimiteValidacao
                Where
                    [Email] = @Email
            ";

            try
            {
                var resultado = await _connection.ExecuteAsync(
                    sqlQuery,
                    parametros
                    );

                return novaValidacao.Codigo;

            } catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string emailRequisicao, CancellationToken cancellationToken)
        {
            var parametros = new
            {
                Email = emailRequisicao
            };

            try
            {
                var sqlQuery = @"
	                SELECT 
		                [Id],
                        [PrimeiroNome],
                        [UltimoSobrenome],
                        [SenhaHash] AS HashSenha,
                        [Email] As Endereco,
                        [CodigoValidacao] As Codigo,
                        [LimiteValidacao],
                        [ValidacaoRealizada]
	                FROM 
		                [usuario]
	                WHERE
		                [email] = @email;
                ";

                var resultado = await _connection.QueryAsync<Usuario, Nome, Senha, Email, Validacao, Usuario?>(
                        sqlQuery,
                        (usuario, nome, senha, email, validacao) =>
                        {
                            if (usuario != null)
                            {
                                usuario.Nome = nome;
                                usuario.Senha = senha;
                                usuario.Email = email;
                                usuario.Email.Validacao = validacao;
                            }
                            return usuario;
                        },
                        parametros,
                        splitOn: "PrimeiroNome, HashSenha, Endereco, Codigo"
                    );

                return resultado.FirstOrDefault();
            } catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return new Usuario();
            }
        }

        public async Task<bool> ValidarContaBanco(string email, CancellationToken cancellationToken)
        {
            try
            {
                var parametros = new
                {
                    email,
                    ValidacaoRealizada = DateTime.Now
                };

                var resultado = await _connection.ExecuteAsync(
                        PRC_VALIDAR_USUARIO_EMAIL,
                        parametros,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                return resultado > 0;

            } catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
