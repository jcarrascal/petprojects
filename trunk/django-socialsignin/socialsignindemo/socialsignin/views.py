# -*- coding: utf-8 -*-
from cgi import MiniFieldStorage
from datetime import datetime, timedelta
from django.conf import settings
from django.contrib.auth import authenticate, login, REDIRECT_FIELD_NAME
from django.contrib.auth.views import login as django_login_view
from django.contrib import messages
from django.core.urlresolvers import reverse
from django.db import transaction
from django.db.models import get_model
from django.http import HttpResponse, HttpResponseRedirect
from django.shortcuts import get_object_or_404, render_to_response
from django.template import RequestContext
from django.utils.translation import ugettext_lazy as _
from django.views.decorators.csrf import csrf_protect, csrf_exempt
#from facebook import GraphAPI
from linkedin.linkedin import LinkedIn
from openid.consumer.consumer import Consumer, SUCCESS, CANCEL, FAILURE
from openid.consumer.discover import DiscoveryFailure
from openid.extensions.ax import AttrInfo, FetchRequest as AXFetchRequest
from socialsignin.forms import AuthenticationForm
from socialsignin.shortcuts import send_template_mail
from socialsignin.models import OPENID_AX_PARAMS_BY_PROVIDER
from tweepy import OAuthHandler, TweepError
from urllib import urlencode, urlopen
from urlparse import parse_qs
from WindowsLiveLogin import WindowsLiveLogin


LOCAL_USER_MODEL = get_model(*settings.LOCAL_USER_MODEL)
if not LOCAL_USER_MODEL:
	raise ImproperlyConfigured('Could not get local user model.')

MAX_FAILED_ATTEMPTS = getattr(settings, 'MAX_FAILED_ATTEMPTS', 5)
LOGIN_VIEW = getattr(settings, 'LOGIN_VIEW', 'django.contrib.auth.views.login')
LOGIN_REDIRECT_VIEW = getattr(settings, 'LOGIN_REDIRECT_VIEW', '')

FACEBOOK_APP_ID = getattr(settings, 'FACEBOOK_APP_ID', '')
FACEBOOK_SECRET_KEY = getattr(settings, 'FACEBOOK_SECRET_KEY', '')

LIVEID_APP_ID = getattr(settings, 'LIVEID_APP_ID', '')
LIVEID_SECRET_KEY = getattr(settings, 'LIVEID_SECRET_KEY', '')
LIVEID_OFFERS = getattr(settings, 'LIVEID_OFFERS', 'Contacts.View')
LIVEID_POLICY_VIEW = getattr(settings, 'LIVEID_POLICY_VIEW', '')

TWITTER_CONSUMER_KEY = getattr(settings, 'TWITTER_CONSUMER_KEY', '')
TWITTER_CONSUMER_SECRET = getattr(settings, 'TWITTER_CONSUMER_SECRET', '')

LINKEDIN_CONSUMER_KEY = getattr(settings, 'LINKEDIN_CONSUMER_KEY', '')
LINKEDIN_CONSUMER_SECRET = getattr(settings, 'LINKEDIN_CONSUMER_SECRET', '')


def is_local_redirect(next):
	return next.startswith('/')

def reverse_absolute_uri(request, *args, **kwargs):
	params = '?' + urlencode({ 'next': request.REQUEST.get('next', '') }) if request.REQUEST.has_key('next') else ''
	return request.build_absolute_uri(reverse(*args, **kwargs) + params)

def redirect_if_authenticated(request, *args, **kwargs):
	user = authenticate(*args, **kwargs)
	if user is not None and user.is_active:
		login(request, user)
		if request.GET.has_key('next') and is_local_redirect(request.GET['next']):
			return HttpResponseRedirect(request.GET['next'])
		else:
			return HttpResponseRedirect(reverse(LOGIN_REDIRECT_VIEW))
	messages.error(request, 'Sorry, we don\'t support your account... yet.')
	return HttpResponseRedirect(reverse(LOGIN_VIEW))


def login_view(request, template_name='registration/login.html',
          redirect_field_name=REDIRECT_FIELD_NAME,
          authentication_form=AuthenticationForm,
          locked_template_name='registration/locked_account.html'):
	"""Extends Django's original login() view with support for a remember_me checkbox and
	locking accounts for half-hour after MAX_FAILED_ATTEMPTS."""

	intended = None
	if request.POST.has_key('username'):
		try:
			intended = LOCAL_USER_MODEL.objects.get(username=request.POST['username'])
			if intended.locked_until and intended.locked_until > datetime.now():
				return render_to_response(locked_template_name, { 'intended': intended },
				                          context_instance=RequestContext(request))
			elif intended.locked_until:
				intended.failed_attempts = None
				intended.locked_until = None
				intended.save()
		except LOCAL_USER_MODEL.DoesNotExist:
			pass
	if request.POST.has_key('remember_me'):
		if not request.POST['remember_me']:
			request.session.set_expiry(0)
	response = django_login_view(request, template_name, redirect_field_name, authentication_form)
	if intended:
		if type(response) == HttpResponseRedirect:
			intended.failed_attempts = None
			intended.locked_until = None
			intended.save()
			return response
		intended.failed_attempts = (intended.failed_attempts or 0) + 1
		if intended.failed_attempts >= MAX_FAILED_ATTEMPTS:
			intended.locked_until = datetime.now() + timedelta(minutes=30)
		intended.save()
	return response


def facebook_login(request):
	redirect_uri = reverse_absolute_uri(request, facebook_done)
	params = { 'client_id': FACEBOOK_APP_ID, 'redirect_uri': redirect_uri }
	return HttpResponseRedirect('https://graph.facebook.com/oauth/authorize?' + urlencode(params))

@transaction.commit_on_success
def facebook_done(request):
	redirect_uri = reverse_absolute_uri(request, facebook_done)
	params = {
		'client_id':     FACEBOOK_APP_ID,
		'redirect_uri':  redirect_uri,
		'client_secret': FACEBOOK_SECRET_KEY,
		'code':          request.GET.get('code', '')
	}
	url = 'https://graph.facebook.com/oauth/access_token?' + urlencode(params)
	response = urlopen(url).read()
	response_qs = parse_qs(response)
	if not response_qs.has_key('access_token'):
		if request.GET.has_key('error_reason'):
			messages.error(request, request.GET.get('error_reason', 'Unknown reason.'))
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	access_token = response_qs['access_token'][-1]
	facebook = GraphAPI(access_token)
	return redirect_if_authenticated(request, facebook=facebook, access_token=access_token)


def openid_login(request, openid_url=None, return_to=None, provider='Default'):
	openid_url = openid_url or request.REQUEST.get('openid_url', None)
	consumer = Consumer(request.session, None)
	try:
		auth_request = consumer.begin(openid_url)
	except DiscoveryFailure:
		messages.error(request, 'Invalid openid URL')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	return_to = return_to or reverse_absolute_uri(request, openid_done)
	realm = getattr(settings, 'OPENID_REALM', request.build_absolute_uri('/'))
	if provider in OPENID_AX_PARAMS_BY_PROVIDER:
		axr = AXFetchRequest()
		for alias, type_uri in OPENID_AX_PARAMS_BY_PROVIDER[provider].items():
			axr.add(AttrInfo(type_uri, alias=alias, required=True))
		auth_request.addExtension(axr)
	return HttpResponseRedirect(auth_request.redirectURL(realm, return_to))

@transaction.commit_on_success
def openid_done(request, current_url=None, on_success=None, on_failure=None, provider='Default'):
	current_url = current_url or reverse_absolute_uri(request, openid_done)
	on_success = on_success or default_on_success
	on_failure = on_failure or default_on_failure
	consumer = Consumer(request.session, None)
	openid_response = consumer.complete(request.GET, current_url)
	if openid_response.status == SUCCESS:
		return on_success(request, provider, openid_response)
	elif openid_response.status == CANCEL:
		return on_failure(request, provider, _('The authentication was cancelled'), openid_response)
	elif openid_response.status == FAILURE:
		return on_failure(request, provider, openid_response.message, openid_response)

def default_on_success(request, provider, openid_response):
	return redirect_if_authenticated(request, provider=provider, openid_response=openid_response)

def default_on_failure(request, provider, message, openid_response):
	messages.error(request, message)
	return HttpResponseRedirect(reverse(LOGIN_VIEW))


def google_login(request):
	openid_url = 'https://www.google.com/accounts/o8/id'
	return_to = reverse_absolute_uri(request, google_done)
	return openid_login(request, openid_url=openid_url, return_to=return_to, provider='Google')

@transaction.commit_on_success
def google_done(request):
	current_url = reverse_absolute_uri(request, google_done)
	return openid_done(request, current_url=current_url, provider='Google')


def liveid_login(request):
	returnurl = reverse_absolute_uri(request, liveid_done)
	policyurl = request.build_absolute_uri(reverse(LIVEID_POLICY_VIEW))
	liveid = WindowsLiveLogin(LIVEID_APP_ID, LIVEID_SECRET_KEY, policyurl=policyurl)
	liveid.setDebug(True)
	print liveid.getConsentUrl(LIVEID_OFFERS)
	return HttpResponseRedirect(liveid.getConsentUrl(LIVEID_OFFERS))

@csrf_exempt
@transaction.commit_on_success
def liveid_done(request):
	returnurl = reverse_absolute_uri(request, liveid_done)
	policyurl = request.build_absolute_uri(reverse(LIVEID_POLICY_VIEW))
	liveid = WindowsLiveLogin(LIVEID_APP_ID, LIVEID_SECRET_KEY, policyurl=policyurl, returnurl=returnurl)
	field_storage = dict([(n, MiniFieldStorage(n, v)) for n, v in request.POST.items()])
	return redirect_if_authenticated(request, liveid=liveid, field_storage=field_storage)


def twitter_login(request):
	oauth_callback = reverse_absolute_uri(request, twitter_done)
	oauth = OAuthHandler(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET, oauth_callback)
	authorization_url = oauth.get_authorization_url()
	request.session['twitter_request_token'] = oauth.request_token
	return HttpResponseRedirect(authorization_url)

@transaction.commit_on_success
def twitter_done(request):
	oauth_token = request.GET.get('oauth_token', None)
	oauth_verifier = request.GET.get('oauth_verifier', None)
	if oauth_token is None or oauth_verifier is None:
		messages.error(request, 'Mising required parameters.')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	request_token = request.session.get('twitter_request_token', None)
	if request_token is None or request_token.key != oauth_token:
		messages.error(request, 'Invalid token')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	twitter = OAuthHandler(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET)
	twitter.set_request_token(request_token.key, request_token.secret)
	try:
		access_token = twitter.get_access_token(oauth_verifier)
	except TweepError, e:
		messages.error(request, e)
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	return redirect_if_authenticated(request, twitter=twitter, access_token=access_token)


def linkedin_login(request):
	callback = reverse_absolute_uri(request, linkedin_done)
	api = LinkedIn(LINKEDIN_CONSUMER_KEY, LINKEDIN_CONSUMER_SECRET, callback)
	if not api.requestToken():
		messages.error(request, 'Invalid token.')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	request.session['linkedin_request_token'] = (api.request_token, api.request_token_secret)
	return HttpResponseRedirect(api.getAuthorizeURL())

@transaction.commit_on_success
def linkedin_done(request):
	oauth_token = request.GET.get('oauth_token', None)
	oauth_verifier = request.GET.get('oauth_verifier', None)
	if not oauth_token or not oauth_verifier:
		messages.error(request, 'Mising required parameters.')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	request_token = request.session.get('linkedin_request_token', None)
	if not request_token or request_token[0] != oauth_token:
		messages.error(request, 'Invalid token')
		return HttpResponseRedirect(reverse(LOGIN_VIEW))
	callback = reverse_absolute_uri(request, linkedin_done)
	linkedin = LinkedIn(LINKEDIN_CONSUMER_KEY, LINKEDIN_CONSUMER_SECRET, callback)
	if not linkedin.accessToken(request_token[0], request_token[1], oauth_verifier):
		messages.error(request, 'Invalid token.')
		return None
	return redirect_if_authenticated(request, linkedin=linkedin)


def yahoo_login(request):
	openid_url = 'http://yahoo.com'
	return_to = reverse_absolute_uri(request, yahoo_done)
	return openid_login(request, openid_url=openid_url, return_to=return_to, provider='Default')

@transaction.commit_on_success
def yahoo_done(request):
	current_url = reverse_absolute_uri(request, yahoo_done)
	return openid_done(request, current_url=current_url, provider='Yahoo')


@csrf_protect
@transaction.commit_on_success
def register(request, registration_form, captcha_field='captcha', template_name='socialsignin/register.html',
             email_template_name='socialsignin/register_email.txt'):
	if request.method == 'POST':
		form = registration_form(data=request.POST, files=request.FILES,
			initial={ captcha_field: request.META['REMOTE_ADDR'] })
		if form.is_valid():
			new_user = form.save()
			activate_link = reverse('socialsignin.views.activate', args=(new_user.id, new_user.activation_key))
			ctx = { 'new_user': new_user, 'activate_link': activate_link }
			send_template_mail((new_user.email,), _(u'Activate your account'),
			                   email_template_name, request, ctx)
			return HttpResponseRedirect(reverse('socialsignin.views.register_done'))
	else:
		form = registration_form()
	return render_to_response(template_name, { 'reg_form': form },
		context_instance=RequestContext(request))

def register_done(request, template_name='socialsignin/register_done.html'):
	return render_to_response(template_name, {}, context_instance=RequestContext(request))


def username_available(request):
	try:
		LOCAL_USER_MODEL.objects.get(username=request.GET['new_username'])
		return HttpResponse('"The Username is already in use."')
	except:
		return HttpResponse('true')


def email_available(request):
	try:
		LOCAL_USER_MODEL.objects.get(email=request.GET['email'])
		return HttpResponse('"The E-mail address is already in use."')
	except:
		return HttpResponse('true')


@csrf_protect
@transaction.commit_on_success
def activate(request, user_id, activation_key, template_name='socialsignin/activate_invalid.html'):
	try:
		user = get_object_or_404(LocalUser, id=user_id, activation_key=activation_key)
		user.activation_key = None
		user.is_active = True
		user.save()
		return HttpResponseRedirect(reverse(activate_done))
	except:
		return render_to_response(template_name, {},
			context_instance=RequestContext(request))
