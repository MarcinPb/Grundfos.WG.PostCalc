USE WG2OPC
SELECT
    DVATTR.OpcChannel,
    CASE
        WHEN DVATTR.OpcVariableSuffix IS NULL THEN SO.Label + '_' + CAST(SO.ID AS VARCHAR(max))
        ELSE DVATTR.OpcVariableSuffix + '_' + SO.Label + '_' + CAST(SO.ID AS VARCHAR(max))
    END AS [Tag Name],
    'K' + RIGHT('000' + CAST((((ROW_NUMBER() OVER (PARTITION BY DVATTR.OpcChannel ORDER BY SO.ID)) * 2) - 1) AS nvarchar(10)), 4) AS Address,
    ATTR.OpcDataType AS [Data Type],
    '1' AS [Respect Data Type],
    'R/W' AS [Client Access],
    100 AS [Scan Rate],
    '' AS [Scaling],
    '' AS [Raw Low],
    '' AS [Raw High],
    '' AS [Scaled Low],
    '' AS [Scaled High],
    '' AS [Scaled Data Type],
    '' AS [Clamp Low],
    '' AS [Clamp High],
    '' AS [Eng Units],
    '' AS [Description],
    '' AS [Negate Value]
FROM DeviceAttributes DVATTR
    INNER JOIN SimulationObjects SO ON DVATTR.DeviceID = SO.DeviceID
    INNER JOIN Attributes ATTR ON DVATTR.WgAttributeName = ATTR.WgAttributeName
ORDER BY DVATTR.OpcChannel



