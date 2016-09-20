CREATE PROCEDURE [dbo].[GetApprovedNeedsDonationStatus]
	@approver_id int
AS
	SELECT N.[Id], N.[Title], N.[User_Id], I.[Name], N.[Actual Amount], N.[Balance Amount]  
	FROM [Need] AS N JOIN [Individual] AS I ON N.User_Id = I.Id 
	WHERE [Approver_Id] = @approver_id AND N.Approval_Status = 'A'
RETURN 0
