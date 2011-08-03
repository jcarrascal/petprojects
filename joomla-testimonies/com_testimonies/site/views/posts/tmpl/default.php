<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');

$form = $this->form;

?>
<link href="<?php echo $this->baseurl ?>/components/com_testimonies/css/default.css" rel="stylesheet" type="text/css"/>
<link href="<?php echo $this->baseurl ?>/components/com_foxcontact/css/neon.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
<script type="text/javascript">
	window.addEvent('domready', function(){ 
		var JTooltips = new Tips($$('.hasTip'), { maxTitleChars: 300, fixed: false}); 
	});
	$(function() {
		$('#jform_message').focusin(function() {
			$('#form_extended').slideDown();
		});
		$('#cancel_form').click(function() {
			$('#form_extended').slideUp();
		});
	});
</script>
<div id="com_testimonies">
	<div id="header"><a href="<?php echo $this->baseurl ?>/index.php"><img alt="" height="149" src="<?php echo $this->baseurl ?>/components/com_testimonies/images/header.png" width="965"></a></div>
	<div id="form">
		<form action="" class="foxform" method="post">
			<?php echo $form->getInput('message') ?>
			<div id="form_extended">
				<table class="form">
					<tr>
						<th><?php echo $form->getLabel('name') ?></th>
						<th><?php echo $form->getLabel('email') ?></th>
					</tr>
					<tr>
						<td><?php echo $form->getInput('name') ?></td>
						<td><?php echo $form->getInput('email') ?></td>
					</tr>
					<tr>
						<th><?php echo $form->getLabel('picture') ?></th>
						<th><?php echo $form->getLabel('neighborhood') ?></th>
					</tr>
					<tr>
						<td><?php echo $form->getInput('picture') ?></td>
						<td><?php echo $form->getInput('neighborhood') ?></td>
					</tr>
				</table>
				<div style="text-align:center">
					<button class="foxbutton" type="submit"><span><?php echo JText::_('COM_TESTIMONIES_BUTTON_SUBMIT') ?></span></button>
					<button class="foxbutton" id="cancel_form" type="button"><span><?php echo JText::_('COM_TESTIMONIES_BUTTON_CANCEL') ?></span></button>
				</div>
			</div>
		</form>
	</div>
	<div id="testimonies">
		<table>
			<tr>
			<?php $i = 0; foreach($this->items as $item): ?>
				<?php if ($i > 0 && ($i % 9) == 0) { ?></tr><tr><?php } ?>
				<?php if ($i == 21) { ?><td id="featured-testimony" colspan="3" rowspan="2"><img alt="" height="142" src="<?php echo $this->baseurl ?>/components/com_testimonies/images/participa.png" width="218"></div><?php $i += 3; } ?>
				<?php if ($i == 30) { $i += 3; } ?>
				<td id="testimony-<?php echo $item->id ?>" class="testimony hasTip" title="<?php echo $this->escape($item->name), '::', $this->escape($item->message) ?>">
					<img alt="" height="66" src="<?php echo $this->baseurl ?>/images/com_testimonies/<?php echo $this->escape($item->picture) ?>" width="66">
				</td>
			<?php ++$i; endforeach; ?>
			</tr>
		</table>
	</div>
	<div id="pagination"><?php echo $this->pagination->getPagesLinks(); ?></div>
</div>
<script type="text/javascript">
</script>