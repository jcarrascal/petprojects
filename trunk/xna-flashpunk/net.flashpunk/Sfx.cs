namespace net.flashpunk 
{
	using flash.events.Event;
	using flash.media.Sound;
	using flash.media.SoundChannel;
	using flash.media.SoundTransform;
	using flash.utils.Dictionary;
	
	/**
	 * Sound effect object used to play embedded sounds.
	 */
	public class Sfx 
	{
		/**
		 * Optional callback for when the sound finishes playing.
		 */
		public Function complete;
		
		/**
		 * Creates a sound effect from an embedded source. Store a reference to
		 * this object so that you can play the sound using play() or loop().
		 * @param	source		The embedded sound class to use.
		 * @param	complete	Optional callback for when the sound finishes playing.
		 */
		public Sfx(Class source, Function complete = null) 
		{
			_sound = _sounds[source];
			if (!_sound) _sound = _sounds[source] = new source;
			this.complete = complete;
		}
		
		/**
		 * Plays the sound once.
		 * @param	vol		Volume factor, a value from 0 to 1.
		 * @param	pan		Panning factor, a value from -1 to 1.
		 */
		public void play(float vol = 1, float pan = 0)
		{
			if (_channel) stop();
			_vol = _transform.volume = vol < 0 ? 0 : vol;
			_pan = _transform.pan = pan < -1 ? -1 : (pan > 1 ? 1 : pan);
			_channel = _sound.play(0, 0, _transform);
			_channel.addEventListener(Event.SOUND_COMPLETE, onComplete);
			_looping = false;
			_position = 0;
		}
		
		/**
		 * Plays the sound looping. Will loop continuously until you call stop(), play(), or loop() again.
		 * @param	vol		Volume factor, a value from 0 to 1.
		 * @param	pan		Panning factor, a value from -1 to 1.
		 */
		public void loop(float vol = 1, float pan = 0)
		{
			play(vol, pan);
			_looping = true;
		}
		
		/**
		 * Stops the sound if it is currently playing.
		 * @return
		 */
		public bool stop()
		{
			if (!_channel) return false;
			_position = _channel.position;
			_channel.removeEventListener(Event.SOUND_COMPLETE, onComplete);
			_channel.stop();
			_channel = null;
			return true;
		}
		
		/**
		 * Resumes the sound from the position stop() was called on it.
		 */
		public void resume()
		{
			_channel = _sound.play(_position, 0, _transform);
			_channel.addEventListener(Event.SOUND_COMPLETE, onComplete);
			_position = 0;
		}
		
		/** @private Event handler for sound completion. */
		private void onComplete(Event e = null)
		{
			if (_looping) loop(_vol, _pan);
			else stop();
			_position = 0;
			if (complete != null) complete();
		}
		
		/**
		 * Alter the volume factor (a value from 0 to 1) of the sound during playback.
		 */
		public float volume { get { return _vol; }
		public void volume { set
		{
			if (value < 0) value = 0;
			if (!_channel || _vol == value) return;
			_vol = _transform.volume = value;
			_channel.soundTransform = _transform;
		}
		
		/**
		 * Alter the panning factor (a value from -1 to 1) of the sound during playback.
		 */
		public float pan { get { return _pan; }
		public void pan { set
		{
			if (value < -1) value = -1;
			if (value > 1) value = 1;
			if (!_channel || _pan == value) return;
			_pan = _transform.pan = value;
			_channel.soundTransform = _transform;
		}
		
		/**
		 * If the sound is currently playing.
		 */
		public bool playing { get { return _channel != null; }
		
		/**
		 * Position of the currently playing sound, in seconds.
		 */
		public float position { get { return (_channel ? _channel.position : _position) / 1000; }
		
		/**
		 * Length of the sound, in seconds.
		 */
		public float length { get { return _sound.length / 1000; }
		
		// Sound infromation.
		/** @private */ private float _vol = 1;
		/** @private */ private float _pan = 0;
		/** @private */ private Sound _sound;
		/** @private */ private SoundChannel _channel;
		/** @private */ private SoundTransform _transform = new SoundTransform;
		/** @private */ private float _position = 0;
		/** @private */ private bool _looping;
		
		// Stored Sound objects.
		/** @private */ private static Dictionary _sounds = new Dictionary;
	}
}