<?php

class ArticleModel extends BB\MVC\Model
{

	function fetchFrontPageArticles($pageIndex, $pageSize=10)
	{
		$offset = $pageIndex * $pageSize;
		$db = $this->connect();
		$sql = "select a.*, c.name categoryName, c.slug categorySlug,
		mc.name mainCategoryName, mc.slug mainCategorySlug
	from bb_article a
		inner join bb_articlecategory ac on ac.articleId = a.articleId
		inner join bb_category c on c.categoryId = ac.categoryId
		inner join bb_category mc on case when c.parentId is null then mc.categoryId = c.categoryId
			else mc.parentId is null and mc.lft < c.lft and mc.rgt > c.rgt end
	where a.inFrontPage <> 0 and a.isPublished = 1 and a.publishedAt <= CURRENT_TIMESTAMP
		and (a.expiresAt is null or a.expiresAt >= CURRENT_TIMESTAMP)
	order by a.publishedAt desc
	limit $offset, $pageSize";
		$articles = new stdClass();
		$articles->pageIndex = $pageIndex;
		$articles->items = $db->fetchAll($sql);
		$sql = "select count(*)
	from bb_article a
		inner join bb_articlecategory ac on ac.articleId = a.articleId
		inner join bb_category c on c.categoryId = ac.categoryId
		inner join bb_category mc on case when c.parentId is null then mc.categoryId = c.categoryId
			else mc.parentId is null and mc.lft < c.lft and mc.rgt > c.rgt end
	where a.inFrontPage <> 0 and a.isPublished = 1 and a.publishedAt <= CURRENT_TIMESTAMP
		and (a.expiresAt is null or a.expiresAt >= CURRENT_TIMESTAMP)";
		$articles->pagesCount = ceil($db->fetchScalar($sql) / $pageSize);
		foreach ($articles->items as $article)
		{
			$article->url = ROOT_URL . '/articles'
				. as_url($article->mainCategorySlug != $article->categorySlug ? '/' . $article->mainCategorySlug : '')
				. '/' . as_url($article->categorySlug) . '/' . as_url($article->slug);
			$article->publishedAt = new Zend_Date($article->publishedAt);
			if (null != $article->expiresAt)
				$article->expiresAt = new Zend_Date($article->expiresAt);
		}
		return $articles;
	}
}
