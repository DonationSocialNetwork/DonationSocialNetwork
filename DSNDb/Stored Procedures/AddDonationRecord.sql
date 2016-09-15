CREATE PROCEDURE [dbo].[AddDonationRecord]
	@donorId int,
	@expenseId int,
	@donationAmount int,
	@beneficiaryUserId int
AS
	declare @amt int
    select @amt=[Balance Amount] from need where id = @expenseId
	set @amt = @amt - @donationAmount
	if(@amt<0)
	begin
		raiserror(N'too much amount',10,1) --doesn't work, better error handling needed to be caught by backend code
	end
	
	insert into Donation(Expense_id, Donor_id, [Timestamp], Amount) values(@donorId, @expenseId, GETDATE(), @donationAmount)
	update need set [Balance Amount] = [Balance Amount] - @donationAmount where Id = @expenseId

RETURN 0
