<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controlleradmin');

/**
 * Recipients Controller
 */
class TestimoniesControllerRecipients extends JControllerAdmin
{
	/**
	 * Proxy for getModel.
	 * @since	1.6
	 */
	public function getModel($name = 'Recipient', $prefix = 'TestimoniesModel')
	{
		$model = parent::getModel($name, $prefix, array('ignore_request' => true));
		return $model;
	}
}
