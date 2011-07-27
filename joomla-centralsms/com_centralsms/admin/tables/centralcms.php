<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
// import Joomla table library
jimport('joomla.database.table');
 
/**
 * CentralCMS Table class
 */
class CentralCMSTableCentralCMS extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db) 
	{
		parent::__construct('#__centralcms', 'id', $db);
	}
}
