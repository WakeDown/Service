DECLARE @id_claim INT
SET @id_claim = 55727

SELECT tttt.* , u.display_name
from
(
SELECT  id_claim ,
        claim_state ,
        CONVERT(NVARCHAR, ttt.change_date, 104) + ' '
        + CONVERT(NVARCHAR, ttt.change_date, 108) AS change_date
        ,
        id_creator
FROM    ( SELECT    tt.id_claim ,
                    tt.claim_state ,
                    change_date
                    ,
                    id_creator
          FROM      ( SELECT    t.id_claim ,
                                ( CASE WHEN cc.old_id_claim IS NULL
                                       THEN ISNULL(( SELECT TOP 1
                                                            ccc.dattim1
                                                     FROM   dbo.zipcl_zip_claims ccc
                                                     WHERE  ccc.old_id_claim = cc.id_claim
                                                            AND ccc.id_claim_state = t.id_claim_state
                                                     ORDER BY ccc.id_claim
                                                   ),
                                                   ISNULL(( SELECT TOP 1
                                                              cc.dattim2
                                                            FROM
                                                              dbo.zipcl_zip_claims cc
                                                            WHERE
                                                              cc.old_id_claim = t.id_claim
                                                            ORDER BY cc.id_claim DESC
                                                          ), cc.dattim1))
                                       ELSE cc.dattim1
                                  END ) AS change_date ,
                                cs.name AS claim_state ,
                                cs.history_order
                                ,
                                 ( CASE WHEN cc.old_id_claim IS NULL
                                       THEN ISNULL(( SELECT TOP 1
                                                            ccc.id_creator
                                                     FROM   dbo.zipcl_zip_claims ccc
                                                     WHERE  ccc.old_id_claim = cc.id_claim
                                                            AND ccc.id_claim_state = t.id_claim_state
                                                     ORDER BY ccc.id_claim
                                                   ),
                                                   ISNULL(( SELECT TOP 1
                                                              cc.id_creator
                                                            FROM
                                                              dbo.zipcl_zip_claims cc
                                                            WHERE
                                                              cc.old_id_claim = t.id_claim
                                                            ORDER BY cc.id_claim DESC
                                                          ), cc.id_creator))
                                       ELSE cc.id_creator
                                  END ) AS id_creator 
                      FROM      ( SELECT    MIN(c.id_claim) AS id_claim ,
                                            c.id_claim_state
                                  FROM      dbo.zipcl_zip_claims c
                                  WHERE     c.id_claim = @id_claim
                                            OR c.old_id_claim = @id_claim
                                  GROUP BY  id_claim_state
                                ) AS t
                                INNER JOIN dbo.zipcl_zip_claims cc ON t.id_claim = cc.id_claim
                                INNER JOIN dbo.zipcl_claim_states cs ON cc.id_claim_state = cs.id_claim_state
                    ) AS tt
          UNION ALL
          SELECT    tt.id_claim ,
                    tt.claim_state ,
                    change_date
                    ,
                    null AS id_creator
          FROM      ( SELECT    t.id_claim ,
                                ( CASE WHEN cc.old_id_claim IS NULL
                                       THEN ISNULL(( SELECT TOP 1
                                                            ccc.dattim1
                                                     FROM   dbo.zipcl_zip_claims ccc
                                                     WHERE  ccc.old_id_claim = cc.id_claim
                                                            AND ccc.id_et_way_claim_state = t.id_et_way_claim_state
                                                     ORDER BY ccc.id_claim
                                                   ),
                                                   ISNULL(( SELECT TOP 1
                                                              cc.dattim2
                                                            FROM
                                                              dbo.zipcl_zip_claims cc
                                                            WHERE
                                                              cc.old_id_claim = t.id_claim
                                                            ORDER BY  cc.id_claim DESC
                                                          ), cc.dattim1))
                                       ELSE cc.dattim1
                                  END ) AS change_date ,
                                cs.name AS claim_state ,
                                cs.history_order
                      FROM      ( SELECT    MIN(c.id_claim) AS id_claim ,
                                            c.id_et_way_claim_state
                                  FROM      dbo.zipcl_zip_claims c
                                  WHERE     c.id_claim = @id_claim
                                            OR c.old_id_claim = @id_claim
                                  GROUP BY  id_et_way_claim_state
                                ) AS t
                                INNER JOIN dbo.zipcl_zip_claims cc ON t.id_claim = cc.id_claim
                                INNER JOIN dbo.zipcl_claim_states cs ON cc.id_et_way_claim_state = cs.id_claim_state
                    ) AS tt
        ) AS ttt        
        ) AS tttt
        INNER JOIN users u ON tttt.id_creator = u.id_user
ORDER BY tttt.change_date