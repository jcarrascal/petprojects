<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// import Joomla table library
jimport('joomla.database.table');

/**
 * Testimonies Messages Table.
 */
class TestimoniesTableMessages extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db)
	{
		parent::__construct('#__testimonies_messages', 'id', $db);
	}
}
