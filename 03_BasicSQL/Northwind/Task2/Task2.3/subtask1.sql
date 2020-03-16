--Определить продавцов, которые обслуживают регион 'Western' (таблица Region)

SELECT DISTINCT 
	EmployeesT.[EmployeeId]			AS 'EmployeeId',
	EmployeesT.[FirstName]			AS 'First name'
FROM [dbo].[Employees] EmployeesT
		INNER JOIN [dbo].[EmployeeTerritories] EmployeeTerritoriesT 
			ON EmployeesT.[EmployeeID] = EmployeeTerritoriesT.[EmployeeID]
		INNER JOIN [dbo].[Territories] TerritoriesT
			ON EmployeeTerritoriesT.[TerritoryID] = TerritoriesT.[TerritoryID]
		INNER JOIN [dbo].[Region] RegionT
			ON RegionT.[RegionID] = TerritoriesT.[RegionID]
WHERE RegionT.[RegionDescription] = 'Western';