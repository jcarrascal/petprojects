<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Post View
 */
class TestimoniesViewPost extends JView
{
	/**
	 * Post view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$this->form = $this->get('Form');
		$this->item = $this->get('Item');
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
		JRequest::setVar('hidemainmenu', true);
		$isNew = ($this->item->id == 0);
		JToolBarHelper::title($isNew ? JText::_('COM_TESTIMONIES_MANAGER_POST_NEW') : JText::_('COM_TESTIMONIES_MANAGER_RECIPIENT_EDIT'));
		JToolBarHelper::apply('post.apply');
		JToolBarHelper::save('post.save');
		JToolBarHelper::save2new('post.save2new');
		JToolBarHelper::cancel('post.cancel', $isNew ? 'JTOOLBAR_CANCEL' : 'JTOOLBAR_CLOSE');
	}
}
