<?php


define('DEBUG', true);
define('LIBRARY_PATH', dirname(__FILE__) . '/../blackbirdcms');
define('APPLICATION_PATH', dirname(__FILE__) . '/../application');
define('ROOT_URL', '');
define('CONFIG_PATH', APPLICATION_PATH . '/config/config.sample.php');


require LIBRARY_PATH . '/views/view.php';
$view = new BB\Views\View(array(
	'templatePaths' => array(APPLICATION_PATH . '/components/articles/templates'),
	'layoutPaths' => array(APPLICATION_PATH . '/themes/default')
));
echo $view->render('index');

require LIBRARY_PATH . '/bootstrap.php';
$bootstrap = new BB\Bootstrap();
$bootstrap->run(CONFIG_PATH);

