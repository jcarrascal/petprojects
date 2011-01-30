<?php

define('LIBRARY_PATH', dirname(__FILE__) . '/../../../blackbirdcms/');
require_once LIBRARY_PATH . '/mvc/router.php';

class RouterTest extends PHPUnit_Framework_TestCase
{

	function testThatParseCreatesUsablePatternForSimpleRoute()
	{
		$router = new BB\MVC\Router();
		$pattern = $router->parse('/:controller/:action/*', array());
		$this->assertTrue(preg_match($pattern, '/hello/world/', $matches) != 0, $pattern);
		$this->assertEquals('hello', $matches['controller']);
		$this->assertEquals('world', $matches['action']);

		$this->assertTrue(preg_match($pattern, '/hello/cruel/world/', $matches) != 0, $pattern);
		$this->assertEquals('hello', $matches['controller']);
		$this->assertEquals('cruel', $matches['action']);

		$this->assertFalse(preg_match($pattern, '/hello/world!/', $matches) != 0, $pattern);
	}

	function testThatParseCreatesPatternForCustomRoute()
	{
		$router = new BB\MVC\Router();
		$pattern = $router->parse('/:lang/:year/:month/:day', array(
				'lang' => '[a-z]{2}',
				'year' => '[0-9]{4}',
				'month' => '[0-9]{2}',
				'day' => '[0-9]{2}',
			));
		$this->assertTrue(preg_match($pattern, '/en/2011/01/17', $matches) != 0, $pattern);
		$this->assertEquals('en', $matches['lang']);
		$this->assertEquals('2011', $matches['year']);
		$this->assertEquals('01', $matches['month']);
		$this->assertEquals('17', $matches['day']);

		$this->assertFalse(preg_match($pattern, '/spa/2011/01/17', $matches) != 0, $pattern);
		$this->assertFalse(preg_match($pattern, '/en/201a/01/17', $matches) != 0, $pattern);
		$this->assertFalse(preg_match($pattern, '/en/2011/0a/17', $matches) != 0, $pattern);
		$this->assertFalse(preg_match($pattern, '/en/2011/01/1a', $matches) != 0, $pattern);
	}

	function testThatParseCreatesPatternWithOptionalSegments()
	{
		$router = new BB\MVC\Router();
		$pattern = $router->parse('/[:lang/]:controller/:action/', array('lang' => '[a-z]{2}'));
		$this->assertTrue(preg_match($pattern, '/en/contact/form/', $matches) != 0, $pattern);
		$this->assertEquals('en', $matches['lang']);
		$this->assertEquals('contact', $matches['controller']);
		$this->assertEquals('form', $matches['action']);

		$this->assertTrue(preg_match($pattern, '/contact/form/', $matches) != 0, $pattern);
		$this->assertEquals('', $matches['lang']);
		$this->assertEquals('contact', $matches['controller']);
		$this->assertEquals('form', $matches['action']);

		$this->assertFalse(preg_match($pattern, '/contact/', $matches) != 0, $pattern);
	}

	function testThatMatchesWithOrWithoutTrailingSlash()
	{
		$router = new BB\MVC\Router();
		$pattern = $router->parse('/:controller/:action/', array('lang' => '[a-z]{2}'));
		$this->assertTrue(preg_match($pattern, '/products/list', $matches) != 0, $pattern);
		$this->assertTrue(preg_match($pattern, '/products/list/', $matches) != 0, $pattern);

		$pattern = $router->parse('/:controller/:action/*', array('lang' => '[a-z]{2}'));
		$this->assertTrue(preg_match($pattern, '/products/list', $matches) != 0, $pattern);
		$this->assertTrue(preg_match($pattern, '/products/list/', $matches) != 0, $pattern);
		$this->assertTrue(preg_match($pattern, '/products/list/10', $matches) != 0, $pattern);
		$this->assertTrue(preg_match($pattern, '/products/list/10/', $matches) != 0, $pattern);
	}

	function testThatRoutesToControllerAction()
	{
		$router = new BB\MVC\Router();
		$router->clearRoutes();
		$router->appendRoute('/:controller/:action/*');
		$values = $router->route('/hello/world/');
		$this->assertEquals('hello', $values['controller']);
		$this->assertEquals('world', $values['action']);
		$this->assertEquals(array(), $values['params']);

		$values = $router->route('/hello/world/1/2/3');
		$this->assertEquals('hello', $values['controller']);
		$this->assertEquals('world', $values['action']);
		$this->assertEquals(array(1, 2, 3), $values['params']);
	}

	function testThatRoutesToModules()
	{
		$router = new BB\MVC\Router();
		$router->clearModulePaths();
		$router->appendModulePath(LIBRARY_PATH . '/modules');

		$router->clearRoutes();
		$router->appendRoute('/:module/:controller[/:action]/*', array('action' => 'index'));
		$router->appendRoute('/:controller[/:action]/*', array('action' => 'index'));

		$values = $router->route('/articles/index/edit/');
		$this->assertEquals('articles', $values['module']);
		$this->assertEquals('index', $values['controller']);
		$this->assertEquals('edit', $values['action']);

		$values = $router->route('/products/edit/');
		$this->assertEquals('products', $values['controller']);
		$this->assertEquals('edit', $values['action']);

		$values = $router->route('/products/');
		$this->assertEquals('products', $values['controller']);
		$this->assertEquals('index', $values['action']);
	}
}
