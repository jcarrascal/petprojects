<?php

defined('_JEXEC') or die('Restricted access');// no direct access

class ModTestimonies
{
	function fetchLatestTestimony()
	{
		$db = JFactory::getDbo();
		$query = $db->getQuery(true);
		$query->select('a.name, a.message, a.picture');
		$query->from('#__testimonies_posts AS a');
		$query->orderby('a.id desc');
		$query->limit('1');
		$db->setQuery($query);
		return $db->loadObject();
	}
}
