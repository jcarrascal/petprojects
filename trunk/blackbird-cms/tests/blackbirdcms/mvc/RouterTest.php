<?php

require_once dirname(__FILE__) . '/../../../blackbirdcms/mvc/router.php';

class RouterTest extends PHPUnit_Framework_TestCase
{
	public function testThatParseCreatesUsablePatternForSimpleRoute()
	{
		$pattern = Router::parse('/:controller/:action/*', array());
		$this->assertTrue(preg_match($pattern, '/hello/world/', $matches) != 0);
		$this->assertEquals($matches['controller'], 'hello');
		$this->assertEquals($matches['action'], 'world');

		$this->assertTrue(preg_match($pattern, '/hello/cruel/world/', $matches) != 0);
		$this->assertEquals($matches['controller'], 'hello');
		$this->assertEquals($matches['action'], 'cruel');

		$this->assertFalse(preg_match($pattern, '/hello/world!/', $matches) != 0);
	}

	public function testThatParseCreatesPatternForCustomRoute()
	{
		$pattern = Router::parse('/:lang/:year/:month/:day', array(
			'lang'  => '[a-z]{2}',
			'year'  => '[0-9]{4}',
			'month' => '[0-9]{2}',
			'day'   => '[0-9]{2}',
		));
		$this->assertTrue(preg_match($pattern, '/en/2011/01/17', $matches) != 0);
		$this->assertEquals($matches['lang'], 'en');
		$this->assertEquals($matches['year'], '2011');
		$this->assertEquals($matches['month'], '01');
		$this->assertEquals($matches['day'], '17');

		$this->assertFalse(preg_match($pattern, '/spa/2011/01/17', $matches) != 0);
		$this->assertFalse(preg_match($pattern, '/en/201a/01/17', $matches) != 0);
		$this->assertFalse(preg_match($pattern, '/en/2011/0a/17', $matches) != 0);
		$this->assertFalse(preg_match($pattern, '/en/2011/01/1a', $matches) != 0);
	}
}

