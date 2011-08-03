<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modellist');
jimport('joomla.form.form');
 
/**
 * Testimonies Model
 */
class TestimoniesModelPosts extends JModelList
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
				'name', 'a.name',
				'email', 'a.email',
				'neighborhood', 'a.neighborhood',
				'message', 'a.message',
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
		$this->setState('list.limit', 48);
		$db = JFactory::getDBO();
		$query = $db->getQuery(true);
		$query->select('id, name, email, neighborhood, message, picture');
		$query->from('#__testimonies_posts a');

		// Filter by search by names
		$search = $this->getState('filter.search');
		if (!empty($search)) {
			if (stripos($search, 'id:') === 0) {
				$query->where('a.id = '.(int) substr($search, 3));
			} else {
				$search = $db->Quote('%'.$db->getEscaped($search, true).'%');
				$query->where('(a.name LIKE '.$search.' OR a.email LIKE '.$search.' OR a.neighborhood LIKE '.$search.' OR a.message LIKE '.$search.')');
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
		$this->setState('list.limit', 48);

		// List state information.
		parent::populateState('id', 'desc');
	}

	/**
	 * Returns a reference to the a Table object, always creating it.
	 *
	 * @param	type	The table type to instantiate
	 * @param	string	A prefix for the table class name. Optional.
	 * @param	array	Configuration array for model. Optional.
	 * @return	JTable	A database object
	 * @since	1.6
	 */
	public function getTable($type = 'Posts', $prefix = 'TestimoniesTable', $config = array())
	{
		return JTable::getInstance($type, $prefix, $config);
	}

	/**
	 * Method to get the record form.
	 *
	 * @param	array	$data		Data for the form.
	 * @param	boolean	$loadData	True if the form is to load its own data (default case), false if not.
	 * @return	mixed	A JForm object on success, false on failure
	 * @since	1.6
	 */
	public function getForm($data = array(), $loadData = true)
	{
		JForm::addFormPath(JPATH_COMPONENT.'/models/forms');
		JForm::addFieldPath(JPATH_COMPONENT.'/models/fields');
		// Get the form.
		$form = JForm::getInstance('com_testimonies.posts', 'posts', array('control' => 'jform', 'load_data' => $loadData), false, false);
		if (empty($form))
		{
			return false;
		}
		return $form;
	}

	/**
	 * Method to get the data that should be injected in the form.
	 *
	 * @return	mixed	The data for the form.
	 * @since	1.6
	 */
	protected function loadFormData()
	{
		// Check the session for previously entered form data.
		$data = JFactory::getApplication()->getUserState('com_testimonies.edit.posts.data', array());
		if (empty($data))
		{
			$data = $this->getItem();
		}
		return $data;
	}

	function validate($form, $data) {
		if (trim($data['message']) == '') {
			$this->setError('Por favor ingrese su mensaje.');
			return false;
		}
		if (trim($data['name']) == '') {
			$this->setError('Por favor ingrese su nombre.');
			return false;
		}
		if (trim($data['email']) == '') {
			$this->setError('Por favor ingrese su email.');
			return false;
		}
		if (!preg_match('/^[A-Za-z0-9._\-]+@[A-Za-z0-9._\-]+\.[A-Za-z]{2,6}$/', $data['email'])) {
			$this->setError('Por favor ingrese una dirección de email válida.');
			return false;
		}
		$db = $this->getDBO();
		$query = 'select count(*) counter from #__testimonies_posts where email = ' . $db->quote($data['email']);
		$db->setQuery($query);
		$row = $db->loadRow();
		if ($row[0] > 0) {
			$this->setError('La dirección de email ya existe.');
			return false;
		}
		if (!is_uploaded_file($filename = $_FILES['jform']['tmp_name']['picture'])) {
			$this->setError('Debe seleccionar una imagen JPG, GIF o PNG.');
			return false;
		}
		switch ($_FILES['jform']['error']['picture'])
		{
			case 1: $this->setError(JText::_( 'FILE TO LARGE THAN PHP INI ALLOWS' )); return false;
			case 2: $this->setError(JText::_( 'FILE TO LARGE THAN HTML FORM ALLOWS' )); return false;
			case 3: $this->setError(JText::_( 'ERROR PARTIAL UPLOAD' )); return false;
			case 4: $this->setError(JText::_( 'ERROR NO FILE' )); return false;
		}
		if (($size = getimagesize($filename)) === false) {
			$this->setError('Solo se permiten imágenes JPG, GIF o PNG.');
			return false;
		}
		list($w, $h) = $size;
		$side = min($w, $h);
		$data['picture'] = md5($data['email']) . '.png';
		$src = imagecreatefromstring(file_get_contents($filename));
		$dest = imagecreatetruecolor(66, 66);
		imagecopyresized($dest, $src, 0, 0, ($w - $side) / 2, ($h - $side) / 2, 66, 66, $side, $side);
		imagepng($dest, JPATH_SITE . '/images/com_testimonies/' . $data['picture'], 9, PNG_ALL_FILTERS);
		return $data;
	}

	function save($data)
	{
		$table = $this->getTable();
		$this->setState('list.start', 0);
		$store = $this->getStoreId('getstart');
		$this->cache[$store] = 0;
		return $table->save($data);
	}
}
