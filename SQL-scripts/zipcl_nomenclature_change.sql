DECLARE @catalog_num NVARCHAR(50) ,
    @nomenclature_num NVARCHAR(50),
    @cnt int

DECLARE curs CURSOR
FOR
    SELECT  catalog_num ,
            nomenclature_num
    FROM    zipcl_numenclature_change
    WHERE   CONVERT(DATE, dattim1) = CONVERT(DATE, GETDATE())
    
OPEN curs
FETCH NEXT                        
                        FROM curs
				INTO @catalog_num, @nomenclature_num
				
WHILE @@FETCH_STATUS = 0
    BEGIN
               
               
                                    
        UPDATE  dbo.zipcl_claim_units
        SET     nomenclature_num = @nomenclature_num
        WHERE   catalog_num = @catalog_num
        
        SELECT @cnt = COUNT(1) FROM dbo.zipcl_claim_units WHERE catalog_num = @catalog_num GROUP BY catalog_num
        
        PRINT '@cnt=' + CONVERT(NVARCHAR(50), @cnt) +  ' @catalog_num=' + @catalog_num + ' @nomenclature_num=' + @nomenclature_num
                                    
        FETCH NEXT
					FROM curs
					INTO @catalog_num, @nomenclature_num
    END

CLOSE curs

DEALLOCATE curs 

