IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE ID = OBJECT_ID(N'plusdashdb'))
USE plusdashdb ;

GO

IF OBJECT_ID('Users', 'u') IS NOT NULL
DROP TABLE Users ;
