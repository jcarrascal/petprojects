# -*- coding: utf-8 -*-
from django.contrib.auth.decorators import login_required
from django.core.urlresolvers import reverse
from django.http import HttpResponseRedirect
from django.shortcuts import render_to_response
from django.template import RequestContext


def index(request):
	if not request.user.is_anonymous():
		return HttpResponseRedirect(reverse(profile))
	return render_to_response('example/index.html', {}, context_instance=RequestContext(request))

def privacy_policy(request):
	return render_to_response('example/privacy_policy.html', {}, context_instance=RequestContext(request))

@login_required
def profile(request):
	return render_to_response('example/profile.html', {}, context_instance=RequestContext(request))
