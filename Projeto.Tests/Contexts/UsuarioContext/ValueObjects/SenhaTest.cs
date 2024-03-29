using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;

namespace Projeto.Tests.Contexts.UsuarioContext.ValueObjects
{
    [TestClass]
    public class SenhaTest
    {
        [TestMethod]
        [TestCategory("ValueObjects-Senha")]
        [DataRow("1234567")]
        [DataRow("012345678901234567891")]
        public void Dado_uma_senha_invalida_deve_gerar_erro(string senha)
        {
            var valueObjectTeste = new Senha(senha);

            Assert.IsFalse(valueObjectTeste.IsValid);
        }

        [TestMethod]
        [TestCategory("ValueObjects-Senha")]
        [DataRow("01234567890123456")]
        public void Dado_uma_senha_valida_deve_prosseguir(string senha)
        {
            var valueObjectTeste = new Senha(senha);

            Assert.IsTrue(valueObjectTeste.IsValid);
        }
    }
}
