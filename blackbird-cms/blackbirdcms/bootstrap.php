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

namespace BB;

/**
 * Initializes the framework and loads configuration settings.
 */
class Bootstrap
{
	private $mRouter;
	private $mRequest;
	private $mConfig;

	function __construct()
	{
		if (DEBUG)
		{
			$this->requestStarted = explode(' ', microtime());
			error_reporting(E_ALL | E_STRICT);
			ini_set('display_errors', true);
		}
	}

	function loadConfiguration($filename)
	{
		\ob_start();
		$this->mConfig = include($filename);
		\ob_end_clean();
		return $this->mConfig;
	}

	function getRouter()
	{
		if ($this->mRouter == null)
		{
			require LIBRARY_PATH . '/mvc/router.php';
			$this->mRouter = new MVC\Router($this->mConfig);
			$this->initializeControllers($this->mRouter);
			$this->initializeModules($this->mRouter);
			$this->initializeRoutes($this->mRouter);
		}
		return $this->mRouter;
	}

	protected function initializeControllers($router)
	{
		$router->clearControllerPaths();
		$router->appendControllerPath(APPLICATION_PATH . '/controllers/');
		$router->appendControllerPath(LIBRARY_PATH . '/controllers/');
	}

	protected function initializeModules($router)
	{
		$router->clearModulePaths();
		$router->appendModulePath(APPLICATION_PATH . '/modules/');
		$router->appendModulePath(LIBRARY_PATH . '/modules/');
	}

	protected function initializeRoutes($router)
	{
		$router->clearRoutes();
		$router->appendRoute('/:module/:controller[/:action]/*', array('action' => 'index'));
		$router->appendRoute('/:controller[/:action]/*', array('action' => 'index'));
		$router->appendRoute('/', array('controller' => 'index', 'action' => 'index'));
	}

	function getRequest()
	{
		if ($this->mRequest == null)
		{
			session_start();
			require LIBRARY_PATH . '/mvc/request.php';
			$this->mRequest = new MVC\Request($_REQUEST, $_GET, $_POST,
					$_FILES, $_SESSION, $_COOKIE, $_SERVER, $_ENV);
		}
		return $this->mRequest;
	}

	function run()
	{
		$router = $this->getRouter();
		$request = $this->getRequest();
		try
		{
			$router->dispatch($request);
		}
		catch (Exception $ex)
		{
			$router->dispatchError($request, $ex);
		}
		$this->postResponse();
	}

	function postResponse()
	{
		if (DEBUG)
		{
			$this->responseSent = explode(' ', microtime());
			$totalTime = ($this->requestStarted[1] - $this->requestStarted[1]) + ($this->requestStarted[0] - $this->requestStarted[0]);
			echo "\n<!--\n";
			echo "Page rendered in $totalTime seconds\n";
			echo "-->";
		}
	}
}
