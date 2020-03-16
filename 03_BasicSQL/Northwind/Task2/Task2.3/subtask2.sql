--Выдать в результатах запроса имена всех заказчиков из таблицы Customers и 
--суммарное количество их заказов из таблицы Orders.
--Принять во внимание, что у некоторых заказчиков нет заказов, 
--но они также должны быть выведены в результатах запроса. 
--Упорядочить результаты запроса по возрастанию количества заказов.

SELECT CustomersT.[ContactName]		AS 'ContactName',
	COUNT(OrdersT.[OrderId])		AS 'OrdersCount'
FROM [dbo].[Customers] CustomersT
	LEFT JOIN [dbo].[Orders] OrdersT
		ON CustomersT.[CustomerId] = OrdersT.[CustomerId]
GROUP BY CustomersT.[CustomerID], CustomersT.[ContactName]
ORDER BY 'OrdersCount';