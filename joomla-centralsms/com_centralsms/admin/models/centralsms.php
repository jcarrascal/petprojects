<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modellist');

/**
 * CentralSMS List Model.
 */
class CentralSMSModelCentralSMSs extends JModelList
{
	/**
	 * Method to build an SQL query to load the list data.
	 *
	 * @return	string	An SQL query
	 */
	protected function getListQuery()
	{DIE('foo');
		$db = JFactory::getDBO();
		$query = $db->getQuery(true);
		$query->select('id, firstname, lastname, neighborhood, cellphone');
		$query->from('#__centralsms');
		return $query;
	}
}