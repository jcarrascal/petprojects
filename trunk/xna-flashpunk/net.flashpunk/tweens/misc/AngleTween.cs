namespace net.flashpunk.tweens.misc 
{
	using net.flashpunk.FP;
	using net.flashpunk.Tween;
	
	/**
	 * Tweens from one angle to another.
	 */
	public class AngleTween : Tween
	{
		/**
		 * The current value.
		 */
		public float angle = 0;
		
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public AngleTween(Function complete = null, uint type = 0) 
		{
			base(0, type, complete);
		}
		
		/**
		 * Tweens the value from one angle to another.
		 * @param	fromAngle		Start angle.
		 * @param	toAngle			End angle.
		 * @param	duration		Duration of the tween.
		 * @param	ease			Optional easer function.
		 */
		public void tween(float fromAngle, float toAngle, float duration, Function ease = null)
		{
			_start = angle = fromAngle;
			float d = toAngle - angle,
				float a = Math.abs(d);
			if (a > 181) _range = (360 - a) * (d > 0 ? -1 : 1);
			else if (a < 179) _range = d;
			else _range = FP.choose(180, -180);
			_target = duration;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			angle = (_start + _range * _t) % 360;
			if (angle < 0) angle += 360;
		}
		
		// Tween information.
		/** @private */ private float _start;
		/** @private */ private float _range;
	}
}