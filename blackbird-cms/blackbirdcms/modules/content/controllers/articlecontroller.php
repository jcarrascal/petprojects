<?php

require_once LIBRARY_PATH . '/modules/content/models/articlemodel.php';

class ArticleController extends BB\MVC\Controller
{

	function indexAction($mainArticlesCount=10, $summaryArticlesCount=4, $linkArticlesCount=5)
	{
		$model = new ArticleModel($this->config);
		$this->viewData['mainArticlesCount'] = (int) $mainArticlesCount;
		$this->viewData['summaryArticlesCount'] = (int) $summaryArticlesCount;
		$this->viewData['linkArticlesCount'] = (int) $linkArticlesCount;
		$this->viewData['articles'] = $model->fetchFrontPageArticles((int) $this->request['page'],
				$mainArticlesCount + $summaryArticlesCount + $linkArticlesCount);
		return $this->view();
	}
}
