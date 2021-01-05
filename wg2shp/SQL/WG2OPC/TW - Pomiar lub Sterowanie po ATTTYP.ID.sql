DECLARE @opcGroupPrefix AS nvarchar(64) = 'OPCTest_WG_' -- 'OPC_WG_STER_' / 'OPCTest_WG_'
DECLARE @opcGroupIndex AS int = 1
DECLARE @attributeType AS int = 1

USE WG2OPC

SELECT
    DVATTR.DeviceID,
    OBJ.ID,
    '1' AS ItemNo,
    LEFT((OBJ.Label + '.' + CAST(OBJ.ID AS VARCHAR(max)) + '.' + ATTR.TwVariableCaption), 32) AS VariableName,
    '-',
    ATTYP.Name AS VariableClass,
    'dowolny' AS ValueType,
    DVATTR.DeviceID + ' ' + DVATTR.WgAttributeName AS Description,
    '26' AS VariableID,
    'OPCDrv' AS SourceName,
    @opcGroupPrefix + RIGHT('00' + CAST(((ROW_NUMBER() OVER (ORDER BY OBJ.DeviceID, OBJ.ID))  / 4900) + @opcGroupIndex AS NVARCHAR(100)), 2) +
        '%' + DVATTR.OpcChannel + '.' + 'DEV' + '.' +
        CASE
            WHEN DVATTR.OpcVariableSuffix IS NULL THEN OBJ.Label + '_' + CAST(OBJ.ID AS VARCHAR(max))
            ELSE DVATTR.OpcVariableSuffix + '_' + OBJ.Label + '_' + CAST(OBJ.ID AS VARCHAR(max))
        END
        + '#0'
        AS OpcTag,
    'v',
    'LO(600s)',
    '-',
    '-',
    '-',
    '-',
    '^',
    '0',
    '7',
    '-',
    '-'
FROM DeviceAttributes DVATTR
    INNER JOIN SimulationObjects OBJ ON DVATTR.DeviceID = OBJ.DeviceID
    INNER JOIN Attributes ATTR ON DVATTR.WgAttributeName = ATTR.WgAttributeName
	INNER JOIN AttributeTypes ATTYP on ATTYP.ID = ATTR.AttributeTypeID AND ATTYP.ID = @attributeType
ORDER BY ItemNo
