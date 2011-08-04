namespace net.flashpunk.tweens.misc
{
	using net.flashpunk.Tween;
	
	/**
	 * A simple alarm, useful for timed events, etc.
	 */
	public class Alarm : Tween
	{
		/**
		 * Constructor.
		 * @param	duration	Duration of the alarm.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public Alarm(float duration, Function complete = null, uint type = 0) 
		{
			base(duration, type, complete, null);
		}
		
		/**
		 * Sets the alarm.
		 * @param	duration	Duration of the alarm.
		 */
		public void reset(float duration)
		{
			_target = duration;
			start();
		}
		
		/**
		 * How much time has passed since reset.
		 */
		public float elapsed { get { return _time; }
		
		/**
		 * Current alarm duration.
		 */
		public float duration { get { return _target; }
		
		/**
		 * Time remaining on the alarm.
		 */
		public float remaining { get { return _target - _time; }
	}
}