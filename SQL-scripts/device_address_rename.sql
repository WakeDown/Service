select * 
--UPDATE c2d
--SET c2d.ADDRESS = ar.address_new, c2d.OBJECT_NAME = ar.object_name_new
FROM dbo.srvpl_contract2devices c2d
INNER JOIN dbo.srvpl_devices d ON c2d.id_device = d.id_device
INNER JOIN dbo.srvpl_address_rename ar ON d.serial_num = LTRIM(RTRIM(ar.serial_num))
WHERE d.enabled = 1