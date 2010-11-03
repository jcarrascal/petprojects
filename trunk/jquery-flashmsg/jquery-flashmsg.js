/*!
 * FlashMsg plugin for jQuery: Displays a non-modal message for a certain ammount of time in the center of the page.
 * You may use any FlashMsg under the terms of either the MIT License or the GNU General Public License (GPL) Version 2.
 * https://petprojects.googlecode.com/svn/trunk/MIT-LICENSE.txt
 * https://petprojects.googlecode.com/svn/trunk/GPL-LICENSE.txt
 */
(function($) {

var settings = {
	id: 'flashMsg',
	displayTime: 5000,  // Hide in 5 seconds.
	opacity: 0.8
};

var flashMsg,
	flashMsgContainer,
	timeout,
	waitTime = 0;

var methods = {
	setup: function(options) {
		settings = $.extend({}, settings, options);
		flashMsg = $('<div id="' + settings.id + '" class="flashMsg"><div class="container"></div></div>')
			.appendTo(document.body);
		flashMsgContainer = $('.container', flashMsg);
		return this;
	},

	show: function(msg) {
		var p = $('<p/>').html(msg);
		if (timeout) {
			clearTimeout(timeout);
			p.hide().appendTo(flashMsgContainer);
			waitTime += 200;
			setTimeout(function() { p.slideToggle() }, waitTime);
		} else {
			p.appendTo(flashMsgContainer);
			flashMsg.show().animate({ opacity: settings.opacity }, 200, function() {
				$(window)
					.mousemove(methods.hide)
					.click(methods.hide)
					.keypress(methods.hide);
			});
		}
		timeout = setTimeout(methods.hide, settings.displayTime);
	},
	hide: function() {
		clearTimeout(timeout);
		timeout = null;
		waitTime = 0;
		$(window)
			.unbind('mousemove', methods.hide)
			.unbind('click', methods.hide)
			.unbind('keypress', methods.hide);
		flashMsg.animate({ opacity: 0 }, 500, function() {
			flashMsg.hide();
			flashMsgContainer.html('');
		});
	}
}

$.flashMsg = function(methodName) {
	if (!flashMsg && methodName != 'setup') {
		// First invocation and setup is not been called.
		methods.setup({});
	}
	var method = methods[methodName];
	if (!method)
		return methods.show(methodName);
	return method.apply(this, Array.prototype.slice.call(arguments, 1));
}

})(jQuery);
