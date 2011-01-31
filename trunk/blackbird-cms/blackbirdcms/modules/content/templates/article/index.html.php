<?php
$this->inherits('default');

$this->beginBlock('mainColumn');
$articles = $this->articles;
$articlesCount = count($articles->items);
$mainArticlesCount = $this->mainArticlesCount;
$summaryArticlesCount = $this->summaryArticlesCount;
$linkArticlesCount = $this->linkArticlesCount;
?>
<div id="article-index" class="hfeed">
	<?php
	for ($i = 0; $i < $articlesCount && $i < $mainArticlesCount; ++$i)
	{
		$article = $articles->items[$i];
		$article->url = ROOT_URL . '/content/article/view/' . as_url($article->slug);
		$article->authorUrl = ROOT_URL . '/content/author/view/' . as_url($article->authorName);
		$article->categoryUrl = ROOT_URL . '/content/category/view/' . as_url($article->categorySlug);
	?>
		<div class="article <?php echo as_attribute($article->slug) ?> hentry">
			<h3 class="article-title entry-title"><a href="<?php echo $article->url ?>"><?php echo as_html($article->title) ?></a></h3>
			<p class="article-meta">
											Published on
				<span class="published" title="<?php echo date('c', $article->publishedOn) ?>"><?php echo strftime('%d %b %Y', $article->publishedOn) ?></span>
			by
			<span class="article-author author">
				<a class="vcard" href="<?php echo $article->authorUrl ?>"><span class="fn"><?php echo as_html($article->authorName) ?></span></a>
			</span>
			on
			<span class="article-category"><a href="<?php echo $article->categoryUrl ?>"><?php echo as_html($article->categoryName) ?></a></span>
			.
		</p>
		<div id="article-content entry-content"><?php echo $article->content ?></div>
		<p class="article-footer">
			<a href="<?php echo $article->url ?>" rel="bookmark">Read more...</a>
			| <a href="<?php echo $article->url ?>#comments">Comments <?php if ($article->commentsCount > 0)
				echo "($article->commentsCount)" ?></a>
			<?php
				if ($article->tags)
				{
			?>
																			|
				<?php
					foreach ($article->tags as $tag)
					{
						$tag->url = ROOT_URL . '/content/tags/view/' . $tag->slug;
				?>
					<a class="tag" href="<?php echo $tag->url ?>" rel="tag"><span><?php echo as_html($tag->name) ?></span></a>
			<?php
					}
				}
			?>
			</p>
		</div>
	<?php } ?>
		</div>
<?php $this->endBlock(); ?>
