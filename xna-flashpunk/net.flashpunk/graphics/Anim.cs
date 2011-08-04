namespace net.flashpunk.graphics 
{
	/**
	 * Template used by Spritemap to define animations. Don't create
	 * these yourself, instead you can fetch them with Spritemap's add().
	 */
	public class Anim 
	{
		/**
		 * Constructor.
		 * @param	name		Animation name.
		 * @param	frames		Array of frame indices to animate.
		 * @param	frameRate	Animation speed.
		 * @param	loop		If the animation should loop.
		 */
		public Anim(string name, Array frames, float frameRate = 0, bool loop = true) 
		{
			_name = name;
			_frames = frames;
			_frameRate = frameRate;
			_loop = loop;
			_frameCount = frames.length;
		}
		
		/**
		 * Plays the animation.
		 * @param	reset		If the animation should force-restart if it is already playing.
		 */
		public void play(bool reset = false)
		{
			_parent.play(_name, reset);
		}
		
		/**
		 * Name of the animation.
		 */
		public string name { get { return _name; }
		
		/**
		 * Array of frame indices to animate.
		 */
		public Array frames { get { return _frames; }
		
		/**
		 * Animation speed.
		 */
		public float frameRate { get { return _frameRate; }
		
		/**
		 * Amount of frames in the animation.
		 */
		public uint frameCount { get { return _frameCount; }
		
		/**
		 * If the animation loops.
		 */
		public bool loop { get { return _loop; }
		
		/** @private */ internal Spritemap _parent;
		/** @private */ internal string _name;
		/** @private */ internal Array _frames;
		/** @private */ internal float _frameRate;
		/** @private */ internal uint _frameCount;
		/** @private */ internal bool _loop;
	}
}