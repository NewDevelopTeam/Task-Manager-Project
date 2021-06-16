USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'plusdashDb')
CREATE DATABASE plusdashDb