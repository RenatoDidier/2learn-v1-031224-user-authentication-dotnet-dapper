using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta;
using Projeto.Core.Contexts.UsuarioContext.UseCases.ValidarConta.Contratos;
using Projeto.Tests.Contexts.UsuarioContext.Repository.Criar;
using Projeto.Tests.Contexts.UsuarioContext.Repository.Validar;

namespace Projeto.Tests.Contexts.UsuarioContext.UseCases.ValidarConta
{
    [TestClass]
    public class ValidarUsuarioRequestHandlerTest
    {
        private readonly IRepository _repository;
        private readonly IService _service;
        private readonly ValidarUsuarioRequestHandler _handler;

        public ValidarUsuarioRequestHandlerTest()
        {
            _repository = new FakeValidarRepository();
            _service = new FakeService();
            _handler = new ValidarUsuarioRequestHandler(_repository, _service);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailinvalido", "123456")]
        [DataRow("emailvalido@gmail.com", "12345")]
        [DataRow("emailvalido@gmail.com", "1234567")]
        public void Dado_um_request_invalido_deve_gerar_erro(string email, string codigoVerificacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerificacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(400, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailinvalido@gmail.com", "123456")]
        public void Dado_um_request_com_email_invalido_deve_gerar_erro(string email, string codigoVerficacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerficacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(401, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailvalidovalidado@gmail.com", "123456")]
        public void Dado_um_request_com_email_valido_e_validado_deve_gerar_erro(string email, string codigoVerificacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerificacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(402, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailvalidoexpirado@gmail.com", "123456")]
        public void Dado_um_request_com_email_valido_expirado_deve_gerar_erro(string email, string codigoVerificacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerificacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(404, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailvalido@gmail.com", "SDWEQW")]
        public void Dado_um_request_com_email_valido_com_codigo_invalido_deve_gerar_erro(string email, string codigoVerificacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerificacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(406, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Validar")]
        [DataRow("emailvalido@gmail.com", "000000")]
        public void Dado_um_request_com_email_valido_com_codigo_valido_deve_prosseguir(string email, string codigoVerificacao)
        {
            ValidarUsuarioRequest request = new(email, codigoVerificacao);

            var resultado = _handler.Handle(request, new CancellationToken());

            Assert.AreEqual(201, resultado.Result.Status);
        }
    }
}
