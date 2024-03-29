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
        //public CredencialEnum[] Credenciais { get; set; } = null!;

        //public bool ValidarCredenciais()
        //{
        //    var nenhumCredencialInvalido = true;

        //    foreach (var credencial in Credenciais)
        //    {
        //        if (!System.Enum.IsDefined(typeof(CredencialEnum), credencial))
        //        {
        //            nenhumCredencialInvalido =  false;
        //        }
        //    }
        //    return nenhumCredencialInvalido;
        //}
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
