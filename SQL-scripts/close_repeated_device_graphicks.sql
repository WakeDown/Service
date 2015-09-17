

DECLARE @id_contract INT ,
    @id_device INT ,
    @planing_year INT ,
    @planing_month INT

DECLARE curs CURSOR
FOR
    SELECT  id_contract ,
            id_device ,
            YEAR(CONVERT(DATETIME, '01.' + planing_date, 104)) AS planing_year ,
            MONTH(CONVERT(DATETIME, '01.' + planing_date, 104)) AS planing_month
    FROM    ( SELECT    id_contract ,
                        id_device ,
                        RIGHT(CONVERT(NVARCHAR, sc.planing_date, 104), 7) AS planing_date ,
                        COUNT(1) AS cnt
              FROM      dbo.srvpl_service_claims sc
              WHERE     sc.enabled = 1
                        AND NOT EXISTS ( SELECT 1
                                         FROM   dbo.srvpl_service_cames scam
                                         WHERE  scam.enabled = 1
                                                AND scam.id_service_claim = sc.id_service_claim )
              GROUP BY  id_contract ,
                        id_device ,
                        RIGHT(CONVERT(NVARCHAR, sc.planing_date, 104), 7)
            ) AS t
    WHERE   t.cnt > 1
--ORDER BY cnt desc

OPEN curs
FETCH NEXT FROM curs
INTO @id_contract, @id_device, @planing_year, @planing_month


WHILE @@FETCH_STATUS = 0
    BEGIN
    
        UPDATE  sc
        SET     sc.enabled = 0
        FROM    dbo.srvpl_service_claims sc
        WHERE   sc.enabled = 1
                AND id_contract = @id_contract
                AND id_device = @id_device
                AND YEAR(sc.planing_date) = @planing_year
                AND MONTH(sc.planing_date) = @planing_month
                AND sc.id_service_claim != ( SELECT MAX(scc.id_service_claim)
                                             FROM   dbo.srvpl_service_claims scc
                                             WHERE  scc.enabled = 1
                                                    AND scc.id_contract = @id_contract
                                                    AND scc.id_device = @id_device
                                                    AND YEAR(sc.planing_date) = @planing_year
                                                    AND MONTH(sc.planing_date) = @planing_month
                                           )



        FETCH NEXT FROM curs
INTO @id_contract, @id_device, @planing_year, @planing_month

    END

CLOSE curs

DEALLOCATE curs
