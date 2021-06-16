IF EXISTS (SELECT * FROM sys.databases WHERE name = 'plusdashDb')
USE plusdashDb
GO

IF OBJECT_ID('UserSessions') IS NULL
CREATE TABLE UserSessions
(
	Id uniqueidentifier PRIMARY KEY,
	Value varbinary(MAX),
	LastActivity datetimeoffset(7),
	Expires datetimeoffset(7),
	UserId int NOT NULL
)