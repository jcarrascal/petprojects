<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.modellist');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * MassMailer Export List Model.
 */
class MassMailerModelExport extends MassMailerModelRecipients
{
	function getBaseName()
	{
		return strftime('MassMailer_Recipients_%Y-%m-%d-%H-%M-%S');
	}

	/**
	 * Get the content
	 *
	 * @return	string	The content.
	 * @since	1.6
	 */
	public function getContent()
	{
		if (!isset($this->content)) {
			$this->content = "\xEF\xBB\xBF";
			$this->content.=
			'"'.str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_FIRSTNAME')).'";"'.
				str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_LASTNAME')).'";"'.
				str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_EMAIL')).'";"'.
				str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_GENDER')).'";"'.
				str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_DATE_OF_BIRTH')).'";"'.
				str_replace('"','""',JText::_('COM_MASSMAILER_RECIPIENT_HEADING_NEIGHBORHOOD'))."\"\n";
			foreach($this->getItems() as $item) {
				$this->content.=
				'"'.str_replace('"','""',$item->firstname).'";"'.
					str_replace('"','""',$item->lastname).'";"'.
					str_replace('"','""',$item->neighborhood).'";"'.
					str_replace('"','""',$item->country).'";"'.
					str_replace('"','""',$item->cellphone).'"'."\n";
			}
		}
		return $this->content;
	}
}