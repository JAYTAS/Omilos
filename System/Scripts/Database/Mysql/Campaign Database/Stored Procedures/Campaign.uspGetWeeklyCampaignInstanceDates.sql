DELIMITER $$

DROP PROCEDURE IF EXISTS `Campaign`.`uspGetWeeklyCampaignInstanceDates` $$

CREATE PROCEDURE `Campaign`.`uspGetWeeklyCampaignInstanceDates`
/*
	Description : This is to get Campaign Instance dates for a campaign schedule.
	Paramenters : 
		1. startDate : Start date of the campaign
        2. endDate	 : End date of the campaign
        3. separationCount : How many days once like once in 2days
        4. maxNumberOfOccurrences : Number instance requsted by the user
        5. daysOfWeek : days of the week on which instance has to take place
    Example		: CALL Campaign.uspGetWeeklyCampaignInstanceDates('2018-11-01', '2019-01-31', 1, 46, 0);
*/
(
	IN startDate DATE,
    IN endDate DATE,
    IN separationCount INT(11),
    IN maxNumberOfOccurrences INT(11),
    IN daysOfWeek INT(11)
)

WeeklyCampaignInstanceDatesBlock:BEGIN

	DECLARE isFirstWeekPartial BIT;
    DECLARE numberOfWeeksInACampaign INT;
    DECLARE dayOfCampaignStartWeek INT;
    DECLARE tempDate DATE;
	DECLARE i INT DEFAULT 0;
    DECLARE nthSignificantBitValue INT DEFAULT 0;
    DECLARE dayInterval INT DEFAULT 0;

    DROP TEMPORARY TABLE IF EXISTS campaignInstanceDates;
    
    CREATE TEMPORARY TABLE  campaigninstancedates (
		InstanceDate DATE
    );
    
    DROP TEMPORARY TABLE IF EXISTS campaignInstanceDays;
    
    CREATE TEMPORARY TABLE  campaignInstanceDays (
		SelectedDay INT
    );   
    
    SET dayOfCampaignStartWeek = (select dayofweek(startDate));
    SET isFirstWeekPartial = dayOfCampaignStartWeek <> 1;
    SET numberOfWeeksInACampaign = (SELECT ceil((DATEDIFF(endDate, startDate) + 1)/7)) - 1;
    
    WHILE i < 7 DO
		
        SET nthSignificantBitValue  = (SELECT POWER(2, i));
        SET i = i + 1;
        
        IF (daysOfWeek & nthSignificantBitValue = nthSignificantBitValue)
        THEN
			INSERT INTO campaignInstanceDays (SelectedDay) VALUES (i);
		END IF;
        
    END WHILE;
    
    IF isFirstWeekPartial = 1 THEN
		
        INSERT INTO campaigninstancedates 
        SELECT DATE_ADD(startDate, INTERVAL SelectedDay - dayOfCampaignStartWeek DAY)
        FROM campaignInstanceDays
        WHERE SelectedDay >= dayOfCampaignStartWeek;
    
    END IF;
    
    SET i = separationCount;
    
    IF isFirstWeekPartial = 1 THEN
		SET dayInterval = 7 - dayOfCampaignStartWeek + (7 * (separationCount - 1));
	ELSE
		SET dayInterval = -1;
	END IF;
    
    WHILE i <= numberOfWeeksInACampaign DO
        
        INSERT INTO campaigninstancedates 
        SELECT DATE_ADD(startDate, INTERVAL dayInterval + SelectedDay DAY)
        FROM campaignInstanceDays;
		
        SET dayInterval = dayInterval + 7 * separationCount;
        SET i = i + separationCount;
        
    END WHILE;
    
    SELECT * FROM campaigninstancedates WHERE InstanceDate <= endDate LIMIT maxNumberOfOccurrences;
    
END WeeklyCampaignInstanceDatesBlock$$

DELIMITER ;