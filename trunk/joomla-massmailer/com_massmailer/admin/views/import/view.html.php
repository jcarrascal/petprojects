<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Import View
 */
class MassMailerViewImport extends JView
{
	/**
	 * Import view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$this->status = JRequest::getString('status', 'normal');
		parent::display($tpl);
	}
}
