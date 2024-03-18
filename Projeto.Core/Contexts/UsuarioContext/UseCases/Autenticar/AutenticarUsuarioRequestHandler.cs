using MediatR;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar
{
    public class AutenticarUsuarioRequestHandler : IRequestHandler<AutenticarUsuarioRequest, AutenticarUsuarioResponse>
    {
        private readonly IRepository _repository;
        public AutenticarUsuarioRequestHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioRequest request, CancellationToken cancellationToken)
        {
            #region 01. Validar Requisição
            var failFastValidation = Validador.GarantirRequisicao(request);
            if (!failFastValidation.IsValid)
                return new AutenticarUsuarioResponse("Requisição inválida", 401, failFastValidation.Notifications);
            #endregion

            #region 02. Obter perfil do banco
            Usuario? usuario;
            try
            {
                usuario = await _repository.ObterUsuarioCompletoPorEmailAsync(request.Email, new CancellationToken());
                if (usuario == null)
                    return new AutenticarUsuarioResponse("Usuário inválido", 401);
            } catch
            {
                return new AutenticarUsuarioResponse("Problema ao consultar o banco", 500);
            }
            #endregion

            #region 03. Validar se a senha está válida
            if (!usuario.Senha.VerficacaoHash(request.Senha))
                return new AutenticarUsuarioResponse("Usuário inválido", 401);
            #endregion

            #region 04. Validar se a conta está ativa
            if (!usuario.Email.Validacao.CodigoValidado)
                return new AutenticarUsuarioResponse("Conta inativada. Por favor, ative a sua conta antes de prosseguir", 401);
            #endregion

            #region 05. Retornar os dados para o usuário
            DadosUsuarioResponse dadosResposta = new DadosUsuarioResponse();
            dadosResposta.Id = usuario.Id;
            dadosResposta.Email = usuario.Email;
            dadosResposta.Nome = usuario.Nome.ToString();
            dadosResposta.Credencial = usuario.Credencial.ToString();

            return new AutenticarUsuarioResponse("Usuário autenticado com sucesso", dadosResposta);
            #endregion

        }
    }
}
