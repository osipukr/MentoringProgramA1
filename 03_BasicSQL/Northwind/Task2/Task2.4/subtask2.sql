--Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT

SELECT EmployeesT.[EmployeeID]	AS 'EmployeeID'
FROM [dbo].[Employees] EmployeesT
WHERE (SELECT COUNT(OrdersT.[OrderID])
		FROM [dbo].[Orders] OrdersT
		WHERE OrdersT.[EmployeeID] = EmployeesT.[EmployeeID]) > 150;