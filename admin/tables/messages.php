<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// import Joomla table library
jimport('joomla.database.table');

/**
 * CentralSMS Messages Table.
 */
class CentralSMSTableMessages extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db)
	{
		parent::__construct('#__centralsms_messages', 'id', $db);
	}
}
