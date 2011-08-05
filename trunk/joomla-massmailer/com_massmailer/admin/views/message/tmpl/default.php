<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');

$form = $this->form;

?>
<script language="javascript">
	Joomla.submitbutton = function(task) {
		if (document.formvalidator.isValid(document.id('item-form'))) {
			Joomla.submitform(task, document.getElementById('item-form'));
		} else {
			alert('<?php echo $this->escape(JText::_('JGLOBAL_VALIDATION_FORM_FAILED')) ?>');
		}
	}
	<?php if ($this->success) : ?>
		window.top.setTimeout('window.parent.SqueezeBox.close()', 5000);
	<?php endif ?>
</script>
<form action="<?php echo JRoute::_('index.php?option=com_massmailer&view=message&layout=save&tmpl=component'); ?>" method="post" name="adminForm" id="item-form" class="form-validate">
	<fieldset class="adminform">
		<legend><?php echo JText::_('COM_MASSMAILER_MESSAGE_DETAILS'); ?></legend>
		<ul class="adminformlist">
			<li>
				<label class="hasTip" title="<?php echo JText::_('COM_MASSMAILER_MESSAGE_FROM_EMAIL_DESC') ?>" for="jform_from_email"><?php echo JText::_('COM_MASSMAILER_MESSAGE_FROM_EMAIL_LABEL') ?></label>
				<input id="jform_from_email" class="inputbox required" name="from_email" type="text" style="width:350px" value="<?php echo $this->escape($form->from_email) ?>"/>
			</li>
			<li>
				<label class="hasTip" title="<?php echo JText::_('COM_MASSMAILER_MESSAGE_FROM_NAME_DESC') ?>" for="jform_from_name"><?php echo JText::_('COM_MASSMAILER_MESSAGE_FROM_NAME_LABEL') ?></label>
				<input id="jform_from_name" class="inputbox required" name="from_name" type="text" style="width:350px" value="<?php echo $this->escape($form->from_name) ?>"/>
			</li>
			<li>
				<label class="hasTip" title="<?php echo JText::_('COM_MASSMAILER_MESSAGE_SUBJECT_DESC') ?>" for="jform_subject"><?php echo JText::_('COM_MASSMAILER_MESSAGE_SUBJECT_LABEL') ?></label>
				<input id="jform_subject" class="inputbox required" name="subject" type="text" style="width:350px" value="<?php echo $this->escape($form->subject) ?>"/>
			</li>
			<li>
				<label class="hasTip" title="<?php echo JText::_('COM_MASSMAILER_MESSAGE_CONTENT_DESC') ?>" for="jform_content"><?php echo JText::_('COM_MASSMAILER_MESSAGE_CONTENT_LABEL') ?></label>
				<div style="clear:both"></div>
				<?php
					$editor =& JFactory::getEditor();
					echo $editor->display('content', $form->content, '500', '250', '60', '20', false);
				?>
			</li>
		</ul>
	</fieldset>
	<div class="clr"></div>
	<button type="button" onclick="this.form.submit();"><?php echo JText::_('COM_MASSMAILER_BUTTON_SEND') ?></button>
	<button type="button" onclick="window.parent.SqueezeBox.close();"><?php echo JText::_('COM_MASSMAILER_BUTTON_CANCEL') ?></button>
	<div>
		<input type="hidden" name="task" value="message.save" />
		<?php echo JHtml::_('form.token'); ?>
	</div>
</form>
