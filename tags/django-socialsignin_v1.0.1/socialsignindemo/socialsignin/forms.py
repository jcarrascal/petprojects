# -*- coding: utf-8 -*-
from django import forms
from django.conf import settings
from django.db.models import get_model
from django.utils.translation import ugettext_lazy as _


LOCAL_USER_MODEL = get_model(*settings.LOCAL_USER_MODEL)
if not LOCAL_USER_MODEL:
	raise ImproperlyConfigured('Could not get local user model.')

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
		if len(LocalUser.objects.filter(username=self.cleaned_data['new_username'])) > 0:
			raise forms.ValidationError(_(u'This Username is already in use.'))
		return self.cleaned_data['new_username']

	def clean_email(self):
		if len(LocalUser.objects.filter(email=self.cleaned_data['email'])) > 0:
			raise forms.ValidationError(_(u'This E-mail address is already in use.'))
		return self.cleaned_data['email']

	def save(self):
		user = LOCAL_USER_MODEL.objects.create(self.cleaned_data['new_username'],
		                                       self.cleaned_data['password1'],
		                                       self.cleaned_data['email'])
		user.first_name = self.cleaned_data['first_name']
		user.last_name = self.cleaned_data['last_name']
		user.save()
		return user
