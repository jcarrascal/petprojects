<?php


/*
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as
 * published by the Free Software Foundation, either version 3 of the 
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>.
*/


// Check to ensure this file is included in Joomla!
defined('_JEXEC') or die();


jimport('joomla.application.component.model');


class ContactFormModelContactForm extends JModel
{
	function __construct()
	{
		parent::__construct();

		global $mainframe;

		$params = $mainframe->getPageParameters('com_contactform');
		$this->_recipient = $params->get('recipient', 'com_contactform@mailinator.com');
		$this->_prefix = $params->get('prefix', '[ContactForm]');

		$this->showHomePhone = $params->get('showHomePhone', false);
		$this->showCellPhone = $params->get('showCellPhone', false);
		$this->showCompany = $params->get('showCompany', false);
		$this->showAddress = $params->get('showAddress', false);
		$this->showCity = $params->get('showCity', false);
	}

	var $name = '';
	var $email = '';
	var $subject = '';
	var $message = '';
	var $company = '';
	var $homePhone = '';
	var $address = '';
	var $city = '';
	var $cellPhone = '';

	var $_article_id = 0;
	var $_article = null;

	function shouldDisplayArticle()
	{
		return $this->_article_id > 0;
	}

	function setArticleId($value)
	{
		$this->_article_id = $value;
	}

	function getArticle()
	{
		if ($this->_article_id > 0 && null == $this->_article)
		{
			$user		=& JFactory::getUser();
			$aid		= (int) $user->get('aid', 0);

			jimport('joomla.utilities.date');
			$jnow		= new JDate();
			$now		= $jnow->toMySQL();
			$nullDate	= $this->_db->getNullDate();

			$query = 'SELECT a.*, u.name AS author, u.usertype, cc.title AS category, s.title AS section,' 
				. "\n    CASE WHEN CHAR_LENGTH(a.alias) THEN CONCAT_WS(':', a.id, a.alias) ELSE a.id END as slug,"
				. "\n    CASE WHEN CHAR_LENGTH(cc.alias) THEN CONCAT_WS(':', cc.id, cc.alias) ELSE cc.id END as catslug,"
				. "\n    g.name AS groups, s.published AS sec_pub, cc.published AS cat_pub, s.access AS sec_access, cc.access AS cat_access"
				. "\n  FROM #__content AS a"
				. "\n    LEFT JOIN #__categories AS cc ON cc.id = a.catid"
				. "\n    LEFT JOIN #__sections AS s ON s.id = cc.section AND s.scope = 'content'"
				. "\n    LEFT JOIN #__users AS u ON u.id = a.created_by"
				. "\n    LEFT JOIN #__groups AS g ON a.access = g.id"
				. "\n  WHERE a.id = " . (int)$this->_article_id . ' AND a.access <= ' . (int) $aid
				. "\n    AND ( (a.created_by = " . (int)$user->id . ' OR (a.state = 1 OR a.state = -1))'
				. "\n      AND ( a.publish_up = " . $this->_db->Quote($nullDate) . ' OR a.publish_up <= ' . $this->_db->Quote($now) . ' )'
				. "\n      AND ( a.publish_down = " . $this->_db->Quote($nullDate) . ' OR a.publish_down >= ' . $this->_db->Quote($now) . ' ) )'
				;
			$this->_db->setQuery($query);
			$this->_article = $this->_db->loadObject();
			$this->_article->event = new stdClass();
			$this->_article->event->afterDisplayTitle = null;
			$this->_article->event->beforeDisplayContent = null;
		}

		return $this->_article;
	}

    function send()
    {
		jimport('joomla.mail.helper');

		$this->name     = JMailHelper::cleanLine(JRequest::getString('name', ''));
		$this->email    = JMailHelper::cleanAddress(JRequest::getString('email', ''));
		$this->subject  = JMailHelper::cleanSubject(JRequest::getString('subject', ''));
		$this->message  = JMailHelper::cleanBody(JRequest::getString('message', ''));

		$this->homePhone = JMailHelper::cleanBody(JRequest::getString('homePhone', ''));
		$this->cellPhone = JMailHelper::cleanBody(JRequest::getString('cellPhone', ''));
		$this->company = JMailHelper::cleanBody(JRequest::getString('company', ''));
		$this->address = JMailHelper::cleanBody(JRequest::getString('address', ''));
		$this->city = JMailHelper::cleanBody(JRequest::getString('city', ''));

		if ('' == $this->name)
		{
			$this->setError(JText::_('CF_INVALID_NAME'));
			return false;
		}
		if (!JMailHelper::isEmailAddress($this->email))
		{
			$this->setError(JText::_('CF_INVALID_EMAIL'));
			return false;
		}
		if ('' == $this->subject)
		{
			$this->setError(JText::_('CF_INVALID_SUBJECT'));
			return false;
		}
		if ('' == $this->message)
		{
			$this->setError(JText::_('CF_INVALID_MESSAGE'));
			return false;
		}

		$body = JText::_('CF_FIELD_NAME') . ': ' . $this->name
			. "\r\n" . JText::_('CF_FIELD_EMAIL') . ': ' . $this->email
			. "\r\n\r\n$this->message\r\n"
			. ($this->showHomePhone ? "\r\n" . JText::_('CF_FIELD_HOME_PHONE') . ': ' . $this->homePhone : '')
			. ($this->showCellPhone ? "\r\n" . JText::_('CF_FIELD_CELL_PHONE') . ': ' . $this->cellPhone : '')
			. ($this->showCompany ? "\r\n" . JText::_('CF_FIELD_COMPANY') . ': ' . $this->company : '')
			. ($this->showAddress ? "\r\n" . JText::_('CF_FIELD_ADDRESS') . ': ' . $this->address : '')
			. ($this->showCity ? "\r\n" . JText::_('CF_FIELD_CITY') . ': ' . $this->city : '')
			;

		jimport('joomla.utilities.utility');

		$result = JUtility::sendMail($this->email, $this->name, $this->_recipient, "$this->_prefix $this->subject",
			$body, false);
		if (JError::isError($result))
		{
			$this->setError($result->message);
			return false;
		}
		return true;
	}

	var $_recipient;
	var $_prefix;
}


?>
