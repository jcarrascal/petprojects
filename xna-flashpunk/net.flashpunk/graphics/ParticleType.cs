namespace net.flashpunk.graphics 
{
	using flash.display.Texture2D;
	using flash.geom.Rectangle;
	using net.flashpunk.FP;
	
	/**
	 * Template used to define a particle type used by the Emitter class. Instead
	 * of creating this object yourself, fetch one with Emitter's add() function.
	 */
	public class ParticleType 
	{
		/**
		 * Constructor.
		 * @param	name			Name of the particle type.
		 * @param	frames			Array of frame indices to animate through.
		 * @param	source			Source image.
		 * @param	frameWidth		Frame width.
		 * @param	frameHeight		Frame height.
		 * @param	frameCount		Frame count.
		 */
		public ParticleType(string name, Array frames, Texture2D source, uint frameWidth, uint frameHeight)
		{
			_name = name;
			_source = source;
			_width = source.width;
			_frame = new Rectangle(0, 0, frameWidth, frameHeight);
			_frames = frames;
			_frameCount = frames.length;
		}
		
		/**
		 * Defines the motion range for this particle type.
		 * @param	angle			Launch Direction.
		 * @param	distance		Distance to travel.
		 * @param	duration		Particle duration.
		 * @param	angleRange		Random amount to add to the particle's direction.
		 * @param	distanceRange	Random amount to add to the particle's distance.
		 * @param	durationRange	Random amount to add to the particle's duration.
		 * @param	ease			Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setMotion(float angle, float distance, float duration, float angleRange = 0, float distanceRange = 0, float durationRange = 0, Function ease = null)
		{
			_angle = angle * FP.RAD;
			_distance = distance;
			_duration = duration;
			_angleRange = angleRange * FP.RAD;
			_distanceRange = distanceRange;
			_durationRange = durationRange;
			_ease = ease;
			return this;
		}
		
		/**
		 * Defines the motion range for this particle type based on the vector.
		 * @param	x				X distance to move.
		 * @param	y				Y distance to move.
		 * @param	duration		Particle duration.
		 * @param	durationRange	Random amount to add to the particle's duration.
		 * @param	ease			Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setMotionVector(float x, float y, float duration, float durationRange = 0, Function ease = null)
		{
			_angle = Math.atan2(y, x);
			_angleRange = 0;
			_duration = duration;
			_durationRange = durationRange;
			_ease = ease;
			return this;
		}
		
		/**
		 * Sets the alpha range of this particle type.
		 * @param	start		The starting alpha.
		 * @param	finish		The finish alpha.
		 * @param	ease		Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setAlpha(float start = 1, float finish = 0, Function ease = null)
		{
			start = start < 0 ? 0 : (start > 1 ? 1 : start);
			finish = finish < 0 ? 0 : (finish > 1 ? 1 : finish);
			_alpha = start;
			_alphaRange = finish - start;
			_alphaEase = ease;
			createBuffer();
			return this;
		}
		
		/**
		 * Sets the color range of this particle type.
		 * @param	start		The starting color.
		 * @param	finish		The finish color.
		 * @param	ease		Optional easer function.
		 * @return	This ParticleType object.
		 */
		public ParticleType setColor(uint start = 0xFFFFFF, uint finish = 0, Function ease = null)
		{
			start &= 0xFFFFFF;
			finish &= 0xFFFFFF;
			_red = (start >> 16 & 0xFF) / 255;
			_green = (start >> 8 & 0xFF) / 255;
			_blue = (start & 0xFF) / 255;
			_redRange = (finish >> 16 & 0xFF) / 255 - _red;
			_greenRange = (finish >> 8 & 0xFF) / 255 - _green;
			_blueRange = (finish & 0xFF) / 255 - _blue;
			_colorEase = ease;
			createBuffer();
			return this;
		}
		
		/** @private Creates the buffer if it doesn't exist. */
		private void createBuffer()
		{
			if (_buffer) return;
			_buffer = new Texture2D(_frame.width, _frame.height, true, 0);
			_bufferRect = _buffer.rect;
		}
		
		// Particle information.
		/** @private */ internal string _name;
		/** @private */ internal Texture2D _source;
		/** @private */ internal uint _width;
		/** @private */ internal Rectangle _frame;
		/** @private */ internal Array _frames;
		/** @private */ internal uint _frameCount;
		
		// Motion information.
		/** @private */ internal float _angle;
		/** @private */ internal float _angleRange;
		/** @private */ internal float _distance;
		/** @private */ internal float _distanceRange;
		/** @private */ internal float _duration;
		/** @private */ internal float _durationRange;
		/** @private */ internal Function _ease;
		
		// Alpha information.
		/** @private */ internal float _alpha = 1;
		/** @private */ internal float _alphaRange = 0;
		/** @private */ internal Function _alphaEase;
		
		// Color information.
		/** @private */ internal float _red = 1;
		/** @private */ internal float _redRange = 0;
		/** @private */ internal float _green = 1;
		/** @private */ internal float _greenRange = 0;
		/** @private */ internal float _blue = 1;
		/** @private */ internal float _blueRange = 0;
		/** @private */ internal Function _colorEase;
		
		// Buffer information.
		/** @private */ internal Texture2D _buffer;
		/** @private */ internal Rectangle _bufferRect;
	}
}