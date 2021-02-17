USE plusdashdb;
GO

ALTER TABLE Users ADD ValidatedEmail BINARY NOT NULL;
GO