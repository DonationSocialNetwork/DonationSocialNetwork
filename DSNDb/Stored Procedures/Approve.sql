CREATE PROCEDURE [dbo].[Approve]
	@needId int
AS
	UPDATE [dbo].[Need]
	SET Approval_Status = 'A'
	WHERE [Id] = @needId
RETURN 0
