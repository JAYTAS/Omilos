CREATE DATABASE `subscription`;

CREATE TABLE `subscription`.`subscription` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `SubscriptionId` varchar(36) NOT NULL,
  `Name` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `SubscriptionId_UNIQUE` (`SubscriptionId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

CREATE TABLE `subscription`.`contact` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `ContactId` varchar(36) NOT NULL,
  `SubscriptionId` varchar(36) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `CustomColumn1` varchar(25) DEFAULT NULL,
  `CustomColumn2` varchar(25) DEFAULT NULL,
  `CustomColumn3` varchar(25) DEFAULT NULL,
  `CustomColumn4` varchar(25) DEFAULT NULL,
  `CustomColumn5` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ContactId_UNIQUE` (`ContactId`),
  KEY `FK_Subscription_Contact_idx` (`SubscriptionId`),
  CONSTRAINT `FK_Subscription_Contact` FOREIGN KEY (`SubscriptionId`) REFERENCES `subscription` (`SubscriptionId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

CREATE TABLE `subscription`.`group` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `GroupId` varchar(36) NOT NULL,
  `SubscriptionId` varchar(36) NOT NULL,
  `Name` varchar(150) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `GroupId_UNIQUE` (`GroupId`),
  KEY `FK_Subscription_Group_idx` (`SubscriptionId`),
  CONSTRAINT `FK_Subscription_Group` FOREIGN KEY (`SubscriptionId`) REFERENCES `subscription` (`SubscriptionId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

CREATE TABLE `subscription`.`group_contact_association` (
  `GroupId` varchar(36) NOT NULL,
  `ContactId` varchar(36) NOT NULL,
  `HasOptedOut` bit(1) NOT NULL,
  PRIMARY KEY (`GroupId`,`ContactId`),
  KEY `FK_Contact_Association_idx` (`ContactId`),
  CONSTRAINT `FK_Contact_Association` FOREIGN KEY (`ContactId`) REFERENCES `contact` (`ContactId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Group_Association` FOREIGN KEY (`GroupId`) REFERENCES `group` (`GroupId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;