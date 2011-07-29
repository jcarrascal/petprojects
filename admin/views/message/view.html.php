<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Message View
 */
class CentralSMSViewMessage extends JView
{
	/**
	 * Message view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$form = $this->get('Form');
		$item = $this->get('Item');
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		$this->form = $form;
		$this->item = $item;
		parent::display($tpl);
	}
}
