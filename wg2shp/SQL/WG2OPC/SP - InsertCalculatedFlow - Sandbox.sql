SELECT *
FROM CalculatedFlow
WHERE ID = 1220417 AND Value IS NOT NULL and Timestamp > '2019-07-03 06:21:00.000'
ORDER BY Timestamp DESC

select * from config.CalculatedFlowConfig
where id = 1220417

select fwd.D_TIME, fwd.D_VALUE_INT fwd, rev.D_VALUE_INT rev
from AR_0000_2019 fwd
	inner join AR_0000_2019 rev on fwd.d_time = rev.d_time
	inner join AR_0000_2019 ratio on ratio.D_TIME = fwd.d_time
where
	fwd.D_VAR_ID = 1001255
	and rev.D_VAR_ID = 1001257
	and ratio.D_VAR_ID = 1169466
	and fwd.d_time > '2019-07-03 01:00'
order by fwd.D_TIME desc

