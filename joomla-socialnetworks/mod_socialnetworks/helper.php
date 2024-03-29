<?php

defined('_JEXEC') or die('Restricted access');// no direct access

class ModSocialNetworks
{
	function __construct($params)
	{
		$this->youtubeUser = $params->get('youtubeUser');
		$this->twitterUser = $params->get('twitterUser');
		$this->facebookFeed = $params->get('facebookFeed');
		$this->cacheSeconds = (int)$params->get('cacheSeconds', 3600);
		$this->youtube = false;
		$this->twitter = false;
		$this->facebook = false;
	}

	function isYoutubeVisible() { return $this->youtubeUser != ''; }

	function fetchLatestYoutube()
	{
		$youtube = $this->youtube or $this->cacheGet("fetchLatestYoutube_{$this->youtubeUser}", $this->cacheSeconds);
		if ($youtube === false)
		{
			try {
				$youtubeUser = urlencode($this->youtubeUser);
				$url = "http://gdata.youtube.com/feeds/api/users/{$this->youtubeUser}/playlists?max-results=1&alt=json";
				$playlists = json_decode(file_get_contents($url));
				$playlistId = $playlists->feed->entry[0]->{'yt$playlistId'}->{'$t'};
				if ($playlistId == '')
					return $this->youtube = $this->cacheGet("fetchLatestYoutube_{$this->youtubeUser}");
				$url = 'http://gdata.youtube.com/feeds/api/playlists/' . urlencode($playlistId);
				$playlist = new SimpleXMLElement(str_replace('yt:', '', str_replace('media:', '', file_get_contents($url))));
				$youtube = new stdclass();
				$youtube->content_url = (string)$playlist->entry->group->content['url'];
				$youtube->videoid = substr($youtube->content_url, strlen('http://www.youtube.com/v/'), 11);
				$youtube->title = (string)$playlist->entry->group->title;
				$youtube->description = (string)$playlist->entry->group->description;
				$youtube->keywords = (string)$playlist->entry->group->keywords;
				$youtube->player = (string)$playlist->entry->group->player['url'];
				$youtube->thumbnail = (string)$playlist->entry->group->thumbnail[0]['url'];
				$this->cacheSet($this->youtube = $youtube, "fetchLatestYoutube_{$this->youtubeUser}");
			} catch (Exception $ex) {
				$youtube = $this->cacheGet("fetchLatestYoutube_{$this->youtubeUser}");
			}
		}
		return $youtube;
	}

	function isTwitterVisible() { return $this->twitterUser != ''; }

	function fetchLatestTwitter()
	{
		$twitter = $this->twitter or $this->cacheGet("fetchLatestTwitter_{$this->twitterUser}", $this->cacheSeconds);
		if ($twitter === false)
		{
			try {
				$url = 'https://api.twitter.com/1/statuses/user_timeline.json?count=1&screen_name=' .
					urlencode($this->twitterUser);
				$json = json_decode(file_get_contents($url));
				if (count($json) == 0)
					return $this->twitter = $this->cacheGet("fetchLatestTwitter_{$this->twitterUser}");
				$twitter = new stdClass();
				$twitter->screen_name = $json[0]->user->screen_name;
				$twitter->name = $json[0]->user->name;
				$twitter->text = $json[0]->text;
				$twitter->created_at = strtotime($json[0]->created_at);
				$this->cacheSet($this->twitter = $twitter, "fetchLatestTwitter_{$this->twitterUser}");
			} catch (Exception $ex) {
				$twitter = $this->cacheGet("fetchLatestTwitter_{$this->twitterUser}");
			}
		}
		return $twitter;
	}

	function isFacebookVisible() { return $this->facebookFeed != ''; }

	function fetchLatestFacebook()
	{
		$facebook = $this->facebook or $this->cacheGet("fetchLatestFacebook_{$this->facebookFeed}", $this->cacheSeconds);
		if ($facebook === false)
		{
			try
			{
				$content = file_get_contents($this->facebookFeed);
				$fb = new SimpleXmlElement($content);
				$facebook = new stdClass();
				$facebook->text = (string)($fb->channel->item[0]->description);
				$facebook->link = (string)($fb->channel->item[0]->link);
				$facebook->pubDate = date('Y-m-d', strtotime($fb->channel->item[0]->pubDate));
				$this->cacheSet($this->facebook = $facebook, 'fetchLatestFacebook_' . substr($this->facebookFeed, 19));
			} catch (Exception $ex) {
				$facebook = $this->cacheGet("fetchLatestFacebook_{$this->facebookFeed}");
			}
		}
		return $facebook;
	}

	function cacheGet($type, $lifeTime = 0)
	{
		$db = JFactory::getDbo();
		$query = $db->getQuery(true);
		$query->select('a.serialized');
		$query->from('#__socialnetworks_cache AS a');
		$query->where('a.type = ' . $db->Quote($type));
		if ($lifeTime > 0)
			$query->where('a.updated_at >= ' . $db->quote(date('Y-m-d H:i:s', time() - $lifeTime)));
		$db->setQuery($query);
		$data = $db->loadObject();
		if ($error = $db->getErrorMsg())
			throw new Exception($error);
		if (!empty($data))
			return unserialize($data->serialized);
		return false;
	}

	function cacheSet($data, $type)
	{
		$db = JFactory::getDbo();
		$type = $db->quote($type);
		$updated_at = $db->quote(date('Y-m-d H:i:s'));
		$serialized = $db->quote(serialize($data));
		$sql = "insert into #__socialnetworks_cache (type, updated_at, serialized) values
			($type, $updated_at, $serialized)
			on duplicate key update updated_at = values(updated_at), serialized = values(serialized)";
		$db->setQuery($sql);
		$db->query();
		if ($error = $db->getErrorMsg())
			throw new Exception($error);
	}
}
