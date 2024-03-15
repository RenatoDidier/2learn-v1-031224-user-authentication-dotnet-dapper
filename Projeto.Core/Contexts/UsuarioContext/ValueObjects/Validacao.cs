using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.CompartilhadoContext.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.ValueObjects
{
    public class Validacao : ValueObject
    {
        public string Codigo { get; } = CriadorStringAleatorio.GerarSeisCaracteres();
        public DateTime? DataLimiteValidacao { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? DataValidacaoRealizada { get; private set; }
        public bool CodigoValidado => DataLimiteValidacao == null && DataValidacaoRealizada != null;

        public void VerificacaoCodigo(string codigo)
        {
            if (CodigoValidado)
                AddNotification("Código verficação", "O código já foi validado");

            if (DataLimiteValidacao < DateTime.UtcNow)
                AddNotification("Código verificação", "Este código não é mais válido");

            if (!string.Equals(codigo.Trim(), Codigo.Trim(), StringComparison.CurrentCultureIgnoreCase))
                AddNotification("Código verificação", "Código inválido");

            DataLimiteValidacao = null;
            DataValidacaoRealizada = DateTime.UtcNow;
        }
    }
}
