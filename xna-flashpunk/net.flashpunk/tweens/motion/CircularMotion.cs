namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	using net.flashpunk.FP;
	using net.flashpunk.utils.Ease;
	
	/**
	 * Determines a circular motion.
	 */
	public class CircularMotion : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public CircularMotion(Function complete = null, uint type = 0)
		{
			base(0, complete, type, null);
		}
		
		/**
		 * Starts moving along a circle.
		 * @param	centerX		X position of the circle's center.
		 * @param	centerY		Y position of the circle's center.
		 * @param	radius		Radius of the circle.
		 * @param	angle		Starting position on the circle.
		 * @param	clockwise	If the motion is clockwise.
		 * @param	duration	Duration of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotion(float centerX, float centerY, float radius, float angle, bool clockwise, float duration, Function ease = null)
		{
			_centerX = centerX;
			_centerY = centerY;
			_radius = radius;
			_angle = _angleStart = angle * FP.RAD;
			_angleFinish = _CIRC * (clockwise ? 1 : -1);
			_target = duration;
			_ease = ease;
			start();
		}
		
		/**
		 * Starts moving along a circle at the speed.
		 * @param	centerX		X position of the circle's center.
		 * @param	centerY		Y position of the circle's center.
		 * @param	radius		Radius of the circle.
		 * @param	angle		Starting position on the circle.
		 * @param	clockwise	If the motion is clockwise.
		 * @param	speed		Speed of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotionSpeed(float centerX, float centerY, float radius, float angle, bool clockwise, float speed, Function ease = null)
		{
			_centerX = centerX;
			_centerY = centerY;
			_radius = radius;
			_angle = _angleStart = angle * FP.RAD;
			_angleFinish = _CIRC * (clockwise ? 1 : -1);
			_target = (_radius * _CIRC) / speed;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			_angle = _angleStart + _angleFinish * _t;
			x = _centerX + Math.cos(_angle) * _radius;
			y = _centerY + Math.sin(_angle) * _radius;
		}
		
		/**
		 * The current position on the circle.
		 */
		public float angle { get { return _angle; }
		
		/**
		 * The circumference of the current circle motion.
		 */
		public float circumference { get { return _radius * _CIRC; }
		
		// Circle information.
		/** @private */ private float _centerX = 0;
		/** @private */ private float _centerY = 0;
		/** @private */ private float _radius = 0;
		/** @private */ private float _angle = 0;
		/** @private */ private float _angleStart = 0;
		/** @private */ private float _angleFinish = 0;
		/** @private */ private const float _CIRC = Math.PI * 2;
	}
}