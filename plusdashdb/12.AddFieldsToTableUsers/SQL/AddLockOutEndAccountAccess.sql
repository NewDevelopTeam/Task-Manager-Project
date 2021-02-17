USE plusdashdb;
GO

ALTER TABLE Users ADD 

LockOutEnd TIME NOT NULL, 
AccountAccess BINARY NOT NULL;

GO