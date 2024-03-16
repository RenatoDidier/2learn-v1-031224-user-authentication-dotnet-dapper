
namespace Projeto.Api.Extensions
{
    public static class UsuarioContextExtension
    {
        public static void AdicionarUsuarioContext(this IServiceCollection services)
        {
            services.AddTransient<
                    Projeto.Core.Contexts.UsuarioContext.UseCases.Criar.Contratos.IRepository,
                    Projeto.Repository.Contexts.UsuarioContext.UseCases.Criar.Repository
                >();

            services.AddTransient<
                    Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos.IRepository,
                    Projeto.Repository.Contexts.UsuarioContext.UseCases.ValidarConta.Repository
                >();
        }
    }
}
