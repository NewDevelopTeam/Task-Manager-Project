IF EXISTS (SELECT * FROM sys.databases WHERE name = 'plusdashDb')
USE plusdashDb
GO

IF OBJECT_ID('UserSessions') IS NOT NULL
DROP TABLE UserSessions
