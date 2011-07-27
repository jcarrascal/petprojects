DROP TABLE IF EXISTS `#__centralsms_recipients`;
CREATE TABLE  `#__centralsms_recipients` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(100) NOT NULL,
  `lastname` varchar(100) NOT NULL,
  `neighborhood` varchar(100) NOT NULL DEFAULT '',
  `country` integer NOT NULL DEFAULT 57,
  `cellphone` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `AK_centralsms_cellphone` (`cellphone`)
) ENGINE=InnoDB;

DROP TABLE IF EXISTS `#__centralsms_messages`;
CREATE TABLE  `#__centralsms_messages` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `code` varchar(100) NOT NULL,
  `text` varchar(700) NOT NULL,
  `recipients` longtext NOT NULL,
  `sent_on` DATETIME NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;
