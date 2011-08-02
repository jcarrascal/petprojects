<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Posts View
 */
class TestimoniesViewPosts extends JView
{
	/**
	 * Posts view display method
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
		JToolBarHelper::addNew('post.add');
		JToolBarHelper::editList('post.edit');
		JToolBarHelper::deleteList('', 'posts.delete');
	}
}
