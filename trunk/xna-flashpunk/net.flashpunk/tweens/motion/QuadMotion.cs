namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	using net.flashpunk.FP;
	using net.flashpunk.utils.Ease;
	
	/**
	 * Determines motion along a quadratic curve.
	 */
	public class QuadMotion : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public QuadMotion(Function complete = null, uint type = 0)
		{
			base(0, complete, type, null);
		}
		
		/**
		 * Starts moving along the curve.
		 * @param	fromX		X start.
		 * @param	fromY		Y start.
		 * @param	controlX	X control, used to determine the curve.
		 * @param	controlY	Y control, used to determine the curve.
		 * @param	toX			X finish.
		 * @param	toY			Y finish.
		 * @param	duration	Duration of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotion(float fromX, float fromY, float controlX, float controlY, float toX, float toY, float duration, Function ease = null)
		{
			_distance = -1;
			x = _fromX = fromX;
			y = _fromY = fromY;
			_controlX = controlX;
			_controlY = controlY;
			_toX = toX;
			_toY = toY;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/**
		 * Starts moving along the curve at the speed.
		 * @param	fromX		X start.
		 * @param	fromY		Y start.
		 * @param	controlX	X control, used to determine the curve.
		 * @param	controlY	Y control, used to determine the curve.
		 * @param	toX			X finish.
		 * @param	toY			Y finish.
		 * @param	speed		Speed of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotionSpeed(float fromX, float fromY, float controlX, float controlY, float toX, float toY, float speed, Function ease = null)
		{
			_distance = -1;
			x = _fromX = fromX;
			y = _fromY = fromY;
			_controlX = controlX;
			_controlY = controlY;
			_toX = toX;
			_toY = toY;
			_target = distance / speed;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			x = _fromX * (1 - _t) * (1 - _t) + _controlX * 2 * (1 - _t) * _t + _toX * _t * _t;
			y = _fromY * (1 - _t) * (1 - _t) + _controlY * 2 * (1 - _t) * _t + _toY * _t * _t;
		}
		
		/**
		 * The distance of the entire curve.
		 */
		public float distance { get
		{
			if (_distance >= 0) return _distance;
			Vector2 a = FP.point,
				Vector2 b = FP.point2;
			a.x = x - 2 * _controlX + _toX;
			a.y = y - 2 * _controlY + _toY;
			b.x = 2 * _controlX - 2 * x;
			b.y = 2 * _controlY - 2 * y;
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
		
		// Curve information.
		/** @private */ private float _distance = -1;
		/** @private */ private float _fromX = 0;
		/** @private */ private float _fromY = 0;
		/** @private */ private float _toX = 0;
		/** @private */ private float _toY = 0;
		/** @private */ private float _controlX = 0;
		/** @private */ private float _controlY = 0;
	}
}