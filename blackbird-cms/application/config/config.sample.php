<?php

$config = array(
	'databases' => array(
		'default' => array(
			'driver' => 'mysql',
			'host' => 'mysql.example.com',
			'username' => 'secret',
			'password' => 'password',
			''
		),
	),
	'view' => array(
		'theme' => 'default',
		'metaAuthor' => 'Julio César Carrascal Urquijo <jcarrascal@gmail.com>',
		'metaDescription' => 'Blackbird is a new content management system for PHP5',
		'googleAnalytics' => 'UA-XXXXX-X', // change the UA-XXXXX-X to be your site's ID
	),
);

if (defined('DEBUG'))
{
	$config['databases']['default']['host'] = 'localhost';
	$config['databases']['default']['username'] = 'root';
	$config['databases']['default']['password'] = '123456';
	$config['databases']['default']['database'] = 'blackbirdcms';
}

return $config;
