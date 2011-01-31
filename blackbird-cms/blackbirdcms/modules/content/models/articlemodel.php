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
	where a.inFrontPage = 1 and a.isPublished = 1 and a.publishedAt <= CURRENT_TIMESTAMP
		and (a.expiresAt is null or a.expiresAt >= CURRENT_TIMESTAMP)";
		$articles->pagesCount = ceil($db->fetchScalar($sql) / $pageSize);
		$sql = "select a.*, c.name categoryName, c.slug categorySlug
	from bb_article a
		left join bb_category c using (categoryId)
	where a.inFrontPage = 1 and a.isPublished = 1 and a.publishedAt <= CURRENT_TIMESTAMP
		and (a.expiresAt is null or a.expiresAt >= CURRENT_TIMESTAMP)
	order by a.publishedAt desc
	limit $offset, $pageSize";
		$articles->items = $db->fetchAll($sql);
		return $articles;
	}
}
