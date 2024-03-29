<?php if ($helper->isYoutubeVisible() && ($youtube = $helper->fetchLatestYoutube())) { ?>
	<div class="youtube column first">
		<h4><a href="<?php echo $youtube->player ?>"><?php echo htmlentities($youtube->title, ENT_COMPAT, 'UTF-8') ?></a></h4>
		<div class="embed"><iframe width="560" height="349" src="http://www.youtube.com/embed/<?php echo $youtube->videoid ?>" frameborder="0" allowfullscreen></iframe></div>
		<div class="description"><?php echo htmlentities(trim($youtube->description) == '' ? $youtube->title : $youtube->description, ENT_COMPAT, 'UTF-8') ?></div>
	</div>
<?php } ?>

<?php if ($helper->isTwitterVisible() && ($twitter = $helper->fetchLatestTwitter())) { ?>
	<div class="twitter column<?php if (!$helper->isYoutubeVisible()) { ?> first<?php } ?>">
		<div class="text">
			<a class="screen_name" href="http://twitter.com/<?php echo $twitter->screen_name ?>">@<?php echo $twitter->screen_name ?></a>
			<?php echo htmlentities($twitter->text, ENT_COMPAT, 'UTF-8') ?>
		</div>
	</div>
<?php } ?>

<?php if ($helper->isFacebookVisible() && ($facebook = $helper->fetchLatestFacebook())) { ?>
	<div class="facebook column<?php if (!$helper->isYoutubeVisible() && !$helper->isFacebookVisible()) { ?> first<?php } ?>">
		<div class="text">
			<?php echo $facebook->text ?>
		</div>
	</div>
<?php } ?>
