<?php

namespace BB\MVC;

require_once dirname(__FILE__) . '/../../../blackbirdcms/mvc/request.php';

class RequestTest extends \PHPUnit_Framework_TestCase
{
	function testThatHasWriteAccessToSuperGlobalVariables()
	{
		$array = array();
		$request = new Request($array);
		$request->request['hello'] = 'world';
		$this->assertEquals('world', $array['hello']);
	}
}
