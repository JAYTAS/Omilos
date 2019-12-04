INSERT INTO `taxonomy`.`master_table`
(`Name`, `StartDate`)
VALUES ('TimeZone', '2018-10-30');
select * from taxonomy.master_code_additional_property_name;
INSERT INTO `taxonomy`.`master_code`
(`MasterTableId`, `Code`, `Description`, `StartDate`)
VALUES (2, 'CST', 'Central Standard Time', '2018-10-30');

INSERT INTO `taxonomy`.`master_code`
(`MasterTableId`, `Code`, `Description`, `StartDate`)
VALUES (2, 'EST', 'Eastern Standard Time', '2018-10-30');

INSERT INTO `taxonomy`.`master_code`
(`MasterTableId`, `Code`, `Description`, `StartDate`)
VALUES (2, 'MST', 'Mountain Standard Time', '2018-10-30');

INSERT INTO `taxonomy`.`master_code`
(`MasterTableId`, `Code`, `Description`, `StartDate`)
VALUES (2, 'PST', 'Pacific Standard Time', '2018-10-30');

INSERT INTO `taxonomy`.`master_code_additional_property_name`
(`MasterTableId`, `Name`, `StartDate`)
VALUES (2, 'ZoneCode', '2018-10-30');

INSERT INTO `taxonomy`.`master_code_additional_property_value`
(`MasterCodeId`, `MasterCodePropertyNameId`, `Value`, `StartDate`)
VALUES (6, 3, 'US/Central', '2018-10-30');

INSERT INTO `taxonomy`.`master_code_additional_property_value`
(`MasterCodeId`, `MasterCodePropertyNameId`, `Value`, `StartDate`)
VALUES (7, 3, 'US/Eastern', '2018-10-30');

INSERT INTO `taxonomy`.`master_code_additional_property_value`
(`MasterCodeId`, `MasterCodePropertyNameId`, `Value`, `StartDate`)
VALUES (8, 3, 'US/Mountain', '2018-10-30');

INSERT INTO `taxonomy`.`master_code_additional_property_value`
(`MasterCodeId`, `MasterCodePropertyNameId`, `Value`, `StartDate`)
VALUES (9, 3, 'US/Pacific', '2018-10-30');