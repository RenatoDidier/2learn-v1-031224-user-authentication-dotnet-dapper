using Projeto.Core.Contexts.UsuarioContext.Models;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;
using SendGrid;
using Projeto.Core;
using SendGrid.Helpers.Mail;

namespace Projeto.Repository.Contexts.UsuarioContext.UseCases.ValidarConta
{
    public class Service : IService
    {
        public async Task<bool> EnviarCodigoVerificacaoEmailAsync(Usuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                var apiSendgrid = new SendGridClient(Configuracao.SendgridApi.ChaveApi);
                var remetente = new EmailAddress(Configuracao.SendgridDestinatario.EmailDestinatarioPadrao, Configuracao.SendgridDestinatario.NomeDestinatarioPadrao);
                const string titulo = "Código de verificação";

                var destinatario = new EmailAddress(usuario.Email.ToString(), usuario.Nome.ToString());
                var conteudoEmail = $"Código de verificação: {usuario.Email.Validacao.Codigo}";

                var criacaoEmailConfiguracao = MailHelper.CreateSingleEmail(remetente, destinatario, titulo, conteudoEmail, conteudoEmail);

                var resposta = await apiSendgrid.SendEmailAsync(criacaoEmailConfiguracao);

                Console.WriteLine(resposta);

                return resposta.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
