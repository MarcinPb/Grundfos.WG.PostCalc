print $(test)

-- sqlcmd -S localhost -E -i "c:\Repos\wg2shp\SQL\WG2OPC\ScriptWithArguments.sql" -v test = 'argtest'
-- sqlcmd -S WIN-C02NRM4VBF3\MSSQL2017 -U tw_user -P Gfosln123.