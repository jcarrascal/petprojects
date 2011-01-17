<?php


/** Returns the same string. */
function as_text($value)
{
	return $value;
}

/** Encodes using the htmlspecialchars() function. UTF-8 safe. */
function as_html($value)
{
	return htmlspecialchars($value, ENT_COMPAT, 'UTF-8');
}

/** Encodes using the htmlentities() function for use in a tag attribute. UTF-8 safe. */
function as_attribute($value)
{
	return htmlentities($value, ENT_COMPAT, 'UTF-8');
}

/** Encodes using the urlencoder() function for use in the href="" attribute. */
function as_url($value)
{
	return urlencode($value);
}

/** Encodes using the addslashes() function for use in a JavaScript string. */
function as_string($value)
{
	return addslashes($value);
}

