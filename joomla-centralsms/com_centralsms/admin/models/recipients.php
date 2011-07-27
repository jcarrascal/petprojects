<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modellist');

/**
 * CentralSMS Recipients List Model.
 */
class CentralSMSModelRecipients extends JModelList
{
	/**
	 * Constructor.
	 *
	 * @param	array	An optional associative array of configuration settings.
	 * @see		JController
	 * @since	1.6
	 */
	public function __construct($config = array())
	{
		if (empty($config['filter_fields'])) {
			$config['filter_fields'] = array(
				'firstname', 'a.firstname',
				'lastname', 'a.lastname',
				'neighborhood', 'a.neighborhood',
				'cellphone', 'a.cellphone',
			);
		}
		parent::__construct($config);
	}

	/**
	 * Method to build an SQL query to load the list data.
	 *
	 * @return	string	An SQL query
	 */
	protected function getListQuery()
	{
		$db = JFactory::getDBO();
		$query = $db->getQuery(true);
		$query->select('id, firstname, lastname, neighborhood, cellphone');
		$query->from('#__centralsms_recipients');

		// Filter by search by names
		$search = $this->getState('filter.search');
		if (!empty($search)) {
			if (stripos($search, 'id:') === 0) {
				$query->where('a.id = '.(int) substr($search, 3));
			} else {
				$search = $db->Quote('%'.$db->getEscaped($search, true).'%');
				$query->where('(a.firstname LIKE '.$search.' OR a.lastname LIKE '.$search.')');
			}
		}

		// Add the list ordering clause.
		$orderCol  = $this->state->get('list.ordering');
		$orderDirn = $this->state->get('list.direction');
		$query->order($db->getEscaped($orderCol.' '.$orderDirn));

		return $query;
	}
}