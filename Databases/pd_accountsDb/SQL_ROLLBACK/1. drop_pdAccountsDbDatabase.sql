USE MASTER
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'pdAccountsDb')
DROP DATABASE pdAccountsDb