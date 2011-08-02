<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.modellist');

/**
 * Testimonies Recipients List Model.
 */
class TestimoniesModelRecipients extends JModelList
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
		$query->select('id, firstname, lastname, neighborhood, country, cellphone');
		$query->from('#__testimonies_recipients a');

		// Filter by search by names
		$search = $this->getState('filter.search');
		if (!empty($search)) {
			if (stripos($search, 'id:') === 0) {
				$query->where('a.id = '.(int) substr($search, 3));
			} else {
				$search = $db->Quote('%'.$db->getEscaped($search, true).'%');
				$query->where('(a.firstname LIKE '.$search.' OR a.lastname LIKE '.$search.' OR a.cellphone LIKE '.$search.')');
			}
		}

		// Add the list ordering clause.
		$orderCol  = $this->state->get('list.ordering');
		$orderDirn = $this->state->get('list.direction');
		$query->order($db->getEscaped($orderCol.' '.$orderDirn));

		return $query;
	}

	/**
	 * Method to auto-populate the model state.
	 *
	 * Note. Calling getState in this method will result in recursion.
	 *
	 * @since	1.6
	 */
	protected function populateState($ordering = null, $direction = null)
	{
		// Initialise variables.
		$app = JFactory::getApplication('administrator');

		// Load the filter state.
		$search = $this->getUserStateFromRequest($this->context.'.filter.search', 'filter_search');
		$this->setState('filter.search', $search);

		// Load the parameters.
		$params = JComponentHelper::getParams('com_testimonies');
		$this->setState('params', $params);

		// List state information.
		parent::populateState('firstname', 'asc');
	}

	public function getCountries()
	{
		return array(
			54  => 'Argentina',
			591 => 'Bolivia',
			56  => 'Chile',
			57  => 'Colombia',
			593 => 'Ecuador',
			52  => 'Mexico',
			507 => 'Panamá',
			595 => 'Paraguay',
			51  => 'Perú',
			598 => 'Uruguay',
			58  => 'Venezuela',
		);
	}
}