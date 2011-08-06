DROP TABLE IF EXISTS `#__massmailer_recipients`;
CREATE TABLE  `#__massmailer_recipients` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(100) NOT NULL,
  `lastname` varchar(100) NOT NULL,
  `email` varchar(200) NOT NULL,
  `gender` enum('', 'M', 'F') NOT NULL DEFAULT '',
  `date_of_birth` datetime,
  `neighborhood` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `AK_massmailer_cellphone` (`email`) USING BTREE
);

DROP TABLE IF EXISTS `#__massmailer_messages`;
CREATE TABLE  `#__massmailer_messages` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `from_email` varchar(200) NOT NULL,
  `from_name` varchar(100) NOT NULL,
  `subject` varchar(200) NULL,
  `content` longtext NULL,
  `created_on` DATETIME NOT NULL,
  PRIMARY KEY (`id`)
);

DROP TABLE IF EXISTS `#__massmailer_emails`;
CREATE TABLE  `#__massmailer_emails` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `message_id` int(10) NOT NULL,
  `recipient` varchar(200) NOT NULL,
  `variables` longtext NULL,
  `process` varchar(100) NULL,
  `started_on` DATETIME NULL,
  `sent_on` DATETIME NULL,
  PRIMARY KEY (`id`)
);
