from django.template import Library
from django.conf import settings
from django.contrib.auth.forms import AuthenticationForm as DjangoAuthenticationForm

register = Library()
AUTHENTICATION_FORM = getattr(settings, 'AUTHENTICATION_FORM', DjangoAuthenticationForm)


@register.inclusion_tag('socialsignin/login_widget.html', takes_context=True)
def login_widget(context, auth_form=None):
	return {
		'MEDIA_URL': context.get('MEDIA_URL', ''),
		'request':   context.get('request', None),
		'auth_form': auth_form or DjangoAuthenticationForm(),
	}
