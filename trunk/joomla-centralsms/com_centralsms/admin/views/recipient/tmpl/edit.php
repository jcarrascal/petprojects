<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
// load tooltip behavior
JHtml::_('behavior.tooltip');
?>
<form action="<?php echo JRoute::_('index.php?option=com_centralsms&layout=edit&id='.(int) $this->item->id); ?>" method="post" name="adminForm" id="centralsms-form">
	<fieldset class="adminform">
		<legend><?php echo JText::_( 'COM_CENTRALSMS_RECIPIENT_DETAILS' ); ?></legend>
		<ul class="adminformlist">
			<?php foreach($this->form->getFieldset() as $field): ?>
				<li><?php echo $field->label;echo $field->input;?></li>
			<?php endforeach; ?>
		</ul>
	</fieldset>
	<div>
		<input type="hidden" name="task" value="centralsms.edit" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
