--Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
--Запрос сделать с помощью оператора IN. Возвращать колонки с именем пользователя 
--и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.

SELECT 
    CustomersT.[ContactName]    AS 'Contact Name',
    CustomersT.[Country]		AS 'Country'
FROM [dbo].[Customers] CustomersT
WHERE CustomersT.[Country] NOT IN ('USA', 'Canada')
ORDER BY CustomersT.[ContactName];