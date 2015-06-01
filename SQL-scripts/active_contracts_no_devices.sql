--Активные договоры без привязанных аппаратов
--- № договора
--- Тип договора
--- Контрагент
--- Период действия

select number, (SELECT name FROM dbo.srvpl_contract_types ct WHERE ct.id_contract_type = c.id_contract_type) AS TYPE,
(SELECT name_inn FROM dbo.get_contractor(NULL) ctr WHERE ctr.id = c.id_contractor ) AS contractor,
c.date_begin, c.date_end
from dbo.srvpl_contracts c
WHERE c.enabled = 1 AND dbo.srvpl_fnc('checkContractIsActiveNow',
                                                              NULL,
                                                              c.id_contract,
                                                              NULL, NULL) = '1' 
                                                              and not EXISTS(SELECT 1 FROM dbo.srvpl_contract2devices c2d WHERE c2d.enabled = 1 AND c2d.id_contract = c.id_contract)