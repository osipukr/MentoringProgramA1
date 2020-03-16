--Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. 
--Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен возвращать 
--только колонки CustomerID и Country и отсортирован по Country.

SELECT 
    CustomersT.[CustomerId]     AS 'CustomerId',
    CustomersT.[Country]		AS 'Country'
FROM [dbo].[Customers] CustomersT
WHERE SUBSTRING(CustomersT.[Country], 1, 1) BETWEEN 'b' AND 'g'
ORDER BY CustomersT.[Country];