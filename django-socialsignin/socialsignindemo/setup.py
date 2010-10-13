#!$HOME/local/bin/python2.7
# -*- coding: utf-8 -*-
__author__="Julio César Carrascal Urquijo"
__date__ ="$23/09/2010 12:39:57 PM$"

from setuptools import setup,find_packages

setup (
	name = 'socialsignin',
	version = '1.0.1',
	packages = ['socialsignin', 'socialsignin.templatetags'],

	# Declare your packages' dependencies here, for eg:
	#install_requires=[],

	# Fill in these to make your Egg ready for upload to
	# PyPI
	author = 'Julio César Carrascal Urquijo',
	author_email = 'jcarrascal@gmail.com',
	summary = 'Sign-in from social networking sites and Open ID providers.',
	url = 'http://code.google.com/p/petprojects/',
	license = 'MIT / GPLv2',
	long_description= 'Provides a Django application with easy sign-in from social networking sites and Open ID providers like Facebook, Google, Windows Live ID, Twitter, Linked In and Yahoo.',

	# could also include long_description, download_url, classifiers, etc.
)
