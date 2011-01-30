<?php

/*
 * Blackbird CMS - Content management system for PHP5
 * Copyright (C) 2011 Julio César Carrascal Urquijo
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace BB\MVC;

require_once LIBRARY_PATH . '/mvc/controller.php';

class HttpException extends \Exception
{

}

/** Thrown when the specified action hasn't been located, is private or requires authentication. */
class Http401UnauthorizedException extends HttpException
{

}

/** Thrown when the request can't be routed to a controller or the specified controller can't be located. */
class Http404NotFoundException extends HttpException
{

}

/**
 * Routes requests to their corresponding controller/action.
 * @author Julio César Carrascal Urquijo <jcarrascal@gmail.com>
 */
class Router
{
	/** Matches alphanumeric characters and underscore. */
	const WORD = '[A-Za-z_][A-Za-z0-9_]*';

	/** Matches only digits. */
	const INTEGER = '[0-9]+';

	private $mConfig;
	private $mRoutes = array();
	private $mKnownPatterns = array(
		'module' => Router::WORD,
		'controller' => Router::WORD,
		'action' => Router::WORD,
		'year' => Router::INTEGER,
		'month' => Router::INTEGER,
		'day' => Router::INTEGER,
	);
	private $mControllerPaths = array();
	private $mModulePaths = array();

	function __construct($config=array())
	{
		$this->mConfig = $config;
	}

	/**
	 * Removes existing routes.
	 * @return Router
	 */
	function clearRoutes()
	{
		$this->mRoutes = array();
		return $this;
	}

	/**
	 * Append a new route to the end of the routes list. This new route will be
	 * tried last and will have lower precedence. For information on the
	 * allowed syntax for the route string see parse().
	 * @param string $route A well formed route string.
	 * @param array $defaults Default values for optional indexes.
	 * @param array $options Character classes for custom indexes.
	 * @return Router
	 */
	function appendRoute($route, $defaults=array(), $options=array())
	{
		$this->mRoutes[] = array($this->parse($route, $options), $defaults);
		return $this;
	}

	/**
	 * Prepend a new route to the begining of the routes list. This new route
	 * will be tried first and will have higher precedence. For information on
	 * the allowed syntax for the route string see parse().
	 * @param string $route A well formed route string.
	 * @param array $defaults Default values for optional indexes.
	 * @param array $options Character classes for custom indexes.
	 * @return Router
	 */
	function prependRoute($route, $defaults=array(), $options=array())
	{
		array_unshift($this->mRoutes, array($this->parse($route, $options), $defaults));
		return $this;
	}

	/**
	 * Removes existing controller paths.
	 * @return Router
	 */
	function clearControllerPaths()
	{
		$this->mControllerPaths = array();
		return $this;
	}

	/**
	 * Append a new controller path to the end of the list. This new controller
	 * path will be tried last and will have lower precedence.
	 * @param string $path
	 * @return Router
	 */
	function appendControllerPath($path)
	{
		$this->mControllerPaths[] = $path;
		return $this;
	}

	/**
	 * Prepend a new controller path to the begining of the list. This new
	 * controller path will be tried first and will have higher precedence.
	 * @param string $path
	 * @return Router
	 */
	function prependControllerPath($path)
	{
		array_unshift($this->mControllerPaths, $path);
		return $this;
	}

	/**
	 * Removes existing module paths.
	 * @return Router
	 */
	function clearModulePaths()
	{
		$this->mModulePaths = array();
		return $this;
	}

	/**
	 * Append a new module path to the end of the list. This new module path
	 * will be tried last and will have lower precedence.
	 * @param string $name
	 * @param string $path
	 * @return Router
	 */
	function appendModulePath($path)
	{
		$this->mModulePaths[] = $path;
		return $this;
	}

	/**
	 * Prepend a new module path to the begining of the list. This new module
	 * path will be tried first and will have higher precedence.
	 * @param string $path
	 * @return Router
	 */
	function prependModule($path)
	{
		array_unshift($this->mModulePaths, $path);
		return $this;
	}

	/**
	 * Parses the $route string into a PCRE regular expression that will be
	 * used to map URL request to the corresponding controller/action. The
	 * allowed syntax for the route string is:
	 * {{{
	 * /:word    Will capture alphanumeric characters inside the 'word' index.
	 *           You can change the regexp for word by passing in the $options
	 *           parameter something like:
	 *           array('word' =&gt; '[a-z]+')
	 * [/:word]  Will make the 'word' index optional. You can pass a default
	 *           value in $defaults like this:
	 *           array('word' =&gt; 'word')
	 * /*        At the end of the route will mean that extra parameters can be
	 *           added to the url and will be stored in the 'params' index.
	 * }}}
	 * @param string $route A well formed route string.
	 * @param array $options Character classes for custom indexes.
	 * @return string
	 */
	function parse($route, $options)
	{
		$route = str_replace(']', ')?', str_replace('[', '(?:', $route));
		if (preg_match_all('/(:[A-Za-z0-9_]+)/', $route, $matches))
		{
			foreach ($matches[0] as $match)
			{
				$name = substr($match, 1);
				if (isset($options[$name]))
					$pattern = $options[$name];
				else if (isset($this->mKnownPatterns[$name]))
					$pattern = $this->mKnownPatterns[$name];
				else
					$pattern = $this->WORD;
				$route = str_replace($match, "(?P<$name>$pattern)", $route);
			}
		}
		if (substr($route, -2) == '/*')
			$route = substr($route, 0, -2) . '(?P<params>/.*)?';
		else if (substr($route, -1) == '/')
			$route .= '?';
		else
			$route .= '/?';
		return "|^$route$|";
	}

	/**
	 * Returns the corresponding controller/action for the given $requestUri by
	 * matching all configured routes in order.
	 * @param string $requestUri
	 * @return array
	 */
	function route($requestUri)
	{
		foreach ($this->mRoutes as $route)
		{
			list ($pattern, $defaults) = $route;
			if (preg_match($pattern, $requestUri, $values))
			{
				if (isset($values['module']) && !$this->isModule($values['module']))
					continue;
				foreach ($defaults as $key => $value)
				{
					if (!isset($values[$key]) || $values[$key] == '')
						$values[$key] = $value;
				}
				if (isset($values['params']))
					$values['params'] = preg_split('|/|', $values['params'], 0, PREG_SPLIT_NO_EMPTY);
				return $values;
			}
		}
		return false;
	}

	function dispatch($request)
	{
		$requestUri = $request->server['REQUEST_URI'];

		$values = $this->route($requestUri);
		if ($values === false)
			throw new Http404NotFoundException("Can't route the $requestUri url.", 404);

		do
		{
			$request->pushData('routing', $values);
			$controller = $this->locateController($values, $request);
			if (!is_object($controller))
				throw new Http404NotFoundException("Can't find the {$values['controller']} controller.", 404);

			$values = $controller->route($values);
			$action_name = $this->locateAction($values, $controller);
			if ($action_name === false)
				throw new Http401UnauthorizedException("You are not authorized to call the {$values['action']} action.");

			$continue = $controller->invoke($action_name, $values);
		}
		while ($continue);
	}

	protected function locateController($values, $request)
	{
		$found = true;
		$controller = $values['controller'];
		if (isset($values['module']))
		{
			$module = $values['module'];
			foreach ($this->mModulePaths as $path)
			{
				$filename = "$path/$module/controllers/{$controller}controller.php";
				if (is_readable($filename))
				{
					$found = true;
					break;
				}
			}
		}
		else
		{
			foreach ($this->mControllerPaths as $path)
			{
				$filename = "$path/{$controller}controller.php";
				if (is_readable($filename))
				{
					$found = true;
					break;
				}
			}
		}
		if (!$found)
			return false;
		require_once $filename;
		$class_name = $controller . 'Controller';
		if (!\class_exists($class_name))
			return false;
		return new $class_name($request, $this->mConfig);
	}

	protected function locateAction($values, $controller)
	{
		$action_name = $values['action'] . 'Action';
		if (!\method_exists($controller, $action_name))
			return false;
		return $action_name;
	}

	private function isModule($name)
	{
		foreach ($this->mModulePaths as $path)
		{
			if (is_dir("$path/$name/controllers"))
				return true;
		}
		return false;
	}
}
