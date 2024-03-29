CREATE PROCEDURE [PRC_BUSCAR_USUARIO] (
	@email NVARCHAR(150)
) AS
BEGIN

	SELECT 
		[Id], 
		[Email] AS 'usuario.Email.Endereco', 
		[SenhaHash] AS 'usuario.Senha.HashSenha', 
		[PrimeiroNome] AS 'usuario.Nome.PrimeiroNome', 
		[UltimoSobrenome] AS 'usuario.Nome.UltimoSobrenome', 
		[CodigoValidacao] AS 'usuario.Email.Validacao.Codigo', 
		[LimiteValidacao] AS 'usuario.Email.Validacao.LimiteValidacao', 
		[ValidacaoRealizada] AS 'usuario.Email.Validacao.ValidacaoRealizada'
	FROM 
		[usuario]
	WHERE
		@email = [email];

END

CREATE PROCEDURE [PRC_BUSCAR_USUARIO] (
	@email NVARCHAR(150)
) AS
BEGIN

	SELECT 
		[Id], 
		[Email], 
		[SenhaHash], 
		[PrimeiroNome], 
		[UltimoSobrenome], 
		[CodigoValidacao], 
		[LimiteValidacao], 
		[ValidacaoRealizada]
	FROM 
		[usuario]
	WHERE
		@email = [email];

END