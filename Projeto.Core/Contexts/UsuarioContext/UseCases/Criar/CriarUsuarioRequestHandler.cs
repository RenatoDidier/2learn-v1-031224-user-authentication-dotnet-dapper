using MediatR;
using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.UseCases.Criar
{
    public class CriarUsuarioRequestHandler : IRequestHandler<CriarUsuarioRequest, CriarUsuarioResponse>
    {
        private readonly IRepository _repository;

        public CriarUsuarioRequestHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<CriarUsuarioResponse> Handle(CriarUsuarioRequest request, CancellationToken cancellationToken)
        {
            #region 01. Validar a requisição

            var fastValidation = Validador.GarantirRequisicao(request);

            if (!fastValidation.IsValid)
                return new CriarUsuarioResponse("Requisição inválida", 400, fastValidation.Notifications);
            #endregion

            #region 02. Criação dos Value Objects
            Email email = new(request.Email);
            Nome nome = new(request.PrimeiroNome, request.UltimoSobrenome);
            Senha senha = new(request.Senha);

            Usuario usuario = new()
            {
                Id = CriadorStringAleatorio.GerarOitoCracteres(),
                Email = email,
                Senha = senha,
                Nome = nome,
                Credencial = request.Credencial
            };

            #endregion

            #region 03. Validar se há esse e-mail na base
            try
            {
                var usuarioExiste = await _repository.ExisteUsuarioAsync(usuario.Email.ToString(), new CancellationToken());

                if (usuarioExiste)
                    return new CriarUsuarioResponse("E-mail existente", 400);

            }
            catch
            {
                return new CriarUsuarioResponse("Problema para acessar o banco", 500);
            }
            #endregion

            #region 04. Salvar usuário na base
            try
            {
                var cadastroUsuario = await _repository.CriarUsuarioAsync(usuario, new CancellationToken());

                if (!cadastroUsuario)
                    return new CriarUsuarioResponse("Problema para cadastrar usuário", 400);

                return new CriarUsuarioResponse("Conta criada com sucesso",
                        new RespostaUsuario(
                            usuario.Id,
                            nome.ToString(),
                            usuario.Email.ToString())
                    ); ;

            }
            catch
            {
                return new CriarUsuarioResponse("Problema Para acessar o banco", 500);
            }
            #endregion
        }
    }
}
