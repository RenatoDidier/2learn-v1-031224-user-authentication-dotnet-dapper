namespace Projeto.Core.Contexts.UsuarioContext.ValueObjects.Exceptions
{
    public class InvalidSenhaException : Exception
    {
        private const string MensagemPadrao = "Senha inválida";

        public InvalidSenhaException(string mensagem = MensagemPadrao)
            : base(mensagem)
        {
        }

        public static void EnviarSeInvalido(
                string? item,
                string mensagem = MensagemPadrao
            )
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new InvalidSenhaException(mensagem);
        }
    }
}
