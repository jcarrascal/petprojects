# -*- coding: utf-8 -*-
from django.db import models
from django.conf import settings
from django.contrib.auth.models import User


class LocalUser(User):
	user = models.OneToOneField(User, unique=True)
	activation_key = models.CharField(blank=True, max_length=40, null=True)
	modified_email_key = models.CharField(blank=True, max_length=40, null=True)
	modified_email = models.EmailField(blank=True, null=True)
	avatar_url = models.URLField(blank=True, null=True, verify_exists=False)
	local_avatar = models.ImageField(blank=True, max_length=200, null=True, upload_to='avatars/%Y/%m/')
	failed_attempts = models.IntegerField(blank=True, null=True)
	locked_until = models.DateField(blank=True, null=True)


OPENID_AX_PARAMS_BY_PROVIDER = getattr(settings, 'OPENID_AX_PARAMS_BY_PROVIDER', {
	'Default': {
		'fullname': 'http://axschema.org/namePerson',
		'email':    'http://axschema.org/contact/email',
	},
	'Google': {
		'first_name': 'http://axschema.org/namePerson/first',
		'last_name':  'http://axschema.org/namePerson/last',
		'email':      'http://axschema.org/contact/email',
	},
})


class FacebookProfile(models.Model):
	user = models.ForeignKey(User, related_name='facebook_profiles')
	facebook_uid = models.CharField(db_index=True, max_length=50, unique=True)
	access_token = models.CharField(max_length=200)
	profile_picture = models.URLField()
	profile_url = models.URLField()


class OpenIDProfile(models.Model):
	user = models.ForeignKey(User, related_name='openid_profiles')
	identity_url = models.URLField(db_index=True, unique=True)
	access_token = models.CharField(blank=True, max_length=100, null=True)


class LiveIDProfile(models.Model):
	user = models.ForeignKey(User, related_name='liveid_profiles')
	liveid_uid = models.CharField(db_index=True, max_length=50, unique=True)
	access_token = models.CharField(max_length=2000)
	profile_url = models.URLField()


class TwitterProfile(models.Model):
	user = models.ForeignKey(User, related_name='twitter_profiles')
	twitter_uid = models.IntegerField(db_index=True, unique=True)
	access_token = models.CharField(max_length=200)
	profile_picture = models.URLField()
	profile_url = models.URLField()


class LinkedInProfile(models.Model):
	user = models.ForeignKey(User, related_name='linkedin_profiles')
	linkedin_uid = models.CharField(db_index=True, max_length=50, unique=True)
	access_token = models.CharField(max_length=200)
	profile_picture = models.URLField()
	profile_url = models.URLField()
