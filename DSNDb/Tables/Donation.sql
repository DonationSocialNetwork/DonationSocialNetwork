CREATE TABLE [dbo].[Donation]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [Expense_id] INT not NULL constraint fk_1 references Need(id),
	[Donor_id] int not null constraint fk_2 references [User](id),
	[Timestamp] datetime not null,
	[Amount] int not null
)
