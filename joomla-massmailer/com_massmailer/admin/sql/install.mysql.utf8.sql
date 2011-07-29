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
  `code` varchar(100) NOT NULL,
  `text` varchar(700) NOT NULL,
  `recipients` longtext NOT NULL,
  `sent_on` DATETIME NOT NULL,
  `status_message` varchar(200) NULL,
  `request` longtext NULL,
  `response` longtext NULL,
  PRIMARY KEY (`id`)
);
