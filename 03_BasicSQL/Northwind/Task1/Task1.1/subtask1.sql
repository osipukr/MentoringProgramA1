--Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) 
--включительно и которые доставлены с ShipVia >= 2. Запрос должен возвращать только колонки OrderID,
--ShippedDate и ShipVia. 

DECLARE
    @date DATETIME = Convert(DATETIME, '1998-05-06'),
    @shipVia INT = 2;

SELECT 
    OrderT.[OrderID]           AS 'OrderID',
    OrderT.[ShippedDate]       AS 'ShippedDate',
    OrderT.[ShipVia]           AS 'ShipVia'
FROM [dbo].[Orders] OrderT
WHERE OrderT.[ShippedDate] >= @date AND OrderT.[ShipVia] >= @shipVia;