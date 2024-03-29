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
        public List<Credencial> Credenciais { get; set; } = new();
        public bool AdicionarCredenciais(CredencialEnum[] credenciais)
        {
            var nenhumCredencialInvalido = true;

            foreach (var credencial in credenciais)
            {
                if (!System.Enum.IsDefined(typeof(CredencialEnum), credencial))
                {
                    nenhumCredencialInvalido =  false;
                }

                Credencial novaCredencial = new();
                novaCredencial.Titulo = credencial;

                Credenciais.Add(novaCredencial);
            }

            return nenhumCredencialInvalido;
        }
    }

}
