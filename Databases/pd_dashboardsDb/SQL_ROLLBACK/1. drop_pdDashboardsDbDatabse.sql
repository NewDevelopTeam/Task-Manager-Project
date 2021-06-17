USE MASTER
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'pdDashboardsDb')
DROP DATABASE pdDashboardsDb