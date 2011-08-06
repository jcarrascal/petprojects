<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.modeladmin');

/**
 * Testimonies Post Model.
 */
class TestimoniesModelPost extends JModelAdmin
{
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
		// Get the form.
		$form = $this->loadForm('com_testimonies.posts', 'posts', array('control' => 'jform', 'load_data' => $loadData));
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

	/**
	 * Method to validate the form data.
	 *
	 * @param   object  $form   The form to validate against.
	 * @param   array   $data   The data to validate.
	 * @param   string  $group  The name of the field group to validate.
	 *
	 * @return  mixed  Array of filtered data if valid, false otherwise.
	 *
	 * @see     JFormRule
	 * @see     JFilterInput
	 * @since   11.1
	 */
	function validate($form, $data, $group = null)
	{
		if (($data = parent::validate($form, $data, $group)) === false)
			return false;
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
}