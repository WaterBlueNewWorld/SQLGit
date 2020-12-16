ALTER PROCEDURE sp_setpermission
AS
BEGIN
    
    EXEC sp_configure 'show advanced options', 1
    RECONFIGURE 
    
    EXEC sp_configure 'Ad Hoc Distributed Queries', 1
    RECONFIGURE 

END;
GO