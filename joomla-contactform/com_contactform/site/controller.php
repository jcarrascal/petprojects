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


jimport('joomla.application.component.controller');


class ContactFormController extends JController
{
	function display()
	{
		JRequest::setVar('view', JRequest::getCmd('view', 'ContactForm'));
		parent::display();
	}

/*	function send()
	{
		$model = $this->getModel('ContactForm');
		if ($model->send())
			$this->setRedirect(JRoute::_('index.php?option=com_contactform&task=success', false));
		else
		{
			$view = $this->getView('ContactForm', 'html');
			$view->assign('message', $model->getError());
			$view->assign('model', $model);
			$view->display();
		}
	}

	function success()
	{
		$view = $this->getView('ContactForm', 'html');
		$view->display('success');
	}*/
}


?>
