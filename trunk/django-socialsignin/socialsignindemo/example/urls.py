from django.conf.urls.defaults import *


urlpatterns = patterns('example.views',
	(r'^$', 'index'),
	(r'^privacy_policy/$', 'privacy_policy'),
	(r'^profile/$', 'profile'),
)
