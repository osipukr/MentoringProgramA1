--Выдать всех поставщиков (колонка CompanyName в таблице Suppliers), 
--у которых нет хотя бы одного продукта 
--на складе (UnitsInStock в таблице Products равно 0). 
--Использовать вложенный SELECT для этого запроса с использованием оператора IN

SELECT SuppliersT.CompanyName	AS 'CompanyName'
FROM dbo.Suppliers SuppliersT
WHERE SuppliersT.SupplierID IN (SELECT ProductsT.[SupplierID]
									FROM [dbo].[Products] ProductsT
									WHERE ProductsT.[UnitsInStock] = 0);