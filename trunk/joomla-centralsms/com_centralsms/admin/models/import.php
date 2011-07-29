<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.model');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * CentralSMS Export List Model.
 */
class CentralSMSModelImport extends JModel
{
	function batchImport($rows)
	{
		$db = $this->getDBO();
		$query = "insert into #__centralsms_recipients (firstname, lastname, neighborhood, country, cellphone) values\n";
		foreach ($rows as $row) {
			$query .= '(' . $db->quote($row['firstname']) . ', ' . $db->quote($row['lastname']) . ', ' .
				$db->quote($row['neighborhood']) . ', ' . $db->quote($row['country']) . ', ' .
				$db->quote($row['cellphone']) . ")\n";
		}
		$query .= 'on duplicate key update firstname=values(firstname), firstname=values(lastname), ' .
			'neighborhood=values(neighborhood), country=values(country)';
		var_dump($query);
		$db->setQuery($query);
		$result = $db->query();
		if ($result === false) {
			$this->setError($db->getErrorMsg());
			return false;
		}
		return true;
	}
}
