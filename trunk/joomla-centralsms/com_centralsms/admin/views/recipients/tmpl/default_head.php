<?php
defined('_JEXEC') or die('Restricted access');// no direct access
?>
<tr>
	<th width="5">
		<?php echo JText::_('COM_CENTRALSMS_CENTRALSMS_HEADING_ID') ?>
	</th>
	<th width="20">
		<input type="checkbox" name="toggle" value="" onclick="checkAll(<?php echo count($this->items) ?>);" />
	</th>
	<th>
		<?php echo JText::_('COM_CENTRALSMS_CENTRALSMS_HEADING_FIRSTNAME') ?>
	</th>
	<th>
		<?php echo JText::_('COM_CENTRALSMS_CENTRALSMS_HEADING_LASTNAME') ?>
	</th>
	<th>
		<?php echo JText::_('COM_CENTRALSMS_CENTRALSMS_HEADING_NEIGHBORHOOD') ?>
	</th>
	<th>
		<?php echo JText::_('COM_CENTRALSMS_CENTRALSMS_HEADING_CELLPHONE') ?>
	</th>
</tr>
