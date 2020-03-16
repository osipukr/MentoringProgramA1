--По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса надо возвращать две колонки c 
--названиями Year и Total. Написать проверочный запрос, который вычисляет количество всех заказов.

SELECT 
	YEAR(OrdersT.[OrderDate])		AS 'Order Year',
	COUNT(OrdersT.[OrderId])		AS 'Orders count'
FROM [dbo].[Orders] OrdersT
GROUP BY YEAR(OrdersT.[OrderDate]);