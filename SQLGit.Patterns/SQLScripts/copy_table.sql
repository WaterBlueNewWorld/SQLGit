SET NOCOUNT ON;

DECLARE @sql NVARCHAR(MAX), @cols NVARCHAR(MAX) = N'', @tablename NVARCHAR(MAX);

SELECT @cols += N',' + name + ' ' + system_type_name
FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM dbo.ADS$', NULL, 1);

SET @cols = STUFF(@cols, 1, 1, N'');

SET @sql = N'CREATE TABLE ADS$(' + @cols + ');'

--DECLARE @dbs TABLE(db SYSNAME);

--INSERT @dbs VALUES(N'db1'),(N'db2');
-- SELECT whatever FROM dbo.databases

/*SELECT @sql += N'
  INSERT ARANCELES SELECT ' + @cols + ' FROM ' + QUOTENAME(db) + '.dbo.tablename;'
  FROM @dbs;*/

/*SET @sql += N'
  SELECT ' + @cols + ' FROM ARANCELES;';*/

PRINT @sql;