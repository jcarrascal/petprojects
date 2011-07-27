<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
JHtml::_('behavior.tooltip');
JHtml::_('behavior.multiselect');
$listOrder	= $this->escape($this->state->get('list.ordering'));
$listDirn	= $this->escape($this->state->get('list.direction'));
?>
<form action="<?php echo JRoute::_('index.php?option=com_centralsms'); ?>" method="post" name="adminForm">
	<fieldset id="filter-bar">
		<div class="filter-search fltlft">
			<label class="filter-search-lbl" for="filter_search"><?php echo JText::_('JSEARCH_FILTER_LABEL'); ?></label>
			<input type="text" name="filter_search" id="filter_search" value="<?php echo $this->escape($this->state->get('filter.search')); ?>" title="<?php echo JText::_('COM_CENTRALSMS_SEARCH_BY_NAMES'); ?>" />
			<button type="submit"><?php echo JText::_('JSEARCH_FILTER_SUBMIT'); ?></button>
			<button type="button" onclick="document.id('filter_search').value='';this.form.submit();"><?php echo JText::_('JSEARCH_FILTER_CLEAR'); ?></button>
		</div>
	</fieldset>
	<div class="clr"> </div>

	<table class="adminlist">
		<thead>
			<tr>
				<th width="1%">
					<input type="checkbox" name="checkall-toggle" value="" title="<?php echo JText::_('JGLOBAL_CHECK_ALL'); ?>" onclick="Joomla.checkAll(this)" />
				</th>
				<th>
					<?php echo JHtml::_('grid.sort',  'COM_CENTRALSMS_RECIPIENT_HEADING_FIRSTNAME', 'firstname', $listDirn, $listOrder); ?>
				</th>
				<th>
					<?php echo JHtml::_('grid.sort',  'COM_CENTRALSMS_RECIPIENT_HEADING_LASTNAME', 'lastname', $listDirn, $listOrder); ?>
				</th>
				<th>
					<?php echo JHtml::_('grid.sort',  'COM_CENTRALSMS_RECIPIENT_HEADING_NEIGHBORHOOD', 'neighborhood', $listDirn, $listOrder); ?>
				</th>
				<th>
					<?php echo JHtml::_('grid.sort',  'COM_CENTRALSMS_RECIPIENT_HEADING_LASTNAME', 'cellphone', $listDirn, $listOrder); ?>
				</th>
			</tr>
		</thead>
		<tfoot>
			<tr>
				<td colspan="5"><?php echo $this->pagination->getListFooter(); ?></td>
			</tr>
		</tfoot>
		<?php foreach($this->items as $i => $item): ?>
			<tr class="row<?php echo $i % 2 ?>">
				<td>
					<?php echo JHtml::_('grid.id', $i, $item->id) ?>
				</td>
				<td>
					<a href="<?php echo JRoute::_('index.php?option=com_centralsms&task=recipient.edit&id='. (int)$item->id) ?>">
						<?php echo $this->escape($item->firstname); ?>
					</a>
				</td>
				<td>
					<a href="<?php echo JRoute::_('index.php?option=com_centralsms&task=recipient.edit&id='. (int)$item->id) ?>">
						<?php echo $this->escape($item->lastname) ?>
					</a>
				</td>
				<td>
					<?php echo $this->escape($item->neighborhood) ?>
				</td>
				<td>
					<?php echo $this->escape($item->cellphone) ?>
				</td>
			</tr>
		<?php endforeach ?>
	</table>

	<div>
		<input type="hidden" name="task" value="" />
		<input type="hidden" name="boxchecked" value="0" />
		<input type="hidden" name="filter_order" value="<?php echo $listOrder; ?>" />
		<input type="hidden" name="filter_order_Dir" value="<?php echo $listDirn; ?>" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
