<?php


class BB_Views_ViewException extends Exception
{
}

class BB_Views_View
{
	private $mConfig;
	private $mCurrentBlocks;
	private $mBlockContents;
	private $mProperties;

	function  __construct($config)
	{
		$this->mConfig = (array)$config;
		$this->mCurrentBlocks = array();
		$this->mBlockContents = array();
		$this->mProperties = array();
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

	function render($template, $type='html')
	{
		$filename = $this->locateFilename($template, $type);
		return $this->captureTemplate($filename);
	}

	function locateFilename($template, $type)
	{
		foreach ($this->mConfig['templatePaths'] as $templatePath)
		{
			$filename = "$templatePath/$template.$type.php";
			if (is_readable($filename))
				return $filename;
		}
		throw new BB_Views_ViewException("Template '$template.$type.php' wasn't found in templatePaths: " . serialize($this->config['templatePaths']));
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