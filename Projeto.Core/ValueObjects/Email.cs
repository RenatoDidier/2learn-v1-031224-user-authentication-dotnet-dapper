
using System.Text.RegularExpressions;

namespace Projeto.Core.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string PadraoRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string endereco)
        {
            if (string.IsNullOrEmpty(endereco))
                AddNotification("Endereço de e-mail", "O endereço não pode ser vazio");

            Endereco = endereco.Trim().ToLower();

            if (!EmailRegex().IsMatch(Endereco))
                AddNotification("Endereço de e-mail", "O endereço inválido");
        }

        public string Endereco { get; }
        public Validacao Validacao { get; private set; } = new ();

        [GeneratedRegex(PadraoRegex)]
        private static partial Regex EmailRegex();
    }
}
