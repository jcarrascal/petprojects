<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controller');

$controller = JController::getInstance('MassMailer');
$controller->execute(JRequest::getCmd('task'));
$controller->redirect();
