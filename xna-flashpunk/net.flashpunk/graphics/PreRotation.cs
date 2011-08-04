namespace net.flashpunk.graphics 
{
	using flash.display.Bitmap;
	using flash.display.Texture2D;
	using flash.geom.Matrix;
	using flash.geom.Vector2;
	using flash.geom.Rectangle;
	using flash.utils.Dictionary;
	using net.flashpunk.FP;
	using net.flashpunk.Graphic;
	
	/**
	 * Creates a pre-rotated Image strip to increase runtime performance for rotating graphics.
	 */
	public class PreRotation : Image
	{
		/**
		 * Current angle to fetch the pre-rotated frame from.
		 */
		public float frameAngle = 0;
		
		/**
		 * Constructor.
		 * @param	source			The source image to be rotated.
		 * @param	frameCount		How many frames to use. More frames result in smoother rotations.
		 * @param	smooth			Make the rotated graphic appear less pixelly.
		 */
		public PreRotation(Class source, uint frameCount = 36, bool smooth = false) 
		{
			Texture2D r = _rotated[source];
			_frame = new Rectangle(0, 0, _size[source], _size[source]);
			if (!r)
			{
				// produce a rotated bitmap strip
				Texture2D temp = (new source).bitmapData,
					uint size = _size[source] = Math.ceil(FP.distance(0, 0, temp.width, temp.height));
				_frame.width = _frame.height = size;
				uint width = _frame.width * frameCount,
					uint height = _frame.height;
				if (width > _MAX_WIDTH)
				{
					width = _MAX_WIDTH - (_MAX_WIDTH % _frame.width);
					height = Math.ceil(frameCount / (width / _frame.width)) * _frame.height;
				}
				r = new Texture2D(width, height, true, 0);
				Matrix m = FP.matrix,
					float a = 0,
					float aa = (Math.PI * 2) / -frameCount,
					uint ox = temp.width / 2,
					uint oy = temp.height / 2,
					uint o = _frame.width / 2,
					uint x = 0,
					uint y = 0;
				while (y < height)
				{
					while (x < width)
					{
						m.identity();
						m.translate(-ox, -oy);
						m.rotate(a);
						m.translate(o + x, o + y);
						r.draw(temp, m, null, null, null, smooth);
						x += _frame.width;
						a += aa;
					}
					x = 0;
					y += _frame.height;
				}
			}
			_source = r;
			_width = r.width;
			_frameCount = frameCount;
			base(_source, _frame);
		}
		
		/** @private Renders the PreRotated graphic. */
		override public void render(Texture2D target, Vector2 point, Vector2 camera) 
		{
			frameAngle %= 360;
			if (frameAngle < 0) frameAngle += 360;
			_current = uint(_frameCount * (frameAngle / 360));
			if (_last != _current)
			{
				_last = _current;
				_frame.x = _frame.width * _last;
				_frame.y = uint(_frame.x / _width) * _frame.height;
				_frame.x %= _width;
				updateBuffer();
			}
			base.render(target, point, camera);
		}
		
		// Rotation information.
		/** @private */ private uint _width;
		/** @private */ private Rectangle _frame;
		/** @private */ private uint _frameCount;
		/** @private */ private int _last = -1;
		/** @private */ private int _current = -1;
		
		// Global information.
		/** @private */ private static Dictionary _rotated = new Dictionary;
		/** @private */ private static Dictionary _size = new Dictionary;
		/** @private */ private const uint _MAX_WIDTH = 4000;
		/** @private */ private const uint _MAX_HEIGHT = 4000;
	}
}