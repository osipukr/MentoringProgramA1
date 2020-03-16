--Версия 1.1
--Добавляет таблицу данных кредитных карт сотрудников: 
--номер карты, дата истечения, имя card holder, ссылку на сотрудника,

IF NOT EXISTS (SELECT * FROM SYS.TABLES SysTables WHERE SysTables.[Name] = 'CreditCards')
BEGIN
	CREATE TABLE [dbo].[CreditCards](
		[CreditCardId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
		[ExpirationDate] DATETIME DEFAULT(NULL),
		[CardHolderName] VARCHAR(200) NOT NULL,
		[EmployeeId] INT REFERENCES [dbo].[Employees]([EmployeeID]));
END