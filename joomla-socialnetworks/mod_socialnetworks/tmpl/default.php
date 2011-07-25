<?php
$helper->fetchLatestFacebook();
if ($helper->isYoutubeVisible()) {
	$youtube = $helper->fetchLatestYoutube();
?>
	<div class="youtube column first">
		<h4><a href="<?php echo $youtube->player ?>"><?php echo htmlentities($youtube->title, ENT_COMPAT, 'UTF-8') ?></a></h4>
		<div class="embed"><iframe width="560" height="349" src="http://www.youtube.com/embed/<?php echo $youtube->videoid ?>" frameborder="0" allowfullscreen></iframe></div>
		<div class="description"><?php echo htmlentities($youtube->description, ENT_COMPAT, 'UTF-8') ?></div>
	</div>
<?php } ?>

<?php
if ($helper->isTwitterVisible()) {
	$tweet = $helper->fetchLatestTweeter();
?>
	<div class="twitter column<?php if (!$helper->isYoutubeVisible()) { ?> first<?php } ?>">
		<div class="text">
			<a class="screen_name" href="http://twitter.com/<?php echo $tweet->screen_name ?>">@<?php echo $tweet->screen_name ?></a>
			<?php echo htmlentities($tweet->text, ENT_COMPAT, 'UTF-8') ?>
		</div>
	</div>
<?php } ?>

<?php
if ($helper->isFacebookVisible()) {
	$tweet = $helper->fetchLatestFacebook();
?>
	<div class="facebook column<?php if (!$helper->isYoutubeVisible() && !$helper->isFacebookVisible()) { ?> first<?php } ?>">
		<div class="text">
		</div>
	</div>
<?php } ?>
