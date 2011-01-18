<?php

//namespace BB\MVC;


class Router
{
	const WORD = '[A-Za-z_][A-Za-z0-9]*';
	const INTEGER = '[0-9]+';

	static $mRoutes = array();
	static $mPatterns = array(
		'module' => Router::WORD,
		'controller' => Router::WORD,
		'action' => Router::WORD,
		'year' => Router::INTEGER,
		'month' => Router::INTEGER,
		'day' => Router::INTEGER,
	);

	static function clear()
	{
		Router::$mRoutes = array();
	}

	static function append($route, $defaults=array(), $options=array())
	{
		Router::$mRoutes[] = array(Router::parse($route, $options), $defaults);
	}

	static function prepend($route, $defaults=array(), $options=array(), $parse=true)
	{
		array_unshift(Router::$mRoutes, array(Router::parse($route, $options), $defaults));
	}

	static function parse($route, $options)
	{
		$route = str_replace(']', ')?', str_replace('[', '(?:', $route));
		if (preg_match_all('/(:[A-Za-z0-9_]+)/', $route, $matches))
		{
			foreach ($matches[0] as $match)
			{
				$name = substr($match, 1);
				if (isset($options[$name]))
					$pattern = $options[$name];
				else if (isset(Router::$mPatterns[$name]))
					$pattern = Router::$mPatterns[$name];
				else
					$pattern = Router::WORD;
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

	function route($url)
	{
		foreach (Router::$mRoutes as $route)
		{
			list ($pattern, $defaults) = $route;
			if (preg_match($pattern, $url, $values))
			{
				foreach ($defaults as $key => $value)
				{
					if (!isset($values[$key]) || $values[$key] == '')
						$values[$key] = $value;
				}
				return $values;
			}
		}
		return false;
	}
}
