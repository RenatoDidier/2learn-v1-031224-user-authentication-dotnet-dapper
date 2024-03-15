using Projeto.Core.Contexts.CompartilhadoContext.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.ValueObjects
{
    public class Nome : ValueObject
    {
        public Nome(string nome, string sobrenome)
        {
            PrimeiroNome = nome.Trim().ToUpper();
            UltimoSobrenome = sobrenome.Trim().ToUpper();
        }
        public string PrimeiroNome { get; }
        public string UltimoSobrenome { get; }
        public string EnviarNomeCompleto()
        {
            return $"{char.ToUpper(PrimeiroNome[0])}{PrimeiroNome.Substring(1)} {char.ToUpper(UltimoSobrenome[0])}{UltimoSobrenome.Substring(1)}";
        }
    }
}
