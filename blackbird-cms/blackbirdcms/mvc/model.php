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

require_once LIBRARY_PATH . '/sql/connection.php';

class Model
{
	private $mConfig;

	function __construct($config)
	{
		$this->mConfig = $config;
	}

	function connect($database='default')
	{
		return \BB\SQL\Connection::connect($this->mConfig['databases'][$database]);
	}

	/**
	 * Returns the value of the specified configuration parameter.
	 * @param string $name The name of the parameter.
	 * @return mixed The value for the parameter or null if it doesn't exists.
	 */
	function config($name)
	{
		if (isset($this->mConfig[$name]))
			return $this->mConfig[$name];
		return null;
	}
}
