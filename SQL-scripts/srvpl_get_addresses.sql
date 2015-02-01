SELECT * FROM (
select serial_num,	ADDRESS,(SELECT name_inn FROM get_contractor(c.id_contractor)) as contractor,	number, OBJECT_NAME
FROM dbo.srvpl_contract2devices c2d
INNER JOIN dbo.srvpl_contracts c ON c2d.id_contract = c.id_contract
INNER JOIN dbo.srvpl_devices d ON c2d.id_device = d.id_device
WHERE c2d.enabled = 1 AND c.enabled = 1 AND d.enabled = 1) AS t
GROUP BY serial_num,	ADDRESS, contractor,	number, OBJECT_NAME

