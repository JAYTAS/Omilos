DELIMITER $$

DROP PROCEDURE IF EXISTS `Campaign`.`uspLoadSmsNotificationForCampaignRemainder` $$

CREATE PROCEDURE `Campaign`.`uspLoadSmsNotificationForCampaignRemainder`
/*
	Description : This is to Load SMS Remainder notifiation for Campaigns.
				  It can be called eveyday or when the campaign scheduled on same day
	Paramenters : CampaignId
    Example		: CALL Campaign.uspLoadSmsNotificationForCampaignRemainder();
*/
(
	IN campaignId VARCHAR(36)
)

CampaignRemainderSmsNotificationBlock:BEGIN
	
    DECLARE smsChannelValue INT;
    SET smsChannelValue = 2;
    
    INSERT INTO notification.sms_notification (NotificationId, SubscriptionId, MessageType, SourceType, SourceId, 
											   PhoneNumber, NotificationDate, NotificationTime, Message, Status,
                                               ContactType, ContactId, GroupId)
	SELECT  uuid() 'NotificationId',
			CMP.SubscriptionId,
			2 'MessageType',
            2 'SourceType',
            CMPINS.InstanceId 'SourceId',
            CONT.PhoneNumber,
            CMPINS.StartDate 'NotificationDate',
            CMPINS.StartTime 'NotificationTime',
			replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(CMST.RemainderMessage, '{{FirstName}}', CONT.FirstName),
				   '{{LastName}}', CONT.LastName), '{{Email}}', CONT.Email), '{{PhoneNumber}}', CONT.PhoneNumber),
                   '{{CustomColumn1}}', CONT.CustomColumn1), '{{CustomColumn2}}', CONT.CustomColumn2), '{{CustomColumn3}}', CONT.CustomColumn3),
                   '{{CustomColumn4}}', CONT.CustomColumn4),'{{CustomColumn5}}', CONT.CustomColumn5),
                   '{{CampaignStartTime}}', CONCAT(DATE_FORMAT(CONVERT_TZ(CONCAT(CMPINS.StartDate, ' ', CMPINS.StartTime), 
																		  'UTC', 
																		  taxonomy.udfGetMasterCode_AdditionalPropertyValue('TimeZone', 
																															CONT.TimeZone, 
																															'ZoneCode')
                                                                   ), '%r'), ' ', CONT.TIMEZONE)) 'Message',
			0 'Status',
            1 'ContactType',
            CONT.ContactId,
            GRPCONTASSC.GroupId
    FROM campaign.campaign CMP
    INNER JOIN campaign.message_template CMST
		ON CMP.CampaignId = CMST.CampaignId
    INNER JOIN campaign.campaign_instance CMPINS
		ON CMP.CampaignId = CMPINS.CampaignId
    INNER JOIN subscription.group_contact_association GRPCONTASSC
		ON CMP.GroupId = GRPCONTASSC.GroupId
	INNER JOIN subscription.contact CONT
		ON GRPCONTASSC.ContactId = CONT.ContactId
	LEFT JOIN campaign.campaign_instance_exception CMPINSEXP
		ON CMPINS.InstanceId = CMPINSEXP.InstanceId
	WHERE (campaignId IS NULL OR CMP.CampaignId = campaignId) AND
		  CMP.Status = 1 AND
		  CMP.IsRemainderMessageRequired = 1 AND
          GRPCONTASSC.HasOptedOut = '0' AND
          CMPINS.StartDate = utc_date() AND
		  (CMP.NotificationChannels & 2 = 2) AND
		  (CMST.NotificationChannel & 2 = 2) AND
          CMST.RemainderMessage IS NOT NULL AND
          CMPINSEXP.CampaignInstanceExceptionId IS NULL;
    
END CampaignRemainderSmsNotificationBlock$$

DELIMITER ;