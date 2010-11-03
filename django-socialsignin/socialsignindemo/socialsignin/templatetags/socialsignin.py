from django.template import Library
from django.conf import settings

register = Library()
AUTHENTICATION_FORM = getattr(settings, 'AUTHENTICATION_FORM', ('django.contrib.auth.forms', 'AuthenticationForm'))


@register.inclusion_tag('socialsignin/login_widget.html', takes_context=True)
def login_widget(context, auth_form=None):
	request = context.get('request', None)
	next = request.REQUEST.get('next', '') if request else ''
	return {
		'auth_form': auth_form or getattr(__import__(AUTHENTICATION_FORM[0], fromlist=(AUTHENTICATION_FORM[1],)),
		                                  AUTHENTICATION_FORM[1])(),
		'MEDIA_URL': context.get('MEDIA_URL', ''),
		'request':   request,
		'next':      next,
	}
