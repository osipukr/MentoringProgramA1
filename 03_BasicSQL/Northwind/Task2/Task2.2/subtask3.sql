--По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя. 
--Необходимо определить это только для заказов, сделанных в 1998 году. 

DECLARE 
	@year INT = 1998;

SELECT 
	OrdersT.[EmployeeID]		AS 'EmployeeId',
	OrdersT.[CustomerID]		AS 'CustomerId',
	COUNT(OrdersT.[OrderID])	AS 'Amount'
FROM [dbo].[Orders] OrdersT
WHERE YEAR(OrdersT.[OrderDate]) = @year
GROUP BY OrdersT.[EmployeeID], OrdersT.[CustomerID];