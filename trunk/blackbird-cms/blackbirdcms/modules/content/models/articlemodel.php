<?php

class ArticleModel extends BB\MVC\Model
{

	function fetchFrontPageArticles($pageIndex, $pageSize=10)
	{
		$offset = $pageIndex * $pageSize;
		$db = $this->connect();
		$articles = new stdClass();
		$articles->pageIndex = $pageIndex;
		$sql = "select count(*)
	from bb_article a
		left join bb_category c using (categoryId)
	where a.inFrontPage = 1 and a.isPublished = 1 and a.publishedOn <= CURRENT_TIMESTAMP
		and (a.expiresOn is null or a.expiresOn >= CURRENT_TIMESTAMP)";
		$articles->pagesCount = ceil($db->fetchScalar($sql) / $pageSize);
		$sql = "select a.*, c.name categoryName, c.slug categorySlug,
		0 commentsCount
	from bb_article a
		left join bb_category c using (categoryId)
	where a.inFrontPage = 1 and a.isPublished = 1 and a.publishedOn <= CURRENT_TIMESTAMP
		and (a.expiresOn is null or a.expiresOn >= CURRENT_TIMESTAMP)
	order by a.publishedOn desc
	limit $offset, $pageSize";
		$articles->items = $db->fetchAll($sql);
		foreach ($articles->items as $article)
		{
			$article->publishedOn = strtotime($article->publishedOn);
			if ($article->expiresOn != null)
				$article->expiresOn = strtotime($article->expiresOn);
			$article->tags = array();
		}
		return $articles;
	}
}
