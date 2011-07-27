<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.modellist');
require_once dirname(__FILE__) . DS . 'recipients.php';

/**
 * CentralSMS Export List Model.
 */
class CentralSMSModelExport extends CentralSMSModelRecipients
{
	function getBaseName()
	{
		return strftime('CentralCMS_Recipients_%Y-%m-%d-%H-%M-%S');
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
			'"'.str_replace('"','""',JText::_('COM_CENTRALSMS_RECIPIENT_HEADING_FIRSTNAME')).'";"'.
				str_replace('"','""',JText::_('COM_CENTRALSMS_RECIPIENT_HEADING_LASTNAME')).'";"'.
				str_replace('"','""',JText::_('COM_CENTRALSMS_RECIPIENT_HEADING_NEIGHBORHOOD')).'";"'.
				str_replace('"','""',JText::_('COM_CENTRALSMS_RECIPIENT_HEADING_CELLPHONE'))."\"\n";
			foreach($this->getItems() as $item) {
				$this->content.=
				'"'.str_replace('"','""',$item->firstname).'";"'.
					str_replace('"','""',$item->lastname).'";"'.
					str_replace('"','""',$item->neighborhood).'";"'.
					str_replace('"','""',$item->cellphone).'"'."\n";
			}
		}
		return $this->content;
	}
}