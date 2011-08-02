<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// import Joomla table library
jimport('joomla.database.table');

/**
 * CentralCMS Recipients Table.
 */
class TestimoniesTableRecipients extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db)
	{
		parent::__construct('#__testimonies_recipients', 'id', $db);
	}
}
