<?php

defined('_JEXEC') or die('Restricted access');// no direct access

jimport( 'joomla.cache.cache' );
require_once dirname(__FILE__) . '/facebook.php';

class ModSocialNetworks
{
	function __construct($params)
	{
		$this->youtubeUser = $params->get('youtubeUser');
		$this->twitterUser = $params->get('twitterUser');
		$this->facebookUser = $params->get('facebookUser');
		$this->facebookAppId = $params->get('facebookAppId');
		$this->facebookSecret = $params->get('facebookSecret');
		$this->cacheSeconds = (int)$params->get('cacheSeconds', 3600);
		$this->cache = JCache::getInstance();
		$this->youtube = false;
		$this->twitter = false;
		$this->facebook = false;
	}

	function isYoutubeVisible() { return $this->youtubeUser != ''; }

	function fetchLatestYoutube()
	{
		$youtube = $this->youtube or $this->cache->get("fetchLatestYoutube_{$this->youtubeUser}", 'mod_socialnetworks');
		if ($youtube !== false)
			$youtube = unserialize($youtube);
		else
		{
			$youtubeUser = urlencode($this->youtubeUser);
			$url = "http://gdata.youtube.com/feeds/api/users/{$this->youtubeUser}/playlists?max-results=1&alt=json";
			$playlists = json_decode(file_get_contents($url));
			$playlistId = $playlists->feed->entry[0]->{'yt$playlistId'}->{'$t'};
			if ($playlistId == '')
				return null;
			$url = 'http://gdata.youtube.com/feeds/api/playlists/' . urlencode($playlistId);
			$playlist = new SimpleXMLElement(str_replace('yt:', '', str_replace('media:', '', file_get_contents($url))));
			$youtube = new stdclass();
			$youtube->content_url = (string)$playlist->entry->group->content['url'];
			$youtube->youtubeid = substr($youtube->content_url, strlen('http://www.youtube.com/v/'), 11);
			$youtube->title = (string)$playlist->entry->group->title;
			$youtube->description = (string)$playlist->entry->group->description;
			$youtube->keywords = (string)$playlist->entry->group->keywords;
			$youtube->player = (string)$playlist->entry->group->player['url'];
			$youtube->thumbnail = (string)$playlist->entry->group->thumbnail[0]['url'];
			$this->cache->store($this->youtube = serialize($youtube), "fetchLatestYoutube_{$this->twitterUser}", 'mod_socialnetworks');
		}
		return $youtube;
	}

	function isTwitterVisible() { return $this->twitterUser != ''; }

	function fetchLatestTweeter()
	{
		$twitter = $this->twitter or $this->cache->get("fetchLatestTweeter_{$this->twitterUser}", 'mod_socialnetworks');
		if ($twitter !== false)
			$twitter = unserialize($twitter);
		else
		{
			$url = 'https://api.twitter.com/1/statuses/user_timeline.json?count=1&screen_name=' .
				urlencode($this->twitterUser);
			$json = json_decode(file_get_contents($url));
			if (count($json) == 0)
				return null;
			$twitter = new stdClass();
			$twitter->screen_name = $json[0]->user->screen_name;
			$twitter->name = $json[0]->user->name;
			$twitter->text = $json[0]->text;
			$twitter->created_at = strtotime($json[0]->created_at);
			$this->cache->store($this->twitter = serialize($twitter), "fetchLatestTweeter_{$this->twitterUser}", 'mod_socialnetworks');
		}
		return $twitter;
	}

	function isFacebookVisible() { return $this->facebookUser != '' && $this->facebookAppId != '' && $this->facebookSecret != ''; }

	function fetchLatestFacebook()
	{
		$facebook = $this->facebook or $this->cache->get("fetchLatestFacebook_{$this->facebookUser}", 'mod_socialnetworks');
		if ($facebook !== false)
			$facebook = unserialize($facebook);
		else
		{
			$fb = new Facebook(array('appId' => $this->facebookAppId, 'secret' => $this->facebookSecret));
			$this->cache->store($this->facebook = serialize($facebook), "fetchLatestFacebook_{$this->facebookUser}", 'mod_socialnetworks');
		}
		return $facebook;
	}
}
