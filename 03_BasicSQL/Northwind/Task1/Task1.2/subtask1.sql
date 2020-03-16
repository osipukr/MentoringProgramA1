--Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
--Запрос сделать с только помощью оператора IN. Возвращать колонки с именем
--пользователя и названием страны в результатах запроса. Упорядочить результаты 
--запроса по имени заказчиков и по месту проживания.

SELECT 
    CustomersT.[ContactName]    AS 'Contact Name',
    CustomersT.[Country]		AS 'Country'
FROM [dbo].[Customers] CustomersT
WHERE CustomersT.[Country] IN ('USA', 'Canada')
ORDER BY CustomersT.[ContactName], CustomersT.[Address];