CREATE PROCEDURE [dbo].[GetDonations]
	@donorId int 
AS
	SELECT d.Expense_Id as expenseId, n.title as expenseName,  n.User_id as beneficiaryId,
			i.name as beneficiaryName, d.Timestamp as donationTime, d.Amount as donationAmt
			from donation d, Need n, individual i
			where d.Donor_id=@donorId and d.Expense_id = n.id and i.id = n.User_Id
RETURN 0
