--Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается 
--на буквы из диапазона b и g, не используя оператор BETWEEN. 

SELECT 
    CustomersT.[CustomerId]     AS 'CustomerId',
    CustomersT.[Country]		AS 'Country'
FROM [dbo].[Customers] CustomersT WHERE SUBSTRING(CustomersT.[Country], 1, 1) IN ('b', 'c', 'd', 'e', 'f', 'g')
ORDER BY CustomersT.[Country];

SELECT 
    CustomersT.[CustomerId]     AS 'CustomerId',
    CustomersT.[Country]		AS 'Country'
FROM [dbo].[Customers] CustomersT WHERE SUBSTRING(Country, 1, 1) >= 'b' AND SUBSTRING(Country, 1, 1) <= 'g'
ORDER BY CustomersT.[Country];