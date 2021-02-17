USE plusdashdb;
GO

EXEC sp_rename 'Users.ID', 'UserId', 'COLUMN';
GO