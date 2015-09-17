SELECT tt.catalog_num, (SELECT TOP 1 name FROM dbo.zipcl_claim_units cuu WHERE cuu.catalog_num = tt.catalog_num  GROUP BY name)
FROM(
SELECT t.catalog_num, cnt, ROW_NUMBER() OVER (ORDER BY t.cnt desc) AS row_num
FROM
(

SELECT cu.catalog_num 
, count(1) AS cnt FROM dbo.zipcl_claim_units cu 
WHERE cu.enabled = 1
GROUP BY cu.catalog_num

) AS t
) AS tt
WHERE row_num <= 10
ORDER BY tt.cnt DESC
/*
B039 9510
AD02 7018
AD04 2059
AD04 2058
type 28
B121 9640
G060 2326
B039 3820
AA06 6637
AE04 4062*/

SELECT  name FROM dbo.zipcl_claim_units cu WHERE cu.catalog_num = 'B039 9510'