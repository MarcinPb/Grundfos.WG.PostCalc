SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Grzegorz Bu³ka
-- Create date: 2019-03-19
-- Description:	Procedure for dropping a list of
--              TelWin tables.
-- =============================================
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[TwDropTables]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[TwDropTables]
END
GO

CREATE PROCEDURE TwDropTables
	@varID nvarchar(64) = ''
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @sqlCommand AS NVARCHAR(MAX)
    DECLARE @tableName nvarchar(128)

    DECLARE tableCursor CURSOR LOCAL FOR
    SELECT TABLE_NAME 
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'DBO' AND TABLE_NAME LIKE 'AR[_]' + RIGHT('00000' + @varID, 6) + '[_]____'

    OPEN tableCursor  
    FETCH NEXT FROM tableCursor INTO @tableName

    SET NOCOUNT ON

    WHILE @@FETCH_STATUS = 0  
    BEGIN
        SET @sqlCommand = 
            ' DROP TABLE [dbo].[' + @tableName + ']'
        PRINT @sqlCommand
        EXEC (@sqlCommand)
        FETCH NEXT FROM tableCursor INTO @tableName 
    END
END
GO




