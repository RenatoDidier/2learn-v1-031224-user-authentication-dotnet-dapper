using MediatR;
using Microsoft.AspNetCore.Mvc;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos;
//using Projeto.Core.Contexts.UsuarioContext;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IRequestHandler<
        Core.Contexts.UsuarioContext.Request, 
        Core.Contexts.UsuarioContext.Response> _handlerUsuario;

    public UserController(
            IRepository repository,
            IRequestHandler<Core.Contexts.UsuarioContext.Request, Core.Contexts.UsuarioContext.Response> handlerUsuario
        )
    {
        _repository = repository;
        _handlerUsuario = handlerUsuario;
    }

    [HttpGet("/")]
    public string Get()
    {
        return "Api rodando";
    }

    [HttpPost("/v1/usuario/criar")]
    public async Task<Core.Contexts.UsuarioContext.Response> CriarUsuario(
            [FromBody] Core.Contexts.UsuarioContext.Request request
        )
    {
        var resultado = await _handlerUsuario.Handle(request, new CancellationToken());

        return resultado;
    }
}
