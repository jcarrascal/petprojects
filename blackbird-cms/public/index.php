<?php

define('DEBUG', true);
define('LIBRARY_PATH', dirname(__FILE__) . '/../blackbirdcms');
define('APPLICATION_PATH', dirname(__FILE__) . '/../application');
define('ROOT_URL', '');
define('CONFIG_PATH', APPLICATION_PATH . '/config/config.sample.php');


require LIBRARY_PATH . '/bootstrap.php';
$bootstrap = new BB\Bootstrap();
$bootstrap->run(CONFIG_PATH);

require LIBRARY_PATH . '/mvc/router.php';
var_dump($pattern = Router::parse('/:controller/:action/*', array()));
var_dump($url = '/hello/world/', preg_match($pattern, $url, $matches), $matches);
var_dump($url = '/hello/cruel/world/', preg_match($pattern, $url, $matches), $matches);

