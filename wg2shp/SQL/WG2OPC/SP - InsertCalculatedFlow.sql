--Processing P1: fwd: 1001255, rev: 1001257, fwdRatio: 1169466, revRatio: 1169466, interval: 1, res: 1220417 @startDate: Jun 25 2019  1:00AM
--Rows added: 0 since Jul  3 2019  8:00AM
	-- Add the parameters for the stored procedure here
	DECLARE @id int = 1220417				-- result variable ID
	DECLARE @fwd int = 1001255				-- forward counted variable ID
	DECLARE @fwdRatio int = 1169466			-- forward ratio variable ID
	DECLARE @fwdFactor float = 1			-- forward ratio factor
	DECLARE @rev int =1001257 				-- reverse counter variable ID
	DECLARE @revRatio int = 1169466			-- reverse ratio variable ID
	DECLARE @revFactor float = -1		-- reverse ratio factor
	DECLARE @startDate AS datetime = '2019-07-03 01:00'	-- reverse counter variable ID
	DECLARE @interval int = 10			-- interval by which the results will be aggregated

	SET NOCOUNT ON;

	DECLARE @status AS INT = 16
	DECLARE @TempTable TABLE
	(
		[Timestamp] [datetime],
		[Fwd] float,
		[Rev] float,
		[Count] [int]
	)

	INSERT INTO @TempTable
	SELECT
		DATEADD(mi, ((DATEDIFF(mi, @startDate, FWD.D_TIME)/@interval)*@interval), @startDate) Timestamp,
		SUM(FWD.D_VALUE_INT / FWD_RATIO.D_VALUE_FLO) Fwd,
		SUM(REV.D_VALUE_INT / REV_RATIO.D_VALUE_FLO) Rev,
		COUNT(FWD.D_VALUE_INT) Count
	FROM AR_0000_2019 FWD
		INNER JOIN AR_0000_2019 FWD_RATIO ON FWD.D_TIME = FWD_RATIO.D_TIME
		INNER JOIN AR_0000_2019 REV ON FWD.D_TIME = REV.D_TIME
		INNER JOIN AR_0000_2019 REV_RATIO ON FWD.D_TIME = REV_RATIO.D_TIME
	WHERE FWD.D_TIME >= @startDate
		 AND FWD.D_VAR_ID = @fwd
		 AND FWD_RATIO.D_VAR_ID = @fwdratio
		 AND REV.D_VAR_ID = @rev
		 AND REV_RATIO.D_VAR_ID = @revRatio
	GROUP BY
		DATEDIFF(mi, @startDate, FWD.D_TIME)/@interval
	ORDER BY   
		DATEDIFF(mi, @startDate, FWD.D_TIME)/@interval desc

	INSERT INTO CalculatedFlow
	SELECT @id, T.Timestamp, (((T.Fwd * @fwdFactor) + (T.Rev * @revFactor)) / Count) * 60
	FROM @TempTable T
		LEFT JOIN CalculatedFlow CF ON CF.Timestamp = T.Timestamp AND CF.ID = @id
	WHERE CF.Timestamp IS NULL
	
