namespace Projeto.Core.ValueObjects
{
    public class Nome : ValueObject
    {
        public Nome(string nome, string sobrenome)
        {
            PrimeiroNome = nome.Trim().ToUpper();
            UltimoSobrenome = sobrenome.Trim().ToUpper();
        }
        public string PrimeiroNome { get; set; }
        public string UltimoSobrenome { get; set; }
    }
}
