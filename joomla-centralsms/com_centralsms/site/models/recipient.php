<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modelitem');
 
/**
 * CentralSMS Model
 */
class CentralSMSModelRecipient extends JModelItem
{
	function getMsg()
	{
		return 'Hello, from model';
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
	public function getTable($type = 'CentralSMS', $prefix = 'CentralSMSTable', $config = array()) 
	{
		return JTable::getInstance($type, $prefix, $config);
	}
}
