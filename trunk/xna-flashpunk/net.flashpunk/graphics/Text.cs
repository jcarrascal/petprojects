namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using flash.text.TextField;
	using flash.text.TextFormat;
	using flash.text.TextLineMetrics;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	
	/**
	 * Used for drawing text using embedded fonts.
	 */
	public class Text : Image
	{
		/**
		 * The font to assign to new Text objects.
		 */
		public static string font = "default";
		
		/**
		 * The font size to assign to new Text objects.
		 */
		public static uint size = 16;
		
		/**
		 * Constructor.
		 * @param	text		Text to display.
		 * @param	x			X offset.
		 * @param	y			Y offset.
		 * @param	width		Image width (leave as 0 to size to the starting text string).
		 * @param	height		Image height (leave as 0 to size to the starting text string).
		 */
		public Text(string text, float x = 0, float y = 0, uint width = 0, uint height = 0)
		{
			_field.embedFonts = true;
			_field.defaultTextFormat = _form = new TextFormat(Text.font, Text.size, 0xFFFFFF);
			_field.text = _text = text;
			if (!width) width = _field.textWidth + 4;
			if (!height) height = _field.textHeight + 4;
			_source = new Texture2D(width, height, true, 0);
			base(_source);
			updateBuffer();
			this.x = x;
			this.y = y;
		}
		
		/** @private Updates the drawing buffer. */
		override public void updateBuffer(bool clearBefore = false) 
		{
			_field.setTextFormat(_form);
			_field.width = _width = _field.textWidth + 4;
			_field.height = _height = _field.textHeight + 4;
			_source.fillRect(_sourceRect, 0);
			_source.draw(_field);
			base.updateBuffer(clearBefore);
		}
		
		/** @private Centers the Text's originX/Y to its center. */
		override public void centerOrigin() 
		{
			originX = _width / 2;
			originY = _height / 2;
		}
		
		/**
		 * Text string.
		 */
		public string text { get { return _text; }
		public void text { set
		{
			if (_text == value) return;
			_field.text = _text = value;
			updateBuffer();
		}
		
		/**
		 * Font family.
		 */
		public string font { get { return _font; }
		public void font { set
		{
			if (_font == value) return;
			_form.font = _font = value;
			updateBuffer();
		}
		
		/**
		 * Font size.
		 */
		public uint size { get { return _size; }
		public void size { set
		{
			if (_size == value) return;
			_form.size = _size = value;
			updateBuffer();
		}
		
		/**
		 * Width of the text image.
		 */
		override public uint width { get { return _width; }
		
		/**
		 * Height of the text image.
		 */
		override public uint height { get { return _height; }
		
		// Text information.
		/** @private */ private TextField _field = new TextField;
		/** @private */ private uint _width;
		/** @private */ private uint _height;
		/** @private */ private TextFormat _form;
		/** @private */ private string _text;
		/** @private */ private string _font;
		/** @private */ private uint _size;
		
		// Default font family.
		// Use this option when compiling with Flex SDK 3 or lower
		// [Embed(source = '04B_03__.TTF', fontFamily = 'default')]
		// Use this option when compiling with Flex SDK 4
		[Embed(source = '04B_03__.TTF', embedAsCFF="false", fontFamily = 'default')]
		/** @private */ private static Class _FONT_DEFAULT;
	}
}