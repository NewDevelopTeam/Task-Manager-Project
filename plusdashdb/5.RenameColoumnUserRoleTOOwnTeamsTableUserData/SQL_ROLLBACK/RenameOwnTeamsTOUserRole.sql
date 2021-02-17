USE plusdashdb
GO

EXEC sp_rename 'UserData.OwnTeams', 'UserRole', 'COLUMN';