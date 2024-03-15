using Flunt.Notifications;

namespace Projeto.Core.Contexts.CompartilhadoContext.UseCases
{
    public abstract class ResponsePadrao
    {
        public string? Mensagem { get; set; }
        public int Status { get; set; }
        public bool HaErro => Status is < 200 or >= 300;
        public IEnumerable<Notification>? Notificacoes { get; set; }
    }
}
