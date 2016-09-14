CREATE PROCEDURE [dbo].[GetApprovals]
	@approver_id int
AS
	SELECT N.[Id], N.[Title], N.[Actual Amount], N.[User_Id], I.[Name], N.[Approval_Status]
	FROM [Need] AS N INNER JOIN [Individual] AS I ON N.[User_Id] = I.[Id]
	WHERE Facilitator_Id = @approver_id
RETURN 0
