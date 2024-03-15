using Projeto.Core.Contexts.CompartilhadoContext.Helpers;
using Projeto.Core.Contexts.CompartilhadoContext.ValueObjects;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects.Exceptions;
using System.Security.Cryptography;

namespace Projeto.Core.Contexts.UsuarioContext.ValueObjects
{
    public class Senha : ValueObject
    {
        private const int TamanhoSalt = 16;
        private const int TamanhoChave = 32;
        private const int Interacoes = 10000;
        private const char CaracterSeparacao = '.';

        public Senha(string? textoSenha = null)
        {
            if (string.IsNullOrWhiteSpace(textoSenha))
                throw new InvalidSenhaException();

            HashSenha = GeradorHash(textoSenha);
        }
        public bool VerficacaoHash(string senhaDigitada)
            => ValidarHash(HashSenha, senhaDigitada);
        public string HashSenha { get; } = string.Empty;
        public string CodigoReset { get; } = CriadorStringAleatorio.GerarSeisCaracteres();

        public static string GeradorHash(
                string senha,
                short tamanhoSalt = TamanhoSalt,
                short tamanhoChave = TamanhoChave,
                int interacoes = Interacoes,
                char caracterSeparacao = CaracterSeparacao
            )
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidSenhaException();

            senha += Configuracao.SenhaSegredos.ChaveSenhaSalt;

            using var algoritmo = new Rfc2898DeriveBytes(
                    senha,
                    tamanhoSalt,
                    interacoes,
                    HashAlgorithmName.SHA256
                );

            var chave = Convert.ToBase64String(algoritmo.GetBytes(tamanhoChave));
            var salt = Convert.ToBase64String(algoritmo.Salt);

            return $"{interacoes}{caracterSeparacao}{salt}{caracterSeparacao}{chave}";
        }

        private static bool ValidarHash(
                string hash,
                string senha,
                short tamanhoChave = TamanhoChave,
                int interacoes = Interacoes,
                char caracterSeparacao = CaracterSeparacao
            )
        {
            senha += Configuracao.SenhaSegredos.ChaveSenhaSalt;

            var partes = hash.Split(CaracterSeparacao, 3);

            if (partes.Length != 3)
                return false;

            var hashInteracao = Convert.ToInt32(partes[0]);
            var salt = Convert.FromBase64String(partes[1]);
            var chave = Convert.FromBase64String(partes[2]);

            if (hashInteracao != interacoes)
                return false;

            using var algoritmo = new Rfc2898DeriveBytes(
                    senha,
                    salt,
                    interacoes,
                    HashAlgorithmName.SHA256
                );

            var validadorDeChave = algoritmo.GetBytes(tamanhoChave);

            return validadorDeChave.SequenceEqual(chave);
        }
    }
}
