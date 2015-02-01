--Устройства привязанные к договору и без графика
SELECT  c2d.id_contract2devices ,
        c.id_contract ,
        d.id_device ,
        si.id_service_interval ,
        (select name_inn FROM get_contractor(c.id_contractor)) AS contractor ,
        c.number ,
        dbo.get_city_full_name(c2d.id_city) AS city ,
        d.serial_num ,
        si.name
FROM    dbo.srvpl_contract2devices c2d
        INNER JOIN dbo.srvpl_devices d ON c2d.id_device = d.id_device
        INNER JOIN dbo.srvpl_contracts c ON c.id_contract = c2d.id_contract
        INNER JOIN dbo.srvpl_service_intervals si ON c2d.id_service_interval = si.id_service_interval
WHERE   d.enabled = 1
        AND c2d.enabled = 1
        AND c.enabled = 1
        AND si.per_month > 0
        AND NOT EXISTS ( SELECT 1
                         FROM   dbo.srvpl_service_claims sc
                         WHERE  sc.enabled = 1
                                AND c2d.id_contract2devices = sc.id_contract2devices )

