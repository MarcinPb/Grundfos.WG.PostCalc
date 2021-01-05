DECLARE @startDate datetime

SET @startDate = '2019-06-27 05:00'

DECLARE @m_id int, @s_id int, @m_val float, @s_val float, @timestamp datetime, @description nvarchar(256)
DECLARE @status AS int = 16

DECLARE @TempTable TABLE
(
	[Timestamp] [datetime],
	[MeasurementVarID] [int],
	[SimulationVarID] [int],
	[MeasurementValue] [float],
	[SimulationValue] [float],
	[Description] [nvarchar](256)
)

SET @startDate = ISNULL((SELECT TOP 1 Timestamp FROM FlowCorrelation ORDER BY Timestamp DESC), DATETIMEFROMPARTS(DATEPART(YEAR, @startDate), DATEPART(MONTH, @startDate), DATEPART(DAY, @startDate), DATEPART(HOUR, @startDate), 0, 0, 0));
PRINT @startDate

DECLARE config_cursor CURSOR FOR 
SELECT MeasurementVarID, SimulationVarID, Description
FROM config.FlowCorrelationConfig
WHERE IsActive = 1

OPEN config_cursor  
FETCH NEXT FROM config_cursor INTO @m_id, @s_id, @description

WHILE @@FETCH_STATUS = 0  
BEGIN
	PRINT 'Processing ' + @description + ', ' + CAST(@m_id as NVARCHAR(20)) + ', ' + CAST(@s_id as NVARCHAR(20))
	INSERT INTO @TempTable
	SELECT
		M.D_TIME Timestamp,
		M.D_VAR_ID M_ID,
		S.D_VAR_ID S_ID,
		M.D_VALUE_FLO M_VAL,
		S.D_VALUE_FLO S_VAL,
		@description
	FROM AR_0000_2019 M
		INNER JOIN AR_0000_2019 S ON M.D_VAR_ID = @m_id AND S.D_VAR_ID = @s_id AND M.D_TIME = S.D_TIME AND M.D_STATUS = @status AND S.D_STATUS = @status
	WHERE M.D_TIME > @startDate
	ORDER BY M.D_TIME
FETCH NEXT FROM config_cursor INTO @m_id, @s_id, @description
END 

CLOSE config_cursor
DEALLOCATE config_cursor

INSERT INTO FlowCorrelationLog
SELECT *
FROM @TempTable

DECLARE @time_cursor datetime
DECLARE time_cursor CURSOR FOR 
SELECT DISTINCT Timestamp
FROM @TempTable
ORDER BY Timestamp

OPEN time_cursor  
FETCH NEXT FROM time_cursor INTO @time_cursor

WHILE @@FETCH_STATUS = 0  
BEGIN
	INSERT INTO FlowCorrelation
	SELECT @time_cursor, (Avg([MeasurementValue] * [SimulationValue]) - (Avg([MeasurementValue]) * Avg([SimulationValue]))) / (StDevP([MeasurementValue]) * StDevP([SimulationValue]))
	FROM @TempTable
	WHERE Timestamp = @time_cursor
FETCH NEXT FROM time_cursor INTO @time_cursor
END 

CLOSE time_cursor
DEALLOCATE time_cursor

