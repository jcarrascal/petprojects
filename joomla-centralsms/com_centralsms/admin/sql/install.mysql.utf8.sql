DROP TABLE IF EXISTS `#__centralsms`;
CREATE TABLE  `#__centralsms` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(100) NOT NULL,
  `lastname` varchar(100) NOT NULL,
  `neighborhood` varchar(100) NOT NULL DEFAULT '',
  `cellphone` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `AK_centralsms_cellphone` (`cellphone`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
