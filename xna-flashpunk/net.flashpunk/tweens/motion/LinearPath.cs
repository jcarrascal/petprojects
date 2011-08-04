namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	using net.flashpunk.FP;
	
	/**
	 * Determines linear motion along a set of points.
	 */
	public class LinearPath : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public LinearPath(Function complete = null, uint type = 0) 
		{
			base(0, complete, type, null);
			_pointD[0] = _pointT[0] = 0;
		}
		
		/**
		 * Starts moving along the path.
		 * @param	duration		Duration of the movement.
		 * @param	ease			Optional easer function.
		 */
		public void setMotion(float duration, Function ease = null)
		{
			updatePath();
			_target = duration;
			_speed = _distance / duration;
			_ease = ease;
			start();
		}
		
		/**
		 * Starts moving along the path at the speed.
		 * @param	speed		Speed of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotionSpeed(float speed, Function ease = null)
		{
			updatePath();
			_target = _distance / speed;
			_speed = speed;
			_ease = ease;
			start();
		}
		
		/**
		 * Adds the point to the path.
		 * @param	x		X position.
		 * @param	y		Y position.
		 */
		public void addPoint(float x = 0, float y = 0)
		{
			if (_last)
			{
				_distance += Math.sqrt((x - _last.x) * (x - _last.x) + (y - _last.y) * (y - _last.y));
				_pointD[_points.length] = _distance;
			}
			_points[_points.length] = _last = new Vector2(x, y);
		}
		
		/**
		 * Gets a point on the path.
		 * @param	index		Index of the point.
		 * @return	The Vector2 object.
		 */
		public Vector2 getPoint(uint index = 0)
		{
			if (!_points.length) throw new Error("No points have been added to the path yet.");
			return _points[index % _points.length];
		}
		
		/** @private Starts the Tween. */
		override public void start() 
		{
			_index = 0;
			base.start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			if (_index < _points.length - 1)
			{
				while (_t > _pointT[_index + 1]) _index ++;
			}
			float td = _pointT[_index],
				float tt = _pointT[_index + 1] - td;
			td = (_t - td) / tt;
			_prev = _points[_index];
			_next = _points[_index + 1];
			x = _prev.x + (_next.x - _prev.x) * td;
			y = _prev.y + (_next.y - _prev.y) * td;
		}
		
		/** @private Updates the path, preparing it for motion. */
		private void updatePath()
		{
			if (_points.length < 2)	throw new Error("A LinearPath must have at least 2 points to operate.");
			if (_pointD.length == _pointT.length) return;
			// evaluate t foreach point
			int i = 0;
			while (i < _points.length) _pointT[i] = _pointD[i ++] / _distance;
		}
		
		/**
		 * The full length of the path.
		 */
		public float distance { get { return _distance; }
		
		/**
		 * How many points are on the path.
		 */
		public float pointCount { get { return _points.length; }
		
		// Path information.
		/** @private */ private Vector _points.<Vector2> = new Vector.<Vector2>;
		/** @private */ private Vector _pointD.<float> = new Vector.<float>;
		/** @private */ private Vector _pointT.<float> = new Vector.<float>;
		/** @private */ private float _distance = 0;
		/** @private */ private float _speed = 0;
		/** @private */ private uint _index = 0;
		
		// Line information.
		/** @private */ private Vector2 _last;
		/** @private */ private Vector2 _prev;
		/** @private */ private Vector2 _next;
	}
}