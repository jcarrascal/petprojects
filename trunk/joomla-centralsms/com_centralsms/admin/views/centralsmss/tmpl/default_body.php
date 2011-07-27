<?php
// No direct access to this file
defined('_JEXEC') or die('Restricted Access');
?>
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
