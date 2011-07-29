<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.view');

/**
 * Recipient View
 */
class MassMailerViewRecipient extends JView
{
	/**
	 * MassMailers view display method
	 * @return void
	 */
	function display($tpl = null)
	{
		$form = $this->get('Form');
		$item = $this->get('Item');
		if (count($errors = $this->get('Errors'))) {
			JError::raiseError(500, implode('<br />', $errors));
			return false;
		}
		$this->form = $form;
		$this->item = $item;
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
		JToolBarHelper::title($isNew ? JText::_('COM_MASSMAILER_MANAGER_RECIPIENT_NEW') : JText::_('COM_MASSMAILER_MANAGER_RECIPIENT_EDIT'));
		JToolBarHelper::apply('recipient.apply');
		JToolBarHelper::save('recipient.save');
		JToolBarHelper::save2new('recipient.save2new');
		JToolBarHelper::cancel('recipient.cancel', $isNew ? 'JTOOLBAR_CANCEL' : 'JTOOLBAR_CLOSE');
	}
}
