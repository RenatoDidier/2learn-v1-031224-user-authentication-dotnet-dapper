CREATE PROCEDURE [PRC_INSERIR_USUARIO] (
	@id varchar(8),
    @email nvarchar(150),
    @senhaHash nvarchar(200),
    @primeiroNome nvarchar(100),
    @ultimoSobrenome nvarchar(100),
    @codigoValidacao varchar(6),
    @limiteValidacao datetime = NULL,
    @validacaoRealizada datetime = NULL) AS

BEGIN

    INSERT INTO usuario ([id], [email], [senhaHash], [primeiroNome], [ultimoSobrenome], [codigoValidacao], [limiteValidacao], [validacaoRealizada])
    VALUES (@id, @email, @senhaHash, @primeiroNome, @ultimoSobrenome, @codigoValidacao, @limiteValidacao, @validacaoRealizada);

END;