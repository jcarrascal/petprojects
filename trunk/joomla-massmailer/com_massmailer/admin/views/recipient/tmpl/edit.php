<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');
?>
<script language="javascript">
	Joomla.submitbutton = function(task) {
		if (task == 'recipient.cancel') {
			Joomla.submitform(task, document.getElementById('item-form'));
		} else if (document.formvalidator.isValid(document.id('item-form'))) {
			Joomla.submitform(task, document.getElementById('item-form'));
		} else {
			alert('<?php echo $this->escape(JText::_('JGLOBAL_VALIDATION_FORM_FAILED'));?>');
		}
	}
</script>
<form action="<?php echo JRoute::_('index.php?option=com_massmailer&view=recipient&layout=edit&id='.(int) $this->item->id); ?>" method="post" name="adminForm" id="item-form" class="form-validate">
	<fieldset class="adminform">
		<legend><?php echo JText::_( 'COM_MASSMAILER_RECIPIENT_DETAILS' ); ?></legend>
		<ul class="adminformlist">
			<?php foreach($this->form->getFieldset() as $field): ?>
				<li><?php echo $field->label;echo $field->input;?></li>
			<?php endforeach; ?>
		</ul>
	</fieldset>
	<div>
		<input type="hidden" name="task" value="massmailer.edit" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
