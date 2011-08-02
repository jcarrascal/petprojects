<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controlleradmin');

/**
 * Posts Controller
 */
class TestimoniesControllerPosts extends JControllerAdmin
{
	/**
	 * Proxy for getModel.
	 * @since	1.6
	 */
	public function getModel($name = 'Posts', $prefix = 'TestimoniesModel')
	{
		$model = parent::getModel($name, $prefix, array('ignore_request' => true));
		return $model;
	}
}
