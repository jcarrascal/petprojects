<?php

define('BASE_PATH', dirname(__FILE__));
require_once BASE_PATH . '/../../../configuration.php';
require_once BASE_PATH . '/Zend/Mail.php';

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
$conn->query($sql) or die($conn->error);

$sql = "select *
	from {$dbprefix}massmailer_emails e
	inner join {$dbprefix}massmailer_messages m on m.id = e.message_id
	where process = '$process'
	order by e.id";
$result = $conn->query($sql) or die($conn->error);
$template = file_get_contents(dirname(__FILE__) . '/template/index.html');
$messages_count = 0;
while (($row = $result->fetch_object()) != null) {
	$message = prepareMail($row, $template);
	try {
		$message->send();
		$sql = "update {$dbprefix}massmailer_emails
			set sent_on = CURRENT_TIMESTAMP
			where id = {$row->id}";
		$conn->query($sql) or die($conn->error . " $sql");
		++$messages_count;
	} catch (Exception $ex) {
		echo $ex->getMessage(), "<br/>\n", $ex->getTraceAsString();
	}
}
$result->close();

function prepareMail($row, $template)
{
	$variables = unserialize($row->variables);
	$variables['subject'] = $row->subject;
	$variables['content'] = template($row->content, $variables);
	$mail = new Zend_Mail('UTF-8');
	$mail->setType(Zend_Mime::MULTIPART_RELATED);
	$mail->setFrom($row->from_email, $row->from_name);
	$mail->setSubject($row->subject);
	$mail->addTo($row->recipient, "{$variables['firstname']} {$variables['lastname']}");
	$htmlMessage = template($template, $variables);
	if (preg_match_all('/src="([^"]+)"/i', $htmlMessage, $images, PREG_SET_ORDER))
	foreach ($images as $image)
	{
		$att = $mail->createAttachment(file_get_contents(BASE_PATH . '/template/' . $image[1]),
			'image/' . trim(strrchr($image[1], '.'), '.'), Zend_Mime::DISPOSITION_INLINE,
			Zend_Mime::ENCODING_BASE64);
		$att->id = md5($image[1]);
		$htmlMessage = str_replace($image[0], "src=\"cid:$att->id\"", $htmlMessage);
	}
	$mail->setBodyHtml($htmlMessage, null, Zend_Mime::MULTIPART_RELATED);
	return $mail;
}

function template($text, $variables)
{
	$keys = array();
	foreach (array_keys($variables) as $key)
		$keys[] = "{{$key}}";
	$values = array_values($variables);
	return str_replace($keys, $values, $text);
}
