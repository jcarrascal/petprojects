<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Recipients View
 */
class TestimoniesViewPosts extends JView
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
		JToolBarHelper::title(JText::_('COM_TESTIMONIES_MANAGER_POSTS'));
		JToolBarHelper::addNew('recipient.add');
		JToolBarHelper::editList('recipient.edit');
		JToolBarHelper::deleteList('', 'recipients.delete');
	}
}