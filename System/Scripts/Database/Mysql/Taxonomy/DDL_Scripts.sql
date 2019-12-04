CREATE DATABASE `taxonomy`;

CREATE TABLE `taxonomy`.`master_table` (
  `Id` int(19) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

CREATE TABLE `taxonomy`.`master_code` (
  `Id` int(19) NOT NULL AUTO_INCREMENT,
  `MasterTableId` int(19) NOT NULL,
  `Code` varchar(25) NOT NULL,
  `Description` varchar(150) NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`,`MasterTableId`,`Code`,`StartDate`),
  KEY `FK_MasterTable_Code_idx` (`MasterTableId`),
  CONSTRAINT `FK_MasterTable_Code` FOREIGN KEY (`MasterTableId`) REFERENCES `master_table` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

CREATE TABLE `taxonomy`.`master_code_additional_property_name` (
  `Id` int(19) NOT NULL AUTO_INCREMENT,
  `MasterTableId` int(19) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`,`MasterTableId`,`StartDate`),
  KEY `FK_MasterTableId_Property` (`MasterTableId`),
  CONSTRAINT `FK_MasterTableId_Property` FOREIGN KEY (`MasterTableId`) REFERENCES `master_table` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

CREATE TABLE `taxonomy`.`master_code_additional_property_value` (
  `Id` int(19) NOT NULL AUTO_INCREMENT,
  `MasterCodeId` int(19) NOT NULL,
  `MasterCodePropertyNameId` int(19) NOT NULL,
  `Value` varchar(100) DEFAULT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date DEFAULT NULL,
  PRIMARY KEY (`Id`,`MasterCodeId`,`MasterCodePropertyNameId`,`StartDate`),
  KEY `FK_MasterCode_PropertyValue_idx` (`MasterCodeId`),
  KEY `FK_MasterTableProperty_PropertyValue_idx` (`MasterCodePropertyNameId`),
  CONSTRAINT `FK_MasterCode_PropertyValue` FOREIGN KEY (`MasterCodeId`) REFERENCES `master_code` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_MasterTableProperty_PropertyValue` FOREIGN KEY (`MasterCodePropertyNameId`) REFERENCES `master_code_additional_property_name` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;


