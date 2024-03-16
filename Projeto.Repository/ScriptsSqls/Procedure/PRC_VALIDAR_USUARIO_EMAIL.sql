CREATE PROCEDURE [PRC_VALIDAR_USUARIO_EMAIL] (
	@Email NVARCHAR(150),
	@LimiteValidacao DATETIME = null,
	@ValidacaoRealizada DATETIME)  AS
BEGIN
	UPDATE 
		[usuario]
	SET 
		[LimiteValidacao] = @LimiteValidacao, [ValidacaoRealizada] = @ValidacaoRealizada
	WHERE
		[Email] = @Email

END;