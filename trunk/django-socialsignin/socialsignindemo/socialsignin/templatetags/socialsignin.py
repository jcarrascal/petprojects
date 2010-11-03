from django.conf import settings
from django.core.urlresolvers import reverse
from django.template import Library


register = Library()
AUTHENTICATION_FORM = getattr(settings, 'AUTHENTICATION_FORM', ('django.contrib.auth.forms', 'AuthenticationForm'))
LOGIN_VIEW = getattr(settings, 'LOGIN_VIEW', 'django.contrib.auth.views.login')


@register.inclusion_tag('socialsignin/login_widget.html', takes_context=True)
def login_widget(context, auth_form=None):
	request = context.get('request', None)
	next = request.REQUEST.get('next', '') if request else ''
	return {
		'auth_form': auth_form or getattr(__import__(AUTHENTICATION_FORM[0], fromlist=(AUTHENTICATION_FORM[1],)),
		                                  AUTHENTICATION_FORM[1])(),
		'login_url': reverse(LOGIN_VIEW),
		'MEDIA_URL': context.get('MEDIA_URL', ''),
		'request':   request,
		'next':      next,
	}
