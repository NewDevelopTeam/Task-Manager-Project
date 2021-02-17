IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'TrackerDB'))
USE TrackerDB;

GO

IF OBJECT_ID('Users', 'u') IS NULL
CREATE TABLE UserData
(
	UserId INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	MiddleName NVARCHAR(50),
	UserRole NVARCHAR(50),
	UserONOFFLine BIT,
	LoginId INT,

	FOREIGN KEY (LoginId)REFERENCES UserLogIn(LoginId) ON DELETE NO ACTION
);
