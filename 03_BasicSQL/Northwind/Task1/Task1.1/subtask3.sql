--Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) 
--не включая эту дату или которые еще не доставлены. В запросе должны возвращаться только колонки 
--OrderID (переименовать в Order Number) и ShippedDate (переименовать в Shipped Date).
--В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
--для остальных значений возвращать дату в формате по умолчанию.

DECLARE 
    @date DATETIME = CONVERT(DATETIME, '1998-05-06'),
    @DEFAULT_DATETIME_FORMAT INT = 0;

SELECT 
    OrdersT.[OrderID]         AS 'Order Number',
    CASE 
        WHEN OrdersT.[ShippedDate] IS NULL 
        THEN 'Not shipped'
        ELSE CONVERT(VARCHAR(30), OrdersT.[ShippedDate], @DEFAULT_DATETIME_FORMAT)
     END AS 'Shipped Date'
FROM [dbo].[Orders] OrdersT 
WHERE OrdersT.[ShippedDate] > @date OR OrdersT.[ShippedDate] IS NULL;