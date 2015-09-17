DECLARE @id_device_model int, @max_volume INT, @cur_date datetime

DECLARE curs CURSOR LOCAL
FOR
   SELECT id_device_model, max_volume FROM srvpl_device_model_max_volume_load
   WHERE loaded = 0

OPEN curs

FETCH NEXT
						FROM curs
						INTO @id_device_model, @max_volume

WHILE @@FETCH_STATUS = 0
    BEGIN
		SET @cur_date = GETDATE()
    
		UPDATE dbo.srvpl_device_models
		SET max_volume = @max_volume
		WHERE id_device_model = @id_device_model
    
    UPDATE srvpl_device_model_max_volume_load
    set loaded=1, date_load=@cur_date
    WHERE id_device_model = @id_device_model
    
        FETCH NEXT
							FROM curs
							INTO @id_device_model, @max_volume
    END

CLOSE curs

DEALLOCATE curs