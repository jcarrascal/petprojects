<?php

$config = array(
	'databases' => array(
		'default' => 'mysqli://secret+password@mysql.example.com/blackbirdcms',
	),
	'view' => array(
		'templatePaths' => array(
			APPLICATION_PATH . '/templates',
			LIBRARY_PATH . '/modules/content/templates'
		),
		'layoutPaths' => array(LIBRARY_PATH . '/themes/default'),
		'metaAuthor' => 'Julio César Carrascal Urquijo <jcarrascal@gmail.com>',
		'metaDescription' => 'Blackbird is a new content management system for PHP5',
		'googleAnalytics' => 'UA-XXXXX-X', // change the UA-XXXXX-X to be your site's ID
	),
);

if (defined('DEBUG'))
{
	$config['databases']['default'] = 'mysqli://root@localhost/blackbirdcms';
}

return $config;
