DELIMITER $$

DROP FUNCTION IF EXISTS `taxonomy`.`udfGetMasterCode_AdditionalPropertyValue` $$

CREATE FUNCTION `taxonomy`.`udfGetMasterCode_AdditionalPropertyValue`
/*
	Description : This is to get additional property details for a master code in master table
	Paramenters : CampaignId
    Example		: taxonomy.udfGetMasterCode_AdditionalPropertyValue('TimeZone', 'EST', 'ZoneCode');
*/
(
	tableName VARCHAR(50),
    code VARCHAR(25),
    additionalPropertyName VARCHAR(50)
)
RETURNS VARCHAR(100)

BEGIN
	
    DECLARE additionalPropertyValue VARCHAR(100);
    
    SET additionalPropertyValue = (SELECT MCAPV.Value
								   FROM taxonomy.master_table MT
								   INNER JOIN taxonomy.master_code MC
									ON MT.Id = MC.MasterTableId
								   INNER JOIN taxonomy.master_code_additional_property_name MCAPN
									ON MT.Id = MCAPN.MasterTableId
								   LEFT JOIN taxonomy.master_code_additional_property_value MCAPV
									ON MC.Id = MCAPV.MasterCodeId
									AND MCAPN.Id = MCAPV.MasterCodePropertyNameId
								   WHERE MT.Name = tableName
									AND MC.Code = code
									AND MCAPN.Name = additionalPropertyName);
                                    
	return additionalPropertyValue;

END$$

DELIMITER ;