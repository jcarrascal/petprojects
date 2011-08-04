namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.display.Graphics;
	using net.flashpunk.FP;
	
	/**
	 * Special Spritemap object that can display blocks of animated sprites.
	 */
	public class TiledSpritemap : Spritemap
	{
		/**
		 * Constructs the tiled spritemap.
		 * @param	source			Source image.
		 * @param	frameWidth		Frame width.
		 * @param	frameHeight		Frame height.	
		 * @param	width			Width of the block to render.
		 * @param	height			Height of the block to render.
		 * @param	callback		Optional callback for animation end.
		 */
		public TiledSpritemap(source:*, uint frameWidth = 0, uint frameHeight = 0, uint width = 0, uint height = 0, Function callback = null) 
		{
			_imageWidth = width;
			_imageHeight = height;
			base(source, frameWidth, frameHeight, callback);
		}
		
		/** @private Creates the buffer. */
		override protected void createBuffer() 
		{
			if (!_imageWidth) _imageWidth = _sourceRect.width;
			if (!_imageHeight) _imageHeight = _sourceRect.height;
			_buffer = new Texture2D(_imageWidth, _imageHeight, true, 0);
			_bufferRect = _buffer.rect;
		}
		
		/** @private Updates the buffer. */
		override public void updateBuffer(bool clearBefore = false) 
		{
			// get position of the current frame
			_rect.x = _rect.width * _frame;
			_rect.y = uint(_rect.x / _width) * _rect.height;
			_rect.x %= _width;
			if (_flipped) _rect.x = (_width - _rect.width) - _rect.x;
			
			// render it repeated to the buffer
			int xx = int(_offsetX) % _imageWidth,
				int yy = int(_offsetY) % _imageHeight;
			if (xx >= 0) xx -= _imageWidth;
			if (yy >= 0) yy -= _imageHeight;
			FP.point.x = xx;
			FP.point.y = yy;
			while (FP.point.y < _imageHeight)
			{
				while (FP.point.x < _imageWidth)
				{
					_buffer.copyPixels(_source, _sourceRect, FP.point);
					FP.point.x += _sourceRect.width;
				}
				FP.point.x = xx;
				FP.point.y += _sourceRect.height;
			}
			
			// tint the buffer
			if (_tint) _buffer.colorTransform(_bufferRect, _tint);
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
		
		/** @private */ private Graphics _graphics = FP.sprite.graphics;
		/** @private */ private uint _imageWidth;
		/** @private */ private uint _imageHeight;
		/** @private */ private float _offsetX = 0;
		/** @private */ private float _offsetY = 0;
	}
}