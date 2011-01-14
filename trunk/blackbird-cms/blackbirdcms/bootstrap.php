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


/**
 * Initializes the framework and loads configuration settings.
 */
class BB_Bootstrap
{
	function  __construct()
	{
		if (DEBUG)
		{
			$this->requestStarted = explode(' ', microtime());
			error_reporting(E_ALL | E_STRICT);
			ini_set('display_errors', true);
		}
	}

	function run()
	{
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
