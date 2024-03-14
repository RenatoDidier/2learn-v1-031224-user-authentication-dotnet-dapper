using Projeto.Shared.ValueObjects;

namespace Projeto.Shared.Models
{
    public class Usuario
    {
        public string Id { get; set; } = string.Empty;
        public Email Email { get; set; } = null!;
        public Senha Senha { get; set; } = null!;
        public Nome Nome { get; set; } = null!;
        public List<Credencial> credenciais { get; set; } = new();
    }
}
