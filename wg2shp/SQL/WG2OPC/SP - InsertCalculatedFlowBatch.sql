USE [TWDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertCalculatedFlowBatch]    Script Date: 19.06.2019 12:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Grzegorz Bu³ka
-- Create date: 2019-06-18
-- Description:	Inserts calculated flow for all entries
--				configured in the appropriate variable
-- =============================================
CREATE PROCEDURE [dbo].[InsertCalculatedFlowBatch] 
	@time datetime
AS
BEGIN

DECLARE @fwd_c NVARCHAR(50)
DECLARE @rev_c NVARCHAR(50)
DECLARE @ratio_c NVARCHAR(50)
DECLARE @res_c NVARCHAR(50)
DECLARE @fwd INT
DECLARE @rev INT
DECLARE @ratio INT
DECLARE @res INT
DECLARE @str AS NVARCHAR(MAX)
SET @str =
	'(1001255 + 1001257) / 1169466 -> 1220417 | ' +
	'(2001255 + 2001257) / 2169466 -> 2220417 | ' +
	'(3001255 + 3001257) / 3169466 -> 3220417 | ' +
	'(5001255 + 5001257) / 5169466 -> 5220417 | ' +
	'(6001255 + 6001257) / 6169466 -> 6220417'

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		-- SET @str = SELECT * FROM AR_0000_2019 WHERE D_VALUE_STR = 220477

		DECLARE LINE_CUR CURSOR FOR
		SELECT value
		FROM STRING_SPLIT(@str, '|')
		WHERE RTRIM(value) <> ''

		DECLARE @line NVARCHAR(max)
		OPEN LINE_CUR

		FETCH NEXT FROM LINE_CUR INTO @line
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			SET @line = REPLACE(@line, CHAR(10), '')
			SET @line = REPLACE(@line, '(', '')
			SET @line = REPLACE(@line, ')', '')
			SET @line = REPLACE(@line, '+', '')
			SET @line = REPLACE(@line, '-', '')
			SET @line = REPLACE(@line, '/', '')
			SET @line = REPLACE(@line, '>', '')

			DECLARE ITEM_CUR CURSOR FOR
			SELECT value
			FROM STRING_SPLIT(@line, ' ')
			WHERE RTRIM(value) <> ''

			OPEN ITEM_CUR
			FETCH NEXT FROM ITEM_CUR INTO @fwd_c
			SET @fwd = CAST(@fwd_c AS int)
			FETCH NEXT FROM ITEM_CUR INTO @rev_c
			SET @rev = CAST(@rev_c AS int)
			FETCH NEXT FROM ITEM_CUR INTO @ratio_c
			SET @ratio = CAST(@ratio_c AS int)
			FETCH NEXT FROM ITEM_CUR INTO @res_c
			SET @res = CAST(@res_c AS int)
		
			PRINT 'Processing fwd: ' + CAST(@fwd AS NVARCHAR(20)) + ', rev: ' + CAST(@rev AS NVARCHAR(20)) + ', res: ' + CAST(@res AS NVARCHAR(20)) + ' @time: ' + CAST(@time AS NVARCHAR(40))
			EXECUTE [dbo].[InsertCalculatedFlow] @fwd, @rev, @res, @ratio, @time

			CLOSE ITEM_CUR
			DEALLOCATE ITEM_CUR


		FETCH NEXT FROM LINE_CUR INTO @line
		END 

		CLOSE LINE_CUR 
		DEALLOCATE LINE_CUR

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		DECLARE 
			@ErrorMessage NVARCHAR(4000),
			@ErrorSeverity INT,
			@ErrorState INT;
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();
		RAISERROR (
			@ErrorMessage,
			@ErrorSeverity,
			@ErrorState    
		);
		ROLLBACK TRANSACTION
	END CATCH
END
GO


