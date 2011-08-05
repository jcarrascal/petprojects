<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controller');

/**
 * Message Controller
 */
class MassMailerControllerMessage extends JController
{
	function save()
	{
		$model = $this->getModel('Message', 'MassMailerModel');
		$form = $model->getForm();
		if ($model->validate($form) === false) {
			$this->setError($model->getError());
		}
		$saved = $model->save($form);
		$this->setMessage($saved ? JText::_('COM_MASSMAILER_MESSAGE_SAVE_SUCCESS') : $model->getError());
		$this->setRedirect(JRoute::_('index.php?option=com_massmailer&view=message&tmpl=component', false));
	}

	/**
	 * Proxy for getModel.
	 * @since	1.6
	 */
	public function getModel($name = 'Message', $prefix = 'MassMailerModel')
	{
		$model = parent::getModel($name, $prefix, array('ignore_request' => true));
		return $model;
	}
}
