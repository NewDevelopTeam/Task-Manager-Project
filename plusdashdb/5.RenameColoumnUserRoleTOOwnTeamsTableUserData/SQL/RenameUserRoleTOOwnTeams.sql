USE plusdashdb
GO

EXEC sp_rename 'UserData.UserRole', 'OwnTeams', 'COLUMN';