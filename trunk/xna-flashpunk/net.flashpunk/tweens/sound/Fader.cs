namespace net.flashpunk.tweens.sound 
{
	using net.flashpunk.FP;
	using net.flashpunk.Tween;
	
	/**
	 * Global volume fader.
	 */
	public class Fader : Tween
	{
		/**
		 * Constructor.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public Fader(Function complete = null, uint type = 0) 
		{
			base(0, type, complete);
		}
		
		/**
		 * Fades FP.volume to the target volume.
		 * @param	volume		The volume to fade to.
		 * @param	duration	Duration of the fade.
		 * @param	ease		Optional easer function.
		 */
		public void fadeTo(float volume, float duration, Function ease = null)
		{
			if (volume < 0) volume = 0;
			_start = FP.volume;
			_range = volume - _start;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			FP.volume = _start + _range * _t;
		}
		
		// Fader information.
		/** @private */ private float _start;
		/** @private */ private float _range;
	}
}