namespace net.flashpunk.tweens.motion 
{
	using flash.geom.Vector2;
	
	/**
	 * Determines motion along a line, from one point to another.
	 */
	public class LinearMotion : Motion
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public LinearMotion(Function complete = null, uint type = 0)
		{
			base(0,complete, type, null);
		}
		
		/**
		 * Starts moving along a line.
		 * @param	fromX		X start.
		 * @param	fromY		Y start.
		 * @param	toX			X finish.
		 * @param	toY			Y finish.
		 * @param	duration	Duration of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotion(float fromX, float fromY, float toX, float toY, float duration, Function ease = null)
		{
			_distance = -1;
			x = _fromX = fromX;
			y = _fromY = fromY;
			_moveX = toX - fromX;
			_moveY = toY - fromY;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/**
		 * Starts moving along a line at the speed.
		 * @param	fromX		X start.
		 * @param	fromY		Y start.
		 * @param	toX			X finish.
		 * @param	toY			Y finish.
		 * @param	speed		Speed of the movement.
		 * @param	ease		Optional easer function.
		 */
		public void setMotionSpeed(float fromX, float fromY, float toX, float toY, float speed, Function ease = null)
		{
			_distance = -1;
			x = _fromX = fromX;
			y = _fromY = fromY;
			_moveX = toX - fromX;
			_moveY = toY - fromY;
			_target = distance / speed;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			x = _fromX + _moveX * _t;
			y = _fromY + _moveY * _t;
		}
		
		/**
		 * Length of the current line of movement.
		 */
		public float distance { get
		{
			if (_distance >= 0) return _distance;
			return (_distance = Math.sqrt(_moveX * _moveX + _moveY * _moveY));
		}
		
		// Line information.
		/** @private */ private float _fromX = 0;
		/** @private */ private float _fromY = 0;
		/** @private */ private float _moveX = 0;
		/** @private */ private float _moveY = 0;
		/** @private */ private float _distance = - 1;
	}
}