-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: localhost    Database: carservicedb
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `booking`
--

DROP TABLE IF EXISTS `booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `booking` (
  `BookingId` int NOT NULL AUTO_INCREMENT,
  `CustId` int DEFAULT NULL,
  `CarId` int DEFAULT NULL,
  `StartLocation` int DEFAULT NULL,
  `EndLocation` int DEFAULT NULL,
  `Time` datetime DEFAULT NULL,
  `Type` varchar(45) DEFAULT NULL,
  `Hours` int DEFAULT NULL,
  `Price` double DEFAULT NULL,
  `PriceWithTax` double DEFAULT NULL,
  PRIMARY KEY (`BookingId`),
  KEY `CustomerId_idx` (`CustId`),
  KEY `CarId_idx` (`CarId`),
  KEY `Id_idx` (`StartLocation`,`EndLocation`),
  KEY `EndLocation_idx` (`EndLocation`),
  CONSTRAINT `CarId` FOREIGN KEY (`CarId`) REFERENCES `car` (`CarId`),
  CONSTRAINT `CustomerId` FOREIGN KEY (`CustId`) REFERENCES `customer` (`CustomerId`),
  CONSTRAINT `EndLocation` FOREIGN KEY (`EndLocation`) REFERENCES `location` (`LocationId`),
  CONSTRAINT `StartLocation` FOREIGN KEY (`StartLocation`) REFERENCES `location` (`LocationId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `booking`
--

LOCK TABLES `booking` WRITE;
/*!40000 ALTER TABLE `booking` DISABLE KEYS */;
INSERT INTO `booking` VALUES (10,14,2,1,1,'2020-08-08 02:36:37','Hourly',2,280,296.8),(11,14,1,1,1,'2020-10-08 02:36:37','Wedding',5,3000,3180),(12,14,3,1,1,'2020-08-08 02:46:03','Nightlife',0,760,805.6);
/*!40000 ALTER TABLE `booking` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-08  3:04:54
