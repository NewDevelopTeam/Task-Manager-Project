USE master

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'plusdashdb'))
CREATE DATABASE plusdashdb