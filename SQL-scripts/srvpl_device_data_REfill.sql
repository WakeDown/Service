DECLARE @id_contract INT ,
    @id_device INT ,
    @planing_date DATETIME ,
    @counter INT ,
    @counter_color INT ,
    @prev_id_contract INT ,
    @prev_id_device INT ,
    @prev_planing_date DATETIME ,
    @prev_counter INT ,
    @prev_counter_color INT ,
    @place NVARCHAR(50) ,
    @prev_place NVARCHAR(50)

DECLARE curs CURSOR
FOR
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
        

OPEN curs

FETCH NEXT
	FROM curs
	INTO @id_contract, @id_device, @planing_date, @counter, @counter_color,
    @place

--ѕровер€ем есть ли показани€, которые "забегают вперед", т.е. счетчики в последующей дате больше чем в предыдущей
WHILE @@FETCH_STATUS = 0
    BEGIN
		
        SELECT  @prev_id_contract = @id_contract ,
                @prev_id_device = @id_device ,
                @prev_planing_date = @planing_date ,
                @prev_counter = @counter ,
                @prev_counter_color = @counter_color ,
                @prev_place = @place
		
        FETCH NEXT
		FROM curs
		INTO @id_contract, @id_device, @planing_date, @counter, @counter_color,
            @place
		
        IF @prev_id_contract = @id_contract
            AND @prev_id_device = @id_device
            AND @prev_planing_date < @planing_date
            AND ( ( @prev_counter > @counter
                    OR @prev_counter IS NULL
                    OR @counter IS NULL
                  )
                  OR @prev_counter_color > @counter_color
                )
            BEGIN
            
            --<вставка записей во временную таблицу чтобы исключить
                DELETE  tmp_device_counter_error
            
                INSERT  INTO tmp_device_counter_error
                        ( id_contract ,
                          id_device ,
                          [date] ,
                          [COUNTER] ,
                          counter_color ,
                          place ,
                          [prev_date] ,
                          [prev_COUNTER] ,
                          prev_counter_color ,
                          prev_place
                        )
                VALUES  ( @id_contract ,
                          @id_device ,
                          @planing_date ,
                          @COUNTER ,
                          @counter_color ,
                          @place ,
                          @prev_planing_date ,
                          @prev_COUNTER ,
                          @prev_counter_color ,
                          @prev_place
                        ) 
				
				--/>
    
                --PRINT isnull(@prev_place, '') + ' ' + isnull(@place, '')
                -- + ' ' + '@prev_id_contract=' + ISNULL(CONVERT(NVARCHAR,@prev_id_contract), '')
                --    + ' ' + '@prev_id_device=' + ISNULL(CONVERT(NVARCHAR,@prev_id_device), '')
                --    + ' ' + '@prev_planing_date=' + ISNULL(CONVERT(NVARCHAR,@prev_planing_date, 104),
                --                                           '')
                --                                           + ' ' + '@planing_date=' + ISNULL(CONVERT(NVARCHAR,@planing_date, 104),
                --                                           '') + ' '
                --    + '@prev_counter=' + ISNULL(CONVERT(NVARCHAR,@prev_counter), '') + ' '
                --    + '@counter=' + ISNULL(CONVERT(NVARCHAR,@counter), '') + ' '
                --    + '@prev_counter_color=' + ISNULL(CONVERT(NVARCHAR,@prev_counter_color), '')
                --    + ' ' + '@counter_color=' + ISNULL(CONVERT(NVARCHAR,@counter_color), '')
                    
                --    SET @i = @i + 1
            END
            
            
		
    END

CLOSE curs

DEALLOCATE curs


DELETE dbo.srvpl_device_data

DECLARE curs CURSOR
FOR
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
    WHERE   NOT EXISTS ( SELECT 1
                         FROM   tmp_device_counter_error t
                         WHERE  t.id_contract = p.id_contract
                                AND t.id_device = p.id_device
                                AND p.date = t.date
                                AND p.COUNTER = t.counter
                                AND p.counter_color = t.counter_color )
                            ORDER BY p.id_contract, p.id_device, p.date
                            
OPEN curs

FETCH NEXT
	FROM curs
	INTO @id_contract, @id_device, @planing_date, @counter, @counter_color,
    @place
	
WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC dbo.ui_service_planing @action = 'setDeviceData',
            @id_contract = @id_contract, @id_device = @id_device,
            @counter = @counter, @counter_colour = @counter_color,
            @planing_date = @planing_date, @id_creator = 816
    
    --PRINT @id_device
    
    FETCH NEXT
	FROM curs
	INTO @id_contract, @id_device, @planing_date, @counter, @counter_color,
    @place
    
    END

CLOSE curs

DEALLOCATE curs