IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'TrackerDB'))
USE TrackerDB;

GO

IF OBJECT_ID('Users', 'u') IS NULL
DROP TABLE UserData
