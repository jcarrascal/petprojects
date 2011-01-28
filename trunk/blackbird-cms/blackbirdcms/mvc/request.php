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

class Request implements \ArrayAccess
{
	var $mData = array();

	var $request;
	var $get;
	var $post;
	var $files;
	var $session;
	var $cookie;
	var $server;
	var $env;
	var $isSSL;
	var $isAjax;
	var $isFlash;
	var $isMobile;

	/**
	 * Initializes a new instance of the Request class.
	 * @param array $request
	 * @param array $get
	 * @param array $post
	 * @param array $files
	 * @param array $sesion
	 * @param array $cookie
	 * @param array $server
	 * @param array $env
	 */
	function __construct(&$request=array(), $get=array(), $post=array(), $files=array(), $session=array(), $cookie=array(), $server=array(), $env=array())
	{
		$this->request = & $request;
		$this->get = & $get;
		$this->post = & $post;
		$this->files = & $files;
		$this->session = & $session;
		$this->cookie = & $cookie;
		$this->server = & $server;
		$this->env = & $env;

		$this->isSSL = isset($this->server['HTTPS']) && $this->server['HTTPS'] == 'on';
		$this->isAjax = isset($this->server['HTTP_X_REQUESTED_WITH']) &&
			strpos($this->server['HTTP_X_REQUESTED_WITH'], 'XMLHttpRequest') !== false;
		$this->isFlash = isset($this->server['HTTP_USER_AGENT']) &&
			preg_match('/^(Shockwave|Adobe) Flash/', $this->server['HTTP_USER_AGENT']) != 0;
		$this->isMobile = isset($this->server['HTTP_USER_AGENT']) &&
			preg_match('/Android|AvantGo|BlackBerry|DoCoMo|Fennec|iPod|iPhone|J2ME|MIDP|NetFront|' .
				'Nokia|Opera Mini|PalmOS|PalmSource|portalmmm|PluckerReqwirelessWeb|SonyEricsson|' .
				'Symbian|UP\\.Browser|webOS|Windows CE|Xiino/', $this->server['HTTP_USER_AGENT']);
	}

	/**
	 * Store data for later use in the same request.
	 * @param string $key
	 * @param mixed $value
	 */
	function pushData($key, $value)
	{
		if (!isset($this->mData[$key]))
			$this->mData[$key] = array();
		array_unshift($this->mData[$key], $value);
	}

	/**
	 * Retrieve and remove data stored for use in the same request.
	 * @param string $key
	 * @return mixed
	 */
	function popData($key)
	{
		return array_shift($this->mData[$key], $value);
	}

	/**
	 * Retrieve data stored for use in the same request.
	 * @param string $key
	 * @return mixed
	 */
	function peekData($key)
	{
		return $this->mData[$key][0];
	}

	/**
	 * Whether a offset exists
	 * @param string $offset
	 * @return bool
	 */
	function offsetExists($offset)
	{
		return isset($this->request[$offset]);
	}

	/**
	 * Offset to retrieve
	 * @param string $offset
	 * @return mixed
	 */
	function offsetGet($offset)
	{
		if (isset($this->request[$offset]))
			return $this->request[$offset];
		return null;
	}

	/**
	 * Offset to set
	 * @param string $offset
	 * @param mixed $value
	 */
	function offsetSet($offset, $value)
	{
		$this->request[$offset] = $value;
	}

	/**
	 * Offset to unset
	 * @param string $offset
	 */
	function offsetUnset($offset)
	{
		unset($this->request[$offset]);
	}
}
