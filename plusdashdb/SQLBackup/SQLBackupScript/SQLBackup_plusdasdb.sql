USE plusdashdb;

GO

BACKUP DATABASE plusdashdb
TO DISK = 'C:\MS_DataBases\plusdashdb\SQLBackup\SQLBackup_plusdasdb.bak'
   WITH FORMAT,
      MEDIANAME = 'SQLServerBackups',
      NAME = 'Full Backup of SQLTestDB';
GO