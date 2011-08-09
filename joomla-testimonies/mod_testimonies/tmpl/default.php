<?php

$testimony = $helper->fetchLatestTestimony();

?>
<h4>
	<a href="/index.php/testimonios"><img alt="Vivir mejor" height="58" src="/templates/barrancabermejaquiere/images/vivirmejor_s1.png" width="193"></a>
</h4>
<ul class="category-module testimonies">
	<li>
		<h4>
			<img alt="<?php echo htmlentities($testimony->name, ENT_COMPAT, 'UTF-8') ?>" height="66" src="/images/com_testimonies/<?php echo htmlentities($testimony->picture, ENT_COMPAT, 'UTF-8') ?>" width="66">
			<?php echo htmlentities($testimony->name, ENT_COMPAT, 'UTF-8') ?>
		</h4>
		<p class="mod-articles-category-introtext">
			<?php echo htmlentities($testimony->message, ENT_COMPAT, 'UTF-8') ?>
		</p>
	</li>
</ul>