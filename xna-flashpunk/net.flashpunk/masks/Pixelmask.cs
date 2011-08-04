namespace net.flashpunk.masks
{
	using flash.display.Texture2D;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using net.flashpunk.*;
	
	/**
	 * A bitmap mask used for pixel-perfect collision. 
	 */
	public class Pixelmask : Hitbox
	{
		/**
		 * Alpha threshold of the bitmap used for collision.
		 */
		public uint threshold = 1;
		
		/**
		 * Constructor.
		 * @param	source		The image to use as a mask.
		 * @param	x			X offset of the mask.
		 * @param	y			Y offset of the mask.
		 */
		public Pixelmask(source:*, int x = 0, int y = 0)
		{
			// fetch mask data
			if (source is Texture2D) _data = source;
			if (source is Class) _data = FP.getBitmap(source);
			if (!_data) throw new Error("Invalid Pixelmask source image.");
			
			// set mask properties
			_width = data.width;
			_height = data.height;
			_x = x;
			_y = y;
			
			// set callback functions
			_check[Mask] = collideMask;
			_check[Pixelmask] = collidePixelmask;
			_check[Hitbox] = collideHitbox;
		}
		
		/** @private Collide against an Entity. */
		private bool collideMask(Mask other)
		{
			_point.x = parent.x + _x;
			_point.y = parent.y + _y;
			_rect.x = other.parent.x - other.parent.originX;
			_rect.y = other.parent.y - other.parent.originY;
			_rect.width = other.parent.width;
			_rect.height = other.parent.height;
			return _data.hitTest(_point, threshold, _rect);
		}
		
		/** @private Collide against a Hitbox. */
		private bool collideHitbox(Hitbox other)
		{
			_point.x = parent.x + _x;
			_point.y = parent.y + _y;
			_rect.x = other.parent.x + other._x;
			_rect.y = other.parent.y + other._y;
			_rect.width = other._width;
			_rect.height = other._height;
			return _data.hitTest(_point, threshold, _rect);
		}
		
		/** @private Collide against a Pixelmask. */
		private bool collidePixelmask(Pixelmask other)
		{
			_point.x = parent.x + _x;
			_point.y = parent.y + _y;
			_point2.x = other.parent.x + other._x;
			_point2.y = other.parent.y + other._y;
			return _data.hitTest(_point, threshold, other._data, _point2, other.threshold);
		}
		
		/**
		 * Current Texture2D mask.
		 */
		public Texture2D data { get { return _data; }
		public void data { set
		{
			_data = value;
			_width = value.width;
			_height = value.height;
			update();
		}
		
		// Pixelmask information.
		/** @private */ internal Texture2D _data;
		
		// Global objects.
		/** @private */ private Rectangle _rect = FP.rect;
		/** @private */ private Vector2 _point = FP.point;
		/** @private */ private Vector2 _point2 = FP.point2;
	}
}