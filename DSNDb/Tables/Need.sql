CREATE TABLE [dbo].[Need]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [User_Id] INT NOT NULL, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(50) NULL, 
    [Actual Amount] INT NOT NULL, 
    [Balance Amount] INT NOT NULL, 
    [Approver_Id] INT NOT NULL, 
    [Approval_Status] CHAR(1) NOT NULL, 
    [Due Date] DATETIME NOT NULL, 
    CONSTRAINT [FK_Need_ToUser] FOREIGN KEY (User_Id) REFERENCES [User](Id)
)
