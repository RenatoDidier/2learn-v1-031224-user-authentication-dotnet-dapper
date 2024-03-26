using MediatR;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public class ValidarUsuarioRequestHandler : IRequestHandler<ValidarUsuarioRequest, ValidarUsuarioResponse>
    {
        private readonly IRepository _repository;
        private readonly IService _service;
        public ValidarUsuarioRequestHandler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<ValidarUsuarioResponse> Handle(ValidarUsuarioRequest request, CancellationToken cancellationToken)
        {
            #region 01. Fail fast validation
            var failFastValidation = Validador.GarantirRequisicao(request);

            if (!failFastValidation.IsValid)
                return new ValidarUsuarioResponse("Requisição inválida", 401, failFastValidation.Notifications);

            #endregion

            Usuario? usuario;
            #region 02. Obter usuário do banco
            try
            {
                usuario = await _repository.ObterUsuarioPorEmailAsync(request.Email, new CancellationToken());

                if (usuario == null)
                    return new ValidarUsuarioResponse("E-mail inválido", 401);
            } catch
            {
                return new ValidarUsuarioResponse("Problema ao consultar usuário no banco", 500);
            }
            #endregion

            #region 03. Verificar se conta já foi validada
            if (usuario.Email.Validacao.CodigoValidado)
                return new ValidarUsuarioResponse("Está conta já está validada", 401);
            #endregion

            #region 04. Verificar expiração de código
            if (usuario.Email.Validacao.LimiteValidacao < DateTime.Now)
            {
                var novoCodigo = await _repository.GerarNovoCodigoValidacao(request.Email, new CancellationToken());
                if (novoCodigo == null)
                    return new ValidarUsuarioResponse("Código expirado - Contate os responsáveis", 401);

                usuario.Email.Validacao.Codigo = novoCodigo;

                var envioEmail = await _service.EnviarCodigoVerificacaoEmailAsync(usuario, new CancellationToken());
                if (envioEmail)
                {
                    return new ValidarUsuarioResponse("Código expirado - Foi enviado um novo código para o seu e-mail", 401);
                } else
                {
                    return new ValidarUsuarioResponse("Código expirado - Contate os responsáveis", 401);
                }
            }
            #endregion

            #region 05. Verificar código
            if (string.Compare(usuario.Email.Validacao.Codigo, request.CodigoVerificacao, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                return new ValidarUsuarioResponse("Código inválido", 401);
            }

            #endregion

            #region 06. Validar conta
            var resultado = await _repository.ValidarContaBanco(request.Email, new CancellationToken());
            if (!resultado)
                return new ValidarUsuarioResponse("Não foi possível validar a sua conta", 401);

            return new ValidarUsuarioResponse("Conta validada com sucesso");
            #endregion
        }
    }
}
