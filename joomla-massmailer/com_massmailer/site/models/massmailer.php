<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modeladmin');
 
/**
 * MassMailer Model
 */
class MassMailerModelMassMailer extends JModelAdmin
{
	/**
	 * Method to validate the form data.
	 *
	 * @param   object  $form   The form to validate against.
	 * @param   array   $data   The data to validate.
	 * @param   string  $group  The name of the field group to validate.
	 *
	 * @return  mixed  Array of filtered data if valid, false otherwise.
	 *
	 * @see     JFormRule
	 * @see     JFilterInput
	 * @since   11.1
	 */
	function validate($form, $data, $group = null)
	{
		if (($data = parent::validate($form, $data, $group)) === false)
			return false;

		$db = $this->getDBO();
		$query = 'select count(*) counter
			from #__massmailer_recipients
			where email = ' . $db->quote($data['email']);
		$db->setQuery($query);
		$row = $db->loadRow();
		if ($row[0] > 0) {
			$this->setError(JText::_('COM_MASSMAILER_VALIDATION_REGISTERED'));
			return false;
		}

		return $data;
	}

	/**
	 * Returns a reference to the a Table object, always creating it.
	 *
	 * @param	type	The table type to instantiate
	 * @param	string	A prefix for the table class name. Optional.
	 * @param	array	Configuration array for model. Optional.
	 * @return	JTable	A database object
	 * @since	1.6
	 */
	public function getTable($type = 'Recipients', $prefix = 'MassMailerTable', $config = array())
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
		$form = $this->loadForm('com_massmailer.recipients', 'recipients', array('control' => 'jform', 'load_data' => $loadData));
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
		$data = JFactory::getApplication()->getUserState('com_massmailer.edit.recipients.data', array());
		if (empty($data))
		{
			$data = $this->getItem();
		}
		return $data;
	}
}
