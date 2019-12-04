DELIMITER $$

DROP PROCEDURE IF EXISTS `Notification`.`uspRecordUserResponse` $$

CREATE PROCEDURE `Notification`.`uspRecordUserResponse`
/*
	Description : This is to record user response to notification
	Paramenters : messageId
				  notificationChannel like 1 - Email, 2 - SMS, 4 - Voice
                  response
    Example		: CALL `Notification`.`uspRecordUserResponse`('3873f07d-96e1-4502-936b-78949653202f', 2, 'Y');
*/
(
	IN messageId VARCHAR(36),
    IN notificationChannel INT,
    IN response VARCHAR(50)
)

BEGIN
	
	IF (notificationChannel = 2)
    THEN
    
        UPDATE Notification.sms_notification
        SET Response = response
        WHERE NotificationId = messageId;
        
		SELECT @groupId:= GroupId,
			   @contactId:= ContactId
        FROM Notification.sms_notification
        WHERE NotificationId = messageId; 
    
    END IF;

    IF (response = 'OptOut')
    THEN
        UPDATE subscription.group_contact_association
        SET HasOptedOut = 1
        WHERE ContactId = @contactId AND
        GroupId = @groupId;
    END IF;
    
END$$

DELIMITER ;