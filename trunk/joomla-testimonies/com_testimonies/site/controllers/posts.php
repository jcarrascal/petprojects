<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controllerform');

/**
 * Posts Controller
 */
class TestimoniesControllerPosts extends JControllerForm
{
	/**
	 * Method to check if you can add a new record.
	 *
	 * Extended classes can override this if necessary.
	 *
	 * @param   array  $data  An array of input data.
	 *
	 * @return  boolean
	 * @since   11.1
	 */
	protected function allowAdd($data = array())
	{
		return true;
	}
}
