DECLARE @id int , @cat_curr nvarchar(50) , @name_curr nvarchar(50), @cat_new nvarchar(50), @name_new nvarchar(50)
DECLARE curs CURSOR
                                FOR
                                    SELECT  id, LTRIM(rtrim(cat_curr)), LTRIM(rtrim(name_curr)), LTRIM(rtrim(cat_new)), LTRIM(rtrim(name_new)) FROM zipcl_cat_name_change
                                    
                                OPEN curs
                                FETCH NEXT                        
                        FROM curs
				INTO @id, @cat_curr, @name_curr, @cat_new, @name_new
                                
                                WHILE @@FETCH_STATUS = 0
                                    BEGIN
                                        UPDATE dbo.zipcl_claim_units
                                        SET catalog_num= @cat_new, name= @name_new
                                        WHERE (@id IS NOT NULL and id_claim_unit = @id) or
                                        (@id IS NULL and
                                        (RTRIM(LTRIM(catalog_num)) = RTRIM(LTRIM(@cat_curr)) and RTRIM(LTRIM(name)) = RTRIM(LTRIM(@name_curr))))
                                        
                                        PRINT '@id=' + isnull(CONVERT(NVARCHAR(10), @id), '') +  ' @cat_curr='+ @cat_curr + ' @name_curr=' + @name_curr + ' @cat_new=' +  @cat_new + ' @name_new=' + @name_new
                                        
                                        FETCH NEXT
					FROM curs
					INTO  @id, @cat_curr, @name_curr, @cat_new, @name_new
                                    END

                                CLOSE curs

                                DEALLOCATE curs 