/*!
 * Tag Selector plugin for jQuery: Facilitates selecting multiple tags by extending jQuery UI Autocomplete.
 * You may use Tag Selector under the terms of either the MIT License or the GNU General Public License (GPL) Version 2.
 * https://petprojects.googlecode.com/svn/trunk/MIT-LICENSE.txt
 * https://petprojects.googlecode.com/svn/trunk/GPL-LICENSE.txt
 */
(function($) {

	$.fn.tagSelector = function(source) {
		return this.each(function() {
			var input = $('input[type=text]', this),
				hidden = $('input[type=hidden]', this);
			$(this).click(function() { input.focus(); })
				.delegate('.tag a', 'click', function() {
					$(this).parent().remove();
				});
			input.keydown(function(e) {
					if (e.keyCode === $.ui.keyCode.TAB && $(this).data('autocomplete').menu.active)
						e.preventDefault();
				})
				.autocomplete({
					minLength: 0,
					source: source,
					select: function(event, ui) {
						//<span class=tag>@jcarrascal<a>×</a></span>
						var tag = $('<span class="tag"/>')
							.text('@' + ui.item.username)
							.append('<a>×</a>')
							.insertBefore(input);
						hidden.val(hidden.val() + ',' + ui.item.id)
						return false;
					}
				})
				.data('autocomplete')._renderItem = function(ul, item) {
					return $('<li/>')
						.data('item.autocomplete', item)
						.append($('<a/>').text(item.display_name))
						.appendTo(ul);
				};
		});
	};

})(jQuery);
