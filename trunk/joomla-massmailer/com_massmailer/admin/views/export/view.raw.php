<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Export View
 */
class MassMailerViewExport extends JView
{
	/**
	 * Recipients view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$filetype = 'csv';
		$mimetype = 'text/csv';
		$basename = $this->get('BaseName');
		$content  = $this->get('Content');

		// Check for errors.
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode("\n", $errors));
			return false;
		}

		$document = JFactory::getDocument();
		$document->setMimeEncoding($mimetype);
		JResponse::setHeader('Content-disposition', 'attachment; filename="'.$basename.'.'.$filetype.'"; creation-date="'.JFactory::getDate()->toRFC822().'"', true);
		echo $content;
	}
}
