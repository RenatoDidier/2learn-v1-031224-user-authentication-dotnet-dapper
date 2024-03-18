using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Api.Extensions;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRequestHandler<
        CriarUsuarioRequest,
        CriarUsuarioResponse> _handlerCriarUsuario;    
    private readonly IRequestHandler<
        ValidarUsuarioRequest,
        ValidarUsuarioResponse> _handlerValidarConta;
    private readonly IRequestHandler<
        AutenticarUsuarioRequest,
        AutenticarUsuarioResponse> _handlerAutenticarConta;

    public UserController(
            IRequestHandler<CriarUsuarioRequest,
                CriarUsuarioResponse> handlerCriarUsuario,
            IRequestHandler<ValidarUsuarioRequest, 
                ValidarUsuarioResponse> handlerValidarConta,
            IRequestHandler<AutenticarUsuarioRequest,
                AutenticarUsuarioResponse> handlerAutenticarConta
        )
    {
        _handlerCriarUsuario = handlerCriarUsuario;
        _handlerValidarConta = handlerValidarConta;
        _handlerAutenticarConta = handlerAutenticarConta;
    }

    [HttpGet("/")]
    public string Get()
    {
        return "Api rodando";
    }

    [HttpPost("/v1/usuario/criar")]
    public async Task<CriarUsuarioResponse> CriarUsuario(
            [FromBody] CriarUsuarioRequest request
        )
    {
        var resultado = await _handlerCriarUsuario.Handle(request, new CancellationToken());

        return resultado;
    }

    [HttpPost("/v1/usuario/validar")]
    public async Task<ValidarUsuarioResponse> ValidarUsuario(
            [FromBody] ValidarUsuarioRequest request
        )
    {
        var resultado = await _handlerValidarConta.Handle(request, new CancellationToken());

        return resultado;
    }

    [HttpPost("/v1/usuario/autenticar")]
    public async Task<AutenticarUsuarioResponse> AutenticarUsuario(
            [FromBody] AutenticarUsuarioRequest request
        )
    {
        var resultado = await _handlerAutenticarConta.Handle(request, new CancellationToken());

        if (resultado.HaErro || resultado.Dados is null)
            return resultado;

        resultado.Dados.Token = JwtTokenExtension.GerarTokenJwt(resultado.Dados);

        return resultado;
    }

    [Authorize]
    [HttpGet("/v1/blog/teste")]
    public string TelaBlogPrincipal()
    {
        return "Entrou";
    }
}
