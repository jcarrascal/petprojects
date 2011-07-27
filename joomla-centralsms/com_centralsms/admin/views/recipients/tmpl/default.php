<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
// load tooltip behavior
JHtml::_('behavior.tooltip');
?>
<form action="<?php echo JRoute::_('index.php?option=com_centralcms'); ?>" method="post" name="adminForm">
	<table class="adminlist">
		<thead>
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
		</thead>
		<tfoot>
			<tr>
				<td colspan="6"><?php echo $this->pagination->getListFooter(); ?></td>
			</tr>
		</tfoot>
		<?php foreach($this->items as $i => $item): ?>
			<tr class="row<?php echo $i % 2 ?>">
				<td>
					<?php echo $item->id ?>
				</td>
				<td>
					<?php echo JHtml::_('grid.id', $i, $item->id) ?>
				</td>
				<td>
					<?php echo $item->firstname ?>
				</td>
				<td>
					<?php echo $item->lastname ?>
				</td>
				<td>
					<?php echo $item->neighborhood ?>
				</td>
				<td>
					<?php echo $item->cellphone ?>
				</td>
			</tr>
		<?php endforeach ?>
	</table>
</form>
