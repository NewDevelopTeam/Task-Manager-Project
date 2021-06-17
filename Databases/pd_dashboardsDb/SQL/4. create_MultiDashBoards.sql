USE pdDashboardsDb
GO

IF OBJECT_ID('MultiDashBoards') IS NULL
CREATE TABLE MultiDashBoards
(
	Id INT IDENTITY(100000, 1) PRIMARY KEY,
	DashboardName NVARCHAR(50),
	PositionNo INT NOT NULL,
	UserId INT NOT NULL
)