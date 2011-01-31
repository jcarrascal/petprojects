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

require_once LIBRARY_PATH . '/mvc/view.php';
require_once LIBRARY_PATH . '/mvc/model.php';

/**
 * Base class for controller classes. Provides access to $_REQUEST, $_GET,
 * $_POST, $_FILES and configuration arrays through protected properties to
 * allow replacing them when testing a controller.
 */
class Controller
{
	var $request;
	var $config;
	var $viewData = array();

	/**
	 * Initializes a new instance of the Controller class.
	 * @param Request $request
	 * @param array $config
	 */
	function __construct($request=null, $config=null)
	{
		$this->request = $request;
		$this->config = $config;
	}

	/**
	 * Last chance to override the route values to redirect the request to
	 * another action.
	 * @param array $values
	 * @return array
	 */
	function route($values)
	{
		return $values;
	}

	/**
	 * In this controller try to invoke the action specified.
	 * @param string $action
	 * @param array $values
	 * @return mixed
	 */
	function invoke($action, &$values)
	{
		$params = $values;
		unset($params['module']);
		unset($params['controller']);
		unset($params['action']);
		if (count($params['params']) > 0)
			$result = \call_user_func_array(array($this, $action), $params['params']);
		else
			$result = \call_user_func(array($this, $action));
		if (is_object($result))
			return $result->execute($this);
		return false;
	}

	protected function transfer($action, $controller=null, $module=null)
	{
		$values = $request->data['values'][0];
		return new TransferResult($action, $controller == null ? $values['controller'] : $controller, $module);
	}

	protected function view($template=null, $type='html')
	{
		$values = $this->request->peekData('routing');
		if ($template == null)
			$template = $values['action'];
		return new ViewResult($this->config['view'], "{$values['controller']}/$template", $type);
	}
}

class TransferResult
{
	private $mValues;

	function __construct($action, $controller, $module=null)
	{
		$this->mValues = array('action' => $action, 'controller' => $controller);
		if ($module != null)
			$this->mValues['module'] = $module;
	}

	function execute($controller)
	{
		return $this->mValues;
	}
}

class ViewResult
{
	private $mTemplate;
	private $mType;

	function __construct($config, $template, $type)
	{
		$this->mTemplate = $template;
		$this->mType = $type;
	}

	function execute($controller)
	{
		$view = new View($controller->config['view'], $controller->viewData);
		echo $view->render($this->mTemplate, $this->mType);
	}
}
