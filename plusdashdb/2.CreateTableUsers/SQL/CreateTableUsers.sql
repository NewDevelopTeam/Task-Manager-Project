IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'plusdashdb'))
USE plusdashdb;

GO

IF OBJECT_ID('Users', 'u') IS NULL
CREATE TABLE Users
(
	LoginId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL, 
	Email NCHAR(50) NOT NULL,
	Password NVARCHAR(50) NOT NULL,
	ValidatedEmail BINARY NOT NULL,
	LoginAttempts INT NOT NULL,
	LockOutEnd TIME NOT NULL,
	AccountAccess BINARY NOT NULL
)