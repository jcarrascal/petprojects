# -*- coding: utf-8 -*-
from django import forms
from django.conf import settings
from django.contrib.auth.forms import AuthenticationForm as DjangoAuthenticationForm
from django.db.models import get_model
from django.utils.hashcompat import md5_constructor
from django.utils.translation import ugettext_lazy as _
from random import random


LOCAL_USER_MODEL = get_model(*settings.LOCAL_USER_MODEL)
if not LOCAL_USER_MODEL:
	from django.core.exceptions import ImproperlyConfigured
	raise ImproperlyConfigured('Could not get local user model.')


class AuthenticationForm(DjangoAuthenticationForm):
	persistent = forms.BooleanField(label=_('Keep me logged in'), initial=False, required=False)


class RegistrationForm(forms.Form):
	new_username = forms.RegexField(label=_(u'Username'), max_length=30, regex=r'^[A-Za-z][0-9A-Za-z]*$')
	new_username.widget.attrs['maxlength'] = 30
	password1 = forms.CharField(label=_(u'Password'), widget=forms.PasswordInput())
	password1.widget.attrs['maxlength'] = 100
	password2 = forms.CharField(label=_(u'Again'), widget=forms.PasswordInput())
	password2.widget.attrs['maxlength'] = 100
	first_name = forms.CharField(label=_(u'First name'), max_length=30)
	first_name.widget.attrs['maxlength'] = 30
	last_name = forms.CharField(label=_(u'Last name'), max_length=30)
	last_name.widget.attrs['maxlength'] = 30
	email = forms.EmailField(label=_(u'E-mail'), max_length=200)
	email.widget.attrs['maxlength'] = 200

	def clean_new_username(self):
		if len(LOCAL_USER_MODEL.objects.filter(username=self.cleaned_data['new_username'])) > 0:
			raise forms.ValidationError(_(u'This Username is already in use.'))
		return self.cleaned_data['new_username']

	def clean_email(self):
		if len(LOCAL_USER_MODEL.objects.filter(email=self.cleaned_data['email'])) > 0:
			raise forms.ValidationError(_(u'This E-mail address is already in use.'))
		return self.cleaned_data['email']

	def save(self):
		print LOCAL_USER_MODEL, LOCAL_USER_MODEL.objects
		user = LOCAL_USER_MODEL.objects.create_user(username=self.cleaned_data['new_username'],
		                                            email=self.cleaned_data['email'],
											        password=self.cleaned_data['password1'])
		user.first_name = self.cleaned_data['first_name']
		user.last_name = self.cleaned_data['last_name']
		user.is_active = False
		user.activation_key = md5_constructor(str(random()) + user.username).hexdigest()
		user.save()
		return user
