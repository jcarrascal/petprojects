<?php

require_once LIBRARY_PATH . '/modules/content/models/contentmodel.php';

class IndexController extends BB\MVC\Controller
{
	var $model = new ContentModel();

	function indexAction($mainArticlesCount=1, $summaryArticlesCount=4, $linkArticlesCount=5)
	{
		$this->viewData['mainArticlesCount'] = $mainArticlesCount;
		$this->viewData['summaryArticlesCount'] = $summaryArticlesCount;
		$this->viewData['linkArticlesCount'] = $linkArticlesCount;

		$page = $this->request['page'];
		return $this->view();
	}
}
