DELIMITER $$

DROP PROCEDURE IF EXISTS `Campaign`.`uspGetMonthlyCampaignInstanceDates` $$

CREATE PROCEDURE `Campaign`.`uspGetMonthlyCampaignInstanceDates`
/*
	Description : This is to get Campaign Instance dates for a campaign schedule.
	Paramenters : 
		1. startDate : Start date of the campaign
        2. endDate	 : End date of the campaign
        3. separationCount : How many days once like once in 2days
        4. maxNumberOfOccurrences : Number instance requsted by the user
        5. dayOfCampaignMonth : days of the month on which instance has to take place
        6. weekOfMonth : week of month on which instance has to take place
    Example		: CALL Campaign.uspGetMonthlyCampaignInstanceDates('2018-11-01', '2019-01-31', 1, 100, 1, null, null);
*/
(
	IN startDate DATE,
    IN endDate DATE,
    IN separationCount INT(11),
    IN maxNumberOfOccurrences INT(11),
    IN dayOfCampaignMonth INT(11),
    IN dayOfWeek INT(11),
    IN weekOfMonth INT(11)
)

MonthlyCampaignInstanceDatesBlock:BEGIN
	
    DECLARE totalNumberOfMonths INT;
    DECLARE lastDayOfMonth INT;
    DECLARE lastDayOfCurrentMonth INT;
    DECLARE i INT;
    DECLARE dayOfCampaignStartMonth INT;
    DECLARE monthInterval INT;
    DECLARE campaignStartMonth INT;
    DECLARE campaignStartYear INT;
    DECLARE firstDayOfStartDate DATE;
    DECLARE tempDate DATE;
    DECLARE tempDay INT;
    
    DROP TEMPORARY TABLE IF EXISTS campaignInstanceDates;
    
    CREATE TEMPORARY TABLE  campaigninstancedates (
		InstanceDate DATE
    );
    
    SET campaignStartYear = (SELECT YEAR(startDate));
    SET campaignStartMonth = (SELECT MONTH(startDate));
    SET dayOfCampaignStartMonth = (SELECT DAYOFMONTH(startDate));
	SET totalNumberOfMonths = (SELECT TIMESTAMPDIFF(Month, startDate, endDate) + 1);
    SET firstDayOfStartDate = (SELECT DATE_ADD(MAKEDATE(campaignStartYear, 1), INTERVAL campaignStartMonth - 1 MONTH));
    
    IF dayOfCampaignMonth IS NOT NULL THEN
		
        SET lastDayOfMonth = 32;
        SET i = (SELECT IF(dayOfCampaignStartMonth > dayOfCampaignMonth, separationCount, 0));
        
		WHILE i < totalNumberOfMonths DO
			
            SET tempDate = (SELECT DATE_ADD(firstDayOfStartDate, INTERVAL i MONTH));
            SET lastDayOfCurrentMonth = (SELECT DAY(LAST_DAY(tempDate)));
            SET tempDay = (SELECT IF (dayOfCampaignMonth = lastDayOfMonth, lastDayOfCurrentMonth, dayOfCampaignMonth));
                        
            IF(tempDay <= lastDayOfCurrentMonth) THEN
				INSERT INTO campaigninstancedates (InstanceDate) VALUES (DATE_ADD(tempDate, INTERVAL tempDay - 1 DAY));
			END IF;
            
            SET i = i + separationCount;
            
        END WHILE;
        
        SELECT * FROM campaigninstancedates WHERE InstanceDate <= endDate LIMIT maxNumberOfOccurrences;
        
        LEAVE MonthlyCampaignInstanceDatesBlock;
        
    END IF;
    
    
    
END MonthlyCampaignInstanceDatesBlock$$

DELIMITER ;