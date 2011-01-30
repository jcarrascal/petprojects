<?php

require_once LIBRARY_PATH . '/modules/content/models/articlemodel.php';

class ArticleController extends BB\MVC\Controller
{
	var $model;

	function __construct()
	{
		$this->model = new ArticleModel();
	}

	function indexAction($mainArticlesCount=1, $summaryArticlesCount=4, $linkArticlesCount=5)
	{
		$this->viewData['mainArticlesCount'] = (int) $mainArticlesCount;
		$this->viewData['summaryArticlesCount'] = (int) $summaryArticlesCount;
		$this->viewData['linkArticlesCount'] = (int) $linkArticlesCount;
		$this->viewData['viewData'] = $this->model->fetchFrontPageArticles((int) $this->request['page'],
				$mainArticlesCount + $summaryArticlesCount + $linkArticlesCount);
		return $this->view();
	}
}
