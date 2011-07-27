<?php

defined('_JEXEC') or die('Restricted access');// no direct access
 
// load tooltip behavior
JHtml::_('behavior.tooltip');
?>
<form action="<?php echo JRoute::_('index.php?option=com_centralcms'); ?>" method="post" name="adminForm">
	<table class="adminlist">
		<thead><?php echo $this->loadTemplate('head') ?></thead>
		<tfoot><?php echo $this->loadTemplate('foot') ?></tfoot>
		<tbody><?php echo $this->loadTemplate('body') ?></tbody>
	</table>
</form>
