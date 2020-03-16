--По таблице Employees найти для каждого продавца его руководителя.

SELECT 
	EmployeesT.[EmployeeID]		AS 'EmployeeID',
	EmployeesT.[FirstName]		AS 'Seller name',
	(SELECT ManagersT.[FirstName]
		FROM [dbo].[Employees] ManagersT
		WHERE ManagersT.[EmployeeID] = EmployeesT.[ReportsTo])
	AS 'Manager'
FROM [dbo].[Employees] EmployeesT;