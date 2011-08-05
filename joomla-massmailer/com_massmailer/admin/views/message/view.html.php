<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Message View
 */
class MassMailerViewMessage extends JView
{
	/**
	 * Message view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$this->form = $this->get('Form');
		$this->success = JRequest::getBool('success');
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		parent::display($tpl);
	}
}
