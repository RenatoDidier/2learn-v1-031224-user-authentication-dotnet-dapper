CREATE PROCEDURE [PRC_INSERIR_CREDENCIAIS] (
	@UsuarioId varchar(8),
	@CredenciaisId int
) AS
BEGIN
	
	INSERT INTO 
		[usuario_credenciais] ([usuarioId], [credenciaisId])
	VALUES 
		(@@UsuarioId, @CredenciaisId)

END;