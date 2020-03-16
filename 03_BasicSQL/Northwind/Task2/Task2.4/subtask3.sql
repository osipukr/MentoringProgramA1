--Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа 
--(подзапрос по таблице Orders). Использовать оператор EXISTS

SELECT CustomersT.[CustomerId ]
FROM [dbo].[Customers] CustomersT
WHERE NOT EXISTS (SELECT OrdersT.[OrderId] 
					FROM [dbo].[Orders] OrdersT
					WHERE OrdersT.[CustomerID] = CustomersT.[CustomerID]);