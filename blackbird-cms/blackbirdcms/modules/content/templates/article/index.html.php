<?php
$this->inherits('default');
$this->beginBlock('mainColumn');
?>
<div id="article-index">
	<?php foreach ($this->articles->items as $article) { ?>
	<div class="article <?php echo as_attribute($article->slug) ?>">
		<h3><?php echo as_html($article->title) ?></h3>
		<p><?php echo as_html($article->summary) ?></p>
	</div>
	<?php } ?>
</div>
<?php $this->endBlock(); ?>
