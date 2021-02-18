IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'plusdashdb'))
USE plusdashdb;

GO

IF OBJECT_ID('UserData', 'u') IS NOT NULL
DROP TABLE UserData
