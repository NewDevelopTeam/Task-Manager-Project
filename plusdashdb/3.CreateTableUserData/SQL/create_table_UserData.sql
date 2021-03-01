IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'plusdashdb'))
USE plusdashdb;

GO

IF OBJECT_ID('UserData', 'u') IS NULL
CREATE TABLE UserData
(
	UserId INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50),
	OwnTeams NVARCHAR(50),
	TeamsParticipant NVARCHAR(50),
	UserONOFFLine BIT NOT NULL,
	LoginId INT NOT NULL,

	FOREIGN KEY (LoginId)REFERENCES Users(LoginId) ON DELETE NO ACTION
);