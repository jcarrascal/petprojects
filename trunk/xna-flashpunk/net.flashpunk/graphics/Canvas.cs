namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.display.Graphics;
	using flash.geom.ColorTransform;
	using flash.geom.Matrix;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	
	/**
	 * A  multi-purpose drawing canvas, can be sized beyond the normal Flash Texture2D limits.
	 */
	public class Canvas : Graphic
	{
		/**
		 * Optional blend mode to use (see flash.display.BlendMode for blending modes).
		 */
		public string blend;
		
		/**
		 * Constructor.
		 * @param	width		Width of the canvas.
		 * @param	height		Height of the canvas.
		 */
		public Canvas(uint width, uint height) 
		{
			_width = width;
			_height = height;
			_refWidth = Math.ceil(width / _maxWidth);
			_refHeight = Math.ceil(height / _maxHeight);
			_ref = new Texture2D(_refWidth, _refHeight, false, 0);
			uint x, uint y, uint w, uint h, uint i,
				uint ww = _width % _maxWidth,
				uint hh = _height % _maxHeight;
			if (!ww) ww = _maxWidth;
			if (!hh) hh = _maxHeight;
			while (y < _refHeight)
			{
				h = y < _refHeight - 1 ? _maxHeight : hh;
				while (x < _refWidth)
				{
					w = x < _refWidth - 1 ? _maxWidth : ww;
					_ref.setPixel(x, y, i);
					_buffers[i] = new Texture2D(w, h, true, 0);
					i ++; x ++;
				}
				x = 0; y ++;
			}
		}
		
		/** @private Renders the canvas. */
		override public void render(Texture2D target, Vector2 point, Vector2 camera) 
		{
			// determine drawing location
			_point.x = point.x + x - camera.x * scrollX;
			_point.y = point.y + y - camera.y * scrollY;
			
			// render the buffers
			int xx, int yy, Texture2D buffer, float px = _point.x;
			while (yy < _refHeight)
			{
				while (xx < _refWidth)
				{
					buffer = _buffers[_ref.getPixel(xx, yy)];
					if (_tint || blend)
					{
						_matrix.tx = _point.x;
						_matrix.ty = _point.y;
						target.draw(buffer, _matrix, _tint, blend);
					}
					else target.copyPixels(buffer, buffer.rect, _point, null, null, true);
					_point.x += _maxWidth;
					xx ++;
				}
				_point.x = px;
				_point.y += _maxHeight;
				xx = 0;
				yy ++;
			}
		}
		
		/**
		 * Draws to the canvas.
		 * @param	x			X position to draw.
		 * @param	y			Y position to draw.
		 * @param	source		Source Texture2D.
		 * @param	rect		Optional area of the source image to draw from. If null, the entire Texture2D will be drawn.
		 */
		public void draw(int x, int y, Texture2D source, Rectangle rect = null)
		{
			int xx, int yy;
			foreach (Texture2D buffer in _buffers)
			{
				_point.x = x - xx;
				_point.y = y - yy;
				buffer.copyPixels(source, rect ? rect : source.rect, _point, null, null, true);
				xx += _maxWidth;
				if (xx >= _width)
				{
					xx = 0;
					yy += _maxHeight;
				}
			}
		}
		
		/**
		 * Fills the rectangular area of the canvas. The previous contents of that area are completely removed.
		 * @param	rect		Fill rectangle.
		 * @param	color		Fill color.
		 * @param	alpha		Fill alpha.
		 */
		public void fill(Rectangle rect, uint color = 0, float alpha = 1)
		{
			int xx, int yy, Texture2D buffer;
			_rect.width = rect.width;
			_rect.height = rect.height;
			if (alpha >= 1) color |= 0xFF000000;
			else if (alpha <= 0) color = 0;
			else color = (uint(alpha * 255) << 24) | (0xFFFFFF & color);
			foreach (buffer in _buffers)
			{
				_rect.x = rect.x - xx;
				_rect.y = rect.y - yy;
				buffer.fillRect(_rect, color);
				xx += _maxWidth;
				if (xx >= _width)
				{
					xx = 0;
					yy += _maxHeight;
				}
			}
		}
		
		/**
		 * Draws over a rectangular area of the canvas.
		 * @param	rect		Drawing rectangle.
		 * @param	color		Draw color.
		 * @param	alpha		Draw alpha. If < 1, this rectangle will blend with existing contents of the canvas.
		 */
		public void drawRect(Rectangle rect, uint color = 0, float alpha = 1)
		{
			int xx, int yy, Texture2D buffer;
			if (alpha >= 1)
			{
				_rect.width = rect.width;
				_rect.height = rect.height;
				
				foreach (buffer in _buffers)
				{
					_rect.x = rect.x - xx;
					_rect.y = rect.y - yy;
					buffer.fillRect(_rect, 0xFF000000 | color);
					xx += _maxWidth;
					if (xx >= _width)
					{
						xx = 0;
						yy += _maxHeight;
					}
				}
				return;
			}
			foreach (buffer in _buffers)
			{
				_graphics.clear();
				_graphics.beginFill(color, alpha);
				_graphics.drawRect(rect.x - xx, rect.y - yy, rect.width, rect.height);
				buffer.draw(FP.sprite);
				xx += _maxWidth;
				if (xx >= _width)
				{
					xx = 0;
					yy += _maxHeight;
				}
			}
			_graphics.endFill();
		}
		
		/**
		 * Fills the rectangle area of the canvas with the texture.
		 * @param	rect		Fill rectangle.
		 * @param	texture		Fill texture.
		 */
		public void fillTexture(Rectangle rect, Texture2D texture)
		{
			int xx, int yy;
			foreach (Texture2D buffer in _buffers)
			{
				_graphics.clear();
				_graphics.beginBitmapFill(texture);
				_graphics.drawRect(rect.x - xx, rect.y - yy, rect.width, rect.height);
				buffer.draw(FP.sprite);
				xx += _maxWidth;
				if (xx >= _width)
				{
					xx = 0;
					yy += _maxHeight;
				}
			}
			_graphics.endFill();
		}
		
		/**
		 * Draws the Graphic object to the canvas.
		 * @param	x			X position to draw.
		 * @param	y			Y position to draw.
		 * @param	source		Graphic to draw.
		 */
		public void drawGraphic(int x, int y, Graphic source)
		{
			int xx, int yy;
			foreach (Texture2D buffer in _buffers)
			{
				_point.x = x - xx;
				_point.y = y - yy;
				source.render(buffer, _point, FP.zero);
				xx += _maxWidth;
				if (xx >= _width)
				{
					xx = 0;
					yy += _maxHeight;
				}
			}
		}
		
		/**
		 * The tinted color of the Canvas. Use 0xFFFFFF to draw the it normally.
		 */
		public uint color { get { return _color; }
		public void color { set
		{
			value %= 0xFFFFFF;
			if (_color == value) return;
			_color = value;
			if (_alpha == 1 && _color == 0xFFFFFF)
			{
				_tint = null;
				return;
			}
			_tint = _colorTransform;
			_tint.redMultiplier = (_color >> 16 & 0xFF) / 255;
			_tint.greenMultiplier = (_color >> 8 & 0xFF) / 255;
			_tint.blueMultiplier = (_color & 0xFF) / 255;
			_tint.alphaMultiplier = _alpha;
		}
		
		/**
		 * Change the opacity of the Canvas, a value from 0 to 1.
		 */
		public float alpha { get { return _alpha; }
		public void alpha { set
		{
			if (value < 0) value = 0;
			if (value > 1) value = 1;
			if (_alpha == value) return;
			_alpha = value;
			if (_alpha == 1 && _color == 0xFFFFFF)
			{
				_tint = null;
				return;
			}
			_tint = _colorTransform;
			_tint.redMultiplier = (_color >> 16 & 0xFF) / 255;
			_tint.greenMultiplier = (_color >> 8 & 0xFF) / 255;
			_tint.blueMultiplier = (_color & 0xFF) / 255;
			_tint.alphaMultiplier = _alpha;
		}
		
		/**
		 * Shifts the canvas' pixels by the offset.
		 * @param	x	Horizontal shift.
		 * @param	y	Vertical shift.
		 */
		public void shift(int x = 0, int y = 0)
		{
			drawGraphic(x, y, this);
		}
		
		/**
		 * Width of the canvas.
		 */
		public uint width { get { return _width; }
		
		/**
		 * Height of the canvas.
		 */
		public uint height { get { return _height; }
		
		// Buffer information.
		/** @private */ private Vector _buffers.<Texture2D> = new Vector.<Texture2D>;
		/** @private */ protected uint _width;
		/** @private */ protected uint _height;
		/** @private */ protected uint _maxWidth = 4000;
		/** @private */ protected uint _maxHeight = 4000;
		
		// Color tinting information.
		/** @private */ private uint _color = 0xFFFFFF;
		/** @private */ private float _alpha = 1;
		/** @private */ private ColorTransform _tint;
		/** @private */ private ColorTransform _colorTransform = new ColorTransform;
		/** @private */ private Matrix _matrix = new Matrix;
		
		// Canvas reference information.
		/** @private */ private Texture2D _ref;
		/** @private */ private uint _refWidth;
		/** @private */ private uint _refHeight;
		
		// Global objects.
		/** @private */ private Rectangle _rect = new Rectangle;
		/** @private */ private Graphics _graphics = FP.sprite.graphics;
	}
}