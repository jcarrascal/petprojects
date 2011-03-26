from django import forms
from django.forms.util import flatatt
from django.utils.html import escape, conditional_escape
from django.utils.safestring import mark_safe

class TagSelectMultiple(forms.SelectMultiple):
	def render(self, name, value, attrs=None, choices=()):
		if value is None: value = []
		final_attrs = self.build_attrs(attrs, name=name)
		self.name = name
		output = [u'<div class="tagselector" multiple="multiple"%s>' % flatatt(final_attrs)]
		options = self.render_options(choices, value)
		if options:
			output.append(options)
		output.append('<input type=text>')
		output.append('</div>')
		return mark_safe(u'\n'.join(output))

	def render_option(self, selected_choices, option_value, option_label):
		if option_value in selected_choices:
			return u'<span class="tag">%s <a>×</a><input name="%s" type="hidden" value="%s"/></span>' % (
				conditional_escape(force_unicode(option_label)), escape(self.name), escape(option_value))
		return ''
