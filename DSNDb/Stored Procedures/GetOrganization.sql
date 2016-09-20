CREATE PROCEDURE [dbo].[GetOrganization]
	@id int
AS
	SELECT *
	FROM [Organization]
	WHERE Id = @id
RETURN 0
