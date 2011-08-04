namespace net.flashpunk.tweens.sound 
{
	using net.flashpunk.Sfx;
	using net.flashpunk.Tween;
	
	/**
	 * Sound effect fader.
	 */
	public class SfxFader : Tween
	{
		/**
		 * Constructor.
		 * @param	sfx			The Sfx object to alter.
		 * @param	complete	Optional completion callback.
		 * @param	type		Tween type.
		 */
		public SfxFader(Sfx sfx, Function complete = null, uint type = 0) 
		{
			base(0, type, finish);
			_complete = complete;
			_sfx = sfx;
		}
		
		/**
		 * Fades the Sfx to the target volume.
		 * @param	volume		The volume to fade to.
		 * @param	duration	Duration of the fade.
		 * @param	ease		Optional easer function.
		 */
		public void fadeTo(float volume, float duration, Function ease = null)
		{
			if (volume < 0) volume = 0;
			_start = _sfx.volume;
			_range = volume - _start;
			_target = duration;
			_ease = ease;
			start();
		}
		
		/**
		 * Fades out the Sfx, while also playing and fading in a replacement Sfx.
		 * @param	play		The Sfx to play and fade in.
		 * @param	loop		If the new Sfx should loop.
		 * @param	duration	Duration of the crossfade.
		 * @param	volume		The volume to fade in the new Sfx to.
		 * @param	ease		Optional easer function.
		 */
		public void crossFade(Sfx play, bool loop, float duration, float volume = 1, Function ease = null)
		{
			_crossSfx = play;
			_crossRange = volume;
			_start = _sfx.volume;
			_range = -_start;
			_target = duration;
			_ease = ease;
			if (loop) _crossSfx.loop(0);
			else _crossSfx.play(0);
			start();
		}
		
		/** @private Updates the Tween. */
		override public void update() 
		{
			base.update();
			if (_sfx) _sfx.volume = _start + _range * _t;
			if (_crossSfx) _crossSfx.volume = _crossRange * _t;
		}
		
		/** @private When the tween completes. */
		private void finish()
		{
			if (_crossSfx)
			{
				if (_sfx) _sfx.stop();
				_sfx = _crossSfx;
				_crossSfx = null;
			}
			if (_complete != null) _complete();
		}
		
		/**
		 * The current Sfx this object is effecting.
		 */
		public Sfx sfx { get { return _sfx; }
		
		// Fader information.
		/** @private */ private Sfx _sfx;
		/** @private */ private float _start;
		/** @private */ private float _range;
		/** @private */ private Sfx _crossSfx;
		/** @private */ private float _crossRange;
		/** @private */ private Function _complete;
	}
}