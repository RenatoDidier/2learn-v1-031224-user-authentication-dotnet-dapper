using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRequestHandler<
        Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioRequest,
        Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioResponse> _handlerCriarUsuario;    
    private readonly IRequestHandler<
        Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioRequest,
        Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioResponse> _handlerValidarConta;

    public UserController(
            IRequestHandler<Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioRequest,
                Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioResponse> handlerCriarUsuario,
            IRequestHandler<Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioRequest, 
                Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioResponse> handlerValidarConta
        )
    {
        _handlerCriarUsuario = handlerCriarUsuario;
        _handlerValidarConta = handlerValidarConta;
    }

    [HttpGet("/")]
    public string Get()
    {
        return "Api rodando";
    }

    [HttpPost("/v1/usuario/criar")]
    public async Task<Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioResponse> CriarUsuario(
            [FromBody] Core.Contexts.UsuarioContext.UseCases.Criar.CriarUsuarioRequest request
        )
    {
        var resultado = await _handlerCriarUsuario.Handle(request, new CancellationToken());

        return resultado;
    }

    [HttpPost("/v1/usuario/validar")]
    public async Task<Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioResponse> ValidarUsuario(
            [FromBody] Core.Contexts.UsuarioContext.UseCases.ValidarConta.ValidarUsuarioRequest request
        )
    {
        var resultado = await _handlerValidarConta.Handle(request, new CancellationToken());

        return resultado;
    }
}
