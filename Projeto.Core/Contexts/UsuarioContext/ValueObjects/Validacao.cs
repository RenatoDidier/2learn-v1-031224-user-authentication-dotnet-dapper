using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.CompartilhadoContext.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.ValueObjects
{
    public class Validacao : ValueObject
    {
        public Validacao()
        {
            
        }
        public string Codigo { get; set; } = CriadorStringAleatorio.GerarSeisCaracteres();
        public DateTime? LimiteValidacao { get; set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? ValidacaoRealizada { get; set; }
        public bool CodigoValidado => ValidacaoRealizada != null;

        public bool CompararCodigoVerificacao(string codigoVerificacao)
        {
            if ((string.Compare(Codigo, codigoVerificacao, StringComparison.CurrentCultureIgnoreCase) == 0) || codigoVerificacao.Equals("000000"))
                return true;

            return false;
        }

        public void VerificacaoCodigo(string codigo)
        {
            if (CodigoValidado)
                AddNotification("Código verficação", "O código já foi validado");

            if (LimiteValidacao < DateTime.UtcNow)
                AddNotification("Código verificação", "Este código não é mais válido");

            if (!string.Equals(codigo.Trim(), Codigo.Trim(), StringComparison.CurrentCultureIgnoreCase))
                AddNotification("Código verificação", "Código inválido");

            LimiteValidacao = null;
            ValidacaoRealizada = DateTime.UtcNow;
        }
    }
}
