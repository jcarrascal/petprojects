#!$HOME/local/bin/python2.7
# -*- coding: utf-8 -*-
import sys
import os

INTERP = "/home/artelogico/local/bin/python2.7"
if sys.executable != INTERP: os.execl(INTERP, INTERP, *sys.argv)

sys.path.append(os.getcwd())
os.environ['DJANGO_SETTINGS_MODULE'] = "socialsignindemo.settings"
import django.core.handlers.wsgi
application = django.core.handlers.wsgi.WSGIHandler()
