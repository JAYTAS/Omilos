CREATE DATABASE `campaign`;

CREATE TABLE `campaign`.`campaign` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `CampaignId` varchar(36) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `NotificationChannels` int(11) NOT NULL,
  `IsWelcomeMessageRequired` bit(1) NOT NULL,
  `IsRemainderMessageRequired` bit(1) NOT NULL,
  `IsOverDueMessageRequired` bit(1) NOT NULL,
  `GroupId` varchar(36) DEFAULT NULL,
  `SubscriptionId` varchar(36) NOT NULL,
  `Status` int(11) NOT NULL,
  `CampaignManagerEmailId` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `CampaignId_UNIQUE` (`CampaignId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

CREATE TABLE `campaign`.`campaign_instance` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `InstanceId` varchar(36) NOT NULL,
  `CampaignId` varchar(36) NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `InstanceId_UNIQUE` (`InstanceId`),
  KEY `FK_Campaign_CampaignInstance_idx` (`CampaignId`),
  CONSTRAINT `FK_Campaign_CampaignInstance` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`CampaignId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `campaign`.`campaign_instance_exception` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `CampaignInstanceExceptionId` varchar(36) NOT NULL,
  `InstanceId` varchar(36) NOT NULL,
  `IsRescheduled` bit(1) NOT NULL,
  `IsCancelled` bit(1) NOT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `StartTime` time DEFAULT NULL,
  `EndTime` time DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `CampaignInstanceExceptionId_UNIQUE` (`CampaignInstanceExceptionId`),
  KEY `FK_CampaignInstace_Exception_idx` (`InstanceId`),
  CONSTRAINT `FK_CampaignInstace_Exception` FOREIGN KEY (`InstanceId`) REFERENCES `campaign_instance` (`InstanceId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `campaign`.`message_template` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `MessageId` varchar(36) NOT NULL,
  `CampaignId` varchar(36) NOT NULL,
  `NotificationChannel` int(11) NOT NULL,
  `WelcomeMessage` varchar(255) DEFAULT NULL,
  `RemainderMessage` varchar(255) DEFAULT NULL,
  `OverDueMessage` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `MessageId_UNIQUE` (`MessageId`),
  KEY `FK_Campaign_MessageTemplate_idx` (`CampaignId`),
  CONSTRAINT `FK_Campaign_MessageTemplate` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`CampaignId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

CREATE TABLE `campaign`.`schedule` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `ScheduleId` varchar(36) NOT NULL,
  `CampaignId` varchar(36) NOT NULL,
  `IsRecurrence` bit(1) NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `StartTime` time NOT NULL,
  `EndTime` time NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ScheduleId_UNIQUE` (`ScheduleId`),
  KEY `FK_Campaign_Schedule_idx` (`CampaignId`),
  CONSTRAINT `FK_Campaign_Schedule` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`CampaignId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

CREATE TABLE `campaign`.`schedule_recurrencepattern` (
  `ScheduleId` int(19) unsigned NOT NULL,
  `RecurringType` int(11) NOT NULL,
  `SeparationCount` int(11) DEFAULT NULL,
  `MaxNumberOfOccurrences` int(11) DEFAULT NULL,
  `DaysOfWeek` int(11) DEFAULT NULL,
  `WeekOfMonth` int(11) DEFAULT NULL,
  `DayOfMonth` int(11) DEFAULT NULL,
  `MonthOfYear` int(11) DEFAULT NULL,
  PRIMARY KEY (`ScheduleId`),
  KEY `FK_Schedule_RecurrencePattern_idx` (`ScheduleId`),
  CONSTRAINT `FK_Schedule_RecurrencePattern` FOREIGN KEY (`ScheduleId`) REFERENCES `schedule` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
