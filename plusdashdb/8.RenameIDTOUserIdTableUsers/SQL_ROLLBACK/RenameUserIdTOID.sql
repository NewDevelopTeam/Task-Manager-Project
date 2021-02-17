USE plusdashdb;
GO

EXEC sp_rename 'Users.UserId', 'ID', 'COLUMN';
GO