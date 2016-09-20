CREATE PROCEDURE [dbo].[GetExpenseNeeds]
	@userId int
AS
	SELECT [Id]
      ,[User_Id]
      ,[Title]
      ,[Description]
      ,[Actual Amount]
      ,[Balance Amount]
      ,[Approver_Id]
      ,[Approval_Status]
	  ,[Due Date]
  FROM [DSNDb].[dbo].[Need] where [User_Id] = @userId
RETURN 0
