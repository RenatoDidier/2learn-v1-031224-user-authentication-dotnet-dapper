CREATE PROCEDURE [PRC_BUSCAR_USUARIO] (
	@email NVARCHAR(150)
) AS
BEGIN

	SELECT 
		* 
	FROM 
		[usuario]
	WHERE
		@email = [email];

END