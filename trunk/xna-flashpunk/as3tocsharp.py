#!/usr/bin/python

import os, os.path, re

def visit_file(fullpath):
	try:
		infile = open(fullpath, 'rb')
		text = "".join(infile.readlines())
		infile.close()
	except IOError as ex:
		print fullpath, "not processed:", ex
	text = re.sub(r'\bString\b', 'string', text)
	text = re.sub(r'\bNumber\b', 'float', text)
	text = re.sub(r'\bBoolean\b', 'bool', text)
	text = re.sub(r'\bBitmapData\b', 'Texture2D', text)
	text = re.sub(r'\bPoint\b', 'Vector2', text)
	text = re.sub(r'\bObject\b', 'object', text)
	text = re.sub(r'\bextends\b', ':', text)
	text = re.sub(r'\bsuper\b', 'base', text)
	text = re.sub(r'!==', '!=', text)
	text = re.sub(r'([A-Za-z_][A-Za-z0-9_]*)\:([A-Za-z_][A-Za-z0-9_]*)', r'\g<2> \g<1>', text)
	text = re.sub(r'\b(get|set)\b ([A-Za-z_][A-Za-z0-9_]*)\([^(]*\):([A-Za-z_][A-Za-z0-9_]*)', r'\g<3> \g<2> { \g<1>', text)
	text = re.sub(r'function ([A-Za-z_][A-Za-z0-9_]*\([^)]*\))\:([A-Za-z_][A-Za-z0-9_]*)', r'\g<2> \g<1>', text)
	text = re.sub(r'function ([A-Za-z_][A-Za-z0-9_]*\([^)]*\))', r'\g<1>', text)
	text = re.sub(r'\bimport\b', 'using', text)
	text = re.sub(r'\bfor each\b', 'foreach', text)
	text = re.sub(r'\bstatic const\b', 'const', text)
	text = re.sub(r'\bpackage\b', 'namespace', text)
	text = re.sub(r'\bfunction ', '', text)
	text = re.sub(r'\bvar ', '', text)
	try:
		outfile = open(fullpath[:-3] + ".cs", 'wb')
		outfile.write(text)
		outfile.close()
	except IOError as ex:
		print fullpath, "couldn't be saved:", ex

def visit_directory(arg, dirname, names):
	for name in names:
		fullpath = os.path.join(dirname, name)
		if os.path.isfile(fullpath) and name.endswith('.as'):
			visit_file(fullpath)
os.path.walk('.', visit_directory, None)
