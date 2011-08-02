DROP TABLE IF EXISTS `#__testimonies_posts`;
CREATE TABLE  `#__testimonies_posts` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `neighborhood` varchar(100) NOT NULL DEFAULT '',
  `message` longtext NOT NULL,
  `picture` varchar(200) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `AK_testimonies_posts_email` (`email`) USING BTREE
);
