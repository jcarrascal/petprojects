<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Recipients View
 */
class MassMailerViewRecipients extends JView
{
	/**
	 * Recipients view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$this->items      = $this->get('Items');
		$this->pagination = $this->get('Pagination');
		$this->state      = $this->get('State');
		$this->countries  = $this->get('Countries');
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		$this->addToolBar();
		parent::display($tpl);
	}

	/**
	 * Setting the toolbar
	 */
	protected function addToolBar()
	{
		JToolBarHelper::title(JText::_('COM_MASSMAILER_MANAGER_RECIPIENTS'));
		JToolBarHelper::addNew('recipient.add');
		JToolBarHelper::editList('recipient.edit');
		JToolBarHelper::deleteList('', 'recipients.delete');
		JToolBarHelper::divider();
		$bar = JToolBar::getInstance();
		$bar->appendButton('Popup', 'send', 'COM_MASSMAILER_TOOLBAR_SEND', 'index.php?option=com_massmailer&amp;view=message&amp;tmpl=component', 650, 480);
		$bar->appendButton('Link', 'export', 'JTOOLBAR_EXPORT', 'index.php?option=com_massmailer&amp;view=export&amp;format=raw');
		$bar->appendButton('Popup', 'upload', 'COM_MASSMAILER_TOOLBAR_IMPORT', 'index.php?option=com_massmailer&amp;view=import&amp;tmpl=component', 450, 180);
		JToolBarHelper::divider();
		JToolBarHelper::preferences('com_massmailer');
	}
}
