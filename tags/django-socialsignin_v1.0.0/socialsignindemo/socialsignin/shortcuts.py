from django.conf import settings
from django.core.mail import send_mail
from django.template import RequestContext
from django.template.loader import render_to_string
from django.utils.hashcompat import md5_constructor
from urllib import urlencode


def send_template_mail(recipient_list, subject, template_file, request, ctx={}, from_email=None):
	ctx['root_url'] = request.build_absolute_uri('/')
	message = render_to_string(template_file, RequestContext(request, ctx))
	return send_mail(subject, message, from_email or settings.DEFAULT_FROM_EMAIL, recipient_list, settings.DEBUG)


def gravatar_url(email, size=128, default='identicon', rated='PG'):
	# Example: http://www.gravatar.com/avatar/29d4c68042cee463e13e7619beca4c5c?s=128&d=identicon&r=PG
	params = urlencode({ 's': str(size), 'd': default, 'r': rated, })
	return "http://www.gravatar.com/avatar/%s?%s" % (md5_constructor(email).hexdigest(), params)
