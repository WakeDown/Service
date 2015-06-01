--SELECT * FROM dbo.users WHERE enabled = 1 AND	 full_name LIKE '%Сташевская%' 
--516 Пестрикова
--65761 Лучшева
--111296 Сташевская

SELECT * FROM dbo.srvpl_contract2devices c2d
WHERE c2d.enabled = 1

AND id_service_admin = 111296

--UPDATE srvpl_contract2devices
--SET id_service_admin = 111296
--WHERE enabled = 1
--AND id_service_admin = 65761

--Меняем созданные на этого админа выезды

--SELECT * FROM dbo.users WHERE enabled = 1 AND	 full_name LIKE '%Турушкина%' 
--516 Пестрикова
--65761 Лучшева
--111296 Сташевская
--519 Турушкина

SELECT * 
--UPDATE cl
--SET cl.id_service_admin = 519
FROM dbo.srvpl_service_claims cl
WHERE
cl.enabled = 1
--Этот месяц и будущие выезды
AND ((YEAR(planing_date) = year(GETDATE()) AND MONTH(planing_date) >= month(GETDATE())) OR planing_date >= getdate())
AND cl.id_service_admin = 111296