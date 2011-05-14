<?php

/*
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as
 * published by the Free Software Foundation, either version 3 of the 
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>.
*/


// no direct access
defined('_JEXEC') or die('Restricted access');

$model = $this->model;

?>
<form action="<?php echo JRoute::_('index.php?option=com_contactform&task=send') ?>" id="ContactForm" method="post">
	<?php if ($model->showArticle) {
		echo '<h2>', $this->article->title, "</h2>\n";
	} else {
		echo '<h2>', JText::_('CF_FORM_TITLE'), "</h2>\n";
	} ?>

	<?php if (isset($model->message)) { ?>
		<div class="message">
			<?php echo $model->message ?>
		</div>
	<?php } ?>

	<?php if ($model->showArticle) {
		echo '<div class="article-text">', $article->introtext, "\n", $article->fulltext, "</div>\n";
	} ?>

	<fieldset>
		<legend><?php echo JText::_('CF_SECTION_TITLE_CONTACT') ?></legend>
		<table cellspacing="0" cellpadding="0" border="0" width="100%">
			<tbody>
				<tr>
					<th width="30%"><label for="cfFullName"><?php echo JText::_('CF_FIELD_FULL_NAME') ?>:</label></th>
					<td><input class="inputbox" id="cfFullName" name="fullname" type="text" maxlength="100"
					value="<?php echo $this->escape($model->fullname) ?>"/></td>
				</tr>
				<tr>
					<th><label for="cfEmail"><?php echo JText::_('CF_FIELD_EMAIL') ?>:</label></th>
					<td><input class="inputbox" id="cfEmail" name="email" type="text" 
					maxlength="200" value="<?php echo $this->escape($model->email) ?>"/></td>
				</tr>
<?php if ($model->showHomePhone) { ?>
				<tr>
					<th width="30%"><label for="cfHomePhone"><?php echo JText::_('CF_FIELD_HOME_PHONE') ?>:</label></th>
					<td><input class="inputbox" id="cfHomePhone" name="homePhone" type="text" maxlength="100"
					value="<?php echo $this->escape($model->homePhone) ?>"/></td>
				</tr>
<?php
}
if ($model->showCellPhone) {
?>
				<tr>
					<th width="30%"><label for="cfCellPhone"><?php echo JText::_('CF_FIELD_CELL_PHONE') ?>:</label></th>
					<td><input class="inputbox" id="cfCellPhone" name="cellPhone" type="text" maxlength="100"
					value="<?php echo $this->escape($model->cellPhone) ?>"/></td>
				</tr>
<?php
}
if ($model->showCompany) {
?>
				<tr>
					<th width="30%"><label for="cfCompany"><?php echo JText::_('CF_FIELD_COMPANY') ?>:</label></th>
					<td><input class="inputbox" id="cfCompany" name="company" type="text" maxlength="100"
					value="<?php echo $this->escape($model->company) ?>"/></td>
				</tr>
<?php
}
if ($model->showAddress) {
?>
				<tr>
					<th width="30%"><label for="cfAddress"><?php echo JText::_('CF_FIELD_ADDRESS') ?>:</label></th>
					<td><input class="inputbox" id="cfAddress" name="address" type="text" maxlength="100"
					value="<?php echo $this->escape($model->address) ?>"/></td>
				</tr>
<?php
}
if ($model->showCity) {
?>
				<tr>
					<th width="30%"><label for="cfCity"><?php echo JText::_('CF_FIELD_CITY') ?>:</label></th>
					<td><input class="inputbox" id="cfCity" name="city" type="text" maxlength="100"
					value="<?php echo $this->escape($model->city) ?>"/></td>
				</tr>
<?php } ?>
			</tbody>
		</table>
	</fieldset>

	<fieldset title="<?php echo JText::_('CF_SECTION_TITLE_FEEDBACK') ?>">
		<legend><?php echo JText::_('CF_SECTION_TITLE_FEEDBACK') ?></legend>
		<table cellspacing="0" cellpadding="0" border="0" width="100%">
			<tbody>
				<tr>
					<th width="30%"><label for="cfSubject"><?php echo JText::_('CF_FIELD_SUBJECT') ?>:</label></th>
					<td><input class="inputbox" id="cfSubject" name="subject" type="text" maxlength="100"
					value="<?php echo $this->escape($model->subject) ?>"/></td>
				</tr>
				<tr>
					<th colspan="2"><label for="cfMessage"><?php echo JText::_('CF_FIELD_MESSAGE') ?>:</label></th>
				</tr>
				<tr>
					<td colspan="2"><textarea id="cfMessage" class="inputbox" name="message" 
					style="width:90%;height:120px"><?php echo $this->escape($model->message) ?></textarea></td>
				</tr>
			</tbody>
		</table>
	</fieldset>

	<fieldset>
		<table cellspacing="0" cellpadding="0" border="0" width="100%">
			<tfoot>
				<tr>
					<th><button class="button validate" type="submit"><?php echo JText::_('CF_BUTTON_SEND') ?></button>
					<button onclick="history.go(-1)"><?php echo JText::_('CF_BUTTON_CANCEL') ?></button></th>
				</tr>
			</tfoot>
		</table>
	</fieldset>
</form>
