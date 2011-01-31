CREATE DATABASE IF NOT EXISTS `blackbirdcms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE blackbirdcms;
-- MySQL dump 10.13  Distrib 5.1.40, for Win32 (ia32)
--
-- Host: 127.0.0.1    Database: blackbirdcms
-- ------------------------------------------------------
-- Server version	5.1.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bb_article`
--

DROP TABLE IF EXISTS `bb_article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bb_article` (
  `articleId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `categoryId` varchar(45) DEFAULT NULL,
  `title` varchar(100) NOT NULL,
  `slug` varchar(100) NOT NULL,
  `summary` text NOT NULL,
  `content` text NOT NULL,
  `authorName` varchar(100) NOT NULL,
  `authorEmail` varchar(200) NOT NULL,
  `publishedAt` datetime NOT NULL,
  `expiresAt` datetime DEFAULT NULL,
  `inFrontPage` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `isPublished` tinyint(1) NOT NULL DEFAULT '1',
  `picture` varchar(200) NOT NULL,
  PRIMARY KEY (`articleId`),
  UNIQUE KEY `AK_bb_article_slug` (`slug`),
  FULLTEXT KEY `FT_bb_article` (`title`,`summary`,`content`,`authorName`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bb_article`
--

LOCK TABLES `bb_article` WRITE;
/*!40000 ALTER TABLE `bb_article` DISABLE KEYS */;
INSERT INTO `bb_article` VALUES (1,NULL,'Lorem ipsum dolor sit amet, consectetur adipiscing elit','lorem-ipsum-dolor-sit-amet-consectetur-adipiscing-elit','Aenean rhoncus, turpis sed mattis tristique, nisi tellus pellentesque urna, id accumsan quam massa sit amet metus. Maecenas interdum, quam et varius vestibulum, est sapien sollicitudin orci, sed euismod nisl ipsum at libero.','<p>Vivamus eu quam arcu, a euismod dolor. Praesent eget neque tortor, at accumsan purus. Sed consequat iaculis adipiscing. Nunc sit amet libero ac eros porttitor porta eget id ligula. Proin dapibus orci ac nisl semper a sodales justo interdum. Nulla tellus libero, pretium id eleifend sit amet, convallis quis mi. In mauris dui, aliquam eget tincidunt non, blandit ac mauris. Proin pellentesque imperdiet sapien at malesuada. Duis at enim purus, nec egestas lacus. Curabitur condimentum euismod mi, nec varius neque ullamcorper nec. Nunc nec turpis turpis, vitae venenatis nibh. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent auctor eleifend condimentum.</p>\r\n\r\n<p>Nulla ullamcorper est eget lacus condimentum sit amet blandit diam ultrices. Sed volutpat cursus leo, sed tincidunt neque mollis accumsan. Donec ullamcorper urna quis enim feugiat vel ultrices sapien blandit. Vestibulum ullamcorper lacus ut metus tristique a feugiat justo rutrum. Donec aliquet massa nec dui venenatis hendrerit. Maecenas egestas, sapien a convallis sagittis, enim turpis aliquam neque, sed lobortis nisl orci in ante. Praesent eget erat nec nisl tristique posuere. Praesent nec ipsum nec sapien sodales malesuada. Aenean quam enim, eleifend a laoreet a, fringilla vel dui. Phasellus nunc sapien, congue at aliquet eu, fringilla ac elit.</p>\r\n\r\n<p>Cras ultricies varius augue, et mollis metus sollicitudin viverra. Nullam id lobortis turpis. Cras quis mi eu sapien porta vestibulum. Quisque fringilla est et lectus vulputate aliquet. Nullam id lacus lectus, vel euismod enim. Etiam a est sit amet risus venenatis pellentesque. Praesent sit amet ante velit. Nulla rhoncus mauris vel urna porta nec molestie diam commodo. Vestibulum sed lorem ligula, vel sagittis erat.</p>','Administrator','admin@blackbirdcms.com','2010-01-20 00:00:00',NULL,1,1,''),(2,1,'Fusce bibendum adipiscing libero, ut ullamcorper risus tempor non','fusce-bibendum-adipiscing-libero-ut-ullamcorper-risus-tempor-non','Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus molestie dignissim nibh at facilisis. Nulla in orci a diam sodales lobortis semper vel elit.','<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque tincidunt suscipit nibh, non convallis lectus lobortis eu. Donec semper leo id risus tincidunt at tempus libero dignissim. Pellentesque fermentum, elit in porttitor convallis, nibh diam tempus sapien, vel tristique velit erat eget odio.</p>\r\n\r\n<p>Quisque egestas turpis vel magna facilisis non venenatis ipsum iaculis. Aenean egestas sodales velit, et varius leo gravida a. Sed sit amet sapien id lectus vulputate molestie. Nulla sed est lobortis arcu interdum tristique eu non libero. Curabitur justo dui, elementum ut pharetra vitae, dictum sit amet diam. Maecenas quis lorem elit. Nam tempor volutpat est in sodales.</p>\r\n\r\n<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce non nulla mi, non imperdiet mauris. Nunc vitae orci est, eget gravida massa. Vestibulum convallis nulla in urna feugiat eleifend. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>\r\n\r\n<p>Nullam vitae felis sapien, sit amet tristique metus. Ut elementum urna egestas lacus venenatis sed gravida elit ornare. Pellentesque vehicula, lectus ut mattis congue, arcu sem convallis ligula, et convallis sapien sem eu ligula. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi a mauris mauris. Cras et eros nisl, nec vulputate neque.</p>','Administrator','admin@blackbirdcms.com','2011-01-21 00:00:00',NULL,1,1,'');
/*!40000 ALTER TABLE `bb_article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bb_category`
--

DROP TABLE IF EXISTS `bb_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bb_category` (
  `categoryId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `parentId` int(10) unsigned DEFAULT NULL,
  `lft` int(11) NOT NULL DEFAULT '0',
  `rgt` int(11) NOT NULL DEFAULT '0',
  `name` varchar(100) NOT NULL,
  `slug` varchar(100) NOT NULL,
  `description` text NOT NULL,
  `module` varchar(100) NOT NULL DEFAULT 'content',
  `picture` varchar(200) NOT NULL,
  PRIMARY KEY (`categoryId`),
  KEY `AK_bb_category_slug` (`parentId`,`slug`),
  FULLTEXT KEY `FT_bb_category` (`name`,`description`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bb_category`
--

LOCK TABLES `bb_category` WRITE;
/*!40000 ALTER TABLE `bb_category` DISABLE KEYS */;
INSERT INTO `bb_category` VALUES (1,NULL,1,2,'General','general','','content',''),(2,NULL,2,9,'Portfolio','portfolio','','content','In et turpis felis, non convallis neque. Sed nec lorem nulla. Nunc consectetur accumsan lorem, vel luctus quam posuere eu. Vestibulum nibh erat, mattis sed convallis eu, lacinia non mauris. In hac hab'),(3,NULL,3,4,'Web','web','','content','Ut in purus ac est posuere faucibus vel et nibh. Nulla facilisi. Ut venenatis pretium felis vel pulvinar. Praesent sed tortor ut magna rhoncus luctus. Pellentesque rutrum nulla sit amet nulla rhoncus '),(4,NULL,5,6,'Photography','photography','','content','Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed convallis dignissim metus eget bibendum.'),(5,NULL,7,8,'Print','print','','content','Vivamus quis eros eros. Nulla ullamcorper, lorem eu hendrerit fermentum, sapien purus molestie mauris, malesuada gravida nibh ipsum sed neque.'),(6,NULL,10,11,'About Us','about-us','','content','');
/*!40000 ALTER TABLE `bb_category` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-01-31 11:43:40
