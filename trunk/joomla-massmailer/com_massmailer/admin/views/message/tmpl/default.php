<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');
?>
<script language="javascript">
	Joomla.submitbutton = function(task) {
		if (document.formvalidator.isValid(document.id('item-form'))) {
			Joomla.submitform(task, document.getElementById('item-form'));
		} else {
			alert('<?php echo $this->escape(JText::_('JGLOBAL_VALIDATION_FORM_FAILED')) ?>');
		}
	}
	<?php if ($this->item->id > 0) : ?>
		window.top.setTimeout('window.parent.SqueezeBox.close()', 5000);
	<?php endif ?>
</script>
<form action="<?php echo JRoute::_('index.php?option=com_massmailer&view=save&layout=save&tmpl=component&id='.(int) $this->item->id); ?>" method="post" name="adminForm" id="item-form" class="form-validate">
	<fieldset class="adminform">
		<legend><?php echo JText::_('COM_MASSMAILER_MESSAGE_DETAILS'); ?></legend>
		<ul class="adminformlist">
			<?php foreach($this->form->getFieldset() as $field): ?>
				<li><?php echo $field->label;echo $field->input ?></li>
			<?php endforeach; ?>
		</ul>
	</fieldset>
	<div class="clr"></div>
	<button type="button" onclick="this.form.submit();"><?php echo JText::_('COM_MASSMAILER_BUTTON_SEND') ?></button>
	<button type="button" onclick="window.parent.SqueezeBox.close();"><?php echo JText::_('COM_MASSMAILER_BUTTON_CANCEL') ?></button>
	<div>
		<input type="hidden" name="task" value="message.apply" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
