using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Contexts.UsuarioContext.ValueObjects;
using Projeto.Core.ValueObjects;

namespace Projeto.Tests.Contexts.UsuarioContext.ValueObjects
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        [TestCategory("ValueObjects-Email")]
        [DataRow("")]
        [DataRow("emailerrado")]
        public void Dado_um_email_invalido_deve_gerar_erro(string email)
        {
            var valueObjectTeste = new Email(email);

            Assert.IsFalse(valueObjectTeste.IsValid);
        }

        [TestMethod]
        [TestCategory("ValueObjects-Email")]
        [DataRow("rendfv@gmail.com")]
        [DataRow("rendfv@hotmail.com")]
        [DataRow("rendfv@outlock.com")]
        [DataRow("rendfv@yahoo.com.br")]
        public void Dado_um_email_valio_deve_prosseguir(string email)
        {
            var valueObjectTeste = new Email(email);

            Assert.IsTrue(valueObjectTeste.IsValid);
        }


    }
}
