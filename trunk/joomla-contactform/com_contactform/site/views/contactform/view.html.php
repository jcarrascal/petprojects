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


// no direct access
defined('_JEXEC') or die('Restricted access');


jimport('joomla.application.component.view');


class ContactFormViewContactForm extends JView
{
	function display($tpl = null) 
	{
		$this->shouldDisplayArticle = $this->get('shouldDisplayArticle');
		if ($this->shouldDisplayArticle)
			$this->article = $this->get('article');
		$this->showHomePhone = $this->get('showHomePhone');
		$this->showCellPhone = $this->get('showCellPhone');
		$this->showCompany = $this->get('showCompany');
		$this->showAddress = $this->get('showAddress');
		$this->showCity = $this->get('showCity');

		if (count($errors = $this->get('Errors'))) 
		{
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		parent::display($tpl);
	}
}
