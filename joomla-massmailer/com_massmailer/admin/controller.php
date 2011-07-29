<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controller');

/**
 * General Controller of MassMailer component
 */
class MassMailerController extends JController
{
	/**
	 * Display task
	 *
	 * @return void
	 */
	function display($cachable = false)
	{
		// set default view if not set
		JRequest::setVar('view', JRequest::getCmd('view', 'Recipients'));

		// call parent behavior
		parent::display($cachable);
	}
}
