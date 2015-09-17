----//////// Обновляем все у кого совпадают ID

--select m.id_device_model, i.classifier_category, 
----update m set m.id_classifier_category=
--(select id from [usql-1].[Service].dbo.classifier_categories cat where cat.enabled=1 and rtrim(ltrim(cat.number)) = rtrim(ltrim(i.classifier_category)))
--from srvpl_device_models m inner join srvpl_device_model_import i
--on i.id_device_model=m.id_device_model
--where m.enabled=1



----//////// Обновляем все у кого совпадают Название (Name)

--select m.name
----update m set m.id_classifier_category=
----(select id from [usql-1].[Service].dbo.classifier_categories cat where cat.enabled=1 and rtrim(ltrim(cat.number)) = rtrim(ltrim(i.classifier_category)))
--from srvpl_device_models m
--inner join  srvpl_device_model_import i on lower(i.name)=lower(m.name)
--where i.id_device_model is null



----//////// Создаем модели у которых нет ID и они не совпадают по названию


--insert into srvpl_device_models (id_device_type, vendor, name, id_print_type, max_volume, id_classifier_category)
--select 0 as id_device_type, i.vendor, i.name,
--(select id_print_type from srvpl_print_types p where p.enabled=1 and lower(i.print_type)=lower(p.name)) as id_print_type,
--i.max_vol as max_volume,
--(select id from [usql-1].[Service].dbo.classifier_categories cat where cat.enabled=1 and rtrim(ltrim(cat.number)) = rtrim(ltrim(i.classifier_category))) as id_classifier_category
-- from srvpl_device_model_import i
--where i.name is not null and not exists (select 1 from srvpl_device_models m where m.enabled=1 and (m.id_device_model = i.id_device_model or lower(i.name)=lower(m.name)))

--select * from srvpl_device_models mm where mm.name in (
--select name from (
--select name, count(1) as cnt from srvpl_device_models m
--where enabled=1
--group by name) as t
--where t.cnt > 1)

----/////// Выгружаем аппараты оставшиеся без категорий

--select id_device_model, vendor, name from srvpl_device_models where enabled = 1 and id_classifier_category is null


--select * from [usql-1].[Service].dbo.classifier_categories
--select id from [usql-1].[Service].dbo.classifier_categories cat where cat.enabled=1 and rtrim(ltrim(cat.number)) = rtrim(ltrim('1.1.1'))

