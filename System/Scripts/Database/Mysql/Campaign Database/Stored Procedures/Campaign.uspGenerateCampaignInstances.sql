DELIMITER $$

DROP PROCEDURE IF EXISTS `Campaign`.`uspGenerateCampaignInstances` $$

CREATE PROCEDURE `Campaign`.`uspGenerateCampaignInstances`
/*
	Description : This is to Generate Campaign Instances for a campaign.
				  It will be called only whenever there is a change in campaign schedule
	Paramenters : CampaignId
    Example		: CALL Campaign.uspGenerateCampaignInstances('3ed319d3-7e0c-4385-ba8f-ff248cbe6345');
*/
(
	IN campaignId VARCHAR(36)
)

CampaignInstanceBlock:BEGIN
	
    DECLARE isRecurrenceCampaign BIT;
    DECLARE campaignTimeZoneCode VARCHAR(150);
    
    IF NOT EXISTS (SELECT CampaignId FROM campaign.schedule WHERE CampaignId = campaignId)
    THEN
		LEAVE CampaignInstanceBlock;
    END IF;
    
    SELECT @isRecurrenceCampaign:= IsRecurrence, 
		   @campaignTimeZone:= TimeZone
    FROM campaign.schedule WHERE CampaignId = campaignId;
    
    SET campaignTimeZoneCode = (SELECT taxonomy.udfGetMasterCode_AdditionalPropertyValue('TimeZone', @campaignTimeZone, 'ZoneCode'));
    
    IF (@isRecurrenceCampaign = 0) 
    THEN
		INSERT INTO campaign.campaign_instance (InstanceId, CampaignId, StartDate, EndDate, StartTime, EndTime)
        SELECT uuid(),
			   CS.CampaignId,
               DATE(CONVERT_TZ(CONCAT(CS.StartDate, ' ', CS.StartTime), campaignTimeZoneCode, 'UTC')), 
               DATE(CONVERT_TZ(CONCAT(CS.EndDate, ' ', CS.EndTime), campaignTimeZoneCode, 'UTC')),
               TIME(CONVERT_TZ(CONCAT(CS.StartDate, ' ', CS.StartTime), campaignTimeZoneCode, 'UTC')),
               TIME(CONVERT_TZ(CONCAT(CS.EndDate, ' ', CS.EndTime), campaignTimeZoneCode, 'UTC'))
        FROM campaign.schedule CS
        WHERE CampaignId = campaignId;
        
        LEAVE CampaignInstanceBlock;
        
    END IF;
	
    SELECT @recurringType:= SCRP.RecurringType,
		   @separationCount:= SCRP.SeparationCount,
           @maxNumberOfOccurrences:= SCRP.MaxNumberOfOccurrences,
           @daysOfWeek:= SCRP.DaysOfWeek,
           @weekOfMonth:= SCRP.WeekOfMonth,
           @dayOfTheMonth:= SCRP.DayOfMonth,
           @monthOfYear:= SCRP.MonthOfYear,
           @startDate:= DATE(CONVERT_TZ(CONCAT(SC.StartDate, ' ', SC.StartTime), campaignTimeZoneCode, 'UTC')),
           @endDate:= DATE(CONVERT_TZ(CONCAT(SC.EndDate, ' ', SC.EndTime), campaignTimeZoneCode, 'UTC')),
           @startTime:= SC.StartTime,
           @endTime:= SC.EndTime
	FROM campaign.schedule SC
    INNER JOIN campaign.schedule_recurrencepattern SCRP
		ON SC.Id = SCRP.ScheduleId
	WHERE SC.CampaignId = campaignId;
        select campaignTimeZoneCode, @startTime;
    
    /* Daily Campaign*/
    IF (@recurringType = 1) 
    THEN
		CALL Campaign.uspGetDailyCampaignInstanceDates(@startDate, @endDate, @separationCount, @maxNumberOfOccurrences, @daysOfWeek);
    END IF;
    
    /* Weekly Campaign*/
    IF (@recurringType = 2) 
    THEN
		CALL Campaign.uspGetWeeklyCampaignInstanceDates(@startDate, @endDate, @separationCount, @maxNumberOfOccurrences, @daysOfWeek);
    END IF;
    
    /* Monthly Campaign*/
    IF (@recurringType = 3) 
    THEN
		CALL Campaign.uspGetMonthlyCampaignInstanceDates(@startDate, @endDate, @separationCount, @maxNumberOfOccurrences, @dayOfTheMonth, @daysOfWeek, @weekOfMonth);
    END IF;
    
    /* Create Campaign Instances*/
	INSERT INTO campaign.campaign_instance (InstanceId, CampaignId, StartDate, EndDate, StartTime, EndTime)
    SELECT uuid(),
		   campaignId,
           CID.InstanceDate,
           CID.InstanceDate,
           TIME(CONVERT_TZ(CONCAT(CID.InstanceDate, ' ', @startTime), campaignTimeZoneCode, 'UTC')),
           TIME(CONVERT_TZ(CONCAT(CID.InstanceDate, ' ', @endTime), campaignTimeZoneCode, 'UTC'))
    FROM campaigninstancedates CID;
    
END CampaignInstanceBlock$$

DELIMITER ;