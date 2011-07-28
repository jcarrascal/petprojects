<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');

$form = $this->form;

?>
<div id="system-message-container">
	<dl id="system-message">
		<dt class="message">Mensaje</dt>
		<dd class="message message">
			<ul>
				<li>El elemento ha sido enviado correctamente.</li>
			</ul>
		</dd>
	</dl>
</div>
<div id="com_centralsms">
<?php
// Page Heading if needed
if ($this->params->get('show_page_heading'))
	echo '<h1><span>', $this->escape($this->params->get('page_heading')), '</span></h1>', PHP_EOL;
?>
<p><?php echo JText::_('COM_CENTRALSMS_INTRO_TEXT') ?></p>
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
	<form action="<?php echo JRoute::_('index.php?option=com_centralsms&layout=save'); ?>" method="post" name="adminForm" id="item-form" class="form-validate foxform">
		<p><?php echo $form->getLabel('firstname') ?><br/>
			<?php echo $form->getInput('firstname') ?></p>
		<p><?php echo $form->getLabel('lastname') ?><br/>
			<?php echo $form->getInput('lastname') ?></p>
		<p><?php echo $form->getLabel('neighborhood') ?><br/>
			<?php echo $form->getInput('neighborhood') ?></p>
		<p><?php echo $form->getLabel('country') ?><br/>
			<?php echo $form->getInput('country') ?></p>
		<p><?php echo $form->getLabel('cellphone') ?><br/>
			<?php echo $form->getInput('cellphone') ?></p>
		<p><button type="submit"><span><?php echo JText::_('COM_CENTRALSMS_BUTTON_SUBMIT') ?></span></button></p>
		<p><?php echo JText::_('COM_CENTRALSMS_PRIVACY_TEXT') ?></p>
		<div>
			<input type="hidden" name="task" value="centralsms.save" />
			<?php echo JHtml::_('form.token'); ?>
		</div>
	</form>
</div>
