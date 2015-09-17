DECLARE @planing_date DATETIME, @id_service_claim_status INT, @id_service_claim_type INT , @id_contract2devices INT, @id_creator INT, @id_contract INT, @id_device INT, @id_service_claim INT, @var_str NVARCHAR(50), @number NVARCHAR(50), @id_service_engeneer INT, @descr NVARCHAR(50), @order_num int
--set @planing_date = '2014-09-01'
SET @id_creator = 816

DECLARE curs CURSOR
                                FOR
                                    
SELECT  tt.id_contract2devices 
--,
--                                tt.id_device ,
--                                tt.id_contract ,
--                                tt.NAME ,
--                                tt.CONTRACT ,
--                                --ISNULL(RIGHT(CONVERT(VARCHAR, tt.last_claim_date, 106), 8), 'пусто') AS last_claim_date ,
--                                tt.last_claim_date ,
--                                --ISNULL(CONVERT(NVARCHAR, tt.last_came_date, 104), 'пусто') AS last_came_date ,
--                                tt.last_came_date ,
--                                ISNULL(( DATEDIFF(MONTH, tt.last_came_date,
--                                                  @planing_date) ), 0) AS no_service_month_count ,
--                                tt.id_service_interval_plan_group
                        FROM    ( SELECT    t.id_contract2devices ,
                                            t.id_device ,
                                            t.id_contract ,
                                            t.NAME ,
                                            t.CONTRACT ,
                                            ( SELECT    sc.planing_date
                                              FROM      dbo.srvpl_service_claims sc
                                              WHERE     sc.id_service_claim = last_claim_id
                                            ) AS last_claim_date ,
                                            ( SELECT TOP 1
                                                        sc.date_came
                                              FROM      dbo.srvpl_service_cames sc
                                              WHERE     sc.enabled = 1
                                                        AND sc.id_service_claim = last_claim_id
                                            ) AS last_came_date ,
                                            t.id_service_interval_plan_group
                                  FROM      ( SELECT    c2d.id_contract2devices ,
                                                        c2d.id_device ,
                                                        c2d.id_contract ,
                                                        dbo.srvpl_fnc('getContract2DevicesShortCollectedName',
                                                              NULL,
                                                              c2d.id_contract2devices,
                                                              NULL, NULL) AS NAME ,
                                                        dbo.srvpl_fnc('getContractCollectedNameNoAmount',
                                                              NULL,
                                                              c2d.id_contract,
                                                              NULL, NULL) AS contract ,
                                                        ( SELECT TOP 1
                                                              scl.id_service_claim
                                                          FROM
                                                              dbo.srvpl_service_claims scl
                                                          WHERE
                                                              scl.enabled = 1
                                                              AND scl.id_device = c2d.id_device
                                                          ORDER BY scl.planing_date DESC ,
                                                              id_service_claim DESC
                                                        ) AS last_claim_id ,
                                                        si.id_service_interval_plan_group
                                            --,(SELECT si.name FROM dbo.srvpl_service_intervals si WHERE si.id_service_interval = c2d.id_service_interval) AS service_interval
                                              FROM      dbo.srvpl_contract2devices c2d
                                                        INNER JOIN dbo.srvpl_devices d ON c2d.id_device = d.id_device
                                                        INNER JOIN dbo.srvpl_contracts c ON c2d.id_contract = c.id_contract
                                                        INNER JOIN dbo.srvpl_service_intervals si ON c2d.id_service_interval = si.id_service_interval
                                              WHERE     c2d.enabled = 1  AND old_id_contract2devices IS NULL                       
                                
                                                
                             --PLANING DATE
                             --Выводим активные договоры на это месяц (даже если один день попадает в месяц то договор считается активным
                        /*AND (  @planing_date IS NULL
                              OR ( @planing_date IS NOT NULL*/
                                                        AND dbo.srvpl_fnc('checkContractIsActiveOnMonth',
                                                              NULL,
                                                              c2d.id_contract,
                                                              @planing_date,
                                                              NULL) = '1'
                                 /*)
                            )*/
                                            ) AS T
                                ) AS tt
                        --ORDER BY contract ,
                        --        no_service_month_count DESC ,
                        --        name
                                
                                
                                SELECT  @id_service_claim_status = id_service_claim_status
                                FROM    dbo.srvpl_service_claim_statuses scs
                                WHERE   scs.enabled = 1
                                        AND LOWER(scs.sysname) = LOWER('NEW')
                                        
                                SELECT  @id_service_claim_type = ISNULL(@id_service_claim_type,
                                                              ( SELECT
                                                              id_service_claim_type
                                                              FROM
                                                              dbo.srvpl_service_claim_types ct
                                                              WHERE
                                                              ct.enabled = 1
                                                              AND UPPER(ct.sys_name) = 'PLAN'
                                                              ))
								
								SET @var_str = 'insServiceClaim'
								
                                IF EXISTS ( SELECT  1
                                            FROM    dbo.srvpl_service_claims t
                                            WHERE   t.id_service_claim = @id_service_claim )
                                    BEGIN
                                        SET @var_str = 'updServiceClaim'                                    
                                    END
						
                                						
                                OPEN curs
                                FETCH NEXT
                        
                        FROM curs
				INTO @id_contract2devices
							
                                WHILE @@FETCH_STATUS = 0
                                    BEGIN
                                        SELECT  @id_contract = c2d.id_contract ,
                                                @id_device = c2d.id_device
                                        FROM    dbo.srvpl_contract2devices c2d
                                        WHERE   c2d.enabled = 1
                                                AND c2d.id_contract2devices = @id_contract2devices
											
                                        EXEC dbo.sk_service_planing @action = @var_str,
                                            @id_service_claim = @id_service_claim,
                                            @id_contract2devices = @id_contract2devices,
                                            @id_contract = @id_contract,
                                            @id_device = @id_device,
                                            @planing_date = @planing_date,
                                            @id_service_claim_type = @id_service_claim_type,
                                            @number = @number,
                                            @id_service_engeneer = @id_service_engeneer,
                                            @descr = @descr,
                                            @order_num = @order_num,
                                            @id_service_claim_status = @id_service_claim_status,
                                            @id_creator = @id_creator                                         
                                        FETCH NEXT
					FROM curs
					INTO @id_contract2devices
                                    END

                                CLOSE curs

                                DEALLOCATE curs