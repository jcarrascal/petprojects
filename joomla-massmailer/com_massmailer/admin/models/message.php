<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.model');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * MassMailer Message Model.
 */
class MassMailerModelMessage extends JModel
{
	private $form = null;

	function getForm()
	{
		if ($this->form == null)
		{
			$params = $this->getParams();
			$form = $this->getState('com_massmailer.message');
			if ($form == null) {
				$form = new stdClass();
				$form->id = 0;
				$form->from_email = JRequest::getString('from_email', $params->get('from_email'));
				$form->from_name = JRequest::getString('from_name', $params->get('from_name'));
				$form->subject = JRequest::getString('subject', '');
				$form->content = JRequest::getString('content', '');
			}
			$this->form = $form;
		}
		return $this->form;
	}
	
	function validate($form)
	{
		if (trim($form->from_email) == '') {
			$this->setError('Por favor ingrese el E-mail del remitente.');
			return false;
		}
		if (trim($form->from_name) == '') {
			$this->setError('Por favor ingrese el Nombre del remitente.');
			return false;
		}
		if (trim($form->subject) == '') {
			$this->setError('Por favor ingrese el Tema.');
			return false;
		}
		if (trim($form->content) == '') {
			$this->setError('Por favor ingrese el Contenido.');
			return false;
		}
		return true;
	}

	function getParams()
	{
		return JComponentHelper::getParams('com_massmailer');
	}

	/**
	 * Method to save the form data.
	 *
	 * @param   array  $data  The form data.
	 *
	 * @return  boolean  True on success, False on error.
	 * @since   11.1
	 */
	public function save($form)
	{
		$db = $this->getDBO();
		if (!$db->insertObject('#__massmailer_messages', $form, 'id')) {
			$this->setError($db->getErrorMsg());
			return false;
		}
		$recipientsModel = new MassMailerModelRecipients();
		foreach ($recipientsModel->getItems() as $item)
		{
			$email = new stdClass();
			$email->message_id = $form->id;
			$email->recipient = $item->email;
			$email->variables = serialize($item);
			if (!$db->insertObject('#__massmailer_emails', $email)) {
				$this->setError($db->getErrorMsg());
				return false;
			}
			$this->setState('com_massmailer.message', $form);
		}
		return true;
	}
}
