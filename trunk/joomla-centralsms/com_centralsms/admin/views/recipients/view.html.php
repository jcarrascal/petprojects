<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.view');
 
/**
 * CentralSMSs View
 */
class CentralSMSViewRecipients extends JView
{
	/**
	 * CentralSMSs view display method
	 * @return void
	 */
	function display($tpl = null) 
	{
		$items = $this->get('Items');
		$pagination = $this->get('Pagination'); 
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		$this->items = $items;
		$this->pagination = $pagination;
		$this->addToolBar();
		parent::display($tpl);
	}
 
	/**
	 * Setting the toolbar
	 */
	protected function addToolBar() 
	{
		JToolBarHelper::title(JText::_('COM_CENTRALSMS_MANAGER_RECIPIENTS'));
		JToolBarHelper::addNew('recipient.add');
		JToolBarHelper::editList('recipient.edit');
		JToolBarHelper::deleteList('', 'recipients.delete');
	}
}
