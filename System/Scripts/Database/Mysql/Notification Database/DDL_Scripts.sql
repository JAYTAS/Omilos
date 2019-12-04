CREATE DATABASE `notification`;

CREATE TABLE `notification`.`sms_notification` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `NotificationId` varchar(36) NOT NULL,
  `SubscriptionId` varchar(36) NOT NULL,
  `MessageType` int(11) NOT NULL,
  `SourceType` int(11) NOT NULL,
  `SourceId` varchar(36) NOT NULL,
  `PhoneNumber` varchar(15) NOT NULL,
  `NotificationDate` date NOT NULL,
  `NotificationTime` time NOT NULL,
  `Message` varchar(100) NOT NULL,
  `Status` int(11) NOT NULL,
  `Response` varchar(50) DEFAULT NULL,
  `Remarks` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NotificationId_UNIQUE` (`NotificationId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;