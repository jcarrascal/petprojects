<?php


define('DEBUG', true);
define('LIBRARY_PATH', dirname(__FILE__) . '/../blackbirdcms');
define('APPLICATION_PATH', dirname(__FILE__) . '/../application');
define('ROOT_URL', '');
define('CONFIG_PATH', APPLICATION_PATH . '/config/config.sample.php');


require LIBRARY_PATH . '/bootstrap.php';
$bootstrap = new BB_Bootstrap();
$bootstrap->run(CONFIG_PATH);
