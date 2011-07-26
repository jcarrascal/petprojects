<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
jimport('joomla.application.component.modelitem');
 
/**
 * CentralSMS Model
 */
class CentralSMSModelCentralSMS extends JModelItem
{
	/**
	 * @var string msg
	 */
	protected $msg;
 
	/**
	 * Get the message
	 * @return string The message to be displayed to the user
	 */
	public function getMsg()
	{
		if (!$this->msg)
		{
			$this->msg = 'Hello World! from model';
		}
		return $this->msg;
	}
}