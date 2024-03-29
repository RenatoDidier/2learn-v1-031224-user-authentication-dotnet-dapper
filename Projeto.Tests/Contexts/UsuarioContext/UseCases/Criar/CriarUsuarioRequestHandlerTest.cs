using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Tests.Contexts.UsuarioContext.Repository.Criar;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Criar;
using Projeto.Core.Contexts.UsuarioContext.Enum;

namespace Projeto.Tests.Contexts.UsuarioContext.UseCases.Criar
{

    [TestClass]
    public class CriarUsuarioRequestHandlerTest
    {
        private readonly Core.Contexts.UsuarioContext.UseCases.Criar.Contratos.IRepository _repository;
        private readonly IService _service;
        private readonly CriarUsuarioRequestHandler _handler;

        public CriarUsuarioRequestHandlerTest()
        {
            _repository = new FakeCriarRepository();
            _service = new FakeService();
            _handler = new CriarUsuarioRequestHandler(_repository, _service);
        }

        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("Renato", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vitae arcu semper, tristique eros nulla.", "rendfv@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vitae arcu semper, tristique eros nulla.", "Didier", "rendfv@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Renato", "Didier", "rendfv@gmail.com", "0123456789012345678901234567", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Renato", "Didier", "rendfv@gmail.com", "senha123", new CredencialEnum[] { })]
        [DataRow("Renato", "Didier", "rendfv@gmail.com", "123456", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Renato", "Didier", "rendfvgmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Renato", "Did", "rendfv@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        [DataRow("Ren", "Didier", "rendfv@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        public void Dado_um_request_invalido_deve_gerar_erro(
                string primeiroNome,
                string ultimoSobrenome,
                string email,
                string senha,
                CredencialEnum[] credenciais
            )
        {
            CriarUsuarioRequest request = new(primeiroNome, ultimoSobrenome, email, senha, credenciais);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(400, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("Renato", "Didier", "rendfv@gmail.com", "senha123", new CredencialEnum[] { (CredencialEnum)5 })]
        public void Dado_um_request_com_credencial_invalida_deve_gerar_erro(
                string primeiroNome,
                string ultimoSobrenome,
                string email,
                string senha,
                CredencialEnum[] credenciais
            )
        {
            CriarUsuarioRequest request = new(primeiroNome, ultimoSobrenome, email, senha, credenciais);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(401, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("Renato", "Didier", "rendfv@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        public void Dado_um_request_com_email_existente_deve_gerar_erro(
                string primeiroNome,
                string ultimoSobrenome,
                string email,
                string senha,
                CredencialEnum[] credenciais
            )
        {
            CriarUsuarioRequest request = new(primeiroNome, ultimoSobrenome, email, senha, credenciais);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(402, resultado.Result.Status);
        }


        [TestMethod]
        [TestCategory("Handler-Criar")]
        [DataRow("Renato", "Didier", "renatodidierf@gmail.com", "senha123", new CredencialEnum[] { CredencialEnum.Convidado })]
        public void Dado_um_request_valido_deve_prosseguir(
                string primeiroNome,
                string ultimoSobrenome,
                string email,
                string senha,
                CredencialEnum[] credenciais
            )
        {
            CriarUsuarioRequest request = new(primeiroNome, ultimoSobrenome, email, senha, credenciais);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(201, resultado.Result.Status);
        }

    }
}
