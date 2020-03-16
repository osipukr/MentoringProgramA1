--Версия 1.3
--Добавляет следующие минорные изменения относительно 1.1:
--Переименование Region в Regions
--Добавление в таблицу клиентов даты основания

IF EXISTS (SELECT * FROM SYS.TABLES SystemTables WHERE SystemTables.[Name] = 'Region')
BEGIN
	EXEC SP_RENAME '[dbo].[Region]', 'Regions';
END

IF NOT EXISTS (SELECT * FROM SYS.COLUMNS SystemColumns 
							WHERE SystemColumns.[OBJECT_ID] = OBJECT_ID(N'[dbo].[Customers]') AND Name = 'FoundationDate')
BEGIN
	ALTER TABLE [dbo].[Customers]
	ADD [FoundationDate] DATETIME
END