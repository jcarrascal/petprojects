<?php

namespace BB\MVC;

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

	private static $mRoutes = array();
	private static $mKnownPatterns = array(
		'module' => Router::WORD,
		'controller' => Router::WORD,
		'action' => Router::WORD,
		'year' => Router::INTEGER,
		'month' => Router::INTEGER,
		'day' => Router::INTEGER,
	);

	/**
	 * Removes existing routes.
	 */
	static function clear()
	{
		Router::$mRoutes = array();
	}

	/**
	 * Append a new route to the end of the routes list. This new route will be
	 * tried last and will have lower precedence. For information on the
	 * allowed syntax for the route string see parse().
	 * @param string $route A well formed route string.
	 * @param array $defaults Default values for optional indexes.
	 * @param array $options Character classes for custom indexes.
	 */
	static function append($route, $defaults=array(), $options=array())
	{
		Router::$mRoutes[] = array(Router::parse($route, $options), $defaults);
	}

	/**
	 * Prepend a new route to the end of the routes list. This new route will
	 * be tried first and will have higher precedence. For information on the
	 * allowed syntax for the route string see parse().
	 * @param string $route A well formed route string.
	 * @param array $defaults Default values for optional indexes.
	 * @param array $options Character classes for custom indexes.
	 */
	static function prepend($route, $defaults=array(), $options=array())
	{
		array_unshift(Router::$mRoutes, array(Router::parse($route, $options), $defaults));
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
				else if (isset(Router::$mKnownPatterns[$name]))
					$pattern = Router::$mKnownPatterns[$name];
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

	/**
	 * Returns the corresponding controller/action for the given $url by
	 * matching all configured routes in order.
	 * @param string $url
	 * @return array
	 */
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
				if (isset($values['params']))
					$values['params'] = preg_split('|/|', $values['params'], 0, PREG_SPLIT_NO_EMPTY);
				return $values;
			}
		}
		return false;
	}
}
