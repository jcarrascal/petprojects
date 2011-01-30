<?php

define('LIBRARY_PATH', dirname(__FILE__) . '/../../../blackbirdcms/');
require_once LIBRARY_PATH . '/mvc/request.php';

class RequestTest extends PHPUnit_Framework_TestCase
{
	function testThatHasWriteAccessToSuperGlobalVariables()
	{
		$array = array();
		$request = new BB\MVC\Request($array);
		$request->request['hello'] = 'world';
		$this->assertEquals('world', $array['hello']);
	}
}
