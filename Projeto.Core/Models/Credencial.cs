namespace Projeto.Core.Models
{
    public class Credencial
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public List<Usuario> Usuarios { get; set; } = new();
    }
}
