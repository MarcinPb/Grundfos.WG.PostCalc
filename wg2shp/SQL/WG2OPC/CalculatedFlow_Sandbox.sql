DECLARE @STARTDATE DATETIME = '2019-06-17 05:30:00.000'
DECLARE @INTERVAL INT = 10
DECLARE @VAR INT = 2001255

SELECT 
  DATEADD(mi, ((DATEDIFF(mi, @startDate, D_TIME)/@interval)*@interval) + @INTERVAL, @startDate), COUNT(D_VALUE_INT), SUM(D_VALUE_INT)
FROM 
  AR_0000_2019
WHERE D_VAR_ID = @VAR AND D_TIME >= @startDate
GROUP BY
  DATEDIFF(mi, @startDate, D_TIME)/@interval
HAVING COUNT(D_VALUE_INT) = 10
ORDER BY   
  DATEDIFF(mi, @startDate, D_TIME)/@interval

  SELECT *
  FROM AR_0000_2019
  WHERE D_VAR_ID = 2001255 AND D_TIME > '2019-06-17 05:30:00.000'