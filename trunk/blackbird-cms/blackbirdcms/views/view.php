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
 * Exception class thrown by View objects.
 */
class ViewException extends \Exception
{
}


/**
 * Renders the specified template and provides automatic html encoding for
 * variables and template inheritance.
 *
 * The View class allows you to render a php template and provides some
 * convenient features like automatic encoding of variables to prevent XSS
 * atacks and template inheritance to facilitate maintaining common parts of
 * the layout.
 *
 * To create a View object you must pass a configuration array with at least
 * the 'templatePaths' parameter with the directories where the templates will
 * be searched.
 *
 * <code>
 *     $config = array('templatePaths' => array('./templates', './themes/default'));
 *     $view = new View($config);
 * </code>
 *
 * You can pass other parameters in the $config array and they will be
 * accesible inside the template using the $this->config(<name>) method.
 *
 * Once you have a configured View object you can set variables and render a
 * template.
 *
 * <code>
 *     $view->title = 'Welcome to my site';
 *     $view->message = 'Hello, world!';
 *     echo $view->render('index');
 * </code>
 *
 * The template can be any PHP source code you might need to output HTML, JSON,
 * CSS or any other textual format. To render the index template create a
 * 'index.html.php' file inside either the './templates' or the
 * './themes/default' directory:
 *
 * <code>
 *     <!doctype html>
 *     <html>
 *     <head><title><?php echo $this->title ?></title></head>
 *     <body>
 *       <p><?php echo $this->message ?></p>
 *     </body>
 *     </html>
 * </code>
 *
 * Template inheritance allows you to abstract the parts that are common to
 * several pages into a master page. You must provide the 'layoutPaths'
 * configuration parameter with directories where master pages will be stored.
 *
 * Using inheritance the previous example could be written like this:
 *
 * <code>
 *     <?php $this->inherits('masterpage', array('title' => $this->title)) ?>
 *     <?php $this->beginBlock('main') ?>
 *         <p><?php echo $this->message ?></p>
 *     <?php $this->endBlock() ?>
 * </code>
 *
 * Here we are passing the 'title' variable and the 'main' block to the
 * 'masterpage' template. The 'masterpage.html.php' file could be something
 * like this:
 *
 * <code>
 *     <!doctype html>
 *     <html>
 *     <head><title><?php echo $this->title ?></title></head>
 *     <body>
 *       <p><?php echo $this->contents('block') ?></p>
 *     </body>
 *     </html>
 * </code>
 *
 * To specify the default encoder function applied to properties use the
 * 'defaultEncoder' configuration parameter. For example, in an email template
 * it would be prefered to specify the 'as_text' encoder function:
 *
 * <code>
 *     $config = array('templatePaths' => array('./emails'), 'defaultEncoder' => 'as_text');
 *     $view = new View($config);
 *     $view->render('welcome_email');
 * </code>
 *
 * An encoder is any function that receives a string and returns another
 * string. Available encoders are 'as_text', 'as_html', 'as_attribute' and
 * 'as_string'. You can write your own encoder function and specify it in the
 * constructor.
 */
class View
{
	private $mTemplatePaths;
	private $mLayoutPaths;
	private $mDefaultEncoder;
	private $mConfig;
	private $mCurrentBlocks;
	private $mBlockContents;
	private $mProperties;
	private $mInherits;

	/**
	 * Initializes a new instance of the View class.
	 * @param array $config Only the 'templatePaths' parameter is required.
	 */
	function __construct($config)
	{
		if (!isset($config['templatePaths']))
			throw new ViewException('You must provide the \'templatePaths\' configuration parameter.');
		$this->mTemplatePaths = $config['templatePaths'];
		unset($config['templatePaths']);
		$this->mLayoutPaths = $config['layoutPaths'];
		unset($config['templatePaths']);
		$this->mDefaultEncoder = isset($config['defaultEncoder']) ? $config['defaultEncoder'] : 'as_html';
		unset($config['defaultEncoder']);
		$this->mConfig = $config;
		$this->mCurrentBlocks = array();
		$this->mBlockContents = array();
		$this->mProperties = array();
		$this->mInherits = null;
	}

	/**
	 * Assign a value to a variable that will be available to the template.
	 * @param string $name The name of the property.
	 * @param mixed $value The value for the property.
	 */
	function __set($name, $value)
	{
		$this->mProperties[$name] = $value;
	}

	/**
	 * Returns the value of the specified property encoded using the default
	 * encoder (usually 'as_html') for security.
	 * @param string $name The name of the property.
	 * @return mixed The value for the property.
	 */
	function __get($name)
	{
		return $this->get($name);
	}

	/**
	 * Returns the value of the specified property. If no encoder is specified
	 * the default function (usually 'as_html') is used.
	 * @param string $name The name of the property.
	 * @param string $encoder The name of the encoder function.
	 * @return mixed The value for the property.
	 */
	function get($name, $encoder=null)
	{
		if (isset($this->mProperties[$name]))
		{
			if ($encoder == null)
				$encoder = $this->mDefaultEncoder;
			$value = $this->mProperties[$name];
			if (is_string($value))
				return call_user_func($encoder, $value);
			return $value;
		}
		return null;
	}

	/**
	 * Returns the value of the specified configuration parameter.
	 * @param string $name The name of the parameter.
	 * @return mixed The value for the parameter.
	 */
	function config($name, $encoder=null)
	{
		if ($encoder == null)
			$encoder = $this->mDefaultEncoder;
		if (isset($this->mConfig[$name]))
			return call_user_func($encoder, $value);
		return null;
	}

	/**
	 * Inheritance allows sharing parts of the layout by several pages of the site.
	 * @param string $template Name of the template.
	 * @param array $properties Variables that will be available in the master page.
	 * @param string $type Type of the template. 'html' by default.
	 */
	function inherits($template, $properties=array(), $type='html')
	{
		if ($this->mInherits != null)
			throw new ViewException("Only single inheritance is supported.");
		$this->mInherits = array($template, $type, $properties);
	}

	/**
	 * Returns a value indicating that at least one of the specified blocks is not empty.
	 * @param string $blockName,... The names of the blocks.
	 * @return string
	 */
	function hasContent($blockName)
	{
		foreach (func_get_args() as $name)
		{
			if (isset($this->mBlockContents[$name]))
				return true;
		}
		return false;
	}

	/**
	 * Returns the content of the specified block.
	 * @param string $blockName The name of the block.
	 * @return string
	 */
	function content($blockName)
	{
		if (isset($this->mBlockContents[$blockName]))
			return $this->mBlockContents[$blockName];
		return null;
	}

	/**
	 * Starts a block stores any text thats output until it's corresponding endBlock() call.
	 * @param string $blockName The name of the block.
	 */
	function beginBlock($blockName)
	{
		array_push($this->mCurrentBlocks, $blockName);
		ob_start();
	}

	/**
	 * Closes the last open block and returns it's content.
	 * @return string
	 */
	function endBlock()
	{
		$blockName = array_pop($this->mCurrentBlocks);
		$contents = $this->mBlockContents[$blockName] = ob_get_contents();
		ob_end_clean();
		return $contents;
	}

	/**
	 * Render the specified template.
	 * @param string $template
	 * @param string $type
	 * @return string
	 */
	function render($template, $type='html')
	{
		$this->mInherits = null;
		$filename = $this->locateTemplate($template, $type);
		$contents = $this->captureTemplate($filename);
		while ($this->mInherits != null)
		{
			list($template, $type, $this->mProperties) = $this->mInherits;
			$this->mInherits = null;
			$filename = $this->locateLayout($template, $type);
			$contents = $this->captureTemplate($filename);
		}
		return $contents;
	}

	private function locateTemplate($template, $type)
	{
		return $this->locateFilename($template, $type, $this->mTemplatePaths);
	}

	private function locateLayout($template, $type)
	{
		return $this->locateFilename($template, $type, $this->mLayoutPaths);
	}

	private function locateFilename($template, $type, $paths)
	{
		foreach ($paths as $path)
		{
			$filename = "$path/$template.$type.php";
			if (is_readable($filename))
				return $filename;
		}
		throw new ViewException("Template '$template.$type.php' wasn't found in templatePaths: " . serialize($this->config['templatePaths']));
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