<?php

/*
 * Blackbird CMS - Content management system for PHP5
 * Copyright (C) 2011 Julio César Carrascal Urquijo
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
