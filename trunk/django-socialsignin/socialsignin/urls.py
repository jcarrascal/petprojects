from django.conf.urls.defaults import *


urlpatterns = patterns('socialsignin.views',
	(r'^facebook/$', 'facebook_login'),
	(r'^facebook/done/$', 'facebook_done'),

	(r'^openid/$', 'openid_login'),
	(r'^openid/done/$', 'openid_done'),

	(r'^google/$', 'google_login'),
	(r'^google/done/$', 'google_done'),

	(r'^liveid/$', 'liveid_login'),
	(r'^liveid/done/$', 'liveid_done'),

	(r'^twitter/$', 'twitter_login'),
	(r'^twitter/done/$', 'twitter_done'),

	(r'^linkedin/$', 'linkedin_login'),
	(r'^linkedin/done/$', 'linkedin_done'),

	(r'^yahoo/$', 'yahoo_login'),
	(r'^yahoo/done/$', 'yahoo_done'),

	(r'^username_available/$', 'username_available'),
	(r'^email_available/', 'email_available'),
)
