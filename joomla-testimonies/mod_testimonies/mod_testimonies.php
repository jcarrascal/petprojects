<?php

defined('_JEXEC') or die('Restricted access');// no direct access

require_once(dirname(__FILE__).DS.'helper.php');

$helper = new ModTestimonies($params);

require(JModuleHelper::getLayoutPath('mod_testimonies'));
