<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
// import Joomla table library
jimport('joomla.database.table');
 
/**
 * CentralCMS Recipients Table.
 */
class CentralSMSTableRecipients extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db) 
	{
		parent::__construct('#__centralsms_recipients', 'id', $db);
	}
}
