<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.modeladmin');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * MassMailer Message Model.
 */
class MassMailerModelMessage extends JModelAdmin
{
	/**
	 * Returns a reference to the a Table object, always creating it.
	 *
	 * @param	type	The table type to instantiate
	 * @param	string	A prefix for the table class name. Optional.
	 * @param	array	Configuration array for model. Optional.
	 * @return	JTable	A database object
	 * @since	1.6
	 */
	public function getTable($type = 'Messages', $prefix = 'MassMailerTable', $config = array())
	{
		return JTable::getInstance($type, $prefix, $config);
	}

	/**
	 * Method to get the record form.
	 *
	 * @param	array	$data		Data for the form.
	 * @param	boolean	$loadData	True if the form is to load its own data (default case), false if not.
	 * @return	mixed	A JForm object on success, false on failure
	 * @since	1.6
	 */
	public function getForm($data = array(), $loadData = true)
	{
		// Get the form.
		$form = $this->loadForm('com_massmailer.message', 'message', array('control' => 'jform', 'load_data' => $loadData));
		if (empty($form))
		{
			return false;
		}
		return $form;
	}

	/**
	 * Method to get the data that should be injected in the form.
	 *
	 * @return	mixed	The data for the form.
	 * @since	1.6
	 */
	protected function loadFormData()
	{
		// Check the session for previously entered form data.
		$data = JFactory::getApplication()->getUserState('com_massmailer.edit.messages.data', array());
		if (empty($data))
		{
			$data = $this->getItem();
		}
		return $data;
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
	public function save($data)
	{
		$recipientsModel = new MassMailerModelRecipients();
		$recipients = array();
		foreach ($recipientsModel->getItems() as $item)
			$recipients[] = $item->country . $item->cellphone;
		$data['code'] = 0;
		$data['text'] = trim($data['text']);
		$data['recipients'] = implode(',', $recipients);
		$data['sent_on'] = date('Y-m-d H:i:s');
		if (!parent::save($data))
			return false;
		$data['id'] = $this->getState($this->getName().'.id');

		$params = $this->getParams();
		try {
			$client = @new SoapClient('http://panel.massmailer.co/ws/sms.wsdl',
				array('exceptions' => 1, 'trace' => true));
		} catch (SoapFault $e) {
			die($e->toString());
			return false;
		}
		$result = $client->smsEnviar($data['recipients'], $data['text'],
			$params->get('login'), $params->get('password'));
		$data['code'] = $result['codigo'];
		$data['status_message'] = $result['mensaje'];
		$data['request'] = $client->__getLastRequestHeaders() . $client->__getLastRequest();
		$data['response'] = $client->__getLastResponseHeaders() . $client->__getLastResponse();
		if ($data['code'] < 0)
			$this->setError($data['status_message']);
		return parent::save($data) && $data['code'] >= 0;
	}
}
