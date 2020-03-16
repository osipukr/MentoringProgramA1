﻿--По таблице Orders найти количество заказов, cделанных каждым продавцом. 
--Заказ для указанного продавца – это любая запись в таблице Orders, где 
--в колонке EmployeeID задано значение для данного продавца. В результатах 
--запроса надо возвращать колонку с именем продавца (Должно высвечиваться имя 
--полученное конкатенацией LastName & FirstName. Эта строка LastName & FirstName
--должна быть получена отдельным запросом в колонке основного запроса. 
--Также основной запрос должен использовать группировку по EmployeeID.) с
--названием колонки ‘Seller’ и колонку c количеством заказов возвращать с 
--названием 'Amount'. Результаты запроса должны быть упорядочены по убыванию количества заказов.

SELECT 
	(SELECT CONCAT(EmployeesT.[LastName],' ', EmployeesT.[FirstName])
		FROM [dbo].[Employees] EmployeesT
		WHERE EmployeesT.[EmployeeID] = OrdersT.[EmployeeID])
	AS 'Seller',
	COUNT(OrdersT.[OrderId]) AS 'Amount'
FROM [dbo].[Orders] OrdersT
GROUP BY OrdersT.[EmployeeID]
ORDER BY 'Amount' DESC;