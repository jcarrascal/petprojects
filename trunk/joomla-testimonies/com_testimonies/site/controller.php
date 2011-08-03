<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controller');

/**
 * Testimonies Component Controller.
 */
class TestimoniesController extends JController
{
	/**
	 * Display task
	 *
	 * @return void
	 */
	function display($cachable = false)
	{
		JRequest::setVar('view', JRequest::getCmd('view', 'Posts'));
		parent::display($cachable);
	}
}
