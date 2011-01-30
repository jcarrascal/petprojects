<?php

require_once LIBRARY_PATH . '/components/articles/models/articles.php';

class IndexController extends BB\MVC\Controller
{

	function indexAction()
	{
		return $this->view();
	}
}