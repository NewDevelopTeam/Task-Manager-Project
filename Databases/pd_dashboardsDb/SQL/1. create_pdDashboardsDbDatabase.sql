USE MASTER
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'pdDashboardsDb')
CREATE DATABASE pdDashboardsDb