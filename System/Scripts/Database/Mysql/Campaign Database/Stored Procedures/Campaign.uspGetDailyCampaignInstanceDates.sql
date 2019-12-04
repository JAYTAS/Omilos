DELIMITER $$

DROP PROCEDURE IF EXISTS `Campaign`.`uspGetDailyCampaignInstanceDates` $$

CREATE PROCEDURE `Campaign`.`uspGetDailyCampaignInstanceDates`
/*
	Description : This is to get Campaign Instance dates for a campaign schedule.
	Paramenters : 
		1. startDate : Start date of the campaign
        2. endDate	 : End date of the campaign
        3. separationCount : How many days once like once in 2days
        4. maxNumberOfOccurrences : Number instance requsted by the user
        5. daysOfWeek : days of the week on which instance has to take place
    Example		: CALL Campaign.uspGetDailyCampaignInstanceDates('2018-11-01', '2019-01-31', 1, 46, 0);
*/
(
	IN startDate DATE,
    IN endDate DATE,
    IN separationCount INT(11),
    IN maxNumberOfOccurrences INT(11),
    IN daysOfWeek INT(11)
)

DailyCampaignInstanceDatesBlock:BEGIN
	
    DECLARE i INT DEFAULT 0;
    DECLARE tempDate DATE;
    DECLARE dayOfWeek INT DEFAULT 0;
    DECLARE dayInterval INT DEFAULT 0;
    DECLARE nthSignificantBitValue INT DEFAULT 0;
    
    DROP TEMPORARY TABLE IF EXISTS campaignInstanceDates;
    
    CREATE TEMPORARY TABLE  campaigninstancedates (
		InstanceDate DATE
    );
    
    SET tempDate = startDate;
    
    IF(separationCount IS NOT NULL) 
    THEN
		SET dayInterval = dayInterval + separationCount;
        WHILE i < maxNumberOfOccurrences AND tempDate <= endDate DO            
            
            INSERT INTO campaigninstancedates (InstanceDate) VALUES (tempDate);            
            SET i = i + 1;
            SET tempDate = (select date_add(tempDate, INTERVAL dayInterval day));

        END WHILE;   
        
        SELECT * FROM campaigninstancedates;
        
        LEAVE DailyCampaignInstanceDatesBlock;
        
    END IF;    
    
    WHILE i < maxNumberOfOccurrences AND tempDate <= endDate DO            
            
		SET dayOfWeek = (SELECT DAYOFWEEK(tempDate));
        SET nthSignificantBitValue  = (SELECT POWER(2, dayOfWeek - 1));
        
		IF (daysOfWeek & nthSignificantBitValue = nthSignificantBitValue)
        THEN
			INSERT INTO campaigninstancedates (InstanceDate) VALUES (tempDate);
            SET i = i + 1;
		END IF;
        
        SET tempDate = (SELECT DATE_ADD(tempDate, INTERVAL dayInterval DAY));
        
	END WHILE;   
        
    SELECT * FROM campaigninstancedates;
    
END DailyCampaignInstanceDatesBlock$$

DELIMITER ;