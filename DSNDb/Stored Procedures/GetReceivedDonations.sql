CREATE PROCEDURE [dbo].[GetReceivedDonations]
	@beneficiaryId int 
AS
	SELECT d.Expense_Id as expenseId, n.title as expenseName, d.Donor_id as donorId,
			i.name as donorName, d.Timestamp as donationTime, d.Amount as donationAmt
			from donation d, Need n, individual i
			where n.User_Id=@beneficiaryId and n.id=d.Expense_id and i.id = d.Donor_id
RETURN 0
