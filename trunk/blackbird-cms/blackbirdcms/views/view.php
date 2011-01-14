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

namespace BB\Views;

/**
 * Exception class thrown
 */
class ViewException extends \Exception
{
}

class View
{
	private $mConfig;
	private $mCurrentBlocks;
	private $mBlockContents;
	private $mProperties;
	private $mInherits;

	function  __construct($config)
	{
		$this->mConfig = (array)$config;
		$this->mCurrentBlocks = array();
		$this->mBlockContents = array();
		$this->mProperties = array();
		$this->mInherits = null;
	}

	function __set($name, $value)
	{
		$this->mProperties[$name] = $value;
	}

	function __get($name)
	{
		if (isset($this->mProperties[$name]))
			return htmlentities($this->mProperties[$name], ENT_COMPAT, 'UTF-8');
		return null;
	}

	function getRaw($name)
	{
		if (isset($this->mProperties[$name]))
			return $this->mProperties[$name];
		return null;
	}

	function config($name)
	{
		if (isset($this->mConfig[$name]))
			return htmlentities($this->mConfig[$name], ENT_COMPAT, 'UTF-8');
		return null;
	}

	function inherits($template, $type='html')
	{
		if ($this->mInherits != null)
			throw new ViewException("Only single inheritance is supported.");
		$this->mInherits = array($template, $type);
	}

	function render($template, $type='html')
	{
		$this->mInherits = null;
		$filename = $this->locateFilename($template, $type);
		$contents = $this->captureTemplate($filename);
		while ($this->mInherits != null)
		{
			list($template, $type) = $this->mInherits;
			$this->mInherits = null;
			$filename = $this->locateFilename($template, $type);
			$contents = $this->captureTemplate($filename);
		}
		return $contents;
	}

	function locateFilename($template, $type)
	{
		foreach ($this->mConfig['templatePaths'] as $templatePath)
		{
			$filename = "$templatePath/$template.$type.php";
			if (is_readable($filename))
				return $filename;
		}
		throw new ViewException("Template '$template.$type.php' wasn't found in templatePaths: " . serialize($this->config['templatePaths']));
	}

	function hasContent($blockName)
	{
		return isset($this->mBlockContents[$blockName]);
	}

	function content($blockName)
	{
		if (isset($this->mBlockContents[$blockName]))
			return $this->mBlockContents[$blockName];
		return null;
	}

	function beginBlock($blockName)
	{
		array_push($this->mCurrentBlocks, $blockName);
		ob_start();
	}

	function endBlock()
	{
		$blockName = array_pop($this->mCurrentBlocks);
		$contents = $this->mBlockContents[$blockName] = ob_get_contents();
		ob_end_clean();
		return $contents;
	}

	private function captureTemplate($filename)
	{
		ob_start();
		include $filename;
		$contents = ob_get_contents();
		ob_end_clean();
		return $contents;
	}
}