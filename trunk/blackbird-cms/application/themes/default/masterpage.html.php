<!doctype html>
<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="no-js"> <!--<![endif]-->
<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

	<title><?php echo $this->pageTitle ?></title>
	<meta name="description" content="<?php echo $this->config('metaDescription') ?>">
	<meta name="author" content="<?php echo $this->config('metaAuthor') ?>">

	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<link rel="shortcut icon" href="<?php echo ROOT_URL ?>/favicon.ico">
	<link rel="apple-touch-icon" href="<?php echo ROOT_URL ?>/apple-touch-icon.png">

	<link rel="stylesheet" href="<?php echo ROOT_URL ?>/css/style.css?v=2">
	<script src="<?php echo ROOT_URL ?>/js/libs/modernizr-1.6.min.js"></script>
</head>

<body>

	<div id=container>
		<header><?php echo $this->content('header') ?></header>

		<div id=main>
			<?php if ($this->hasContent('top')) { ?><div id=top><?php echo $this->content('top') ?></div><?php } ?>

			<?php if ($this->hasContent('leftColumn', 'mainColumn', 'centerColumn', 'rightColumn')) { ?>
			<div id=columns>

				<?php if ($this->hasContent('leftColumn')) { ?><div id=leftColumn><?php echo $this->content('leftColumn') ?></div><?php } ?>

				<?php if ($this->hasContent('mainColumn')) { ?><div id=mainColumn><?php echo $this->content('mainColumn') ?></div><?php } ?>

				<?php if ($this->hasContent('centerColumn')) { ?><div id=centerColumn><?php echo $this->content('centerColumn') ?></div><?php } ?>

				<?php if ($this->hasContent('rightColumn')) { ?><div id=rightColumn><?php echo $this->content('rightColumn') ?></div><?php } ?>

			</div>
			<?php } ?>

			<?php if ($this->hasContent('bottom')) { ?><div id=bottom><?php echo $this->content('bottom') ?></div><?php } ?>

		</div>

		<footer><?php echo $this->content('footer') ?></footer>
	</div> <!-- end of #container -->


	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.js"></script>
	<script>!window.jQuery && document.write('\x3Cscript src="<?php echo ROOT_URL ?>/media/jquery-1.4.4.min.js">\x3C/script>')</script>

	<!--[if lt IE 7 ]>
	<script src="js/libs/dd_belatedpng.js"></script>
	<script> DD_belatedPNG.fix('img, .png_bg'); </script>
	<![endif]-->

	<script>
	var _gaq = [['_setAccount', '<?php echo $this->config('googleAnalytics') ?>'], ['_trackPageview']];
	(function(d, t) {
		var g = d.createElement(t),
		s = d.getElementsByTagName(t)[0];
		g.async = true;
		g.src = ('https:' == location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		s.parentNode.insertBefore(g, s);
	})(document, 'script');
	</script>

</body>
</html>