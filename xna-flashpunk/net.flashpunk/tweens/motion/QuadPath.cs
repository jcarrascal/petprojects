namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	using net.flashpunk.FP;
	
	/**
	 * A series of points which will determine a path from the
	 * beginning point to the end poing using quadratic curves.
	 */
	public class QuadPath : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public QuadPath(Function complete = null, uint type = 0) 
		{
			base(0, complete, type, null);
			_curveT[0] = 0;
		}
		
		/**
		 * Starts moving along the path.
		 * @param	duration	Duration of the movement.
		 * @param	ease		Optional easer function.
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
			_updateCurve = true;
			if (!_points.length) _curve[0] = new Vector2(x, y);
			_points[_points.length] = new Vector2(x, y);
		}
		
		/**
		 * Gets the point on the path.
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
			if (_index < _curve.length - 1)
			{
				while (_t > _curveT[_index + 1]) _index ++;
			}
			float td = _curveT[_index],
				float tt = _curveT[_index + 1] - td;
			td = (_t - td) / tt;
			_a = _curve[_index];
			_b = _points[_index + 1];
			_c = _curve[_index + 1];
			x = _a.x * (1 - td) * (1 - td) + _b.x * 2 * (1 - td) * td + _c.x * td * td;
			y = _a.y * (1 - td) * (1 - td) + _b.y * 2 * (1 - td) * td + _c.y * td * td;
		}
		
		/** @private Updates the path, preparing the curve. */
		private void updatePath()
		{
			if (_points.length < 3)	throw new Error("A QuadPath must have at least 3 points to operate.");
			if (!_updateCurve) return;
			_updateCurve = false;
			
			// produce the curve points
			Vector2 p,
				Vector2 c,
				Vector2 l = _points[1],
				uint i = 2;
			while (i < _points.length)
			{
				p = _points[i];
				if (_curve.length > i - 1) c = _curve[i - 1];
				else c = _curve[i - 1] = new Vector2;
				if (i < _points.length - 1)
				{
					c.x = l.x + (p.x - l.x) / 2;
					c.y = l.y + (p.y - l.y) / 2;
				}
				else
				{
					c.x = p.x;
					c.y = p.y;
				}
				l = p;
				i ++;
			}
			
			// find the total distance of the path
			i = 0;
			_distance = 0;
			while (i < _curve.length - 1)
			{
				_curveD[i] = curveLength(_curve[i], _points[i + 1], _curve[i + 1]);
				_distance += _curveD[i ++];
			}
			
			// find t foreach point on the curve
			i = 1;
			float d = 0;
			while (i < _curve.length - 1)
			{
				d += _curveD[i];
				_curveT[i ++] = d / _distance;
			}
			_curveT[_curve.length - 1] = 1;
		}
		
		/**
		 * Amount of points on the path.
		 */
		public float pointCount { get { return _points.length; }
		
		/** @private Calculates the lenght of the curve. */
		private float curveLength(Vector2 start, Vector2 control, Vector2 finish)
		{
			Vector2 a = FP.point,
				Vector2 b = FP.point2;
			a.x = start.x - 2 * control.x + finish.x;
			a.y = start.y - 2 * control.y + finish.y;
			b.x = 2 * control.x - 2 * start.x;
			b.y = 2 * control.y - 2 * start.y;
			float A = 4 * (a.x * a.x + a.y * a.y),
				float B = 4 * (a.x * b.x + a.y * b.y),
				float C = b.x * b.x + b.y * b.y,
				float ABC = 2 * Math.sqrt(A + B + C),
				float A2 = Math.sqrt(A),
				float A32 = 2 * A * A2,
				float C2 = 2 * Math.sqrt(C),
				float BA = B / A2;
			return (A32 * ABC + A2 * B * (ABC - C2) + (4 * C * A - B * B) * Math.log((2 * A2 + BA + ABC) / (BA + C2))) / (4 * A32);
		}
		
		// Path information.
		/** @private */ private Vector _points.<Vector2> = new Vector.<Vector2>;
		/** @private */ private float _distance = 0;
		/** @private */ private float _speed = 0;
		/** @private */ private uint _index = 0;
		
		// Curve information.
		/** @private */ private bool _updateCurve = true;
		/** @private */ private Vector _curve.<Vector2> = new Vector.<Vector2>;
		/** @private */ private Vector _curveT.<float> = new Vector.<float>;
		/** @private */ private Vector _curveD.<float> = new Vector.<float>;
		
		// Curve points.
		/** @private */ private Vector2 _a;
		/** @private */ private Vector2 _b;
		/** @private */ private Vector2 _c;
	}
}