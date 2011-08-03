<?php

defined('_JEXEC') or die('Restricted access');// no direct access

// load tooltip behavior
JHtml::_('behavior.tooltip');
JHtml::_('behavior.formvalidation');

?>
<link href="<?php echo $this->baseurl ?>/components/com_testimonies/css/default.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript">
	window.addEvent('domready', function(){ 
		var JTooltips = new Tips($$('.hasTip'), { maxTitleChars: 300, fixed: false}); 
	});
</script>
<div id="com_testimonies">
	<div id="header"><a href="<?php echo $this->baseurl ?>/index.php"><img alt="" height="149" src="<?php echo $this->baseurl ?>/components/com_testimonies/images/header.png" width="965"></a></div>
	<?php
	// Page Heading if needed
	if ($this->params->get('show_page_heading'))
		echo '<h1><span>', $this->escape($this->params->get('page_heading')), '</span></h1>', PHP_EOL;
	?>
	<table id="testimonies">
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
	<div id="pagination"><?php echo $this->pagination->getPagesLinks(); ?></div>
</div>
