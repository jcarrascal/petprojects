<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.model');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * MassMailer Export List Model.
 */
class MassMailerModelImport extends JModel
{
	function batchImport($rows)
	{
		$db = $this->getDBO();
		$values = array();
		foreach ($rows as $row) {
			$values[] = '(' . $db->quote($row['firstname']) . ', ' . $db->quote($row['lastname']) . ', ' .
				$db->quote($row['neighborhood']) . ', ' . $db->quote($row['country']) . ', ' .
				$db->quote($row['cellphone']) . ")";
		}
		$query = "insert into #__massmailer_recipients (firstname, lastname, neighborhood, country, cellphone) values\n" .
			implode(",\n", $values) . "\n" .
			'on duplicate key update firstname=values(firstname), lastname=values(lastname), ' .
			'neighborhood=values(neighborhood), country=values(country)';
		$db->setQuery($query);
		$result = $db->query();
		if ($result === false) {
			$this->setError($db->getErrorMsg());
			return false;
		}
		return true;
	}
}
