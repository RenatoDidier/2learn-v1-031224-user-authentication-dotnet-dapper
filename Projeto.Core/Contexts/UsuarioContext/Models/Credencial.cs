using Projeto.Core.Contexts.UsuarioContext.Enum;

namespace Projeto.Core.Contexts.UsuarioContext.Models
{
    public class Credencial
    {
        public int Id { get; set; }
        public CredencialEnum Titulo { get; set; }
        public List<Usuario> Usuarios { get; set; } = new();
    }
}
