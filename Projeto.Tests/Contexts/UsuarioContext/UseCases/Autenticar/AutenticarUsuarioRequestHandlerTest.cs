using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar;
using Projeto.Core.Contexts.UsuarioContext.UseCases.Autenticar.Contratos;
using Projeto.Tests.Contexts.UsuarioContext.Repository.Autenticar;

namespace Projeto.Tests.Contexts.UsuarioContext.UseCases.Autenticar
{
    [TestClass]
    public class AutenticarUsuarioRequestHandlerTest
    {
        private readonly IRepository _repository;
        private readonly AutenticarUsuarioRequestHandler _handler;

        public AutenticarUsuarioRequestHandlerTest()
        {
            _repository = new FakeAutenticarRepository();
            _handler = new AutenticarUsuarioRequestHandler(_repository);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("", "")]
        [DataRow("emailvalido@gmail.com", "")]
        [DataRow("", "senha123")]
        [DataRow("emailvalido@gmail.com", "1234567")]
        [DataRow("emailvalido@gmail.com", "012345678901234567890")]

        public void Dado_um_request_invalido_no_handler_autenticar_deve_gerar_erro(string email, string senha)
        {
            var novoRequest = new AutenticarUsuarioRequest(email, senha);

            var resultado = _handler.Handle(novoRequest, new CancellationToken());

            Assert.AreEqual(401, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailinvalido@gmail.com", "senha123")]
        public void Dado_um_usuario_inexistente_no_handler_autenticar_deve_gerar_erro(string email, string senha)
        {
            var novoRequest = new AutenticarUsuarioRequest(email, senha);

            var resultado = _handler.Handle(novoRequest, new CancellationToken());

            Assert.AreEqual(402, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailvalido@gmail.com", "senhainvalida")]
        public void Dado_um_usuario_com_senha_invalida_no_handler_autenticar_deve_gerar_erro(string email, string senha)
        {
            var novoRequest = new AutenticarUsuarioRequest(email, senha);

            var resultado = _handler.Handle(novoRequest, new CancellationToken());

            Assert.AreEqual(403, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailvalido@gmail.com", "senha123")]
        public void Dado_um_usuario_valido_e_nao_validado_deve_gerar_erro(string email, string senha)
        {
            var novoRequest = new AutenticarUsuarioRequest(email, senha);

            var resultado = _handler.Handle(novoRequest, new CancellationToken());

            Assert.AreEqual(404, resultado.Result.Status);
        }

        [TestMethod]
        [TestCategory("Handler-Autenticar")]
        [DataRow("emailvalidovalidado@gmail.com", "senha123")]
        public void Dado_um_usuario_valido_e_validado_deve_gerar_prosseguir(string email, string senha)
        {
            var novoRequest = new AutenticarUsuarioRequest(email, senha);

            var resultado = _handler.Handle(novoRequest, new CancellationToken());

            Assert.AreEqual(201, resultado.Result.Status);
        }
    }
}
