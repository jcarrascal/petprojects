<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// import Joomla table library
jimport('joomla.database.table');

/**
 * MassMailer Messages Table.
 */
class MassMailerTableMessages extends JTable
{
	/**
	 * Constructor
	 *
	 * @param object Database connector object
	 */
	function __construct(&$db)
	{
		parent::__construct('#__massmailer_messages', 'id', $db);
	}
}
