CREATE PROCEDURE [dbo].[GetUserType]
	@userId int
AS
	SELECT [Type]
	FROM [User]
	WHERE Id = @userId
RETURN 0
