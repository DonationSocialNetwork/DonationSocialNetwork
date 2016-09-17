CREATE PROCEDURE [dbo].[GetIndividual]
	@id int
AS
	SELECT *
	FROM [Individual]
	WHERE Id = @id
RETURN 0
