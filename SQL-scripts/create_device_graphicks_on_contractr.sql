DECLARE @id_load INT ,
    @id_contract INT ,
    @date_start DATETIME ,
    @id_device INT ,
    @id_service_interval INT ,
    @id_contract2devices INT ,
    @per_month INT ,
    @date_begin DATETIME ,
    @date_end DATETIME ,
    @cur_date DATETIME ,
    @lst_schedule_dates NVARCHAR(MAX) ,
    @var_str NVARCHAR(50) ,
    @error_text NVARCHAR(MAX),
    @counter INT
    
    SET @counter = 0
    
DECLARE curs_dev CURSOR LOCAL
FOR
SELECT c2d.id_contract, c.date_begin, c2d.id_device, c2d.id_service_interval 
 FROM dbo.srvpl_contract2devices c2d 
 INNER JOIN dbo.srvpl_contracts c ON c.id_contract = c2d.id_contract
 WHERE c2d.enabled = 1 AND c2d.id_contract = 136
    

OPEN curs_dev
FETCH NEXT
                        
                        FROM curs_dev
				INTO @id_contract, @date_start, @id_device,
    @id_service_interval
    --, @id_load
							
WHILE @@FETCH_STATUS = 0
    BEGIN
        BEGIN TRY
			SET @counter = @counter + 1
			
			SET @lst_schedule_dates = null
            SET @var_str = CONVERT(NVARCHAR, @id_device)
    
            SELECT  @id_contract2devices = c2d.id_contract2devices
            FROM    dbo.srvpl_contract2devices c2d
            WHERE   enabled = 1
                    AND c2d.id_contract = @id_contract
                    AND c2d.id_device = @id_device
			
			PRINT @id_contract2devices
															
            SELECT  @per_month = per_month
            FROM    dbo.srvpl_service_intervals si
            WHERE   si.id_service_interval = @id_service_interval
															
            IF @per_month > 0
                BEGIN
                                                              --Формируем график обслуживания для данного аппарата
                    SELECT  @date_begin = CASE WHEN @date_start IS NULL
                                               THEN c.date_begin
                                               ELSE @date_start
                                          END ,
                            @date_end = c.date_end
                    FROM    dbo.srvpl_contracts c
                    WHERE   c.id_contract = @id_contract
					
                    SET @cur_date = @date_begin
						
					PRINT '@date_end=' + ISNULL(CONVERT(NVARCHAR, @date_end), '') +  ' @date_begin=' + ISNULL(CONVERT(NVARCHAR, @date_begin), '') +  ' @cur_date=' + ISNULL(CONVERT(NVARCHAR, @cur_date), '')
															
                    WHILE YEAR(@cur_date) < YEAR(@date_end) or (YEAR(@cur_date) = YEAR(@date_end) AND MONTH(@cur_date) <= MONTH(@date_end))
                        BEGIN
                            SET @lst_schedule_dates = ISNULL(@lst_schedule_dates, '')
                                + ',' + CONVERT(NVARCHAR, @cur_date, 104)
	
                            SET @cur_date = DATEADD(MONTH, @per_month,
                                                    @cur_date)
                        END

                    SET @lst_schedule_dates = RIGHT(@lst_schedule_dates,
                                                    LEN(@lst_schedule_dates)
                                                    - 1)
                                                
                
                
        --ELSE
        --    BEGIN
        --        SET @error_text = 'Неверный график обслуживания для аппарата ID'
        --            + CONVERT(NVARCHAR, @id_device) 
							
        --        print @error_text	
        --    END 
            PRINT '@id_device=' + @var_str
                + ' @lst_schedule_dates=' + ISNULL(@lst_schedule_dates, '')
            											
            EXEC dbo.ui_service_planing @action = N'saveContract2Devices',
                @id_contract2devices = @id_contract2devices, @id_contract = @id_contract,
                @id_creator = 816, @lst_id_device = @var_str,
                @id_service_interval = @id_service_interval,
                @add_scheduled_dates2service_plan = 1,
                @lst_schedule_dates = @lst_schedule_dates
			
            --IF @lst_schedule_dates IS NOT NULL OR LTRIM(RTRIM(@lst_schedule_dates)) != ''
            --    BEGIN
            --        --UPDATE  dbo.srvpl_devices_no_graphik_load
            --        --SET     [LOAD] = 1
            --        --WHERE   id = @id_load
            --    END
           END
        END TRY

        BEGIN CATCH
                                               
        END CATCH                                            
        FETCH NEXT
					FROM curs_dev
					INTO @id_contract, @date_start, @id_device,
            @id_service_interval
            --, @id_load
    END

CLOSE curs_dev

DEALLOCATE curs_dev	

--PRINT @counter