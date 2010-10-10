from django.conf.urls.defaults import *
from django.contrib.auth.forms import AuthenticationForm
from django.views.generic.simple import direct_to_template
from socialsignin.forms import RegistrationForm

# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    (r'^', include('example.urls')),
    (r'^socialsignin/', include('socialsignin.urls')),
	(r'^register/$', 'socialsignin.views.register', { 'registration_form': RegistrationForm }),
	(r'^activate/$', 'socialsignin.views.activate'),
	(r'^activate/done/$', direct_to_template, { 'template': 'socialsignin/activate_done.html' }, 'socialsignin.views.activate_done'),
	(r'^username_available/$', 'socialsignin.views.username_available'),
	(r'^email_available/$', 'socialsignin.views.email_available'),
	(r'^login/$', 'django.contrib.auth.views.login', { 'authentication_form': AuthenticationForm }),
	(r'^logout/', 'django.contrib.auth.views.logout'),
	(r'^password_reset/$', 'django.contrib.auth.views.password_reset', { 'email_template_name': 'socialsignin/password_reset_email.txt' }),

    # Uncomment the admin/doc line below to enable admin documentation:
    # (r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    # (r'^admin/', include(admin.site.urls)),
)

from socialsignindemo import settings
if settings.DEBUG:
	urlpatterns += patterns('',
		(r'^media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.MEDIA_ROOT, 'show_indexes': True}),
	)
