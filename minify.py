#!$HOME\local\bin\python2.7
# -*- coding: utf-8 -*-
import os
import sys

rules = {
	'jquery-flashmsg\\jquery-flashmsg.min.css': [ 'jquery-flashmsg\\jquery-flashmsg.css' ],
	'jquery-flashmsg\\jquery-flashmsg.min.js': [ 'jquery-flashmsg\\jquery-flashmsg.js' ],

	'jquery-tagselector\\jquery-tagselector.min.css': [ 'jquery-tagselector\\jquery-tagselector.css' ],
	'jquery-tagselector\\jquery-tagselector.min.js': [ 'jquery-tagselector\\jquery-tagselector.js' ],
}

compressor = "type %(sources)s | java -jar \"C:\\Program Files (x86)\\yuicompressor-2.4.2\\build\\yuicompressor-2.4.2.jar\" --charset utf-8 --type %(type)s -o %(target)s\n"

for target, sources in rules.items():
	type = target[target.rfind('.') + 1:]
	cmd = compressor % { 'sources': ' '.join(sources), 'type': type, 'target': target }
	print cmd, os.system(cmd)
