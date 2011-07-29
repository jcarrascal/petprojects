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
	<?php if ($this->status == 'success') : ?>
		window.top.setTimeout('window.parent.location.reload(true)', 5000);
	<?php endif ?>
</script>
<form action="<?php echo JRoute::_('index.php?option=com_centralsms&view=import&layout=upload'); ?>" method="post" name="adminForm" id="item-form" class="form-validate" enctype="multipart/form-data">
	<fieldset class="adminform">
		<legend><?php echo JText::_( 'COM_CENTRALSMS_IMPORT_DETAILS' ); ?></legend>
		<ul class="adminformlist">
			<li>
				<label for="id_csv_file"><?php echo JText::_('COM_CENTRALSMS_IMPORT_CSV_FILE_LABEL') ?></label>
				<input id="id_csv_file" class="inputbox required" name="csv_file" type="file" />
			</li>
			<li>
				<label for="id_skip_first"><?php echo JText::_('COM_CENTRALSMS_IMPORT_SKIP_FIRST_LABEL') ?></label>
				<input checked="checked" id="id_skip_first" name="skip_first" type="checkbox" />
			</li>
		</ul>
	</fieldset>
	<div class="clr"></div>
	<button type="button" onclick="this.form.submit();"><?php echo JText::_('COM_CENTRALSMS_BUTTON_SEND') ?></button>
	<button type="button" onclick="window.parent.SqueezeBox.close();"><?php echo JText::_('COM_CENTRALSMS_BUTTON_CANCEL') ?></button>
	<div>
		<input type="hidden" name="task" value="import.upload" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
