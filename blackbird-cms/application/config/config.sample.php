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
);

if (defined('DEBUG'))
{
	$config['databases']['default']['host'] = 'localhost';
	$config['databases']['default']['username'] = 'root';
	$config['databases']['default']['password'] = '123456';
	$config['databases']['default']['database'] = 'blackbirdcms';
}

return $config;
