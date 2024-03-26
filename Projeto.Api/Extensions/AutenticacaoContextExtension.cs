using System.Runtime.CompilerServices;

namespace Projeto.Api.Extensions
{
    public static class AutenticacaoContextExtension
    {
        public static void AdicionarAutenticacaoContext(this IServiceCollection services)
        {
            services.AddTransient<
                    Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos.IRepository,
                    Projeto.Repository.Contexts.UsuarioContext.UseCases.ValidarConta.Repository
                >();

            services.AddTransient<
                    Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos.IRepository,
                    Projeto.Repository.Contexts.UsuarioContext.UseCases.Autenticar.Repository
                >();

            services.AddTransient<
                    Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos.IService,
                    Projeto.Repository.Contexts.UsuarioContext.UseCases.ValidarConta.Service
                >();
        }
    }
}
