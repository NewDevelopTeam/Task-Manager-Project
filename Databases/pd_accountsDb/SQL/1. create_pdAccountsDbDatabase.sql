USE MASTER
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'pdAccountsDb')
CREATE DATABASE pdAccountsDb