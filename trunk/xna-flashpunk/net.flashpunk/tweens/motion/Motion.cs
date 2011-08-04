namespace net.flashpunk.tweens.motion 
{
	using net.flashpunk.Tween;
	
	/**
	 * Base class for motion Tweens.
	 */
	public class Motion : Tween
	{
		/**
		 * Current x position of the Tween.
		 */
		public float x = 0;
		
		/**
		 * Current y position of the Tween.
		 */
		public float y = 0;
		
		/**
		 * Constructor.
		 * @param	duration	Duration of the Tween.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 * @param	ease		Optional easer function.
		 */
		public Motion(float duration, Function complete = null, uint type = 0, Function ease = null) 
		{
			base(duration, type, complete, ease);
		}
	}
}