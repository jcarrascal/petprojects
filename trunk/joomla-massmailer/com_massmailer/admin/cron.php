<?php

require_once dirname(__FILE__) . '/../../../configuration.php';

$config = new JConfig();
$dbprefix = $config->dbprefix;
$conn = new mysqli($config->host, $config->user, $config->password, $config->db)
	or die('Can\'t connect to the database.');
$conn->set_charset('utf8');

$process = md5(microtime());
$sql = "update {$dbprefix}massmailer_emails set process = '$process', started_on = CURRENT_TIMESTAMP
	where sent_on is null and (process is null or started_on < addtime(CURRENT_TIMESTAMP, '-01:00:00'))
	order by id
	limit 3";

$sql = "update {$dbprefix}massmailer_emails set process = '$process', started_on = CURRENT_TIMESTAMP
	where sent_on is null
	order by id
	limit 3";
$conn->query($sql) or die($conn->error);

$sql = "select *
	from {$dbprefix}massmailer_emails e
	inner join {$dbprefix}massmailer_messages m on m.id = e.message_id
	where process = '$process'
	order by e.id";
$result = $conn->query($sql) or die($conn->error);
$template = file_get_contents(dirname(__FILE__) . '/template/index.html');
while (($email = $result->fetch_object()) != null) {
	$variables = unserialize($email->variables);
	$variables['subject'] = $email->subject;
	$variables['content'] = template($email->content, $variables);
	$body = template($template, $variables);
	var_dump($body);
}
$result->close();

function template($text, $variables)
{
	$keys = array_map(function($s) { return "{{$s}}"; }, array_keys($variables));
	$values = array_values($variables);
	return str_replace($keys, $values, $text);
}
