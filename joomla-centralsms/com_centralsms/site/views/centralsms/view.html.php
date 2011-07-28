<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * HTML View class for the CentralSMS Component
 */
class CentralSMSViewCentralSMS extends JView
{
	function display($tpl = null) 
	{
		$application = JFactory::getApplication();
		$this->params = $application->getParams('com_centralsms');
		$this->form = $this->get('Form');
		if (count($errors = $this->get('Errors'))) 
		{
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		parent::display($tpl);
	}
}
