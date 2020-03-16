CREATE TABLE [dbo].[CreditCards] (
    [CreditCardId]   INT           IDENTITY (1, 1) NOT NULL,
    [ExpirationDate] DATETIME      DEFAULT (NULL) NULL,
    [CardHolderName] VARCHAR (200) NOT NULL,
    [EmployeeId]     INT           NULL,
    PRIMARY KEY CLUSTERED ([CreditCardId] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

