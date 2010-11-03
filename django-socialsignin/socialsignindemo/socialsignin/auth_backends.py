# -*- coding: utf-8 -*-
from django.conf import settings
from django.contrib.auth.backends import ModelBackend
from django.core.exceptions import ImproperlyConfigured
from django.db.models import get_model
from openid.extensions.ax import FetchResponse as AXFetchResponse
from socialsignin.models import OPENID_AX_PARAMS_BY_PROVIDER, FacebookProfile, OpenIDProfile, \
	LiveIDProfile, TwitterProfile, LinkedInProfile
from tweepy.api import API
from urllib2 import Request, build_opener, HTTPError
from urllib import unquote
from xml.dom import Node
from xml.dom.minidom import parseString


LOCAL_USER_MODEL = get_model(*settings.LOCAL_USER_MODEL)
if not LOCAL_USER_MODEL:
	raise ImproperlyConfigured('Could not get local user model.')


class LocalUserBackend(ModelBackend):
	def authenticate(self, username=None, password=None):
		try:
			user = LOCAL_USER_MODEL.objects.get(username=username)
			if user.check_password(password):
				return user
		except LOCAL_USER_MODEL.DoesNotExist:
			return None

	def get_user(self, user_id):
		try:
			return LOCAL_USER_MODEL.objects.get(pk=user_id)
		except LOCAL_USER_MODEL.DoesNotExist:
			return None


class FacebookBackend(LocalUserBackend):
	def authenticate(self, facebook, access_token):
		me = facebook.get_object('me')
		if not me:
			return None
		facebook_uid = me['id']
		try:
			profile = FacebookProfile.objects.get(facebook_uid=facebook_uid)
			user = profile.user
		except FacebookProfile.DoesNotExist:
			username = 'fb:' + str(facebook_uid)[-27:]
			user = LOCAL_USER_MODEL(username=username, first_name=me['first_name'], last_name=me['last_name'])
			user.save()
			profile = FacebookProfile(facebook_uid=facebook_uid, user=user)
		profile.access_token = access_token
		profile.profile_url = me['link']
		profile.profile_picture = 'http://graph.facebook.com/' + facebook_uid + '/picture'
		profile.save()
		return user


class OpenIDBackend(LocalUserBackend):
	def authenticate(self, provider, openid_response):
		try:
			profile = OpenIDProfile.objects.get(identity_url=openid_response.identity_url)
			return profile.user
		except OpenIDProfile.DoesNotExist:
			username = 'oi:' + openid_response.identity_url[-27:]
			first_name, last_name, email = '', '', ''
			access_token = None
			if hasattr(openid_response, 'sreg'):
				email = openid_response.sreg.get('email')[:75]
				username = 'oi:' + openid_response.sreg.get('nickname')[-27:]
				first_name, last_name = openid_response.sreg.get('fullname', ' ').split(' ', 1)
			ax = AXFetchResponse.fromSuccessResponse(openid_response)
			if ax:
				if provider == 'Google':
					params = OPENID_AX_PARAMS_BY_PROVIDER[provider]
					username   = 'gm:' + ax.getSingle(params['email'])[-27:]
					first_name = ax.getSingle(params['first_name'])[:30]
					last_name  = ax.getSingle(params['last_name'])[:30]
					email      = ax.getSingle(params['email'])[:75]
				elif provider in OPENID_AX_PARAMS_BY_PROVIDER:
					params = OPENID_AX_PARAMS_BY_PROVIDER[provider]
					email = ax.getSingle(params['email'])[:75]
					first_name, last_name = ax.getSingle(params['fullname']).split(' ', 1)
			user = LOCAL_USER_MODEL(username=username, first_name=first_name, last_name=last_name, email=email)
			user.save()
			profile = OpenIDProfile(user=user, identity_url=openid_response.identity_url, access_token=access_token)
			profile.save()
			return user


def to_signed_64(x):
	return x < 2**63 and x or x - 2**64

def inner_text(node):
	return "".join([n.nodeValue for n in node.childNodes if n.nodeType == Node.TEXT_NODE])

class LiveIDBackend(LocalUserBackend):
	def authenticate(self, liveid, field_storage):
		consent_token = liveid.processConsent(field_storage)
		if not consent_token or not consent_token.isValid():
			return None
		lid = consent_token.getLocationID()
		liveid_uid = to_signed_64(int(lid, 16))
		access_token = unquote(consent_token.getDelegationToken())
		url = 'https://livecontacts.services.live.com/users/@C@%s/rest/livecontacts' % liveid_uid
		req = Request(url)
		req.add_header('Authorization', 'DelegatedToken dt="%s"' % access_token)
		try:
			response = build_opener().open(req)
			contacts_data = response.read()
		except HTTPError:
			return None
		doc = parseString(contacts_data)
		owner = doc.getElementsByTagName('Owner')[0]
		try:
			profile = LiveIDProfile.objects.get(liveid_uid=liveid_uid)
			user = profile.user
		except LiveIDProfile.DoesNotExist:
			first_name = inner_text(owner.getElementsByTagName('FirstName')[0])
			last_name = inner_text(owner.getElementsByTagName('LastName')[0])
			email = inner_text(owner.getElementsByTagName('WindowsLiveID')[0])
			username = 'wl:' + str(liveid_uid)[-27:]
			user = LOCAL_USER_MODEL(username=username, first_name=first_name, last_name=last_name, email=email)
			user.save()
			profile = LiveIDProfile(liveid_uid=liveid_uid, user=user)
		profile.access_token = access_token
		profile.profile_url = 'http://cid-%s.profile.live.com/' % lid
		profile.save()
		return user


class TwitterBackend(LocalUserBackend):
	def authenticate(self, twitter, access_token):
		api = API(twitter)
		me = api.me()
		if not me:
			return None
		twitter_uid = me.id
		try:
			profile = TwitterProfile.objects.get(twitter_uid=twitter_uid)
			user = profile.user
		except TwitterProfile.DoesNotExist:
			username = 'tw:' + str(twitter_uid)[-27:]
			user = LOCAL_USER_MODEL(username=username, first_name=me.name, last_name='')
			user.save()
			profile = TwitterProfile(twitter_uid=twitter_uid, user=user)
		profile.access_token = access_token
		profile.profile_url = 'http://twitter.com/' + me.screen_name
		profile.profile_picture = me.profile_image_url
		profile.save()
		return user


class LinkedInBackend(LocalUserBackend):
	def authenticate(self, linkedin):
		me = linkedin.GetProfile(None, None, 'id', 'first-name', 'last-name', 'picture-url', 'public-profile-url')
		if not me or not me.id:
			return None
		linkedin_uid = me.id
		try:
			profile = LinkedInProfile.objects.get(linkedin_uid=linkedin_uid)
			user = profile.user
		except LinkedInProfile.DoesNotExist:
			username = 'li:' + str(linkedin_uid)[-27:]
			user = LOCAL_USER_MODEL(username=username, first_name=me.first_name, last_name=me.last_name)
			user.save()
			profile = LinkedInProfile(linkedin_uid=linkedin_uid, user=user)
		profile.access_token = linkedin.access_token
		profile.profile_url = me.public_url
		profile.profile_picture = me.picture_url
		profile.save()
		return user
