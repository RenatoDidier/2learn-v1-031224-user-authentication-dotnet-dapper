using Projeto.Core.Contexts.UsuarioContext.Enum;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;

namespace Projeto.Core.Contexts.UsuarioContext.Models
{
    public class Usuario
    {
        public string Id { get; set; } = string.Empty;
        public Email Email { get; set; } = null!;
        public Senha Senha { get; set; } = null!;
        public Nome Nome { get; set; } = null!;
        public CredencialEnum Credencial { get; set; }
        public List<Credencial>? Credenciais { get; set; } = new();
    }
}
