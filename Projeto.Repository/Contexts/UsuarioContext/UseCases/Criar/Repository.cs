using Dapper;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos;
using System.Data;
using System.Data.SqlClient;

namespace Projeto.Repository.Contexts.UsuarioContext.UseCases.Criar
{
    public class Repository : IRepository
    {
        private readonly string PRC_BUSCAR_USUARIO = "PRC_BUSCAR_USUARIO";
        private readonly string PRC_INSERIR_USUARIO = "PRC_INSERIR_USUARIO";

        private readonly SqlConnection _connection;
        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> ExisteUsuarioAsync(string email, CancellationToken cancellationToken)
        {
            var parametros = new
            {
                Email = email
            };

            try
            {
                var resultado = await _connection.QueryFirstOrDefaultAsync<Usuario>(
                        PRC_BUSCAR_USUARIO,
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                return resultado != null;
            } catch ( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<bool> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                var parametros = new
                {
                    usuario.Id,
                    Email = usuario.Email.Endereco,
                    SenhaHash = usuario.Senha.HashSenha,
                    usuario.Nome.PrimeiroNome,
                    usuario.Nome.UltimoSobrenome,
                    CodigoValidacao = usuario.Email.Validacao.Codigo,
                    usuario.Email.Validacao.LimiteValidacao,
                    usuario.Email.Validacao.ValidacaoRealizada,
                    CredenciaisId = usuario.Credencial != null ? (int) usuario.Credencial : 0
                };

                var resultado = await _connection.ExecuteAsync(
                        PRC_INSERIR_USUARIO,
                        parametros,
                        commandType: CommandType.StoredProcedure
                    );

                return resultado > 0;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

    }
}
