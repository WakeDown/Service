DECLARE @id_contract INT ,
    @id_device INT ,
    @var_str NVARCHAR(50) ,
    @id_device_data INT ,
    @is_sys_admin BIT ,
    @id_contractor INT ,
    @planing_date DATE ,
    @counter INT ,
    @counter_colour INT

--1644	4300	2014-06-10 11:22:28.990	54882	NULL	srvpl_contract2devices
--1644	4300	2015-03-03 00:00:00.000	222326	NULL	srvpl_service_claims
--1644	4300	2015-03-10 00:00:00.000	223237	NULL	srvpl_service_claims

SET @planing_date = '2015-03-10'
SET @counter = 223237



SET @id_contract = 1644
SET @id_device = 4300

/*
select * from srvpl_devices d
where d.enabled=1 and serial_num='NQV1X00843'
--1502677  NQV1X00843
--4369  L7006161329
--4300  V4499000065
*/

/*
DELETE dbo.srvpl_device_data
WHERE id_contract = 1644 AND id_device = 4300
*/

/*
select * from srvpl_device_data where id_device = 4300

    SELECT  p.id_contract ,
            p.id_device ,
            p.date ,
            CASE WHEN p.COUNTER = 0 THEN NULL
                 ELSE p.COUNTER
            END AS [COUNTER] ,
            CASE WHEN p.counter_color = 0 THEN NULL
                 ELSE p.counter_color
            END AS counter_color ,
            p.place
    FROM    srvpl_devices_data_pivot p
    WHERE id_device = 1502677
*/


--¬ычисл€ем id договора если его не передали
IF @id_contract IS NULL
    AND @id_contractor IS NOT NULL
    BEGIN
        SELECT TOP 1
                @id_contract = id_contract
        FROM    dbo.srvpl_contracts
        WHERE   enabled = 1
                AND id_contractor = @id_contractor
                AND dbo.srvpl_fnc('checkContractIsActiveOnMonth', NULL,
                                  id_contract, @planing_date, NULL) = '1'
    END
								
								--IF @id_contract IS not NULL AND @id_contractor IS NULL
								--begin
								--	SELECT @id_contractor = id_contractor FROM dbo.srvpl_contracts
								--	WHERE id_contract = @id_contract
								--end
                            
IF @id_device IS NULL
    OR ( @id_contract IS NULL
         AND @id_contractor IS NULL
       )
    OR @planing_date IS NULL
    OR ( @counter IS NULL
         AND @counter_colour IS NULL
       )
    BEGIN
        RETURN
    END
								
SET @var_str = 'insDeviceData'
DECLARE @volume_counter INT ,
    @volume_counter_colour INT

IF EXISTS ( SELECT  id_device_data
            FROM    dbo.srvpl_device_data t
            WHERE   t.ENABLED = 1
                    AND t.id_device = @id_device
                    AND t.id_contract = @id_contract
                    AND YEAR(t.date_month) = YEAR(@planing_date)
                    AND MONTH(t.date_month) = MONTH(@planing_date) )
    BEGIN
        SET @var_str = 'updDeviceData'
                                        
        SELECT TOP 1
                @id_device_data = id_device_data
        FROM    dbo.srvpl_device_data t
        WHERE   t.ENABLED = 1
                AND t.id_device = @id_device
                AND t.id_contract = @id_contract
                AND YEAR(t.date_month) = YEAR(@planing_date)
                AND MONTH(t.date_month) = MONTH(@planing_date)
                                                
                                                --≈сли это админ системы то сохран€ем то значение которое он сохран€ет, иначе не даем сохранить значение которое меньше
        IF @is_sys_admin <> 1
            BEGIN    
                                                
                SELECT  @counter = CASE WHEN counter IS NULL
                                             OR counter < @counter
                                        THEN @counter
                                        ELSE counter
                                   END ,
                        @counter_colour = CASE WHEN counter_colour IS NULL
                                                    OR counter_colour < @counter_colour
                                               THEN @counter_colour
                                               ELSE counter_colour
                                          END
                FROM    dbo.srvpl_device_data
                WHERE   id_device_data = @id_device_data
            END
                                        
                                            
    END

SET @volume_counter = ISNULL(@counter, 0)
    - ISNULL(( SELECT   MAX([counter])
               FROM     dbo.srvpl_device_data t
               WHERE    t.ENABLED = 1
                        AND t.id_device = @id_device
                        AND t.id_contract = @id_contract
                        AND [counter] IS NOT NULL
                        AND t.date_month <= @planing_date
             ), 0)
                                                              
SELECT  @volume_counter = CASE WHEN @volume_counter = ISNULL(@counter, 0)
                                    OR @volume_counter < 0 THEN 0
                               ELSE @volume_counter
                          END
                                            
                                                    
SET @volume_counter_colour = ISNULL(@counter_colour, 0)
    - ISNULL(( SELECT   MAX([counter_colour])
               FROM     dbo.srvpl_device_data t
               WHERE    t.ENABLED = 1
                        AND t.id_device = @id_device
                        AND t.id_contract = @id_contract
                        AND [counter_colour] IS NOT NULL
                        AND t.date_month <= @planing_date
             ), 0)
                                                              
SELECT  @volume_counter_colour = CASE WHEN @volume_counter_colour = ISNULL(@counter_colour,
                                                              0)
                                           OR @volume_counter_colour < 0
                                      THEN 0
                                      ELSE @volume_counter_colour
                                 END
  
  --≈сли показаний затекущий и предыдущий мес€ц не было, но показани€ были ранее, то высчитываем средний объем печати
IF NOT EXISTS ( SELECT  1
                FROM    dbo.srvpl_device_data dd
                WHERE   dd.enabled = 1
                        AND dd.id_device = @id_device
                        AND dd.id_contract = @id_contract
                        AND YEAR(dd.date_month) = YEAR(DATEADD(month, -1,
                                                              @planing_date))
                        AND ( MONTH(dd.date_month) = MONTH(DATEADD(month, -1,
                                                              @planing_date))
                              OR MONTH(dd.date_month) = MONTH(@planing_date)
                            ) )
    AND EXISTS ( SELECT 1
                 FROM   dbo.srvpl_device_data dd
                 WHERE  dd.enabled = 1
                        AND dd.id_device = @id_device
                        AND dd.id_contract = @id_contract
                        AND ( YEAR(dd.date_month) < YEAR(DATEADD(month, -1,
                                                              @planing_date))
                              OR ( YEAR(dd.date_month) = YEAR(DATEADD(month,
                                                              -1,
                                                              @planing_date))
                                   AND MONTH(dd.date_month) < MONTH(DATEADD(month,
                                                              -1,
                                                              @planing_date))
                                 )
                            ) )
    BEGIN
    
        DECLARE @avg_month_days_count DECIMAL(8, 1) ,
            @days_diff DECIMAL(8, 1) ,
            @days_index DECIMAL(8, 1) ,
            @counter_avg INT ,
            @counter_colour_avg INT ,
            @is_average BIT
                                            
        SET @avg_month_days_count = 30--среднее число дней в мес€це
	    
		--¬ычисл€ем сколько дней прошло с момента сохранени€ последнего акта
        SELECT TOP 1
                @days_diff = DATEDIFF(DAY, date_month, @planing_date)
        FROM    dbo.srvpl_device_data dd
        WHERE   dd.enabled = 1
                AND dd.id_device = @id_device
                AND dd.id_contract = @id_contract
        ORDER BY date_month DESC
                        
        SET @days_index = @days_diff / @avg_month_days_count
        
        PRINT @avg_month_days_count
        PRINT '@days_diff=' + CONVERT(NVARCHAR, ISNULL(@days_diff, ''))
        PRINT '@days_index=' + CONVERT(NVARCHAR, ISNULL(@days_index, ''))
                                        
        IF @days_index > 0
            BEGIN
                                            
                SET @counter_avg = ISNULL(@volume_counter, 0) / @days_index
                SET @counter_colour_avg = ISNULL(@volume_counter_colour, 0)
                    / @days_index
            END
			
				
			
        PRINT '@counter_avg=' + CONVERT(NVARCHAR, ISNULL(@counter_avg, ''))							
										
        SET @volume_counter = ISNULL(@counter_avg, @volume_counter)
        SET @volume_counter_colour = ISNULL(@counter_colour_avg,
                                            @volume_counter_colour)
										
        SET @is_average = 1
										
                      
    END
  
                                
                                EXEC @id_device = dbo.sk_service_planing @action = @var_str,
                                    @id_device = @id_device,
                                    @id_device_data = @id_device_data,
                                    @counter = @counter,
                                    @counter_colour = @counter_colour,
                                    @date_month = @planing_date,
                                    @id_contract = @id_contract,
                                    @volume_counter = @volume_counter,
                                    @volume_counter_colour = @volume_counter_colour,
                                    @is_average = @is_average
                                
--PRINT '@id_device=' + CONVERT(NVARCHAR, @id_device)
--PRINT '@id_device_data=' + CONVERT(NVARCHAR, @id_device_data)
PRINT '@counter=' + CONVERT(NVARCHAR, @counter)
--PRINT '@counter_colour=' + CONVERT(NVARCHAR, @counter_colour)
--PRINT '@planing_date=' + CONVERT(NVARCHAR(12), @planing_date)
--PRINT '@id_contract=' + CONVERT(NVARCHAR, @id_contract)
PRINT '@volume_counter=' + CONVERT(NVARCHAR, @volume_counter)
--PRINT '@volume_counter_colour=' + CONVERT(NVARCHAR, @volume_counter_colour)
                                
