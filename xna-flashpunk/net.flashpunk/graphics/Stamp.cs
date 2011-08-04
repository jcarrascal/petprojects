namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.display.DisplayObject;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.*;
	
	/**
	 * A simple non-transformed, non-animated graphic.
	 */
	public class Stamp : Graphic
	{
		/**
		 * Constructor.
		 * @param	source		Source image.
		 * @param	x			X offset.
		 * @param	y			Y offset.
		 */
		public Stamp(source:*, int x = 0, int y = 0) 
		{
			// set the origin
			this.x = x;
			this.y = y;
			
			// set the graphic
			if (!source) return;
			if (source is Class) _source = FP.getBitmap(source);
			else if (source is Texture2D) _source = source;
			if (_source) _sourceRect = _source.rect;
		}
		
		/** @private Renders the Graphic. */
		override public void render(Texture2D target, Vector2 point, Vector2 camera) 
		{
			if (!_source) return;
			_point.x = point.x + x - camera.x * scrollX;
			_point.y = point.y + y - camera.y * scrollY;
			target.copyPixels(_source, _sourceRect, _point, null, null, true);
		}
		
		/**
		 * Source Texture2D image.
		 */
		public Texture2D source { get { return _source; }
		public void source { set
		{
			_source = value;
			if (_source) _sourceRect = _source.rect;
		}
		
		// Stamp information.
		/** @private */ private Texture2D _source;
		/** @private */ private Rectangle _sourceRect;
	}
}