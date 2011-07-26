<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * HTML View class for the SocialNetworks Component
 */
class SocialNetworksViewSocialNetworks extends JView
{
	// Overwriting JView display method
	function display($tpl = null) 
	{
		// Assign data to the view
		$this->msg = 'Hello World';
 
		// Display the view
		parent::display($tpl);
	}
}