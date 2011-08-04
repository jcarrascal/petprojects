namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.display.Graphics;
	using flash.geom.Rectangle;
	using net.flashpunk.FP;
	
	/**
	 * Special Image object that can display blocks of tiles.
	 */
	public class TiledImage : Image
	{
		/**
		 * Constructs the TiledImage.
		 * @param	texture		Source texture.
		 * @param	width		The width of the image (the texture will be drawn to fill this area).
		 * @param	height		The height of the image (the texture will be drawn to fill this area).
		 * @param	clipRect	An optional area of the source texture to use (eg. a tile from a tileset).
		 */
		public TiledImage(texture:*, uint width = 0, uint height = 0, Rectangle clipRect = null)
		{
			_width = width;
			_height = height;
			base(texture, clipRect);
		}
		
		/** @private Creates the buffer. */
		override protected void createBuffer() 
		{
			if (!_width) _width = _sourceRect.width;
			if (!_height) _height = _sourceRect.height;
			_buffer = new Texture2D(_width, _height, true, 0);
			_bufferRect = _buffer.rect;
		}
		
		/** @private Updates the buffer. */
		override public void updateBuffer(bool clearBefore = false)
		{
			if (!_source) return;
			if (!_texture)
			{
				_texture = new Texture2D(_sourceRect.width, _sourceRect.height, true, 0);
				_texture.copyPixels(_source, _sourceRect, FP.zero);
			}
			_buffer.fillRect(_bufferRect, 0);
			_graphics.clear();
			if (_offsetX != 0 || _offsetY != 0)
			{
				FP.matrix.identity();
				FP.matrix.tx = Math.round(_offsetX);
				FP.matrix.ty = Math.round(_offsetY);
				_graphics.beginBitmapFill(_texture, FP.matrix);
			}
			else _graphics.beginBitmapFill(_texture);
			_graphics.drawRect(0, 0, _width, _height);
			_buffer.draw(FP.sprite, null, _tint);
		}
		
		/**
		 * The x-offset of the texture.
		 */
		public float offsetX { get { return _offsetX; }
		public void offsetX { set
		{
			if (_offsetX == value) return;
			_offsetX = value;
			updateBuffer();
		}
		
		/**
		 * The y-offset of the texture.
		 */
		public float offsetY { get { return _offsetY; }
		public void offsetY { set
		{
			if (_offsetY == value) return;
			_offsetY = value;
			updateBuffer();
		}
		
		/**
		 * Sets the texture offset.
		 * @param	x		The x-offset.
		 * @param	y		The y-offset.
		 */
		public void setOffset(float x, float y)
		{
			if (_offsetX == x && _offsetY == y) return;
			_offsetX = x;
			_offsetY = y;
			updateBuffer();
		}
		
		// Drawing information.
		/** @private */ private Graphics _graphics = FP.sprite.graphics;
		/** @private */ private Texture2D _texture;
		/** @private */ private uint _width;
		/** @private */ private uint _height;
		/** @private */ private float _offsetX = 0;
		/** @private */ private float _offsetY = 0;
	}
}