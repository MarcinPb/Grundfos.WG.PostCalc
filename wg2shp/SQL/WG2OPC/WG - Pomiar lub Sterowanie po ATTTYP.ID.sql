USE WG2OPC

DECLARE @attributeType AS int = 1

SELECT
    OBJ.ID AS [Element ID],
    OBJ.Label AS [Element Label],
    'True' AS [Enabled],
    DVATTR.OpcChannel + '.' + 'DEV' + '.' +
        CASE
            WHEN DVATTR.OpcVariableSuffix IS NULL THEN OBJ.Label + '_' + CAST(OBJ.ID AS VARCHAR(max))
            ELSE DVATTR.OpcVariableSuffix + '_' + OBJ.Label + '_' + CAST(OBJ.ID AS VARCHAR(max))
        END AS [OPC Tag],
    ATTR.WgAttributeValue AS [Result Attribute ID],
    ATTR.WgAttributeName AS [Result Attribute Label]
FROM DeviceAttributes DVATTR
    INNER JOIN SimulationObjects OBJ ON DVATTR.DeviceID = OBJ.DeviceID
    INNER JOIN Attributes ATTR ON DVATTR.WgAttributeName = ATTR.WgAttributeName
    INNER JOIN AttributeTypes ATTYP on ATTYP.ID = ATTR.AttributeTypeID AND ATTYP.ID = @attributeType
ORDER BY [Element ID]
