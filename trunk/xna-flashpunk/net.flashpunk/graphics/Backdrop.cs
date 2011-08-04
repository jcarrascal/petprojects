namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	
	/**
	 * A background texture that can be repeated horizontally and vertically
	 * when drawn. Really useful for parallax backgrounds, textures, etc.
	 */
	public class Backdrop : Canvas
	{
		/**
		 * Constructor.
		 * @param	texture		Source texture.
		 * @param	repeatX		Repeat horizontally.
		 * @param	repeatY		Repeat vertically.
		 */
		public Backdrop(texture:*, bool repeatX = true, bool repeatY = true) 
		{
			if (texture is Class) _texture = FP.getBitmap(texture);
			else if (texture is Texture2D) _texture = texture;
			if (!_texture) _texture = new Texture2D(FP.width, FP.height, true, 0);
			_repeatX = repeatX;
			_repeatY = repeatY;
			_textWidth = _texture.width;
			_textHeight = _texture.height;
			base(FP.width * uint(repeatX) + _textWidth, FP.height * uint(repeatY) + _textHeight);
			FP.rect.x = FP.rect.y = 0;
			FP.rect.width = _width;
			FP.rect.height = _height;
			fillTexture(FP.rect, _texture);
		}
		
		/** @private Renders the Backdrop. */
		override public void render(Texture2D target, Vector2 point, Vector2 camera) 
		{
			_point.x = point.x + x - camera.x * scrollX;
			_point.y = point.y + y - camera.y * scrollY;
			
			if (_repeatX)
			{
				_point.x %= _textWidth;
				if (_point.x > 0) _point.x -= _textWidth;
			}
			
			if (_repeatY)
			{
				_point.y %= _textHeight;
				if (_point.y > 0) _point.y -= _textHeight;
			}
			
			_x = x; _y = y;
			x = y = 0;
			base.render(target, _point, FP.zero);
			x = _x; y = _y;
		}
		
		// Backdrop information.
		/** @private */ private Texture2D _texture;
		/** @private */ private uint _textWidth;
		/** @private */ private uint _textHeight;
		/** @private */ private bool _repeatX;
		/** @private */ private bool _repeatY;
		/** @private */ private float _x;
		/** @private */ private float _y;
	}
}