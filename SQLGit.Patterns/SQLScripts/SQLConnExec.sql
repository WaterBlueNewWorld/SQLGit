ALTER PROCEDURE sp_getconn 
	@sqlconn NVARCHAR(MAX),
	@tablename NVARCHAR(MAX),
	@column_n NVARCHAR(MAX)
AS
BEGIN
declare @sqlConnExec varchar(max)
set @sqlConnExec = 'SELECT * FROM OPENDATASOURCE(
''SQLNCLI'',''' +@sqlconn+ '''
).BODIES_300_20201103_C_V533_R0.dbo.'''+ @tablename+''' AS Remote
INNER JOIN dbo.'''+@tablename+''' AS Local ON Remote.'''+ @column_n +''' = Local.'''+ @column_n +''' '
--'Data Source=172.30.20.114;Persist Security Info=True;User ID=saas;Password=VclDev2020.;'

EXEC (@sqlConnExec)
END;
GO