<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport('joomla.application.component.controller');

/**
 * Message Controller
 */
class TestimoniesControllerImport extends JController
{
	function upload()
	{
		$rows = array();
		$skipFirst = JRequest::getBool('skip_first');
		$isFirst = true; $isValid = true;
		$lineNumber = 0;
		foreach (file($_FILES['csv_file']['tmp_name']) as $line) {
			++$lineNumber;
			if ($skipFirst && $isFirst) {
				$skipFirst = false;
				$isFirst = false;
				continue;
			}
			if ($isFirst) {
				if (substr($line, 0, 3) == "\xEF\xBB\xBF")
					$line = substr($line, 3);
				$isFirst = false;
			}
			$fields = preg_split('/[;,|]/', trim($line));
			if (count($fields) !== 5) {
				$this->setMessage(JText::sprintf('COM_TESTIMONIES_IMPORT_INVALID_CSV', $lineNumber));
				$this->setRedirect('index.php?option=com_testimonies&view=import&tmpl=component');
				$isValid = false;
				break;
			}
			$rows[] = array(
				'firstname' => $this->unquote($fields[0]),
				'lastname' => $this->unquote($fields[1]),
				'neighborhood' => $this->unquote($fields[2]),
				'country' => $this->unquote($fields[3]),
				'cellphone' => $cellphone = $this->unquote($fields[4]),
			);
			if (!preg_match('/^[0-9]{10}$/', $cellphone)) {
				$this->setMessage(JText::sprintf('COM_TESTIMONIES_IMPORT_INVALID_CELLPHONE', $lineNumber));
				$this->setRedirect('index.php?option=com_testimonies&view=import&tmpl=component');
				$isValid = false;
				break;
			}
		}
		if ($isValid) {
			$model = $this->getModel('Import', 'TestimoniesModel');
			$this->setRedirect('index.php?option=com_testimonies&view=import&tmpl=component&status=success');
			if ($model->batchImport($rows))
				$this->setMessage(JText::_('COM_TESTIMONIES_IMPORT_SUCCESS'));
			else
				$this->setMessage(JText::_($model->getError()));
		}
		$this->redirect();
	}
	
	private function unquote($str) {
		if ($str{0} == '"' && $str{strlen($str) - 1} == '"')
			return str_replace('""', '"', substr($str, 1, -1));
		var_export($str{0});var_export($str{strlen($str)});
		return $str;
	}
}
