USE master
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'plusdashDb')
DROP DATABASE plusdashDb