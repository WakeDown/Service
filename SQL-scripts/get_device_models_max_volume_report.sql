select id_device_model, dm.vendor, dm.name, case when dm.max_volume IS NULL THEN '' ELSE CONVERT(NVARCHAR, dm.max_volume) END AS max_volume
--dbo.srvpl_fnc('getDeviceModelShortCollectedName', NULL, dm.id_device_model, NULL, null) AS model 
from dbo.srvpl_device_models dm 
WHERE enabled=1
ORDER BY vendor, name
