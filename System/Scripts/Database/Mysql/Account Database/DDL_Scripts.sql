CREATE DATABASE `account`;

CREATE TABLE `account`.`role` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code_UNIQUE` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

CREATE TABLE `account`.`user` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `GraphId` varchar(36) NOT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `EmailId` varchar(255) NOT NULL,
  `CountryCode` varchar(25) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `GraphId_UNIQUE` (`GraphId`),
  UNIQUE KEY `EmailId_UNIQUE` (`EmailId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

CREATE TABLE `account`.`user_logindetail` (
  `UserId` int(19) unsigned NOT NULL,
  `Password` varchar(128) DEFAULT NULL,
  `Salt` varchar(128) DEFAULT NULL,
  `FacebookId` varchar(36) DEFAULT NULL,
  `GoogleId` varchar(36) DEFAULT NULL,
  UNIQUE KEY `UserId_UNIQUE` (`UserId`),
  CONSTRAINT `FK_User_UserLoginDetails` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `account`.`user_role` (
  `Id` int(19) unsigned NOT NULL AUTO_INCREMENT,
  `UserId` varchar(36) NOT NULL,
  `RoleId` int(11) unsigned NOT NULL,
  `Scope` varchar(255) NOT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_User_UserRole_idx` (`UserId`),
  KEY `FK_Role_UserRole_idx` (`RoleId`),
  CONSTRAINT `FK_Role_UserRole` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_User_UserRole` FOREIGN KEY (`UserId`) REFERENCES `user` (`GraphId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
