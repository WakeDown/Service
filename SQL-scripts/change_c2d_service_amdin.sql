--SELECT * FROM dbo.users WHERE enabled = 1 AND	 full_name LIKE '%����������%' 
--516 ����������
--65761 �������
--111296 ����������

SELECT * FROM dbo.srvpl_contract2devices c2d
WHERE c2d.enabled = 1
AND id_service_admin = 111296

--UPDATE srvpl_contract2devices
--SET id_service_admin = 111296
--WHERE enabled = 1
--AND id_service_admin = 65761